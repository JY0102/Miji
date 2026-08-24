using UnityEngine;

namespace Miji.Gameplay.Player
{
    /// <summary>
    /// A의 겉모습만 담당한다. 몸(<see cref="PlayerMotor"/>)의 상태를 읽어
    /// Animator 파라미터로 옮기고, 바라보는 방향으로 스프라이트를 뒤집는다.
    /// 조작·물리에는 손대지 않는다 — 이 컴포넌트를 통째로 꺼도 게임은 그대로 돈다.
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer), typeof(Animator))]
    public class PlayerAnimator : MonoBehaviour
    {
        static readonly int SpeedParam = Animator.StringToHash("Speed");
        static readonly int GroundedParam = Animator.StringToHash("IsGrounded");
        static readonly int VerticalParam = Animator.StringToHash("VerticalVelocity");

        [SerializeField] PlayerMotor motor;

        [Header("방향 전환 — 180도를 3프레임으로 돈다")]
        [Tooltip("측면과 정면 사이 45도. 비우면 정면만 거친다.")]
        [SerializeField] Sprite turnQuarterSprite;
        [Tooltip("대칭 정면 스프라이트. 비우면 즉시 좌우 반전한다.")]
        [SerializeField] Sprite turnFrontSprite;
        [Tooltip("회전 전체 시간. 3등분해서 45°(구방향) → 정면 → 45°(신방향)로 쓴다.")]
        [SerializeField] float turnDuration = 0.14f;

        [Header("낙하 기울기 — 하강 거리로 구동한다")]
        [Tooltip("직립(0) → 최대 다이빙(끝) 순서의 낙하 프레임. 비우면 A_Fall 클립이 그대로 재생된다.")]
        [SerializeField] Sprite[] fallTiltFrames;
        [Tooltip("이만큼(월드 유닛) 떨어지면 최대 기울기에 도달한다. 정점부터 잰다. 작을수록 빨리 기운다.")]
        [SerializeField] float fallForMaxTilt = 1.5f;

        SpriteRenderer sprite;
        Animator animator;
        int lastFacing = 1;
        float turnTimer;
        int turnFrom = 1;   // 회전을 시작할 때 보고 있던 방향
        float fallStartY;   // 하강이 시작된(정점) 월드 y
        bool trackingFall;

        void Awake()
        {
            sprite = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
            if (motor == null) motor = GetComponentInParent<PlayerMotor>();
        }

        void LateUpdate()
        {
            if (motor == null) return;

            animator.SetFloat(SpeedParam, motor.HorizontalSpeed);
            animator.SetBool(GroundedParam, motor.IsGrounded);
            animator.SetFloat(VerticalParam, motor.Velocity.y);

            if (motor.Facing != lastFacing)
            {
                turnFrom = lastFacing;
                lastFacing = motor.Facing;
                if (turnFrontSprite != null) turnTimer = turnDuration;
            }

            // Animator가 이미 이번 프레임의 스프라이트를 썼으므로, 여기서 덮으면 이긴다.
            if (turnTimer > 0f)
            {
                turnTimer -= Time.deltaTime;
                if (Miji.Gameplay.View.TurnView.Apply(sprite, turnTimer, turnDuration, turnFrom, motor.Facing,
                                                      turnQuarterSprite, turnFrontSprite))
                    return;
            }

            // 낙하 기울기 — 정점부터 잰 하강 거리로 프레임을 고른다. 물리에는 손대지 않는다.
            ApplyFallTilt();

            // 기본 스프라이트가 오른쪽(동쪽)을 본다 — 왼쪽을 볼 때만 뒤집는다.
            sprite.flipX = motor.Facing < 0;
        }

        /// <summary>
        /// 공중에서 하강 중일 때만 낙하 프레임으로 덮어쓴다. 접지·상승 중에는 아무것도
        /// 안 하므로 Animator의 클립이 그대로 보인다(프레임 배열이 비어도 마찬가지).
        /// </summary>
        void ApplyFallTilt()
        {
            bool descending = !motor.IsGrounded && motor.Velocity.y < 0f;
            if (!descending)
            {
                trackingFall = false;   // 접지·상승 시 리셋 → 다음 하강은 정점부터 다시 잰다
                return;
            }

            if (!trackingFall)
            {
                fallStartY = motor.transform.position.y;   // 정점 근처를 기준점으로
                trackingFall = true;
            }

            if (fallTiltFrames == null || fallTiltFrames.Length == 0 || fallForMaxTilt <= 0f) return;

            float descent = fallStartY - motor.transform.position.y;
            float t = Mathf.Clamp01(descent / fallForMaxTilt);
            int idx = Mathf.Clamp(Mathf.RoundToInt(t * (fallTiltFrames.Length - 1)), 0, fallTiltFrames.Length - 1);
            if (fallTiltFrames[idx] != null) sprite.sprite = fallTiltFrames[idx];
        }
    }
}
