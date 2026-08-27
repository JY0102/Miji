using System.Collections;
using System.Reflection;
using Miji.Core.Input;
using Miji.Core.Progression;
using Miji.Gameplay.Player;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Miji.Gameplay.PlayTests
{
    /// <summary>
    /// F1 돌진의 물리 경로 — 3상태 게이트 · 속도 · 중력 복원 · 공중 1회 제한.
    /// </summary>
    public class DashPlayTests
    {
        const int GroundLayer = 6;

        GameObject rig;
        PlayerController controller;
        PlayerMotor motor;
        PlayerDash dash;
        Rigidbody2D body;

        [SetUp]
        public void SetUp() => ProgressionState.Current = new ProgressionState();

        [TearDown]
        public void TearDown()
        {
            ProgressionState.Current = new ProgressionState();
            foreach (var go in Object.FindObjectsByType<GameObject>(FindObjectsSortMode.None))
                if (go.scene.isLoaded) Object.Destroy(go);
        }

        static void SetField(object target, string field, object value)
        {
            var info = target.GetType().GetField(field, BindingFlags.Instance | BindingFlags.NonPublic);
            Assert.IsNotNull(info, $"필드 '{field}'를 찾지 못했다");
            info.SetValue(target, value);
        }

        void MakeRig()
        {
            var ground = new GameObject("Ground") { layer = GroundLayer };
            ground.transform.position = new Vector2(0f, -0.5f);
            ground.AddComponent<BoxCollider2D>().size = new Vector2(60f, 1f);

            rig = new GameObject("Player") { layer = 7 };
            rig.transform.position = new Vector2(0f, 0.6f);
            body = rig.AddComponent<Rigidbody2D>();
            body.freezeRotation = true;
            rig.AddComponent<BoxCollider2D>().size = Vector2.one;

            motor = rig.AddComponent<PlayerMotor>();
            SetField(motor, "groundLayers", (LayerMask)(1 << GroundLayer));
            controller = rig.AddComponent<PlayerController>();
            SetField(controller, "motor", motor);
            dash = rig.AddComponent<PlayerDash>();
        }

        static InputIntent AbilityTap => new(0f, false, false, false, false, true);

        IEnumerator Settle(int frames = 12)
        {
            for (var i = 0; i < frames; i++) yield return new WaitForFixedUpdate();
        }

        [UnityTest]
        public IEnumerator Dash_DoesNothingWhileNotAcquired()
        {
            MakeRig();
            yield return Settle();

            controller.SetIntent(AbilityTap);
            yield return null;
            yield return new WaitForFixedUpdate();

            Assert.That(dash.IsDashing, Is.False);
            Assert.That(Mathf.Abs(body.linearVelocity.x), Is.LessThan(0.01f));
        }

        [UnityTest]
        public IEnumerator Dash_DoesNothingWhileLocked()
        {
            // 결별 잠금 — 획득했어도 발동하면 안 된다.
            ProgressionState.Current.SetAbility("F1", AbilityState.Locked);
            MakeRig();
            yield return Settle();

            controller.SetIntent(AbilityTap);
            yield return null;

            Assert.That(dash.IsDashing, Is.False);
        }

        [UnityTest]
        public IEnumerator Dash_MovesFastAndRestoresGravity()
        {
            ProgressionState.Current.SetAbility("F1", AbilityState.Unlocked);
            MakeRig();
            yield return Settle();
            var gravityBefore = body.gravityScale;

            controller.SetIntent(AbilityTap);
            yield return null;
            controller.SetIntent(InputIntent.None);
            yield return new WaitForFixedUpdate();

            Assert.That(dash.IsDashing, Is.True);
            Assert.That(controller.StateId, Is.EqualTo(PlayerStateId.Dashing));
            Assert.That(body.linearVelocity.x, Is.GreaterThan(10f), "돌진 속도(14)로 나가야 한다");
            Assert.That(body.gravityScale, Is.EqualTo(0f), "돌진 중에는 중력이 꺼진다");

            yield return Settle(20); // 0.14s 지나 종료

            Assert.That(dash.IsDashing, Is.False);
            Assert.That(body.gravityScale, Is.EqualTo(gravityBefore), "끝나면 중력이 복원된다");
            Assert.That(Mathf.Abs(body.linearVelocity.x), Is.LessThanOrEqualTo(5.5f), "돌진 관성은 최고속으로 잘린다");
        }

        [UnityTest]
        public IEnumerator AirDash_OnlyOncePerAirtime()
        {
            ProgressionState.Current.SetAbility("F1", AbilityState.Unlocked);
            MakeRig();
            yield return Settle();

            // 공중으로 — 점프 대신 그냥 높이 들어올린다(테스트 내내 착지하지 않을 높이).
            body.position += new Vector2(0f, 30f);
            yield return Settle(3);
            Assert.That(motor.IsGrounded, Is.False, "전제: 공중이어야 한다");

            controller.SetIntent(AbilityTap);
            yield return null;
            controller.SetIntent(InputIntent.None);
            Assert.That(dash.IsDashing, Is.True, "첫 공중 돌진은 나간다");

            yield return Settle(25); // 돌진 종료 + 쿨다운(0.55s)까지 대기
            yield return new WaitForSeconds(0.6f);
            Assert.That(motor.IsGrounded, Is.False, "전제: 아직 공중");

            controller.SetIntent(AbilityTap);
            yield return null;
            controller.SetIntent(InputIntent.None);

            Assert.That(dash.IsDashing, Is.False, "같은 체공에서 두 번째 돌진은 안 나간다");
        }
    }
}
