using UnityEngine;

namespace Miji.Core.Combat
{
    /// <summary>
    /// 맞는 판정 범위. 자기 편을 들고 있고, 피해를 <see cref="IDamageable"/> 주인에게 넘긴다.
    ///
    /// 몸통 콜라이더와 따로 두는 이유: 맞는 범위와 서는 범위가 늘 같지 않고,
    /// 히트박스가 <b>허트박스 레이어만</b> 훑으면 「버릴 쌍」을 아예 보고받지 않는다
    /// (Godot에서 같은 레이어를 써서 자기 자신을 때린 것으로 보고되던 문제).
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class Hurtbox : MonoBehaviour
    {
        [SerializeField] Faction faction = Faction.Hostile;

        [Tooltip("피해를 받을 대상. 비우면 자신·부모에서 Health를 찾는다.")]
        [SerializeField] MonoBehaviour owner;

        IDamageable target;

        public Faction Faction => faction;
        public bool IsAlive => target?.IsAlive ?? false;

        void Awake()
        {
            target = owner as IDamageable ?? GetComponentInParent<IDamageable>();

            if (target == null)
                Debug.LogError($"{nameof(Hurtbox)}: IDamageable을 찾지 못했다. Health를 붙이거나 owner를 지정할 것.", this);

            var col = GetComponent<Collider2D>();
            col.isTrigger = true;
        }

        /// <summary>
        /// 피해를 받는다. 편이 같으면 무시한다.
        /// 반환값은 실제로 전달됐는지 여부 — 히트박스가 「맞췄다」를 판단하는 근거다.
        /// </summary>
        public bool Receive(DamageInfo info)
        {
            if (target == null || !target.IsAlive) return false;
            if (!FactionRules.CanHit(info.Attacker, faction)) return false;

            target.TakeDamage(info);
            return true;
        }
    }
}
