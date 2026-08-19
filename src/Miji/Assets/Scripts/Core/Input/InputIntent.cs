using UnityEngine;

namespace Miji.Core.Input
{
    /// <summary>
    /// 한 프레임의 조작 의도. 「어떤 키를 눌렀나」가 아니라 「무엇을 하려 하나」다.
    ///
    /// 몸(플레이어·NPC·2장의 다른 캐릭터)은 이 구조체만 받는다. 그래서 같은 몸을
    /// 사람이 조작하든 AI가 조작하든 리플레이가 먹이든 구분하지 않는다.
    /// </summary>
    public readonly struct InputIntent
    {
        /// <summary>좌우 이동. -1..1</summary>
        public readonly float Move;

        /// <summary>이번 프레임에 점프를 눌렀다.</summary>
        public readonly bool JumpPressed;

        /// <summary>점프를 누르고 있다 (가변 점프 높이용).</summary>
        public readonly bool JumpHeld;

        public readonly bool AttackPressed;
        public readonly bool InteractPressed;

        /// <summary>F1 돌진 등 능력 버튼.</summary>
        public readonly bool AbilityPressed;

        public InputIntent(float move, bool jumpPressed, bool jumpHeld,
                           bool attackPressed, bool interactPressed, bool abilityPressed)
        {
            Move = Mathf.Clamp(move, -1f, 1f);
            JumpPressed = jumpPressed;
            JumpHeld = jumpHeld;
            AttackPressed = attackPressed;
            InteractPressed = interactPressed;
            AbilityPressed = abilityPressed;
        }

        /// <summary>아무 의도도 없음. 컷신·조작 차단 구간에서 몸에 먹인다.</summary>
        public static InputIntent None => default;

        public bool HasMovement => !Mathf.Approximately(Move, 0f);
    }

    /// <summary>
    /// 조작 가능한 몸. 조작권 인계(2장 여산→열하나)는 이 인터페이스를 가진
    /// 대상을 <see cref="InputRouter"/>에서 바꿔 끼우는 것으로 끝난다.
    /// </summary>
    public interface IPossessable
    {
        void SetIntent(InputIntent intent);
    }
}
