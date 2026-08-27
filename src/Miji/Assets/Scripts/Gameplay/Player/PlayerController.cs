using Miji.Core.Input;
using Miji.Core.StateMachines;
using UnityEngine;

namespace Miji.Gameplay.Player
{
    public enum PlayerStateId
    {
        Grounded,
        Airborne,

        /// <summary>F1 돌진 중. 모터의 일반 이동·중력이 꺼지고 <see cref="PlayerDash"/>가 몸을 몬다.</summary>
        Dashing
    }

    /// <summary>
    /// A(딸각)의 조작 몸. <see cref="IPossessable"/>이므로 자기가 사람에게 조작되는지
    /// AI·리플레이에 조작되는지 모른다 — 의도를 받아서 움직일 뿐이다.
    ///
    /// 상태는 Grounded / Airborne 둘만 둔다. Idle·Move를 나누지 않는 것은 의도다
    /// (둘의 처리가 같고, 서 있음/걷는 중은 속도에서 파생되는 연출 문제다).
    /// 의미 있는 상태 — 돌진(F1)·공격·피격 — 는 해당 기능을 붙일 때 추가한다.
    /// </summary>
    [RequireComponent(typeof(PlayerMotor))]
    public class PlayerController : MonoBehaviour, IPossessable
    {
        [SerializeField] PlayerMotor motor;

        readonly StateMachine<PlayerStateId> states = new();
        InputIntent intent;

        public PlayerMotor Motor => motor;
        public InputIntent Intent => intent;
        public PlayerStateId StateId => states.CurrentKey;

        void Awake()
        {
            if (motor == null) motor = GetComponent<PlayerMotor>();

            states.Add(PlayerStateId.Grounded, new GroundedState(this));
            states.Add(PlayerStateId.Airborne, new AirborneState(this));
            states.Add(PlayerStateId.Dashing, new DashingState());
            states.Change(PlayerStateId.Airborne); // 접지가 확인되면 첫 프레임에 내려온다
        }

        /// <summary>조작 의도 수신. <see cref="InputRouter"/>가 매 프레임 먹인다.</summary>
        public void SetIntent(InputIntent next) => intent = next;

        void Update()
        {
            motor.TickTimers(Time.deltaTime);
            motor.SetJumpHeld(intent.JumpHeld);

            // 점프는 「눌렀다」를 흘리면 안 되므로 물리 스텝이 아니라 프레임에서 받는다.
            if (intent.JumpPressed) motor.RequestJump();

            states.Tick(Time.deltaTime);
        }

        void FixedUpdate()
        {
            // 돌진 중에는 모터의 일반 이동·중력 보정이 몸을 되뺏지 않게 통째로 쉰다.
            if (states.CurrentKey != PlayerStateId.Dashing)
                motor.FixedTick(intent.Move, Time.fixedDeltaTime);

            states.FixedTick(Time.fixedDeltaTime);
        }

        internal void ChangeState(PlayerStateId id) => states.Change(id);

        /// <summary>땅에 있으면 Grounded, 아니면 Airborne. 두 상태가 공유하는 판정.</summary>
        sealed class GroundedState : StateBase
        {
            readonly PlayerController ctx;
            public GroundedState(PlayerController ctx) => this.ctx = ctx;

            public override void Tick(float deltaTime)
            {
                if (!ctx.motor.IsGrounded) ctx.ChangeState(PlayerStateId.Airborne);
            }
        }

        sealed class AirborneState : StateBase
        {
            readonly PlayerController ctx;
            public AirborneState(PlayerController ctx) => this.ctx = ctx;

            public override void Tick(float deltaTime)
            {
                if (ctx.motor.IsGrounded) ctx.ChangeState(PlayerStateId.Grounded);
            }
        }

        /// <summary>진입·탈출을 <see cref="PlayerDash"/>가 시키므로 스스로는 아무것도 안 한다.</summary>
        sealed class DashingState : StateBase { }
    }
}
