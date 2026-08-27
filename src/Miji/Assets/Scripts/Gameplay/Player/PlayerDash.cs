using Miji.Core.Combat;
using Miji.Core.Progression;
using UnityEngine;

namespace Miji.Gameplay.Player
{
    /// <summary>
    /// F1 돌진 — 첫 진행 능력. 이동 게이트(넓은 간격·약한 벽)와 돌진 공격을 겸한다
    /// (`MECHANIC_movement.md` 2절). <see cref="ProgressionState"/>가 Unlocked일 때만 발동 —
    /// 미획득/잠김이면 버튼이 조용히 무시된다(3상태 게이트의 첫 실사용).
    ///
    /// 돌진 중에는 중력을 끄고 수평 속도를 고정한다. 모터의 일반 이동은
    /// <see cref="PlayerController"/>가 Dashing 상태 동안 쉬게 한다.
    /// </summary>
    [RequireComponent(typeof(PlayerController), typeof(Rigidbody2D))]
    public class PlayerDash : MonoBehaviour
    {
        [Tooltip("ProgressionState에서 조회할 능력 ID.")]
        [SerializeField] string abilityId = "F1";

        [Header("돌진 — 무게 층이 아니라 능력이다")]
        [SerializeField] float dashSpeed = 14f;
        [SerializeField] float dashDuration = 0.14f;
        [SerializeField] float cooldown = 0.55f;

        [Tooltip("돌진 공격 겸용 히트박스. 근접 것을 재사용해도 된다. 비우면 이동 전용.")]
        [SerializeField] Hitbox hitbox;

        PlayerController controller;
        PlayerMotor motor;
        Rigidbody2D body;

        float dashLeft;
        float cooldownLeft;
        float savedGravityScale;
        int dashDirection;
        bool airDashUsed;

        public bool IsDashing => dashLeft > 0f;

        void Awake()
        {
            controller = GetComponent<PlayerController>();
            motor = GetComponent<PlayerMotor>();
            body = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            cooldownLeft -= Time.deltaTime;

            if (motor.IsGrounded && !IsDashing) airDashUsed = false;

            if (!IsDashing
                && controller.Intent.AbilityPressed
                && cooldownLeft <= 0f
                && !airDashUsed
                && ProgressionState.Current.IsUsable(abilityId))
            {
                BeginDash();
            }
        }

        void BeginDash()
        {
            dashLeft = dashDuration;
            cooldownLeft = cooldown;
            dashDirection = motor.Facing;
            if (!motor.IsGrounded) airDashUsed = true; // 공중 돌진은 착지까지 한 번

            savedGravityScale = body.gravityScale;
            body.gravityScale = 0f;

            controller.ChangeState(PlayerStateId.Dashing);
            hitbox?.BeginWindow();
        }

        void FixedUpdate()
        {
            if (!IsDashing) return;

            body.linearVelocity = new Vector2(dashDirection * dashSpeed, 0f);
            if (hitbox != null) hitbox.Sweep();

            dashLeft -= Time.fixedDeltaTime;
            if (dashLeft <= 0f) EndDash();
        }

        void EndDash()
        {
            dashLeft = 0f;
            body.gravityScale = savedGravityScale;

            // 돌진 관성이 최고속을 넘겨 남지 않게 — 게이트 수치(점프 아크)를 오염시키지 않는다.
            var velocity = body.linearVelocity;
            velocity.x = Mathf.Clamp(velocity.x, -5.5f, 5.5f);
            body.linearVelocity = velocity;

            controller.ChangeState(motor.IsGrounded ? PlayerStateId.Grounded : PlayerStateId.Airborne);
        }

        void OnDisable()
        {
            if (IsDashing) EndDash();
        }
    }
}
