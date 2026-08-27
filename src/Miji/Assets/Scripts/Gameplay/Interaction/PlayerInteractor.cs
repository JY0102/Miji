using Miji.Gameplay.Player;
using UnityEngine;

namespace Miji.Gameplay.Interaction
{
    /// <summary>
    /// Interact 의도를 받아 주변에서 가장 가까운 <see cref="IInteractable"/>을 실행한다.
    /// </summary>
    [RequireComponent(typeof(PlayerController))]
    public class PlayerInteractor : MonoBehaviour
    {
        [Tooltip("이 반경 안의 상호작용 대상을 찾는다.")]
        [SerializeField] float radius = 0.9f;

        PlayerController controller;

        // ponytail: 레이어 필터 없이 전 콜라이더를 훑는다 — 씬 콜라이더가 수백 개가 되면 Interactable 레이어로 승격
        static readonly Collider2D[] buffer = new Collider2D[16];
        static readonly ContactFilter2D anyCollider = ContactFilter2D.noFilter;

        void Awake() => controller = GetComponent<PlayerController>();

        void Update()
        {
            if (!controller.Intent.InteractPressed) return;

            var found = Physics2D.OverlapCircle(transform.position, radius, anyCollider, buffer);

            IInteractable best = null;
            var bestSqr = float.MaxValue;

            for (var i = 0; i < found; i++)
            {
                var candidate = buffer[i].GetComponentInParent<IInteractable>();
                if (candidate == null || !candidate.CanInteract) continue;

                var sqr = (buffer[i].transform.position - transform.position).sqrMagnitude;
                if (sqr < bestSqr)
                {
                    bestSqr = sqr;
                    best = candidate;
                }
            }

            best?.Interact(gameObject);
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(0.4f, 0.9f, 1f, 0.6f);
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}
