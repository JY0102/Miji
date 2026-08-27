using UnityEngine;

namespace Miji.Core.Rooms
{
    /// <summary>
    /// 룸 하나의 월드 경계. 카메라 클램프와 「지금 어느 룸인가」 판정의 근거다.
    /// Core는 룸의 의미(여관·둥지)를 모른다 — 이름은 데이터일 뿐이다.
    /// </summary>
    public class Room : MonoBehaviour
    {
        [Tooltip("월드 좌표 경계. 카메라는 이 사각형 밖을 비추지 않는다.")]
        [SerializeField] Rect worldBounds = new(-10f, -6f, 20f, 12f);

        public string Id => name;
        public Rect WorldBounds => worldBounds;

        public bool Contains(Vector2 point) => worldBounds.Contains(point);

        /// <summary>에디터 빌더용.</summary>
        public void SetBounds(Rect bounds) => worldBounds = bounds;

        void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(0.3f, 1f, 0.6f, 0.8f);
            Gizmos.DrawWireCube(worldBounds.center, worldBounds.size);
        }
    }
}
