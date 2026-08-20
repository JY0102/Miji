using System.Collections.Generic;
using Miji.Core.StateMachines;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace Miji.Core.Tests
{
    public class StateMachineTests
    {
        enum Key { Idle, Move, Dash }

        /// <summary>호출 순서를 공유 로그에 남기는 상태.</summary>
        class Spy : StateBase
        {
            readonly string name;
            readonly List<string> log;

            public int Ticks { get; private set; }
            public int FixedTicks { get; private set; }

            public Spy(string name, List<string> log)
            {
                this.name = name;
                this.log = log;
            }

            public override void Enter() => log.Add($"{name}:enter");
            public override void Exit() => log.Add($"{name}:exit");
            public override void Tick(float dt) => Ticks++;
            public override void FixedTick(float dt) => FixedTicks++;
        }

        List<string> log;
        StateMachine<Key> fsm;
        Spy idle, move;

        [SetUp]
        public void Setup()
        {
            log = new List<string>();
            fsm = new StateMachine<Key>();
            idle = new Spy("idle", log);
            move = new Spy("move", log);
            fsm.Add(Key.Idle, idle);
            fsm.Add(Key.Move, move);
        }

        [Test]
        public void Change_enters_the_target_state()
        {
            fsm.Change(Key.Idle);

            Assert.AreEqual(Key.Idle, fsm.CurrentKey);
            Assert.AreSame(idle, fsm.Current);
            Assert.AreEqual(new[] { "idle:enter" }, log.ToArray());
        }

        [Test]
        public void Change_exits_previous_before_entering_next()
        {
            fsm.Change(Key.Idle);
            fsm.Change(Key.Move);

            Assert.AreEqual(new[] { "idle:enter", "idle:exit", "move:enter" }, log.ToArray());
        }

        [Test]
        public void Change_to_same_state_is_ignored()
        {
            fsm.Change(Key.Idle);
            log.Clear();

            fsm.Change(Key.Idle);

            Assert.IsEmpty(log, "같은 상태로의 전이는 Exit/Enter를 다시 돌리지 않는다");
        }

        [Test]
        public void ForceChange_reenters_same_state()
        {
            fsm.Change(Key.Idle);
            log.Clear();

            fsm.ForceChange(Key.Idle);

            Assert.AreEqual(new[] { "idle:exit", "idle:enter" }, log.ToArray());
        }

        [Test]
        public void Tick_only_runs_on_current_state()
        {
            fsm.Change(Key.Idle);
            fsm.Tick(0.016f);
            fsm.FixedTick(0.02f);

            Assert.AreEqual(1, idle.Ticks);
            Assert.AreEqual(1, idle.FixedTicks);
            Assert.AreEqual(0, move.Ticks, "현재 상태가 아니면 Tick이 돌지 않는다");
        }

        [Test]
        public void Tick_before_any_change_is_safe()
        {
            Assert.DoesNotThrow(() => fsm.Tick(0.016f));
            Assert.DoesNotThrow(() => fsm.FixedTick(0.02f));
        }

        [Test]
        public void Changed_reports_previous_and_next()
        {
            Key from = default, to = default;
            var fired = 0;

            fsm.Change(Key.Idle);
            fsm.Changed += (p, n) => { from = p; to = n; fired++; };
            fsm.Change(Key.Move);

            Assert.AreEqual(1, fired);
            Assert.AreEqual(Key.Idle, from);
            Assert.AreEqual(Key.Move, to);
        }

        [Test]
        public void Unknown_key_logs_error_and_keeps_current_state()
        {
            fsm.Change(Key.Idle);
            LogAssert.ignoreFailingMessages = true;

            fsm.Change(Key.Dash); // 등록하지 않은 상태

            Assert.AreEqual(Key.Idle, fsm.CurrentKey, "실패한 전이가 현재 상태를 깨뜨리면 안 된다");
            Assert.AreSame(idle, fsm.Current);
        }

        [Test]
        public void Has_reports_registration()
        {
            Assert.IsTrue(fsm.Has(Key.Idle));
            Assert.IsFalse(fsm.Has(Key.Dash));
        }
    }
}
