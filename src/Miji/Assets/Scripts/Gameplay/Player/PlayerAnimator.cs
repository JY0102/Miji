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

        SpriteRenderer sprite;
        Animator animator;

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

            // 기본 스프라이트가 오른쪽(동쪽)을 본다 — 왼쪽을 볼 때만 뒤집는다.
            sprite.flipX = motor.Facing < 0;
        }
    }
}
