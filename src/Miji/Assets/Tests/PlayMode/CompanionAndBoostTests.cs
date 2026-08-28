using System.Collections;
using System.Reflection;
using Miji.Core.Events;
using Miji.Core.Input;
using Miji.Core.Progression;
using Miji.Gameplay.Companion;
using Miji.Gameplay.Player;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Miji.Gameplay.PlayTests
{
    /// <summary>
    /// Phase 3 — F2 받침(둘의 동작)과 B 동행 FSM이 설계대로 작동하는지 못 박는다.
    /// 핵심 불변식: 「A는 스스로 높이를 얻지 못한다」 — F2가 없거나 잠기면 두 번째 도약은 안 나온다.
    /// </summary>
    public class CompanionAndBoostTests
    {
        const int GroundLayer = 6;
        const string F2 = "F2";

        GameObject rig;
        PlayerController controller;
        PlayerMotor motor;
        PlayerBoost boost;
        Rigidbody2D body;

        [SetUp]
        public void SetUp()
        {
            // 이 둘은 static이라 플레이 간·테스트 간에 샌다. 각 테스트를 깨끗한 판에서 시작한다.
            ProgressionState.Current = new ProgressionState();
            EventBus.Clear();
        }

        [TearDown]
        public void TearDown()
        {
            foreach (var go in Object.FindObjectsByType<GameObject>(FindObjectsSortMode.None))
                if (go.scene.isLoaded) Object.Destroy(go);
        }

        // ── 리그 ────────────────────────────────────────────────────

        GameObject MakeBlock(string name, Vector2 center, Vector2 size)
        {
            var go = new GameObject(name) { layer = GroundLayer };
            go.transform.position = center;
            go.AddComponent<BoxCollider2D>().size = size;
            return go;
        }

        void MakePlayer(Vector2 at)
        {
            rig = new GameObject("Player") { layer = 7 };
            rig.transform.position = at;

            body = rig.AddComponent<Rigidbody2D>();
            body.freezeRotation = true;
            rig.AddComponent<BoxCollider2D>().size = Vector2.one;

            motor = rig.AddComponent<PlayerMotor>();
            SetField(motor, "groundLayers", (LayerMask)(1 << GroundLayer));

            controller = rig.AddComponent<PlayerController>();
            SetField(controller, "motor", motor);

            boost = rig.AddComponent<PlayerBoost>();
        }

        static void SetField(object target, string field, object value)
        {
            var info = target.GetType().GetField(field, BindingFlags.Instance | BindingFlags.NonPublic);
            Assert.IsNotNull(info, $"필드 '{field}'를 찾지 못했다 — 이름이 바뀌었으면 테스트도 갱신할 것");
            info.SetValue(target, value);
        }

        static InputIntent Neutral => InputIntent.None;
        static InputIntent JumpTap => new(0f, true, true, false, false, false);
        static InputIntent JumpHold => new(0f, false, true, false, false, false);

        IEnumerator Tap()
        {
            controller.SetIntent(JumpTap);
            yield return null;
            controller.SetIntent(JumpHold);
        }

        IEnumerator Settle(int frames = 12)
        {
            for (var i = 0; i < frames; i++) yield return new WaitForFixedUpdate();
        }

        // 첫 점프 후 공중에 확실히 뜬 시점까지 데려간다(점프 잠금 해제 뒤).
        IEnumerator JumpAndReachAir()
        {
            yield return Tap();
            yield return new WaitForFixedUpdate();
            Assert.Greater(motor.Velocity.y, 0f, "전제: 첫 점프는 올라가야 한다");
            yield return new WaitForSeconds(0.2f);
            Assert.IsFalse(motor.IsGrounded, "전제: 공중이어야 한다");
        }

        // ── F2 받침 ─────────────────────────────────────────────────

        [UnityTest]
        public IEnumerator Boost_lifts_again_when_F2_is_usable()
        {
            MakeBlock("Ground", new Vector2(0f, -0.5f), new Vector2(10f, 1f));
            MakePlayer(new Vector2(0f, 0.6f));
            ProgressionState.Current.SetAbility(F2, AbilityState.Unlocked);
            yield return Settle();

            yield return JumpAndReachAir();
            var before = motor.Velocity.y;

            yield return Tap();                       // 공중에서 다시 점프 = 받침
            yield return new WaitForFixedUpdate();

            Assert.Greater(motor.Velocity.y, before + 1f,
                "F2가 켜져 있으면 공중에서 다시 누를 때 위로 밀어 올려야 한다");
        }

        [UnityTest]
        public IEnumerator No_boost_when_F2_not_acquired()
        {
            MakeBlock("Ground", new Vector2(0f, -0.5f), new Vector2(10f, 1f));
            MakePlayer(new Vector2(0f, 0.6f));
            // F2 미획득 — SetAbility를 부르지 않는다
            yield return Settle();

            yield return JumpAndReachAir();
            var before = motor.Velocity.y;

            yield return Tap();
            yield return new WaitForFixedUpdate();

            Assert.LessOrEqual(motor.Velocity.y, before + 0.01f,
                "F2가 없으면 공중 재점프가 높이를 만들면 안 된다 — 「A는 스스로 높이를 얻지 못한다」");
        }

        [UnityTest]
        public IEnumerator No_boost_when_F2_is_locked()
        {
            MakeBlock("Ground", new Vector2(0f, -0.5f), new Vector2(10f, 1f));
            MakePlayer(new Vector2(0f, 0.6f));
            ProgressionState.Current.SetAbility(F2, AbilityState.Unlocked);
            ProgressionState.Current.SetAbility(F2, AbilityState.Locked); // 결별 — 획득했으나 잠김
            yield return Settle();

            yield return JumpAndReachAir();
            var before = motor.Velocity.y;

            yield return Tap();
            yield return new WaitForFixedUpdate();

            Assert.LessOrEqual(motor.Velocity.y, before + 0.01f,
                "잠긴 F2는 발동하면 안 된다(결별 시 상실이 성립해야 한다)");
        }

        [UnityTest]
        public IEnumerator Boost_fires_once_per_airtime()
        {
            MakeBlock("Ground", new Vector2(0f, -0.5f), new Vector2(10f, 1f));
            MakePlayer(new Vector2(0f, 0.6f));
            ProgressionState.Current.SetAbility(F2, AbilityState.Unlocked);
            yield return Settle();

            yield return JumpAndReachAir();
            yield return Tap();                       // 첫 받침
            yield return new WaitForFixedUpdate();
            yield return new WaitForSeconds(0.3f);    // 쿨다운 지나되 아직 공중
            Assert.IsFalse(motor.IsGrounded, "전제: 아직 공중이어야 한다");

            var before = motor.Velocity.y;
            yield return Tap();                       // 두 번째 받침 시도
            yield return new WaitForFixedUpdate();

            Assert.LessOrEqual(motor.Velocity.y, before + 0.01f,
                "한 번의 체공에 받침은 한 번뿐이어야 한다");
        }

        // ── B 동행 FSM ──────────────────────────────────────────────

        CompanionFollower MakeCompanion(PlayerMotor followTarget)
        {
            var go = new GameObject("Companion");           // SpriteRenderer는 RequireComponent가 붙인다
            var comp = go.AddComponent<CompanionFollower>();
            SetField(comp, "target", followTarget);
            return comp;
        }

        [UnityTest]
        public IEnumerator Companion_cooperates_on_boost_signal_then_returns()
        {
            MakeBlock("Ground", new Vector2(0f, -0.5f), new Vector2(10f, 1f));
            MakePlayer(new Vector2(0f, 0.6f));
            var comp = MakeCompanion(motor);
            yield return null;
            Assert.AreEqual(CompanionStateId.Following, comp.CurrentState, "전제: 평상시엔 Following");

            EventBus.Publish(new BoostRequestedSignal(rig.transform.position, 1));
            yield return null;
            Assert.AreEqual(CompanionStateId.Cooperating, comp.CurrentState,
                "받침 신호를 받으면 협력 상태로 들어가야 한다(신호 반응 — 명령 큐 아님)");

            yield return new WaitForSeconds(0.4f);    // cooperateDuration(0.25) 초과
            Assert.AreEqual(CompanionStateId.Following, comp.CurrentState,
                "받침이 끝나면 평상 추적으로 돌아와야 한다");
        }

        [UnityTest]
        public IEnumerator Companion_snaps_back_when_left_too_far()
        {
            MakeBlock("Ground", new Vector2(0f, -0.5f), new Vector2(10f, 1f));
            MakePlayer(new Vector2(0f, 0.6f));
            var comp = MakeCompanion(motor);
            yield return null;

            // A를 멀리 순간이동시킨다 — 앵커가 B에서 snapDistance(8) 밖으로 벗어난다.
            rig.transform.position = new Vector2(50f, 0.6f);
            yield return null;   // Following.Tick이 거리 초과를 감지 → Snapping
            yield return null;   // Snapping.Enter가 앵커로 붙이고 → Following

            var anchorX = rig.transform.position.x - motor.Facing * 1.1f;
            Assert.Less(Mathf.Abs(comp.transform.position.x - anchorX), 1f,
                "너무 처지면 연출 없이 즉시 A 곁으로 복귀해야 한다");
            Assert.AreEqual(CompanionStateId.Following, comp.CurrentState, "복귀 후엔 다시 Following");
        }
    }
}
