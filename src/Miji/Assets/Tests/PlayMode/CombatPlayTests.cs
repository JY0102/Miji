using System.Collections;
using System.Reflection;
using Miji.Core.Combat;
using Miji.Gameplay.Combat;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Miji.Gameplay.PlayTests
{
    /// <summary>
    /// 히트박스 → 허트박스 → Health의 실제 물리 경로.
    /// 자기피해 가드·편 가드·창 내 중복 방지·넉백이 콜라이더 겹침 위에서 작동하는지 잰다.
    /// </summary>
    public class CombatPlayTests
    {
        const int HitboxLayer = 9;
        const int HurtboxLayer = 10;

        [TearDown]
        public void TearDown()
        {
            foreach (var go in Object.FindObjectsByType<GameObject>(FindObjectsSortMode.None))
                if (go.scene.isLoaded) Object.Destroy(go);
        }

        // ── 리그 ────────────────────────────────────────────────

        static Hitbox MakeAttacker(Vector2 pos)
        {
            var attacker = new GameObject("attacker");
            attacker.transform.position = pos;

            var hbGo = new GameObject("hitbox") { layer = HitboxLayer };
            hbGo.transform.SetParent(attacker.transform);
            hbGo.transform.localPosition = Vector3.zero;

            var col = hbGo.AddComponent<BoxCollider2D>();
            col.size = Vector2.one;

            return hbGo.AddComponent<Hitbox>(); // Awake: owner = root = attacker
        }

        static Health MakeVictim(Vector2 pos, Faction faction = Faction.Hostile, Transform parent = null)
        {
            var go = new GameObject("victim") { layer = HurtboxLayer };
            if (parent != null) go.transform.SetParent(parent);
            go.transform.position = pos;

            var col = go.AddComponent<BoxCollider2D>();
            col.isTrigger = true;
            col.size = Vector2.one;

            var health = go.AddComponent<Health>();
            var hurtbox = go.AddComponent<Hurtbox>();
            typeof(Hurtbox).GetField("faction", BindingFlags.Instance | BindingFlags.NonPublic)
                           .SetValue(hurtbox, faction);

            return health;
        }

        // ── 테스트 ──────────────────────────────────────────────

        [UnityTest]
        public IEnumerator Sweep_DamagesOverlappingHurtbox()
        {
            var hitbox = MakeAttacker(Vector2.zero);
            var victim = MakeVictim(new Vector2(0.5f, 0f));
            yield return new WaitForFixedUpdate();

            hitbox.BeginWindow();
            Assert.That(hitbox.Sweep(), Is.EqualTo(1));
            Assert.That(victim.CurrentHp, Is.EqualTo(victim.MaxHp - 1));
        }

        [UnityTest]
        public IEnumerator Sweep_DoesNotHitSameTargetTwiceInOneWindow()
        {
            var hitbox = MakeAttacker(Vector2.zero);
            var victim = MakeVictim(new Vector2(0.5f, 0f));
            yield return new WaitForFixedUpdate();

            hitbox.BeginWindow();
            hitbox.Sweep();
            Assert.That(hitbox.Sweep(), Is.EqualTo(0));
            Assert.That(victim.CurrentHp, Is.EqualTo(victim.MaxHp - 1));
        }

        [UnityTest]
        public IEnumerator Sweep_SkipsHurtboxUnderOwnRoot()
        {
            var hitbox = MakeAttacker(Vector2.zero);
            // 편 규칙으로는 맞는 조합(Player→Hostile)이라도 자기 루트 아래면 안 때린다.
            var self = MakeVictim(Vector2.zero, Faction.Hostile, hitbox.transform.root);
            yield return new WaitForFixedUpdate();

            hitbox.BeginWindow();
            Assert.That(hitbox.Sweep(), Is.EqualTo(0));
            Assert.That(self.CurrentHp, Is.EqualTo(self.MaxHp));
        }

        [UnityTest]
        public IEnumerator Sweep_SkipsSameFaction()
        {
            var hitbox = MakeAttacker(Vector2.zero);
            var ally = MakeVictim(new Vector2(0.5f, 0f), Faction.Player);
            yield return new WaitForFixedUpdate();

            hitbox.BeginWindow();
            Assert.That(hitbox.Sweep(), Is.EqualTo(0));
            Assert.That(ally.CurrentHp, Is.EqualTo(ally.MaxHp));
        }

        [UnityTest]
        public IEnumerator DamageResponse_AppliesKnockbackAwayFromAttacker()
        {
            var hitbox = MakeAttacker(Vector2.zero);
            var victim = MakeVictim(new Vector2(0.5f, 0f));

            var body = victim.gameObject.AddComponent<Rigidbody2D>();
            body.gravityScale = 0f;
            victim.gameObject.AddComponent<DamageResponse>();
            yield return new WaitForFixedUpdate();

            hitbox.BeginWindow();
            hitbox.Sweep();

            Assert.That(body.linearVelocity.x, Is.GreaterThan(0f), "공격자가 왼쪽이므로 오른쪽으로 밀려야 한다");
            Assert.That(body.linearVelocity.y, Is.GreaterThan(0f), "살짝 위로 뜬다");
        }
    }
}
