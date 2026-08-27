using System.IO;
using Miji.Core.Events;
using Miji.Core.Progression;
using Miji.Core.Save;
using NUnit.Framework;

namespace Miji.Core.Tests
{
    /// <summary>C4 능력 3상태(미획득/해금/잠김)와 C5 JSON 세이브의 규칙.</summary>
    public class ProgressionAndSaveTests
    {
        string tempPath;

        [SetUp]
        public void SetUp() => tempPath = Path.Combine(Path.GetTempPath(), "miji_save_test.json");

        [TearDown]
        public void TearDown()
        {
            EventBus.Clear();
            if (File.Exists(tempPath)) File.Delete(tempPath);
        }

        // ── C4 ProgressionState ─────────────────────────────────

        [Test]
        public void Ability_DefaultsToNotAcquired()
        {
            var state = new ProgressionState();
            Assert.That(state.GetAbility("F1"), Is.EqualTo(AbilityState.NotAcquired));
            Assert.That(state.IsUsable("F1"), Is.False);
        }

        [Test]
        public void Ability_UnlockedIsUsable()
        {
            var state = new ProgressionState();
            state.SetAbility("F1", AbilityState.Unlocked);
            Assert.That(state.IsUsable("F1"), Is.True);
        }

        [Test]
        public void Ability_LockedIsAcquiredButNotUsable()
        {
            // 결별 = 지우지 않고 잠근다 — 상태는 남되 쓸 수 없어야 한다.
            var state = new ProgressionState();
            state.SetAbility("F2", AbilityState.Unlocked);
            state.SetAbility("F2", AbilityState.Locked);

            Assert.That(state.GetAbility("F2"), Is.EqualTo(AbilityState.Locked));
            Assert.That(state.IsUsable("F2"), Is.False);
        }

        [Test]
        public void Ability_ChangePublishesSignal_SameStateDoesNot()
        {
            var state = new ProgressionState();
            var count = 0;
            EventBus.Subscribe<AbilityChangedSignal>(_ => count++);

            state.SetAbility("F1", AbilityState.Unlocked);
            state.SetAbility("F1", AbilityState.Unlocked); // 같은 상태 재설정 — 무신호

            Assert.That(count, Is.EqualTo(1));
        }

        [Test]
        public void Flags_SetAndQuery()
        {
            var state = new ProgressionState();
            Assert.That(state.HasFlag("met_b"), Is.False);
            state.SetFlag("met_b");
            Assert.That(state.HasFlag("met_b"), Is.True);
        }

        // ── C5 SaveSystem ───────────────────────────────────────

        [Test]
        public void Save_RoundTripsEverything()
        {
            var state = new ProgressionState
            {
                CheckpointRoomId = "Room_East",
                CheckpointX = 3.5f,
                CheckpointY = 0.42f
            };
            state.SetAbility("F1", AbilityState.Unlocked);
            state.SetAbility("F2", AbilityState.Locked);
            state.SetFlag("met_b");

            SaveSystem.Save(state, tempPath);
            var loaded = SaveSystem.Load(tempPath);

            Assert.That(loaded, Is.Not.Null);
            Assert.That(loaded.GetAbility("F1"), Is.EqualTo(AbilityState.Unlocked));
            Assert.That(loaded.GetAbility("F2"), Is.EqualTo(AbilityState.Locked));
            Assert.That(loaded.GetAbility("F3"), Is.EqualTo(AbilityState.NotAcquired));
            Assert.That(loaded.HasFlag("met_b"), Is.True);
            Assert.That(loaded.CheckpointRoomId, Is.EqualTo("Room_East"));
            Assert.That(loaded.CheckpointX, Is.EqualTo(3.5f));
            Assert.That(loaded.CheckpointY, Is.EqualTo(0.42f));
        }

        [Test]
        public void Load_MissingFileReturnsNull()
        {
            Assert.That(SaveSystem.Load(tempPath), Is.Null);
        }

        [Test]
        public void Load_DoesNotPublishAbilitySignals()
        {
            var state = new ProgressionState();
            state.SetAbility("F1", AbilityState.Unlocked);
            SaveSystem.Save(state, tempPath);

            var count = 0;
            EventBus.Subscribe<AbilityChangedSignal>(_ => count++);
            SaveSystem.Load(tempPath);

            Assert.That(count, Is.EqualTo(0), "로드 복원은 조용해야 한다 — 해금 연출이 재생되면 안 된다");
        }
    }
}
