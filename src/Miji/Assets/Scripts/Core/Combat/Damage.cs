using UnityEngine;

namespace Miji.Core.Combat
{
    /// <summary>
    /// 피아 구분. 타입 검사(`body is Robot`)로 판별하지 않는 이유는 Godot 프로토타입의 교훈이다 —
    /// 그렇게 짜면 적을 때릴 수 없는 구조가 되어 세 번째 사본을 만들게 된다(IMPL-004 보류 항목).
    /// </summary>
    public enum Faction
    {
        /// <summary>A와 그 편.</summary>
        Player,

        /// <summary>야생 생물·붕괴자 등.</summary>
        Hostile,

        /// <summary>가시·낙사처럼 편이 없는 것. 아무나 때린다.</summary>
        Hazard
    }

    /// <summary>한 번의 타격이 실어 보내는 것.</summary>
    public readonly struct DamageInfo
    {
        public readonly int Amount;

        /// <summary>때린 쪽의 편. 같은 편은 맞지 않는다.</summary>
        public readonly Faction Attacker;

        /// <summary>맞은 지점(월드). 이펙트·넉백 방향 계산에 쓴다.</summary>
        public readonly Vector2 Point;

        /// <summary>밀려나는 방향(정규화). 없으면 <see cref="Vector2.zero"/>.</summary>
        public readonly Vector2 Knockback;

        /// <summary>때린 오브젝트. 로그·추적용이며 없어도 된다.</summary>
        public readonly GameObject Source;

        /// <summary>강공격 여부. 카메라 흔들림 같은 전역 효과는 이 플래그가 선 것에만 반응한다
        /// (`MECHANIC_GAME_FEEL.md` §9 — 어떤 무브가 강공격인지는 무브셋 확정 때 지정).</summary>
        public readonly bool Strong;

        public DamageInfo(int amount, Faction attacker, Vector2 point,
                          Vector2 knockback = default, GameObject source = null,
                          bool strong = false)
        {
            Amount = Mathf.Max(0, amount);
            Attacker = attacker;
            Point = point;
            Knockback = knockback;
            Source = source;
            Strong = strong;
        }
    }

    /// <summary>맞을 수 있는 것. <see cref="Health"/>가 표준 구현이다.</summary>
    public interface IDamageable
    {
        bool IsAlive { get; }
        void TakeDamage(DamageInfo info);
    }

    /// <summary>
    /// 편이 다른지 판정한다. Hazard는 누구든 때리고 누구에게든 맞는다.
    /// </summary>
    public static class FactionRules
    {
        public static bool CanHit(Faction attacker, Faction victim)
        {
            if (attacker == Faction.Hazard || victim == Faction.Hazard) return true;
            return attacker != victim;
        }
    }
}
