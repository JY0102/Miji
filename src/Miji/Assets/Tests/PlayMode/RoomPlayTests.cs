using System.Collections;
using System.Reflection;
using Miji.Core.Events;
using Miji.Core.Rooms;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Miji.Gameplay.PlayTests
{
    /// <summary>C8 룸 추적 — 진입 신호, 룸 간 이동, 룸 밖(낙사)에서 마지막 룸 유지.</summary>
    public class RoomPlayTests
    {
        [TearDown]
        public void TearDown()
        {
            EventBus.Clear();
            foreach (var go in Object.FindObjectsByType<GameObject>(FindObjectsSortMode.None))
                if (go.scene.isLoaded) Object.Destroy(go);
        }

        static Room MakeRoom(string name, Rect bounds)
        {
            var go = new GameObject(name);
            var room = go.AddComponent<Room>();
            room.SetBounds(bounds);
            return room;
        }

        [UnityTest]
        public IEnumerator Tracker_PublishesOnEntryAndTransition()
        {
            var west = MakeRoom("Room_West", new Rect(-10f, -5f, 10f, 10f));
            var east = MakeRoom("Room_East", new Rect(0f, -5f, 10f, 10f));

            var target = new GameObject("target").transform;
            target.position = new Vector2(-5f, 0f);

            var trackerGo = new GameObject("tracker");
            var tracker = trackerGo.AddComponent<RoomTracker>();
            typeof(RoomTracker).GetField("target", BindingFlags.Instance | BindingFlags.NonPublic)
                               .SetValue(tracker, target);

            var signals = 0;
            RoomChangedSignal last = default;
            EventBus.Subscribe<RoomChangedSignal>(s => { signals++; last = s; });

            yield return null; // 첫 Update — 진입
            Assert.That(tracker.Current, Is.SameAs(west));
            Assert.That(signals, Is.EqualTo(1));
            Assert.That(last.Previous, Is.Null);

            target.position = new Vector2(5f, 0f); // 동쪽 룸으로
            yield return null;
            Assert.That(tracker.Current, Is.SameAs(east));
            Assert.That(signals, Is.EqualTo(2));
            Assert.That(last.Previous, Is.SameAs(west));

            target.position = new Vector2(0f, -50f); // 룸 밖(낙사)
            yield return null;
            Assert.That(tracker.Current, Is.SameAs(east), "룸 밖에서는 마지막 룸을 유지한다");
            Assert.That(signals, Is.EqualTo(2));
        }
    }
}
