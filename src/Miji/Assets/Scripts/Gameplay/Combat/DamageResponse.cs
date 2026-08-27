using System.Collections;
using Miji.Core.Combat;
using UnityEngine;

namespace Miji.Gameplay.Combat
{
    /// <summary>
    /// 피격의 로컬 연출 — 플래시 + 넉백. 자기 <see cref="Health.Damaged"/>만 구독하므로
    /// 어떤 엔티티든 이 컴포넌트 하나 붙이면 끝이다(§9-2, 사본 금지).
    /// SpriteRenderer가 없으면 플래시만, Rigidbody2D가 없으면 넉백만 조용히 생략된다.
    /// </summary>
    [RequireComponent(typeof(Health))]
    public class DamageResponse : MonoBehaviour
    {
        [Tooltip("비우면 자신·자식에서 찾는다.")]
        [SerializeField] SpriteRenderer sprite;

        // ponytail: 색 틴트는 곱셈이라 진짜 「하얗게 번쩍」은 불가 — 필요해지면 머티리얼 스왑으로 승격
        [SerializeField] Color flashColor = new(1f, 0.25f, 0.25f);
        [SerializeField] float flashDuration = 0.08f;

        [Tooltip("비우면 자신에서 찾는다. 없으면 넉백 생략(고정 더미 등).")]
        [SerializeField] Rigidbody2D body;

        Health health;
        Color originalColor;
        Coroutine flashing;

        void Awake()
        {
            health = GetComponent<Health>();
            if (sprite == null) sprite = GetComponentInChildren<SpriteRenderer>();
            if (body == null) body = GetComponent<Rigidbody2D>();
            if (sprite != null) originalColor = sprite.color;
        }

        void OnEnable() => health.Damaged += OnDamaged;
        void OnDisable()
        {
            health.Damaged -= OnDamaged;

            // 플래시 도중에 꺼져도 색이 남지 않게.
            if (flashing != null) { StopCoroutine(flashing); flashing = null; }
            if (sprite != null) sprite.color = originalColor;
        }

        void OnDamaged(DamageInfo info)
        {
            if (sprite != null)
            {
                if (flashing != null) StopCoroutine(flashing);
                flashing = StartCoroutine(Flash());
            }

            if (body != null && info.Knockback != Vector2.zero)
                body.linearVelocity = info.Knockback;
        }

        IEnumerator Flash()
        {
            sprite.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            sprite.color = originalColor;
            flashing = null;
        }
    }
}
