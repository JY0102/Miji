using Miji.Core.Combat;
using Miji.Gameplay.Player;
using UnityEngine;

namespace Miji.Gameplay.Enemies
{
    /// <summary>
    /// 지상 근접 잡몹의 행동. 두 종(야생 생물·붕괴자)을 <b>직렬화 값만</b>으로 가른다
    /// (`MECHANIC_movement.md` 전투 절 — 야생=영역 방어, 붕괴자=허스크 계보).
    ///
    /// ⚠️ 임시 구현이다. 적 행동은 PlayMaker FSM으로 짜기로 확정돼 있으나(2026-08-27, `DECISIONS.md`)
    /// 구매 전까지 C# enum-switch FSM으로 대신 짠다(2026-08-31 사용자 결정). 상태·전이를 PlayMaker
    /// 상태표와 1:1로 맞춰 나중 이식이 값 옮기기가 되게 한다 — 전이 판정은 <see cref="NextState"/> 순수 함수.
    ///
    /// 피격·HP·넉백·히트스톱은 CombatCore(Health/Hurtbox/Hitbox/DamageResponse)를 그대로 쓴다. 여긴 행동만.
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Health))]
    public class EnemyAI : MonoBehaviour
    {
        public enum State { Patrol, Chase, Attack, Dead }

        [Header("대상 — 비우면 씬에서 A를 찾는다")]
        [SerializeField] Transform target;

        [Header("감지 — 값의 크기가 곧 두 종을 가른다")]
        [Tooltip("이 수평 거리 안이면 쫓는다.")]
        [SerializeField] float aggroRange = 5f;
        [Tooltip("이 수평 거리 안이면 공격한다.")]
        [SerializeField] float attackRange = 0.9f;
        [Tooltip("이 높이차를 넘으면 감지·공격하지 않는다(다른 층의 A를 못 본다).")]
        [SerializeField] float verticalTolerance = 1.2f;
        [Tooltip("스폰 지점에서 이만큼 벗어나면 포기하고 돌아온다. 0이면 리시 없음(붕괴자 — 계속 다가옴).")]
        [SerializeField] float leashRange = 7f;

        [Header("이동")]
        [Tooltip("순찰 왕복 반경. 0이면 제자리(휴면 허스크).")]
        [SerializeField] float patrolReach = 3f;
        [SerializeField] float patrolSpeed = 1.5f;
        [SerializeField] float chaseSpeed = 3f;

        [Header("공격 — 선딜 → 활성 창 → 후딜 → 쿨다운")]
        [SerializeField] Hitbox hitbox;
        [SerializeField] float windup = 0.25f;
        [SerializeField] float activeDuration = 0.12f;
        [SerializeField] float recover = 0.3f;
        [SerializeField] float cooldown = 0.8f;

        [Header("지형 감지 — 벽·낭떠러지에서 멈춘다")]
        [SerializeField] LayerMask groundMask = 1 << 6;
        [SerializeField] float probeDistance = 0.35f;

        enum AttackPhase { None, Windup, Active, Recover }

        Rigidbody2D body;
        Health health;
        SpriteRenderer sprite;
        Collider2D bodyCol;
        float homeX;
        int facing = 1;           // 바라보는 방향(+1 오른쪽)
        int patrolDir = 1;
        float hitboxHomeX;

        State state = State.Patrol;
        AttackPhase attackPhase = AttackPhase.None;
        float phaseTimer;
        float cooldownLeft;

        public State CurrentState => state;

        void Awake()
        {
            body = GetComponent<Rigidbody2D>();
            health = GetComponent<Health>();
            sprite = GetComponentInChildren<SpriteRenderer>();
            bodyCol = GetComponent<Collider2D>();
            homeX = transform.position.x;

            // 히트박스는 늘 켜두고 Sweep을 부르는 창에만 판정한다(PlayerAttack와 같은 패턴).
            if (hitbox == null) hitbox = GetComponentInChildren<Hitbox>(true);
            if (hitbox != null) hitboxHomeX = Mathf.Abs(hitbox.transform.localPosition.x);
            if (target == null)
            {
                var player = FindFirstObjectByType<PlayerController>();
                if (player != null) target = player.transform;
            }
        }

        void OnEnable() => health.Died += OnDied;
        void OnDisable() => health.Died -= OnDied;

        void OnDied(DamageInfo _)
        {
            state = State.Dead;
            attackPhase = AttackPhase.None;
            // 넉백·중력은 그대로 두어 시체가 밀려나고 떨어진다. 수평 제어만 놓는다.
        }

        /// <summary>
        /// 센서 → 상태. PlayMaker 전이표와 같은 것 — 이식 때 이 표를 그대로 옮긴다.
        /// 리시(영역 이탈)가 최우선(야생이 물러선다), 그다음 사거리, 그다음 감지.
        /// </summary>
        public static State NextState(State current, bool inAggro, bool inAttackRange, bool beyondLeash)
        {
            if (current == State.Dead) return State.Dead;
            if (beyondLeash) return State.Patrol;
            if (inAttackRange) return State.Attack;
            if (inAggro) return State.Chase;
            return State.Patrol;
        }

        void FixedUpdate()
        {
            if (state == State.Dead) return;

            // ── 공격 창이 진행 중이면 붙박여 마무리한다(전이 판정을 잠근다) ──
            if (state == State.Attack && attackPhase != AttackPhase.None)
            {
                TickAttack();
                body.linearVelocity = new Vector2(0f, body.linearVelocity.y);
                return;
            }

            // ── 센서 ──
            bool hasTarget = target != null;
            float dx = hasTarget ? target.position.x - transform.position.x : float.PositiveInfinity;
            float dy = hasTarget ? Mathf.Abs(target.position.y - transform.position.y) : float.PositiveInfinity;
            bool sameLevel = dy <= verticalTolerance;
            bool inAggro = hasTarget && Mathf.Abs(dx) <= aggroRange && sameLevel;
            bool inAttack = hasTarget && Mathf.Abs(dx) <= attackRange && sameLevel;
            bool beyondLeash = leashRange > 0f && Mathf.Abs(transform.position.x - homeX) > leashRange;

            cooldownLeft -= Time.fixedDeltaTime;
            var desired = NextState(state, inAggro, inAttack, beyondLeash);

            // 쿨다운 중엔 공격에 못 들어간다 — 사거리 안이면 붙어서 기다린다(Chase).
            if (desired == State.Attack && cooldownLeft > 0f)
                desired = State.Chase;

            state = desired;

            switch (state)
            {
                case State.Patrol: DoPatrol(); break;
                case State.Chase: DoChase(dx); break;
                case State.Attack: EnterAttack(dx); break;
            }
        }

        void DoPatrol()
        {
            if (patrolReach <= 0f) { Halt(); return; }   // 휴면 허스크

            // 순찰 범위 끝·벽·낭떠러지에서 되돈다.
            float offset = transform.position.x - homeX;
            if (offset > patrolReach) patrolDir = -1;
            else if (offset < -patrolReach) patrolDir = 1;
            else if (Blocked(patrolDir)) patrolDir = -patrolDir;

            Face(patrolDir);
            body.linearVelocity = new Vector2(patrolDir * patrolSpeed, body.linearVelocity.y);
        }

        void DoChase(float dx)
        {
            int dir = dx >= 0f ? 1 : -1;
            Face(dir);
            // 낭떠러지·벽 너머로는 쫓지 않는다(자살 방지). 멈춰서 A가 오길 기다린다.
            body.linearVelocity = Blocked(dir)
                ? new Vector2(0f, body.linearVelocity.y)
                : new Vector2(dir * chaseSpeed, body.linearVelocity.y);
        }

        void EnterAttack(float dx)
        {
            Face(dx >= 0f ? 1 : -1);   // 시작 순간 방향 고정
            Halt();
            attackPhase = AttackPhase.Windup;
            phaseTimer = windup;
        }

        void TickAttack()
        {
            phaseTimer -= Time.fixedDeltaTime;
            switch (attackPhase)
            {
                case AttackPhase.Windup:
                    if (phaseTimer <= 0f)
                    {
                        attackPhase = AttackPhase.Active;
                        phaseTimer = activeDuration;
                        if (hitbox != null) hitbox.BeginWindow();
                    }
                    break;
                case AttackPhase.Active:
                    if (hitbox != null) hitbox.Sweep();
                    if (phaseTimer <= 0f)
                    {
                        attackPhase = AttackPhase.Recover;
                        phaseTimer = recover;
                    }
                    break;
                case AttackPhase.Recover:
                    if (phaseTimer <= 0f)
                    {
                        attackPhase = AttackPhase.None;
                        cooldownLeft = cooldown;   // 다음 스윙까지의 간격
                    }
                    break;
            }
        }

        void Halt() => body.linearVelocity = new Vector2(0f, body.linearVelocity.y);

        void Face(int dir)
        {
            if (dir == 0 || dir == facing) return;
            facing = dir;
            if (sprite != null) sprite.flipX = facing < 0;
            if (hitbox != null)
            {
                var p = hitbox.transform.localPosition;
                p.x = hitboxHomeX * facing;
                hitbox.transform.localPosition = p;
            }
        }

        // 진행 방향에 벽이 있거나(수평 레이) 발밑 앞이 낭떠러지면(하향 레이) 막힌 것으로 본다.
        bool Blocked(int dir)
        {
            if (bodyCol == null) return false;
            var b = bodyCol.bounds;
            var side = new Vector2(dir > 0 ? b.max.x : b.min.x, b.center.y);
            if (Physics2D.Raycast(side, Vector2.right * dir, probeDistance, groundMask)) return true;

            var ahead = new Vector2(side.x + dir * probeDistance, b.min.y + 0.05f);
            return !Physics2D.Raycast(ahead, Vector2.down, probeDistance + 0.2f, groundMask);
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            float h = Application.isPlaying ? homeX : transform.position.x;
            Gizmos.DrawWireCube(new Vector3(h, transform.position.y, 0f), new Vector3(aggroRange * 2f, verticalTolerance * 2f, 0f));
        }
    }
}
