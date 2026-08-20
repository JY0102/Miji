using Miji.Gameplay.Player;
using UnityEngine;

namespace Miji.Gameplay.Companion
{
    /// <summary>
    /// B(무리비)의 테스트용 추종 — A의 바로 뒤를 따라다니는 것이 전부다.
    ///
    /// 확정 설계(이원 무브셋 3절)의 뼈대를 미리 지킨다:
    /// - 플레이어는 B를 관리하지 않는다 — 입력도 콜라이더도 없다
    /// - 너무 처지면 연출 없이 즉시 복귀한다 (「B가 멀어서 못 했다」는 상황 금지)
    /// - B는 무적이므로 Health/Hurtbox를 붙이지 않는다 (붕괴는 전투 피해가 아니다)
    ///
    /// F2 받침·F5 투척의 협력 스냅 로직은 여기가 아니라 G6(B 협력)에서 붙는다.
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

        [Tooltip("이보다 멀어지면 연출 없이 즉시 복귀한다.")]
        [SerializeField] float snapDistance = 8f;

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

        static readonly int SpeedParam = Animator.StringToHash("Speed");
        static readonly int AsleepParam = Animator.StringToHash("IsAsleep");

        SpriteRenderer sprite;
        float stillTimer;
        int lastFace = 1;
        float turnTimer;
        int turnFrom = 1;
        Vector3 basePosition;   // 들썩임을 뺀 실제 추적 위치
        Vector3 velocity;
        float bobPhase;

        void Awake()
        {
            sprite = GetComponent<SpriteRenderer>();
            if (target == null) target = FindFirstObjectByType<PlayerMotor>();
            basePosition = transform.position;
        }

        void LateUpdate()
        {
            if (target == null) return;

            var anchor = target.transform.position
                         + new Vector3(-target.Facing * followDistance, 0f, 0f);

            if ((anchor - basePosition).sqrMagnitude > snapDistance * snapDistance)
            {
                basePosition = anchor;
                velocity = Vector3.zero;
            }
            else
            {
                basePosition = Vector3.SmoothDamp(basePosition, anchor, ref velocity, smoothTime);
            }

            var speed = new Vector2(velocity.x, velocity.y).magnitude;

            if (animator != null)
            {
                animator.SetFloat(SpeedParam, speed);
                transform.position = basePosition;

                // 오래 가만히 있으면 잠든다. 움직이면 즉시 깬다.
                stillTimer = speed < 0.15f ? stillTimer + Time.deltaTime : 0f;
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
    }
}
