using UnityEngine;

namespace Miji.Core.Combat
{
    /// <summary>
    /// 피해가 실제로 적용될 때마다 <see cref="Miji.Core.Events.EventBus"/>로 1회 발행되는 전역 신호.
    ///
    /// 피격의 유일 합류점은 <see cref="Health.TakeDamage"/>다(`MECHANIC_GAME_FEEL.md` §9-2) —
    /// 전역 효과(카메라 흔들림 등)는 이 신호를 구독하고, 로컬 효과(플래시·넉백)는
    /// 각자 자기 <see cref="Health.Damaged"/>를 구독한다. 사본을 만들지 않는다.
    /// </summary>
    public readonly struct HitSignal
    {
        public readonly DamageInfo Info;

        /// <summary>맞은 쪽의 GameObject (Health가 붙은 오브젝트).</summary>
        public readonly GameObject Victim;

        public HitSignal(DamageInfo info, GameObject victim)
        {
            Info = info;
            Victim = victim;
        }
    }
}
