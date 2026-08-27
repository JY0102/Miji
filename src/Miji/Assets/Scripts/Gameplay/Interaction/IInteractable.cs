using UnityEngine;

namespace Miji.Gameplay.Interaction
{
    /// <summary>
    /// 상호작용 가능한 것 — 레버, 물건, B에게 말 걸기의 공통 입구.
    /// 구현체는 MonoBehaviour여야 한다(<see cref="PlayerInteractor"/>가 콜라이더로 찾는다).
    /// </summary>
    public interface IInteractable
    {
        /// <summary>지금 상호작용할 수 있나. 이미 당겨진 일회성 레버 등은 false.</summary>
        bool CanInteract { get; }

        /// <summary>실행한다. actor는 상호작용을 건 쪽(플레이어).</summary>
        void Interact(GameObject actor);
    }
}
