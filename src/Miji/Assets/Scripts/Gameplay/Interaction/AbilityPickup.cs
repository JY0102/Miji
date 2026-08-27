using Miji.Core.Progression;
using UnityEngine;

namespace Miji.Gameplay.Interaction
{
    /// <summary>
    /// 능력 해금 지점 — 상호작용하면 <see cref="ProgressionState"/>의 능력이 Unlocked가 된다.
    /// 「능력 = 소박한 기계 기능의 발견」이므로 획득 연출은 아트·서사가 붙을 때 얹는다.
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class AbilityPickup : MonoBehaviour, IInteractable
    {
        [SerializeField] string abilityId = "F1";
        [SerializeField] SpriteRenderer sprite;

        public bool CanInteract => ProgressionState.Current.GetAbility(abilityId) == AbilityState.NotAcquired;

        void Awake()
        {
            if (sprite == null) sprite = GetComponentInChildren<SpriteRenderer>();
        }

        public void Interact(GameObject actor)
        {
            ProgressionState.Current.SetAbility(abilityId, AbilityState.Unlocked);
            if (sprite != null) sprite.enabled = false; // 집힌 것은 사라진다 (그레이박스 연출)
            Debug.Log($"{name}: 능력 {abilityId} 해금 (by {actor.name})");
        }
    }
}
