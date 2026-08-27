using System;
using System.Collections.Generic;

namespace Miji.Core.Events
{
    /// <summary>
    /// 전역 pub/sub. 신호 타입 하나당 구독자 목록 하나를 들고 있는다.
    ///
    /// Core에는 신호를 정의하지 않는다 — 게임플레이 고유 신호(능력 해금, 스위치 눌림 등)는
    /// Gameplay 층에서 struct로 선언한다. 새 신호가 생겨도 이 파일은 바뀌지 않는다.
    /// </summary>
    public static class EventBus
    {
        static readonly Dictionary<Type, Delegate> handlers = new();

        public static void Subscribe<T>(Action<T> handler) where T : struct
        {
            if (handler == null) return;

            var key = typeof(T);
            handlers[key] = handlers.TryGetValue(key, out var existing)
                ? Delegate.Combine(existing, handler)
                : handler;
        }

        public static void Unsubscribe<T>(Action<T> handler) where T : struct
        {
            if (handler == null) return;

            var key = typeof(T);
            if (!handlers.TryGetValue(key, out var existing)) return;

            var remaining = Delegate.Remove(existing, handler);
            if (remaining == null) handlers.Remove(key);
            else handlers[key] = remaining;
        }

        /// <summary>
        /// 구독자에게 신호를 보낸다. 한 구독자가 던진 예외가 나머지 구독자를 막지 않는다.
        /// </summary>
        public static void Publish<T>(T signal) where T : struct
        {
            if (!handlers.TryGetValue(typeof(T), out var existing)) return;

            // 콜백 중 구독/해제가 일어나도 안전하도록 복사본을 순회한다.
            foreach (var invocation in existing.GetInvocationList())
            {
                try
                {
                    ((Action<T>)invocation).Invoke(signal);
                }
                catch (Exception e)
                {
                    UnityEngine.Debug.LogException(e);
                }
            }
        }

        /// <summary>씬 재시작·플레이모드 종료 시 남은 구독을 비운다.</summary>
        public static void Clear() => handlers.Clear();

        // Enter Play Mode 도메인 리로드가 꺼진 프로젝트라 static 구독이 플레이 간에 살아남는다.
        // 진입마다 비운다 — 구독자들의 OnEnable은 이 뒤에 돈다.
        [UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.SubsystemRegistration)]
        static void ResetOnEnterPlay() => Clear();

        public static int SubscriberCount<T>() where T : struct =>
            handlers.TryGetValue(typeof(T), out var existing) ? existing.GetInvocationList().Length : 0;
    }
}
