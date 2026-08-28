using Miji.Core.Events;
using Miji.Core.StateMachines;
using Miji.Gameplay.Player;
using UnityEngine;

namespace Miji.Gameplay.Companion
{
    /// <summary>B(무리비)의 상태. 위치를 어떻게 모느냐만 가른다 — 뷰(애니·턴·facing)는 공통이다.</summary>
    public enum CompanionStateId
    {
        /// <summary>A 뒤를 감쇠 추적. 평상시.</summary>
        Following,

        /// <summary>너무 처져 연출 없이 즉시 복귀. 한 프레임짜리 보정 후 Following으로.</summary>
        Snapping,

        /// <summary>F2 받침 — A 밑으로 파고들어 받친다(<see cref="BoostRequestedSignal"/> 반응).</summary>
        Cooperating
    }

    /// <summary>
    /// B(무리비)의 동행 — A 뒤를 따르고, 처지면 스냅하고, F2 받침 때 A 밑으로 파고든다.
    ///
    /// 확정 설계(이원 무브셋 3절):
    /// - 플레이어는 B를 관리하지 않는다 — 입력도 콜라이더도 없다. B는 <b>자율</b>이다
    /// - 너무 처지면 연출 없이 즉시 복귀(「B가 멀어서 못 했다」 금지) = <see cref="CompanionStateId.Snapping"/>
    /// - 협력은 <b>명령 큐가 아니라 신호 반응</b>이다 — A의 <see cref="BoostRequestedSignal"/>을 구독한다
    /// - B는 무적이므로 Health/Hurtbox를 붙이지 않는다(붕괴는 전투 피해가 아니다)
    ///
    /// 상태가 하는 일은 <c>basePosition</c>을 움직이는 것뿐이다. 그 뒤 <see cref="ApplyView"/>가
    /// 결과 이동에서 애니/턴/facing/sleep을 파생시킨다 — 상태를 늘려도 뷰는 안 바뀐다.
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer))]
    public class CompanionFollower : MonoBehaviour
    {
        [Tooltip("따라다닐 대상(A). 비우면 씬에서 PlayerMotor를 찾는다.")]
        [SerializeField] PlayerMotor target;

        [Tooltip("A가 보는 방향의 반대쪽으로 이만큼 떨어져 선다.")]
        [SerializeField] float followDistance = 1.1f;

        [Tooltip("목표 지점을 향한 감쇠 추적 시간. 클수록 굼뜨다.")]
        [SerializeField] float smoothTime = 0.22f;

        [Tooltip("이보다 멀어지면 연출 없이 즉시 복귀한다(Snapping).")]
        [SerializeField] float snapDistance = 8f;

        [Header("F2 받침 — A 밑으로 파고들기")]
        [Tooltip("받침 때 A 아래 이만큼(월드 유닛) 지점으로 파고든다.")]
        [SerializeField] float boostUnderOffset = 0.7f;
        [Tooltip("받침 진입의 감쇠 시간. 따라붙기보다 빨라야 「파고드는」 맛이 난다.")]
        [SerializeField] float cooperateSmooth = 0.05f;
        [Tooltip("밑에 받치고 머무는 시간. 지나면 Following으로 돌아간다.")]
        [SerializeField] float cooperateDuration = 0.25f;

        [Header("걸음 들썩임 — 애니메이터가 없을 때만 쓰는 대체 표현")]
        [SerializeField] float bobAmplitude = 0.05f;
        [SerializeField] float bobFrequency = 9f;

        [Tooltip("있으면 Speed 파라미터를 구동하고 코드 들썩임은 끈다(걸음 바운스는 클립 몫).")]
        [SerializeField] Animator animator;

        [Header("방향 전환 — 180도를 3프레임으로 돈다")]
        [SerializeField] Sprite turnQuarterSprite;
        [SerializeField] Sprite turnFrontSprite;
        [SerializeField] float turnDuration = 0.14f;

        [Header("긴 대기 — 잠들기")]
        [Tooltip("이 시간 동안 거의 안 움직이면 잠든다. 움직이면 즉시 깬다.")]
        [SerializeField] float sleepDelay = 7f;

        [Tooltip("B가 A 높이에 이만큼(월드 유닛) 이내로 내려오면 착지로 본다. A 접지와 별개.")]
        [SerializeField] float groundEpsilon = 0.06f;

        static readonly int SpeedParam = Animator.StringToHash("Speed");
        static readonly int AsleepParam = Animator.StringToHash("IsAsleep");
        static readonly int GroundedParam = Animator.StringToHash("Grounded");
        static readonly int VSpeedParam = Animator.StringToHash("VSpeed");

        readonly StateMachine<CompanionStateId> states = new();

        SpriteRenderer sprite;
        float stillTimer;
        int lastFace = 1;
        float turnTimer;
        int turnFrom = 1;
        Vector3 basePosition;   // 들썩임을 뺀 실제 추적 위치. 상태들이 이걸 움직인다.
        Vector3 velocity;
        float bobPhase;

        /// <summary>현재 상태. 디버그·테스트용.</summary>
        public CompanionStateId CurrentState => states.CurrentKey;

        void Awake()
        {
            sprite = GetComponent<SpriteRenderer>();
            if (target == null) target = FindFirstObjectByType<PlayerMotor>();
            basePosition = transform.position;

            states.Add(CompanionStateId.Following, new FollowingState(this));
            states.Add(CompanionStateId.Snapping, new SnappingState(this));
            states.Add(CompanionStateId.Cooperating, new CooperatingState(this));
            states.Change(CompanionStateId.Following);
        }

        void OnEnable() => EventBus.Subscribe<BoostRequestedSignal>(OnBoostRequested);
        void OnDisable() => EventBus.Unsubscribe<BoostRequestedSignal>(OnBoostRequested);

        // A가 받침을 발동 — 어디에 있든 A 밑으로 파고든다. 신호 반응이지 명령 큐가 아니다.
        void OnBoostRequested(BoostRequestedSignal _) => states.Change(CompanionStateId.Cooperating);

        void LateUpdate()
        {
            if (target == null) return;
            states.Tick(Time.deltaTime);   // basePosition을 현재 상태의 규칙으로 움직인다
            ApplyView();                   // 그 이동에서 애니/턴/facing/sleep을 파생 — 상태 무관
        }

        // A 뒤 followDistance 지점. 평상시 추적 목표이자 스냅·접지 판정의 기준.
        Vector3 Anchor() =>
            target.transform.position + new Vector3(-target.Facing * followDistance, 0f, 0f);

        /// <summary>이동 결과에서 뷰(애니메이터/코드 들썩임/턴/facing/sleep)를 파생시키고 스프라이트를 놓는다.</summary>
        void ApplyView()
        {
            var anchor = Anchor();
            var speed = new Vector2(velocity.x, velocity.y).magnitude;

            // B의 접지는 A가 아니라 B 자신이 A 높이까지 내려왔는지로 본다.
            // A가 먼저 착지해도 B는 SmoothDamp 지연으로 아직 공중에 떠 있으므로,
            // A 접지만 보면 B가 공중에서 착지 판정이 나 Fall이 끊긴다(=버그).
            bool bGrounded = target.IsGrounded && (basePosition.y - anchor.y) < groundEpsilon;

            if (animator != null)
            {
                animator.SetFloat(SpeedParam, speed);
                // ★ VSpeed는 B의 지연된 추종 속도가 아니라 A의 실제 세로속도를 쓴다(진입 트리거).
                //   Fall 유지는 bGrounded가 false인 동안 계속되므로 B가 실제로 내려앉을 때까지 간다.
                animator.SetBool(GroundedParam, bGrounded);
                animator.SetFloat(VSpeedParam, target.Velocity.y);
                transform.position = basePosition;

                // 오래 가만히 있으면 잠든다. 움직이면 즉시 깬다. (공중에서는 안 잠든다)
                stillTimer = (speed < 0.15f && bGrounded) ? stillTimer + Time.deltaTime : 0f;
                animator.SetBool(AsleepParam, stillTimer >= sleepDelay);
            }
            else
            {
                // 애니메이터가 없으면 코드 들썩임으로 생물 표시를 대신한다.
                var moving = Mathf.Clamp01(speed / 2f);
                bobPhase += Time.deltaTime * bobFrequency * Mathf.Max(moving, 0.001f);
                var bob = Mathf.Abs(Mathf.Sin(bobPhase)) * bobAmplitude * moving;
                transform.position = basePosition + new Vector3(0f, bob, 0f);
            }

            // 가는 방향을 본다. 멈춰 있으면 A 쪽을 본다.
            var face = Mathf.Abs(velocity.x) > 0.05f
                ? velocity.x
                : target.transform.position.x - basePosition.x;

            var faceSign = face < 0f ? -1 : 1;
            if (faceSign != lastFace)
            {
                turnFrom = lastFace;
                lastFace = faceSign;
                if (turnFrontSprite != null) turnTimer = turnDuration;
            }

            // Animator가 이미 스프라이트를 쓴 뒤라 여기서 덮으면 이긴다.
            if (turnTimer > 0f)
            {
                turnTimer -= Time.deltaTime;
                if (Miji.Gameplay.View.TurnView.Apply(sprite, turnTimer, turnDuration, turnFrom, faceSign,
                                                      turnQuarterSprite, turnFrontSprite))
                    return;
            }

            if (!Mathf.Approximately(face, 0f)) sprite.flipX = face < 0f;
        }

        // ── 상태 ─────────────────────────────────────────────────────
        // 상태는 basePosition/velocity만 건드린다. 나머지(뷰)는 ApplyView가 공통으로 한다.

        sealed class FollowingState : StateBase
        {
            readonly CompanionFollower c;
            public FollowingState(CompanionFollower c) => this.c = c;

            public override void Tick(float dt)
            {
                var anchor = c.Anchor();
                if ((anchor - c.basePosition).sqrMagnitude > c.snapDistance * c.snapDistance)
                {
                    c.states.Change(CompanionStateId.Snapping);
                    return;
                }
                c.basePosition = Vector3.SmoothDamp(c.basePosition, anchor, ref c.velocity, c.smoothTime);
            }
        }

        sealed class SnappingState : StateBase
        {
            readonly CompanionFollower c;
            public SnappingState(CompanionFollower c) => this.c = c;

            // 진입 즉시 앵커로 순간이동하고 다음 틱에 평상 추적으로 복귀한다.
            public override void Enter()
            {
                c.basePosition = c.Anchor();
                c.velocity = Vector3.zero;
            }

            public override void Tick(float dt) => c.states.Change(CompanionStateId.Following);
        }

        sealed class CooperatingState : StateBase
        {
            readonly CompanionFollower c;
            float timer;
            public CooperatingState(CompanionFollower c) => this.c = c;

            public override void Enter() => timer = c.cooperateDuration;

            public override void Tick(float dt)
            {
                var under = c.target.transform.position + Vector3.down * c.boostUnderOffset;
                c.basePosition = Vector3.SmoothDamp(c.basePosition, under, ref c.velocity, c.cooperateSmooth);

                timer -= dt;
                if (timer <= 0f) c.states.Change(CompanionStateId.Following);
            }
        }
    }
}
