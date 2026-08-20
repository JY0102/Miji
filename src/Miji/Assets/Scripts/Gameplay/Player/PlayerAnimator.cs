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

        SpriteRenderer sprite;
        Animator animator;
        int lastFacing = 1;
        float turnTimer;
        int turnFrom = 1;   // 회전을 시작할 때 보고 있던 방향

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

            // 기본 스프라이트가 오른쪽(동쪽)을 본다 — 왼쪽을 볼 때만 뒤집는다.
            sprite.flipX = motor.Facing < 0;
        }
    }
}
