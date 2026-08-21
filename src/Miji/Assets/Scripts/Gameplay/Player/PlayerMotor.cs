using UnityEngine;

namespace Miji.Gameplay.Player
{
    /// <summary>
    /// A의 몸을 실제로 움직이는 부분. 「어떤 상태인가」는 모르고 「어떻게 움직이나」만 안다.
    ///
    /// 조작감 척추(2026-08-21 개정 — `MECHANIC_movement.md` 1절 / `MECHANIC_GAME_FEEL.md` 0절):
    /// **몸은 가볍게, 기계는 연출 층에서.**
    /// 「묵직·기계적」은 폐기됐다. 무게를 물리(가속 지연·낮은 공중 제어·경직)로 표현하면
    /// 손에는 100% 「입력이 씹힌다」로만 읽힌다. A가 기계라는 것은
    /// **소리·애니메이션 관성·파티클**이 말하고, 물리는 즉각 반응한다.
    ///
    /// 그래서 ⑴ 가속이 짧고 ⑵ **공중 제어가 지상에 가깝고** ⑶ 낙하가 상승보다 조금 빠르다.
    ///
    /// 여기에 **억울하지 않게** 만드는 관용 기법을 얹는다 —
    /// 코요테 타임 / 점프 버퍼 / 코너 보정 / 에이펙스 조정 / 반전 스냅.
    /// 관용은 무게와 직교한다 — 가벼워져도 이것들은 그대로 필요하다.
    ///
    /// ⛔ 2단 점프는 여기 없다 — 「A는 스스로 높이를 얻지 못한다」(이원 무브셋 1절).
    /// 두 번째 높이는 F2(B의 받침)에서 온다.
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMotor : MonoBehaviour
    {
        [Header("이동 — 가볍게 (2026-08-21 경량 개정)")]
        [SerializeField] float maxSpeed = 6.2f;
        [SerializeField] float groundAcceleration = 60f;
        [SerializeField] float groundDeceleration = 38f;
        [Tooltip("공중 제어는 지상에 가깝게. 경량 액션 플랫포머의 공통 문법이다(FEEL_REFERENCES A 계열).")]
        [SerializeField] float airAcceleration = 40f;
        [SerializeField] float airDeceleration = 24f;
        [Tooltip("반대 방향을 눌렀을 때의 감속 배수. 턴이 즉각 반응하게 한다(무게는 유지).")]
        [Range(1f, 4f)][SerializeField] float turnAroundMultiplier = 2.1f;

        [Header("점프 — 짧은 한 번")]
        [Tooltip("도달 높이(월드 유닛). 위로 갈 여지를 남겨 낮게 잡는다.\n" +
                 "⚠️ 경량 개정에서도 1.7 유지 — 이 값이 F2 게이트의 물리적 근거다. " +
                 "올리려면 Ledge_TooHigh(3.65)와 WovenNest 다리 높이(2.0/4.0)를 같이 올려야 한다.")]
        [SerializeField] float jumpHeight = 1.7f;
        [Tooltip("상승 중 버튼을 떼면 남은 상승 속도를 이만큼으로 깎는다.")]
        [Range(0f, 1f)][SerializeField] float jumpCutMultiplier = 0.45f;
        [Tooltip("점프 직후 재점프 잠금. 발이 아직 땅 판정에 걸려 있을 때 이중 임펄스를 막는다.")]
        [SerializeField] float jumpLockout = 0.08f;

        [Header("중력 — 낙하가 더 빠르다")]
        [SerializeField] float gravityScale = 3.6f;
        [Tooltip("내려갈 때 중력 배수. 1보다 크면 체공이 짧아진다. 경량 개정에서 1.35 → 1.2로 완화.")]
        [SerializeField] float fallGravityMultiplier = 1.2f;
        [SerializeField] float maxFallSpeed = 18f;

        [Header("에이펙스 조정 — 정점에서만 숨을 준다")]
        [Tooltip("이 속도 이하를 정점으로 본다.")]
        [SerializeField] float apexThreshold = 2.2f;
        [Tooltip("정점 중력 배수. 1보다 작으면 정점이 살짝 늘어나 착지 지점을 조준할 수 있다.")]
        [Range(0.2f, 1f)][SerializeField] float apexGravityMultiplier = 0.65f;
        [Tooltip("정점에서의 공중 가속 배수. 체공이 짧은 대신 그 순간만 제어를 준다.")]
        [Range(1f, 2.5f)][SerializeField] float apexControlBonus = 1.45f;

        [Header("접지·충돌 관용")]
        [SerializeField] LayerMask groundLayers;
        [Tooltip("발밑 감지 박스의 두께.")]
        [SerializeField] float groundCheckThickness = 0.08f;
        [Tooltip("발이 땅에서 떨어진 뒤에도 이 시간 안에는 점프가 먹는다.")]
        [SerializeField] float coyoteTime = 0.09f;
        [Tooltip("착지 전에 누른 점프를 이 시간 동안 기억한다.")]
        [SerializeField] float jumpBufferTime = 0.11f;
        [Tooltip("머리가 천장 모서리에 걸렸을 때 옆으로 밀어 통과시키는 최대 거리. 0이면 끔.")]
        [SerializeField] float cornerCorrection = 0.22f;

        Rigidbody2D body;
        Collider2D footprint;

        float coyoteCounter;
        float jumpBufferCounter;
        float jumpLockoutCounter;
        bool jumpHeld;
        bool roseThisJump;

        public bool IsGrounded { get; private set; }
        public Vector2 Velocity => body.linearVelocity;
        public float HorizontalSpeed => Mathf.Abs(body.linearVelocity.x);

        /// <summary>정점 근처인가. 연출·애니메이션이 참고할 수 있다.</summary>
        public bool IsNearApex => !IsGrounded && Mathf.Abs(body.linearVelocity.y) < apexThreshold;

        /// <summary>바라보는 방향. 멈춰 있어도 유지된다(스프라이트 뒤집기·공격 방향용).</summary>
        public int Facing { get; private set; } = 1;

        void Awake()
        {
            body = GetComponent<Rigidbody2D>();
            footprint = GetComponent<Collider2D>();

            body.gravityScale = gravityScale;
            body.freezeRotation = true;
            body.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            body.interpolation = RigidbodyInterpolation2D.Interpolate;

            if (groundLayers.value == 0)
                Debug.LogWarning($"{nameof(PlayerMotor)}: groundLayers가 비어 있어 접지 판정이 항상 실패한다.", this);
        }

        /// <summary>매 프레임 호출. 코요테·점프 버퍼 같은 시간 창을 굴린다.</summary>
        public void TickTimers(float deltaTime)
        {
            jumpLockoutCounter -= deltaTime;

            // 점프 직후엔 발이 아직 땅에 걸려 있어도 코요테를 재충전하지 않는다.
            var canRefill = IsGrounded && jumpLockoutCounter <= 0f;
            coyoteCounter = canRefill ? coyoteTime : coyoteCounter - deltaTime;

            jumpBufferCounter -= deltaTime;
        }

        public void RequestJump() => jumpBufferCounter = jumpBufferTime;

        public void SetJumpHeld(bool held) => jumpHeld = held;

        /// <summary>물리 스텝. 접지 → 수평 이동 → 점프 → 중력 보정 → 천장 처리 순.</summary>
        public void FixedTick(float dir, float fixedDeltaTime)
        {
            IsGrounded = CheckGrounded();

            ApplyHorizontal(dir, fixedDeltaTime);
            TryConsumeJump();
            ApplyGravityFeel(fixedDeltaTime);
            ResolveCeiling();

            if (!Mathf.Approximately(dir, 0f))
                Facing = dir > 0f ? 1 : -1;
        }

        bool CheckGrounded()
        {
            if (footprint == null) return false;

            var bounds = footprint.bounds;
            var center = new Vector2(bounds.center.x, bounds.min.y - groundCheckThickness * 0.5f);
            var size = new Vector2(bounds.size.x * 0.92f, groundCheckThickness);

            return Physics2D.OverlapBox(center, size, 0f, groundLayers) != null;
        }

        void ApplyHorizontal(float dir, float dt)
        {
            var target = dir * maxSpeed;
            var velocity = body.linearVelocity;
            var wantsMove = !Mathf.Approximately(dir, 0f);

            var rate = IsGrounded
                ? (wantsMove ? groundAcceleration : groundDeceleration)
                : (wantsMove ? airAcceleration : airDeceleration);

            // 반전 스냅 — 가던 방향과 반대를 누르면 더 빨리 속도를 꺾는다.
            var reversing = wantsMove
                            && !Mathf.Approximately(velocity.x, 0f)
                            && Mathf.Sign(dir) != Mathf.Sign(velocity.x);
            if (reversing) rate *= turnAroundMultiplier;

            // 정점에서만 공중 제어를 조금 준다 — 체공이 짧은 것을 보상한다.
            if (IsNearApex) rate *= apexControlBonus;

            velocity.x = Mathf.MoveTowards(velocity.x, target, rate * dt);
            body.linearVelocity = velocity;
        }

        void TryConsumeJump()
        {
            if (jumpBufferCounter <= 0f || coyoteCounter <= 0f) return;

            var velocity = body.linearVelocity;
            velocity.y = JumpVelocity();
            body.linearVelocity = velocity;

            jumpBufferCounter = 0f;
            coyoteCounter = 0f;
            jumpLockoutCounter = jumpLockout;
            roseThisJump = true;
        }

        void ApplyGravityFeel(float dt)
        {
            var velocity = body.linearVelocity;

            // 상승 중 버튼을 떼면 즉시 짧아진다 — 조작이 아니라 기계가 힘을 멈춘 느낌.
            if (roseThisJump && velocity.y > 0f && !jumpHeld)
            {
                velocity.y *= jumpCutMultiplier;
                roseThisJump = false;
            }

            if (velocity.y <= 0f) roseThisJump = false;

            // 구간별 중력 배수 하나로 합친다: 정점 → 완화 / 낙하 → 가중 / 상승 → 그대로.
            var multiplier = 1f;
            if (!IsGrounded && Mathf.Abs(velocity.y) < apexThreshold) multiplier = apexGravityMultiplier;
            else if (velocity.y < 0f) multiplier = fallGravityMultiplier;

            if (!Mathf.Approximately(multiplier, 1f))
                velocity.y += Physics2D.gravity.y * gravityScale * (multiplier - 1f) * dt;

            velocity.y = Mathf.Max(velocity.y, -maxFallSpeed);
            body.linearVelocity = velocity;
        }

        /// <summary>
        /// 천장 처리. 상승 중 머리가 모서리에 살짝 걸렸을 뿐이라면 옆으로 밀어 통과시키고,
        /// 정말 막혀 있으면 상승을 끊어 천장에 달라붙지 않게 한다.
        /// </summary>
        void ResolveCeiling()
        {
            if (footprint == null || body.linearVelocity.y <= 0f) return;
            if (!HeadBlocked(0f)) return;

            if (cornerCorrection > 0f)
            {
                // 가까운 쪽부터 넓혀가며 빠져나갈 틈을 찾는다.
                const int steps = 4;
                for (var i = 1; i <= steps; i++)
                {
                    var offset = cornerCorrection * i / steps;

                    // 진행 방향 쪽을 먼저 시도한다 — 가려던 데로 빠져나가는 게 자연스럽다.
                    var first = Facing >= 0 ? offset : -offset;
                    if (!HeadBlocked(first))
                    {
                        body.position += new Vector2(first, 0f);
                        return;
                    }

                    if (!HeadBlocked(-first))
                    {
                        body.position += new Vector2(-first, 0f);
                        return;
                    }
                }
            }

            var velocity = body.linearVelocity;
            velocity.y = 0f;
            body.linearVelocity = velocity;
        }

        bool HeadBlocked(float xOffset)
        {
            var bounds = footprint.bounds;
            var center = new Vector2(
                bounds.center.x + xOffset,
                bounds.max.y + groundCheckThickness * 0.5f);
            var size = new Vector2(bounds.size.x * 0.86f, groundCheckThickness);

            return Physics2D.OverlapBox(center, size, 0f, groundLayers) != null;
        }

        float JumpVelocity() =>
            Mathf.Sqrt(2f * Mathf.Abs(Physics2D.gravity.y) * gravityScale * jumpHeight);

        void OnDrawGizmosSelected()
        {
            var col = GetComponent<Collider2D>();
            if (col == null) return;

            var bounds = col.bounds;

            Gizmos.color = IsGrounded ? Color.green : Color.red;
            Gizmos.DrawWireCube(
                new Vector3(bounds.center.x, bounds.min.y - groundCheckThickness * 0.5f, 0f),
                new Vector3(bounds.size.x * 0.92f, groundCheckThickness, 0f));

            // 코너 보정이 훑는 폭
            Gizmos.color = new Color(1f, 0.8f, 0.2f, 0.7f);
            Gizmos.DrawWireCube(
                new Vector3(bounds.center.x, bounds.max.y + groundCheckThickness * 0.5f, 0f),
                new Vector3(bounds.size.x * 0.86f + cornerCorrection * 2f, groundCheckThickness, 0f));
        }
    }
}
