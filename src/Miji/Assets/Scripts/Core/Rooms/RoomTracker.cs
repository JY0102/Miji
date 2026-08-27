using Miji.Core.Events;
using UnityEngine;

namespace Miji.Core.Rooms
{
    /// <summary>룸이 바뀌었다. 카메라·세이브·룸 기믹이 구독한다. Previous는 첫 진입 시 null.</summary>
    public readonly struct RoomChangedSignal
    {
        public readonly Room Previous;
        public readonly Room Current;

        public RoomChangedSignal(Room previous, Room current)
        {
            Previous = previous;
            Current = current;
        }
    }

    /// <summary>
    /// 추적 대상(플레이어)이 지금 어느 <see cref="Room"/>에 있는지 안다.
    /// 룸을 벗어나기 전에는 재검색하지 않고, 바뀌면 <see cref="RoomChangedSignal"/>을 쏜다.
    /// </summary>
    public class RoomTracker : MonoBehaviour
    {
        [SerializeField] Transform target;

        Room[] rooms;

        public Room Current { get; private set; }

        void Awake() => rooms = FindObjectsByType<Room>(FindObjectsSortMode.None);

        void Update()
        {
            if (target == null || rooms == null) return;

            Vector2 pos = target.position;
            if (Current != null && Current.Contains(pos)) return;

            foreach (var room in rooms)
            {
                if (!room.Contains(pos)) continue;

                var previous = Current;
                Current = room;
                EventBus.Publish(new RoomChangedSignal(previous, room));
                return;
            }
            // 어느 룸에도 없으면(낙사 등) 마지막 룸을 유지한다 — 카메라가 허공을 쫓지 않게.
        }
    }
}
