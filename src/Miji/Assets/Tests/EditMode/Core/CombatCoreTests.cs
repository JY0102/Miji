using System.Reflection;
using Miji.Core.Combat;
using Miji.Core.Events;
using NUnit.Framework;
using UnityEngine;

namespace Miji.Core.Tests
{
    /// <summary>
    /// CombatCore의 순수 규칙 — 편 판정, 피해 정보, Health의 수명·무적 창, 초크포인트 신호.
    /// 물리(히트박스↔허트박스 겹침)는 PlayMode의 CombatPlayTests가 잰다.
    /// </summary>
    public class CombatCoreTests
    {
        GameObject go;

        [TearDown]
        public void TearDown()
        {
            EventBus.Clear();
            if (go != null) Object.DestroyImmediate(go);
        }

        // EditMode에서는 AddComponent가 Awake를 부르지 않는다 — 직접 부른다.
        Health MakeHealth()
        {
            go = new GameObject("victim");
            var health = go.AddComponent<Health>();
            typeof(Health).GetMethod("Awake", BindingFlags.Instance | BindingFlags.NonPublic)
                          .Invoke(health, null);
            return health;
        }

        static DamageInfo Hit(int amount, Faction attacker = Faction.Hostile, bool strong = false) =>
            new(amount, attacker, Vector2.zero, strong: strong);

        // ── 편 판정 ─────────────────────────────────────────────

        [Test]
        public void FactionRules_SameFactionCannotHit()
        {
            Assert.That(FactionRules.CanHit(Faction.Player, Faction.Player), Is.False);
            Assert.That(FactionRules.CanHit(Faction.Hostile, Faction.Hostile), Is.False);
        }

        [Test]
        public void FactionRules_OpposingFactionsHit()
        {
            Assert.That(FactionRules.CanHit(Faction.Player, Faction.Hostile), Is.True);
            Assert.That(FactionRules.CanHit(Faction.Hostile, Faction.Player), Is.True);
        }

        [Test]
        public void FactionRules_HazardHitsAndIsHitByEveryone()
        {
            Assert.That(FactionRules.CanHit(Faction.Hazard, Faction.Player), Is.True);
            Assert.That(FactionRules.CanHit(Faction.Hazard, Faction.Hazard), Is.True);
            Assert.That(FactionRules.CanHit(Faction.Player, Faction.Hazard), Is.True);
        }

        // ── 피해 정보 ───────────────────────────────────────────

        [Test]
        public void DamageInfo_NegativeAmountClampsToZero()
        {
            Assert.That(Hit(-3).Amount, Is.EqualTo(0));
        }

        [Test]
        public void DamageInfo_StrongFlagIsCarried()
        {
            Assert.That(Hit(1, strong: true).Strong, Is.True);
            Assert.That(Hit(1).Strong, Is.False);
        }

        // ── Health ─────────────────────────────────────────────

        [Test]
        public void Health_TakesDamage()
        {
            var health = MakeHealth();
            health.TakeDamage(Hit(2));
            Assert.That(health.CurrentHp, Is.EqualTo(health.MaxHp - 2));
        }

        [Test]
        public void Health_InvulnerabilityWindowBlocksSecondHit()
        {
            var health = MakeHealth();
            health.TakeDamage(Hit(1));
            health.TakeDamage(Hit(1)); // 같은 순간의 연속 히트 — 무적 창에 막힌다
            Assert.That(health.CurrentHp, Is.EqualTo(health.MaxHp - 1));

            health.ClearInvulnerability();
            health.TakeDamage(Hit(1));
            Assert.That(health.CurrentHp, Is.EqualTo(health.MaxHp - 2));
        }

        [Test]
        public void Health_DiesOnceAndStaysDead()
        {
            var health = MakeHealth();
            var deaths = 0;
            health.Died += _ => deaths++;

            health.TakeDamage(Hit(health.MaxHp));
            Assert.That(health.IsAlive, Is.False);

            health.ClearInvulnerability();
            health.TakeDamage(Hit(1)); // 죽은 것을 다시 때려도
            Assert.That(deaths, Is.EqualTo(1));
        }

        [Test]
        public void Health_HealClampsToMax()
        {
            var health = MakeHealth();
            health.TakeDamage(Hit(2));
            health.Heal(99);
            Assert.That(health.CurrentHp, Is.EqualTo(health.MaxHp));
        }

        // ── 초크포인트 신호 (§9-2) ───────────────────────────────

        [Test]
        public void Health_PublishesHitSignalOnDamage()
        {
            var health = MakeHealth();
            HitSignal? received = null;
            EventBus.Subscribe<HitSignal>(s => received = s);

            health.TakeDamage(Hit(2, strong: true));

            Assert.That(received, Is.Not.Null);
            Assert.That(received.Value.Victim, Is.SameAs(go));
            Assert.That(received.Value.Info.Amount, Is.EqualTo(2));
            Assert.That(received.Value.Info.Strong, Is.True);
        }

        [Test]
        public void Health_DoesNotPublishWhenBlocked()
        {
            var health = MakeHealth();
            var count = 0;
            EventBus.Subscribe<HitSignal>(_ => count++);

            health.TakeDamage(Hit(1));
            health.TakeDamage(Hit(1)); // 무적 창에 막힘 — 신호도 없어야 한다

            Assert.That(count, Is.EqualTo(1));
        }
    }
}
