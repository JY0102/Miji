using UnityEngine;
using UnityEngine.InputSystem;

namespace Miji.Core.Input
{
    /// <summary>
    /// Unity Input System을 읽어 <see cref="InputIntent"/> 하나로 환산한다.
    /// 게임 로직은 InputSystem API를 직접 만지지 않는다 — 여기가 유일한 접점이다.
    ///
    /// 액션 이름으로 조회하므로 생성 코드(.inputactions의 C# 클래스)에 묶이지 않는다.
    /// </summary>
    public class InputReader : MonoBehaviour
    {
        [Header("Input Actions")]
        [Tooltip("Assets/InputSystem_Actions.inputactions")]
        [SerializeField] InputActionAsset actions;

        [SerializeField] string actionMap = "Player";
        [SerializeField] string moveAction = "Move";
        [SerializeField] string jumpAction = "Jump";
        [SerializeField] string attackAction = "Attack";
        [SerializeField] string interactAction = "Interact";
        [SerializeField] string abilityAction = "Sprint";

        InputAction move, jump, attack, interact, ability;
        bool resolved;

        /// <summary>이번 프레임의 조작 의도. 차단 중이면 <see cref="InputIntent.None"/>.</summary>
        public InputIntent Current { get; private set; }

        /// <summary>컷신·암전·메뉴에서 조작을 끊는다. GameFlow가 켠다/끈다.</summary>
        public bool Blocked { get; set; }

        void Awake() => Resolve();

        void OnEnable()
        {
            Resolve();
            if (resolved) actions.FindActionMap(actionMap, false)?.Enable();
        }

        void OnDisable()
        {
            if (resolved) actions.FindActionMap(actionMap, false)?.Disable();
            Current = InputIntent.None;
        }

        void Resolve()
        {
            if (resolved) return;

            if (actions == null)
            {
                Debug.LogError($"{nameof(InputReader)}: InputActionAsset이 비어 있다. 인스펙터에서 지정할 것.", this);
                return;
            }

            var map = actions.FindActionMap(actionMap, false);
            if (map == null)
            {
                Debug.LogError($"{nameof(InputReader)}: 액션 맵 '{actionMap}'을 찾지 못했다.", this);
                return;
            }

            move = map.FindAction(moveAction, false);
            jump = map.FindAction(jumpAction, false);
            attack = map.FindAction(attackAction, false);
            interact = map.FindAction(interactAction, false);
            ability = map.FindAction(abilityAction, false);

            resolved = true;
        }

        void Update()
        {
            if (!resolved || Blocked)
            {
                Current = InputIntent.None;
                return;
            }

            Current = new InputIntent(
                move: move?.ReadValue<Vector2>().x ?? 0f,
                jumpPressed: jump?.WasPressedThisFrame() ?? false,
                jumpHeld: jump?.IsPressed() ?? false,
                attackPressed: attack?.WasPressedThisFrame() ?? false,
                interactPressed: interact?.WasPressedThisFrame() ?? false,
                abilityPressed: ability?.WasPressedThisFrame() ?? false);
        }
    }
}
