using Miji.Core.Events;
using Miji.Core.Progression;
using UnityEngine;

namespace Miji.Gameplay.Player
{
    /// <summary>
    /// F2 받침 — <b>둘의 동작</b>. A가 공중에서 점프를 다시 누르면 B가 파고들어 한 번 더 올린다
    /// (이원 무브셋 2절). <see cref="ProgressionState"/>의 F2가 Unlocked일 때만 발동 —
    /// 미획득/잠김(결별)이면 조용히 무시된다.
    ///
    /// ★ 「A는 스스로 높이를 얻지 못한다」(이원 무브셋 1절)가 여기서도 성립한다 —
    ///   두 번째 높이의 근거는 F2뿐이고, 잠기면 A는 다시 한 번의 도약만 남는다.
    ///   물리적으로 A를 밀어올리는 건 이 컴포넌트지만, <b>서사·게이트상 그 힘은 B의 것</b>이다.
    ///
    /// F1 <see cref="PlayerDash"/>와 나란한 능력 컴포넌트다(같은 3상태 게이트, 같은 부착 방식).
    /// </summary>
    [RequireComponent(typeof(PlayerController), typeof(Rigidbody2D))]
    public class PlayerBoost : MonoBehaviour
    {
        [Tooltip("ProgressionState에서 조회할 능력 ID.")]
        [SerializeField] string abilityId = "F2";

        [Header("받침 — 두 번째 도약")]
        [Tooltip("받침이 주는 두 번째 도약 높이(월드 유닛). jumpHeight(1.7)는 게이트 근거라 불가침이지만 " +
                 "이 값은 자유 튜닝이다. jumpHeight + boostHeight 가 Ledge_TooHigh(3.65)를 넘어야 F2 게이트가 열린다.")]
        [SerializeField] float boostHeight = 2.2f;
        [Tooltip("받침 직후 재발동 잠금. 한 번의 체공에 한 번이지만 연타 오발을 막는다.")]
        [SerializeField] float cooldown = 0.2f;

        PlayerController controller;
        PlayerMotor motor;
        Rigidbody2D body;

        bool usedThisAir;
        float cooldownLeft;

        void Awake()
        {
            controller = GetComponent<PlayerController>();
            motor = GetComponent<PlayerMotor>();
            body = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            cooldownLeft -= Time.deltaTime;

            // 착지하면 다시 한 번 받침을 쓸 수 있다. 한 체공에 한 번.
            if (motor.IsGrounded) usedThisAir = false;

            if (motor.IsGrounded || usedThisAir || cooldownLeft > 0f) return;
            if (!controller.Intent.JumpPressed) return;                     // 공중에서 점프를 「다시」 누른 것
            if (!ProgressionState.Current.IsUsable(abilityId)) return;      // 미획득/잠김이면 무시

            Boost();
        }

        void Boost()
        {
            usedThisAir = true;
            cooldownLeft = cooldown;

            // 정점에서 눌러야 최대 높이가 나오지만, 이르게 눌러도 손해는 없게 Max로 얹는다
            // (이미 그보다 빠르게 상승 중이면 그대로 둔다). B가 밀어 올리는 힘은 A의 무게와 무관하게 일정.
            var boostVelocity = Mathf.Sqrt(2f * Mathf.Abs(Physics2D.gravity.y) * body.gravityScale * boostHeight);
            var v = body.linearVelocity;
            v.y = Mathf.Max(v.y, boostVelocity);
            body.linearVelocity = v;

            // B가 어디 있든 파고들어 받치도록 신호를 쏜다 — 위치·방향을 실어 스냅 목표를 준다.
            EventBus.Publish(new BoostRequestedSignal(transform.position, motor.Facing));
        }

        void OnDisable()
        {
            usedThisAir = false;
            cooldownLeft = 0f;
        }
    }
}
