using System.Collections;
using System.Reflection;
using Miji.Core.Input;
using Miji.Gameplay.Player;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Miji.Gameplay.PlayTests
{
    /// <summary>
    /// 조작감 관용 기법이 실제로 작동하는지 못 박는다.
    /// 이런 것들은 값 하나만 어긋나도 조용히 사라지고, 사라진 걸 손으로는 알아채기 어렵다.
    /// </summary>
    public class MotorFeelTests
    {
        const int GroundLayer = 6;

        GameObject rig;
        PlayerController controller;
        PlayerMotor motor;
        Rigidbody2D body;

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
            var col = go.AddComponent<BoxCollider2D>();
            col.size = size;
            return go;
        }

        void MakePlayer(Vector2 at)
        {
            rig = new GameObject("Player") { layer = 7 };
            rig.transform.position = at;

            body = rig.AddComponent<Rigidbody2D>();
            body.freezeRotation = true;

            var box = rig.AddComponent<BoxCollider2D>();
            box.size = Vector2.one;

            motor = rig.AddComponent<PlayerMotor>();
            SetField(motor, "groundLayers", (LayerMask)(1 << GroundLayer));

            controller = rig.AddComponent<PlayerController>();
            SetField(controller, "motor", motor);
        }

        static void SetField(object target, string field, object value)
        {
            var info = target.GetType().GetField(field, BindingFlags.Instance | BindingFlags.NonPublic);
            Assert.IsNotNull(info, $"필드 '{field}'를 찾지 못했다 — 이름이 바뀌었으면 테스트도 갱신할 것");
            info.SetValue(target, value);
        }

        static float Field(object target, string field) =>
            (float)target.GetType()
                .GetField(field, BindingFlags.Instance | BindingFlags.NonPublic)
                .GetValue(target);

        static InputIntent Neutral => InputIntent.None;
        static InputIntent JumpTap => new(0f, true, true, false, false, false);
        static InputIntent JumpHold => new(0f, false, true, false, false, false);
        static InputIntent WalkRight => new(1f, false, false, false, false, false);

        /// <summary>점프를 한 프레임만 누른다. 계속 눌러두면 버퍼가 매 프레임 갱신돼 테스트가 거짓말을 한다.</summary>
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

        // ── 코요테 타임 ─────────────────────────────────────────────

        [UnityTest]
        public IEnumerator Coyote_time_lets_you_jump_just_after_losing_ground()
        {
            var ground = MakeBlock("Ground", new Vector2(0f, -0.5f), new Vector2(10f, 1f));
            MakePlayer(new Vector2(0f, 0.6f));
            yield return Settle();
            Assert.IsTrue(motor.IsGrounded, "전제: 땅에 서 있어야 한다");

            Object.Destroy(ground); // 발밑이 사라진다
            yield return null;      // 코요테 창(0.09s) 안

            yield return Tap();
            yield return new WaitForFixedUpdate();

            Assert.Greater(motor.Velocity.y, 0f, "땅을 잃은 직후에는 점프가 먹어야 한다");
        }

        [UnityTest]
        public IEnumerator Coyote_time_expires()
        {
            var ground = MakeBlock("Ground", new Vector2(0f, -0.5f), new Vector2(10f, 1f));
            MakePlayer(new Vector2(0f, 0.6f));
            yield return Settle();

            Object.Destroy(ground);
            controller.SetIntent(Neutral);

            var coyote = Field(motor, "coyoteTime");
            yield return new WaitForSeconds(coyote + 0.25f); // 창을 확실히 넘긴다

            var before = motor.Velocity.y;
            yield return Tap();
            yield return new WaitForFixedUpdate();

            Assert.LessOrEqual(motor.Velocity.y, before, "창이 지났으면 공중 점프가 되면 안 된다");
        }

        // ── 이중 점프 방지 ──────────────────────────────────────────

        [UnityTest]
        public IEnumerator No_second_jump_in_the_air()
        {
            MakeBlock("Ground", new Vector2(0f, -0.5f), new Vector2(10f, 1f));
            MakePlayer(new Vector2(0f, 0.6f));
            yield return Settle();

            yield return Tap();
            yield return new WaitForFixedUpdate();
            var launch = motor.Velocity.y;
            Assert.Greater(launch, 0f, "전제: 첫 점프는 올라가야 한다");

            // 잠금이 풀리고 확실히 공중인 시점까지 기다린다.
            yield return new WaitForSeconds(0.2f);
            Assert.IsFalse(motor.IsGrounded, "전제: 아직 공중이어야 한다");

            var beforeSecond = motor.Velocity.y;
            yield return Tap();
            yield return new WaitForFixedUpdate();

            Assert.LessOrEqual(motor.Velocity.y, beforeSecond + 0.01f,
                "공중에서 두 번째 점프가 먹으면 「A는 스스로 높이를 얻지 못한다」가 깨진다");
        }

        [UnityTest]
        public IEnumerator Jump_does_not_double_impulse_on_the_frame_it_leaves_ground()
        {
            MakeBlock("Ground", new Vector2(0f, -0.5f), new Vector2(10f, 1f));
            MakePlayer(new Vector2(0f, 0.6f));
            yield return Settle();

            // 첫 임펄스가 물리에 실제로 적용된 뒤의 속도를 기준으로 잡는다.
            yield return Tap();
            yield return new WaitForFixedUpdate();
            var launch = motor.Velocity.y;
            Assert.Greater(launch, 0f, "전제: 첫 점프는 올라가야 한다");

            // 발이 아직 접지 판정에 걸려 있을 수 있는 시점에 곧바로 한 번 더 누른다.
            yield return Tap();
            yield return new WaitForFixedUpdate();

            Assert.Less(motor.Velocity.y, launch,
                "떠나는 프레임 직후 다시 눌러도 임펄스가 두 번 들어가면 안 된다 (중력만큼 줄어 있어야 정상)");
        }

        // ── 점프 버퍼 ───────────────────────────────────────────────

        [UnityTest]
        public IEnumerator Stale_jump_request_does_not_fire_on_landing()
        {
            MakeBlock("Ground", new Vector2(0f, -0.5f), new Vector2(10f, 1f));
            MakePlayer(new Vector2(0f, 4f)); // 높이 떨어뜨린다
            yield return null;

            yield return Tap();                          // 착지보다 훨씬 이전
            yield return new WaitForSeconds(1.2f);       // 버퍼(0.11s) 만료 + 착지
            Assert.IsTrue(motor.IsGrounded, "전제: 착지해 있어야 한다");

            Assert.LessOrEqual(motor.Velocity.y, 0.01f,
                "오래된 점프 입력이 착지 순간에 되살아나면 안 된다");
        }

        // ── 천장 ───────────────────────────────────────────────────

        [UnityTest]
        public IEnumerator Corner_correction_slips_past_a_ceiling_edge()
        {
            MakeBlock("Ground", new Vector2(0f, -0.5f), new Vector2(20f, 1f));
            // 왼쪽 끝이 x=0인 천장. 플레이어 머리가 그 모서리에 조금 걸리게 세운다.
            MakeBlock("Ceiling", new Vector2(4f, 2.4f), new Vector2(8f, 0.5f));
            MakePlayer(new Vector2(-0.3f, 0.6f));
            yield return Settle();

            var startX = rig.transform.position.x;

            yield return Tap();
            for (var i = 0; i < 12; i++) yield return new WaitForFixedUpdate();

            Assert.Less(rig.transform.position.x, startX - 0.01f,
                "모서리에 걸렸을 때 옆으로 밀어 통과시켜야 한다");
            Assert.Greater(rig.transform.position.y, 0.9f, "보정 후에도 계속 올라가야 한다");
        }

        [UnityTest]
        public IEnumerator Head_bonk_kills_upward_velocity()
        {
            MakeBlock("Ground", new Vector2(0f, -0.5f), new Vector2(20f, 1f));
            // 완전히 막힌 천장 — 빠져나갈 틈이 없다.
            MakeBlock("Ceiling", new Vector2(0f, 2.0f), new Vector2(20f, 0.5f));
            MakePlayer(new Vector2(0f, 0.6f));
            yield return Settle();

            yield return Tap();
            for (var i = 0; i < 20; i++)
            {
                yield return new WaitForFixedUpdate();
                Assert.LessOrEqual(rig.transform.position.y, 1.6f, "천장을 뚫고 올라가면 안 된다");
            }

            Assert.LessOrEqual(motor.Velocity.y, 0.01f, "머리를 부딪히면 상승이 끊겨야 한다");
        }

        // ── 반전 스냅 ───────────────────────────────────────────────

        [UnityTest]
        public IEnumerator Turn_around_is_faster_than_plain_stop()
        {
            MakeBlock("Ground", new Vector2(0f, -0.5f), new Vector2(40f, 1f));
            MakePlayer(new Vector2(0f, 0.6f));
            yield return Settle();

            // 오른쪽으로 충분히 가속
            controller.SetIntent(WalkRight);
            yield return new WaitForSeconds(0.5f);
            var cruising = motor.Velocity.x;
            Assert.Greater(cruising, 1f, "전제: 오른쪽으로 달리고 있어야 한다");

            // 손을 떼고 두 스텝
            controller.SetIntent(Neutral);
            yield return new WaitForFixedUpdate();
            yield return new WaitForFixedUpdate();
            var afterRelease = motor.Velocity.x;

            // 다시 가속시킨 뒤, 이번에는 반대 방향을 눌러 두 스텝
            controller.SetIntent(WalkRight);
            yield return new WaitForSeconds(0.5f);
            controller.SetIntent(new InputIntent(-1f, false, false, false, false, false));
            yield return new WaitForFixedUpdate();
            yield return new WaitForFixedUpdate();
            var afterReverse = motor.Velocity.x;

            Assert.Less(afterReverse, afterRelease,
                "반대 방향 입력이 단순히 손을 뗀 것보다 빠르게 꺾여야 한다");
        }
    }
}
