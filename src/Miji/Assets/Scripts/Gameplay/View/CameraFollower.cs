using Miji.Core.Combat;
using Miji.Core.Events;
using Miji.Core.Rooms;
using Miji.Gameplay.Player;
using UnityEngine;

namespace Miji.Gameplay.View
{
    /// <summary>
    /// 카메라 추종 — 룩어헤드 + 룸 경계 클램프 + 강공격 흔들림 (`MECHANIC_GAME_FEEL.md` §9-4).
    /// Cinemachine을 안 쓰는 것은 §9-4의 명시 결정(신규 의존성 금지).
    ///
    /// 흔들림은 계산된 추종 위치에 감쇠 오프셋을 더하는 방식이라 추종·클램프를 오염시키지 않고,
    /// <see cref="DamageInfo.Strong"/>가 선 피격에만 반응한다(사용자 확정 — 일반 타격은 히트스톱만).
    /// </summary>
    public class CameraFollower : MonoBehaviour
    {
        [SerializeField] Transform target;
        [Tooltip("바라보는 방향을 앞서 비추기 위한 모터. 비우면 룩어헤드 없음.")]
        [SerializeField] PlayerMotor motor;
        [SerializeField] RoomTracker rooms;

        [Header("추종")]
        [SerializeField] Vector2 offset = new(0f, 1f);
        [SerializeField] float smoothTime = 0.16f;

        [Header("룩어헤드 — 가는 쪽을 먼저 보여준다")]
        [SerializeField] float lookAheadDistance = 1.3f;
        [SerializeField] float lookAheadResponse = 4f;

        [Header("흔들림 — 강공격 전용")]
        [SerializeField] float shakeAmplitude = 0.14f;
        [SerializeField] float shakeDecay = 8f;

        Camera cam;
        Vector2 velocity;
        float lookAhead;
        float shake;

        void Awake() => cam = GetComponent<Camera>();

        void OnEnable() => EventBus.Subscribe<HitSignal>(OnHit);
        void OnDisable() => EventBus.Unsubscribe<HitSignal>(OnHit);

        void OnHit(HitSignal signal)
        {
            if (signal.Info.Strong) shake = 1f;
        }

        void LateUpdate()
        {
            if (target == null || cam == null) return;

            if (motor != null)
                lookAhead = Mathf.MoveTowards(lookAhead, motor.Facing * lookAheadDistance,
                                              lookAheadResponse * Time.deltaTime);

            Vector2 desired = (Vector2)target.position + offset + new Vector2(lookAhead, 0f);
            Vector2 pos = Vector2.SmoothDamp(transform.position, desired, ref velocity, smoothTime);

            var half = new Vector2(cam.orthographicSize * cam.aspect, cam.orthographicSize);
            if (rooms != null && rooms.Current != null)
                pos = ClampToRoom(pos, rooms.Current.WorldBounds, half);

            if (shake > 0f)
            {
                pos += Random.insideUnitCircle * (shakeAmplitude * shake);
                shake = Mathf.MoveTowards(shake, 0f, shakeDecay * Time.deltaTime);
            }

            transform.position = new Vector3(pos.x, pos.y, transform.position.z);
        }

        /// <summary>
        /// 카메라 중심을 룸 안으로 조인다. 룸이 화면보다 작은 축은 룸 중앙에 고정한다.
        /// 순수 계산 — EditMode 테스트가 잰다.
        /// </summary>
        public static Vector2 ClampToRoom(Vector2 center, Rect room, Vector2 halfExtents)
        {
            var x = room.width <= halfExtents.x * 2f
                ? room.center.x
                : Mathf.Clamp(center.x, room.xMin + halfExtents.x, room.xMax - halfExtents.x);

            var y = room.height <= halfExtents.y * 2f
                ? room.center.y
                : Mathf.Clamp(center.y, room.yMin + halfExtents.y, room.yMax - halfExtents.y);

            return new Vector2(x, y);
        }
    }
}
