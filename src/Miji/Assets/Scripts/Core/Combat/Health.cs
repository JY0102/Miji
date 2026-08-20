using System;
using UnityEngine;

namespace Miji.Core.Combat
{
    /// <summary>
    /// 체력의 유일한 구현. A·야생 생물·붕괴자가 같은 것을 쓴다.
    ///
    /// Godot 프로토타입에서는 로봇과 더미가 HP·사망을 각각 구현해 사본이 둘로 갈렸고,
    /// 그래서 「적을 때릴 수 없는 구조」가 됐다(IMPL-004 보류 항목:
    /// 「세 번째 사본이 생기기 전에 공용 베이스를 뽑을 것」). 여기서 그 부채를 갚는다.
    ///
    /// ⚠️ B(무리비)에게는 붙지 않는다 — B는 무적이고 붕괴는 전투 피해가 아니다(확정 설계).
    /// </summary>
    public class Health : MonoBehaviour, IDamageable
    {
        [SerializeField] int maxHp = 5;

        [Tooltip("피격 후 무적 시간. 연속 히트로 즉사하는 것을 막는다. 0이면 없음.")]
        [SerializeField] float invulnerabilityDuration = 0.35f;

        float invulnerableUntil = float.NegativeInfinity;

        public int MaxHp => maxHp;
        public int CurrentHp { get; private set; }
        public bool IsAlive => CurrentHp > 0;
        public bool IsInvulnerable => Time.time < invulnerableUntil;

        /// <summary>피해가 실제로 적용됐다.</summary>
        public event Action<DamageInfo> Damaged;

        /// <summary>회복됐다. 인자는 회복량.</summary>
        public event Action<int> Healed;

        /// <summary>죽었다. 한 번만 발생한다.</summary>
        public event Action<DamageInfo> Died;

        void Awake() => CurrentHp = maxHp;

        public void TakeDamage(DamageInfo info)
        {
            // 죽은 것을 다시 때려도 사망이 두 번 발생하지 않는다.
            if (!IsAlive || IsInvulnerable || info.Amount <= 0) return;

            CurrentHp = Mathf.Max(0, CurrentHp - info.Amount);

            if (invulnerabilityDuration > 0f)
                invulnerableUntil = Time.time + invulnerabilityDuration;

            Damaged?.Invoke(info);

            if (!IsAlive) Died?.Invoke(info);
        }

        public void Heal(int amount)
        {
            if (!IsAlive || amount <= 0) return;

            var before = CurrentHp;
            CurrentHp = Mathf.Min(maxHp, CurrentHp + amount);

            var gained = CurrentHp - before;
            if (gained > 0) Healed?.Invoke(gained);
        }

        /// <summary>체크포인트 복귀·리스폰용. 무적 창도 함께 초기화한다.</summary>
        public void RestoreFull()
        {
            CurrentHp = maxHp;
            invulnerableUntil = float.NegativeInfinity;
        }

        /// <summary>테스트·디버그에서 무적 창을 즉시 끝낸다.</summary>
        public void ClearInvulnerability() => invulnerableUntil = float.NegativeInfinity;
    }
}
