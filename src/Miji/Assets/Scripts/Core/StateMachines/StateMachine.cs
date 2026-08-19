using System;
using System.Collections.Generic;

namespace Miji.Core.StateMachines
{
    /// <summary>한 상태. 몸이 무엇인지 모른다 — 플레이어·적·동행자·균형자가 같은 것을 쓴다.</summary>
    public interface IState
    {
        void Enter();
        void Tick(float deltaTime);
        void FixedTick(float fixedDeltaTime);
        void Exit();
    }

    /// <summary>구현할 게 하나뿐일 때 쓰는 기본 구현.</summary>
    public abstract class StateBase : IState
    {
        public virtual void Enter() { }
        public virtual void Tick(float deltaTime) { }
        public virtual void FixedTick(float fixedDeltaTime) { }
        public virtual void Exit() { }
    }

    /// <summary>
    /// 범용 FSM. 상태 객체를 등록해두고 키로 전이한다.
    ///
    /// 전이 조건을 FSM이 들고 있지 않은 것은 의도다 — 조건을 여기 넣으면
    /// 상태가 늘어날 때마다 이 파일이 바뀐다. 전이는 상태 자신이나 소유자가 호출한다.
    /// </summary>
    public class StateMachine<TKey>
    {
        readonly Dictionary<TKey, IState> states = new();

        public TKey CurrentKey { get; private set; }
        public IState Current { get; private set; }

        /// <summary>상태가 바뀔 때 (이전, 다음) 순으로 알린다. 애니메이션·로그용.</summary>
        public event Action<TKey, TKey> Changed;

        public void Add(TKey key, IState state)
        {
            if (state == null) throw new ArgumentNullException(nameof(state));
            states[key] = state;
        }

        public bool Has(TKey key) => states.ContainsKey(key);

        /// <summary>같은 상태로의 전이는 무시한다. 재진입이 필요하면 <see cref="ForceChange"/>.</summary>
        public void Change(TKey key)
        {
            if (Current != null && EqualityComparer<TKey>.Default.Equals(CurrentKey, key)) return;
            ForceChange(key);
        }

        public void ForceChange(TKey key)
        {
            if (!states.TryGetValue(key, out var next))
            {
                UnityEngine.Debug.LogError($"{nameof(StateMachine<TKey>)}: 등록되지 않은 상태 '{key}'로 전이를 시도했다.");
                return;
            }

            var previous = CurrentKey;
            Current?.Exit();

            CurrentKey = key;
            Current = next;
            Current.Enter();

            Changed?.Invoke(previous, key);
        }

        public void Tick(float deltaTime) => Current?.Tick(deltaTime);
        public void FixedTick(float fixedDeltaTime) => Current?.FixedTick(fixedDeltaTime);
    }
}
