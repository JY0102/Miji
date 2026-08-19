using System;
using Miji.Core.Events;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace Miji.Core.Tests
{
    public class EventBusTests
    {
        struct Ping
        {
            public int Value;
        }

        struct Other
        {
            public int Value;
        }

        [SetUp]
        public void Reset() => EventBus.Clear();

        [TearDown]
        public void Cleanup() => EventBus.Clear();

        [Test]
        public void Publish_reaches_subscriber_with_payload()
        {
            var received = 0;
            EventBus.Subscribe<Ping>(p => received = p.Value);

            EventBus.Publish(new Ping { Value = 11 });

            Assert.AreEqual(11, received);
        }

        [Test]
        public void Publish_reaches_every_subscriber()
        {
            var count = 0;
            EventBus.Subscribe<Ping>(_ => count++);
            EventBus.Subscribe<Ping>(_ => count++);

            EventBus.Publish(new Ping());

            Assert.AreEqual(2, count);
        }

        [Test]
        public void Signals_are_isolated_by_type()
        {
            var pings = 0;
            EventBus.Subscribe<Ping>(_ => pings++);

            EventBus.Publish(new Other());

            Assert.AreEqual(0, pings, "다른 타입의 신호가 전달되면 안 된다");
        }

        [Test]
        public void Unsubscribe_stops_delivery()
        {
            var count = 0;
            Action<Ping> handler = _ => count++;

            EventBus.Subscribe(handler);
            EventBus.Unsubscribe(handler);
            EventBus.Publish(new Ping());

            Assert.AreEqual(0, count);
            Assert.AreEqual(0, EventBus.SubscriberCount<Ping>(), "마지막 구독자가 빠지면 목록도 비워야 한다");
        }

        [Test]
        public void Unsubscribe_keeps_other_subscribers()
        {
            var kept = 0;
            Action<Ping> removed = _ => Assert.Fail("해제된 구독자가 호출됐다");

            EventBus.Subscribe(removed);
            EventBus.Subscribe<Ping>(_ => kept++);
            EventBus.Unsubscribe(removed);

            EventBus.Publish(new Ping());

            Assert.AreEqual(1, kept);
        }

        [Test]
        public void Publishing_with_no_subscriber_is_safe()
        {
            Assert.DoesNotThrow(() => EventBus.Publish(new Ping()));
        }

        [Test]
        public void One_throwing_subscriber_does_not_block_the_rest()
        {
            // 예외는 잡아서 로그로 흘린다 — 테스트에서는 그 로그를 무시한다.
            LogAssert.ignoreFailingMessages = true;

            var reached = false;
            EventBus.Subscribe<Ping>(_ => throw new InvalidOperationException("의도된 예외"));
            EventBus.Subscribe<Ping>(_ => reached = true);

            Assert.DoesNotThrow(() => EventBus.Publish(new Ping()));
            Assert.IsTrue(reached, "앞 구독자의 예외가 뒤 구독자를 막으면 안 된다");
        }

        [Test]
        public void Unsubscribing_during_publish_does_not_break_iteration()
        {
            var count = 0;
            Action<Ping> second = _ => count++;

            EventBus.Subscribe<Ping>(_ => EventBus.Unsubscribe(second));
            EventBus.Subscribe(second);

            Assert.DoesNotThrow(() => EventBus.Publish(new Ping()));
            Assert.AreEqual(1, count, "이번 발행은 스냅샷 기준으로 끝까지 돌아야 한다");

            EventBus.Publish(new Ping());
            Assert.AreEqual(1, count, "다음 발행부터는 해제가 반영돼야 한다");
        }

        [Test]
        public void Clear_removes_all_subscriptions()
        {
            EventBus.Subscribe<Ping>(_ => Assert.Fail("Clear 후 호출되면 안 된다"));

            EventBus.Clear();
            EventBus.Publish(new Ping());

            Assert.AreEqual(0, EventBus.SubscriberCount<Ping>());
        }

        [Test]
        public void Null_handler_is_ignored()
        {
            Assert.DoesNotThrow(() => EventBus.Subscribe<Ping>(null));
            Assert.DoesNotThrow(() => EventBus.Unsubscribe<Ping>(null));
        }
    }
}
