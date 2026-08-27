using System;
using UnityEngine;

namespace Miji.Gameplay.Interaction
{
    /// <summary>
    /// 그레이박스용 레버. 당기면 켜짐/꺼짐이 뒤집히고 색으로 보여준다.
    /// 실제 게이트(문·발판)는 <see cref="Toggled"/>를 구독해서 반응한다 —
    /// 스위치 연출(G8)·룸 기믹이 생기면 이 이벤트에 얹는다.
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class GreyboxLever : MonoBehaviour, IInteractable
    {
        [SerializeField] SpriteRenderer sprite;
        [SerializeField] Color offColor = new(0.5f, 0.35f, 0.25f);
        [SerializeField] Color onColor = new(0.35f, 0.85f, 0.4f);
        [SerializeField] bool oneShot;

        public bool IsOn { get; private set; }
        public event Action<bool> Toggled;

        public bool CanInteract => !(oneShot && IsOn);

        void Awake()
        {
            if (sprite == null) sprite = GetComponentInChildren<SpriteRenderer>();
            ApplyColor();
        }

        public void Interact(GameObject actor)
        {
            IsOn = !IsOn;
            ApplyColor();
            Toggled?.Invoke(IsOn);
            Debug.Log($"{name}: 레버 {(IsOn ? "ON" : "OFF")} (by {actor.name})");
        }

        void ApplyColor()
        {
            if (sprite != null) sprite.color = IsOn ? onColor : offColor;
        }
    }
}
