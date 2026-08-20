using UnityEngine;

namespace Miji.Gameplay.View
{
    /// <summary>
    /// 방향 전환을 좌우 반전 한 방이 아니라 **180도 회전 3프레임**으로 보여준다.
    ///
    ///   1) 45° (돌기 전 방향)  2) 정면(대칭)  3) 45° (돌아갈 방향, 뒤집어 씀)
    ///
    /// A와 B가 같은 규칙을 쓰므로 여기 한 곳에 둔다. 스프라이트를 덮어쓰는 방식이라
    /// Animator 상태를 늘리지 않는다 — 애니메이터가 먼저 쓰고 이쪽이 나중에 덮는다.
    /// </summary>
    public static class TurnView
    {
        /// <summary>
        /// 회전 중이면 해당 프레임을 적용하고 true. 스프라이트가 없으면 false(호출자가 평소 처리).
        /// </summary>
        public static bool Apply(SpriteRenderer sprite, float remaining, float duration,
                                 int from, int to, Sprite quarter, Sprite front)
        {
            if (front == null || duration <= 0f) return false;

            // 남은 시간을 3등분: 처음 1/3 = 출발 쪽 45°, 가운데 = 정면, 마지막 1/3 = 도착 쪽 45°.
            var elapsed = duration - remaining;
            var stage = Mathf.Clamp(Mathf.FloorToInt(elapsed / (duration / 3f)), 0, 2);

            // 45°가 없으면 정면 한 장으로 버틴다(예전 동작).
            if (quarter == null) stage = 1;

            switch (stage)
            {
                case 0:
                    sprite.sprite = quarter;
                    sprite.flipX = from < 0;
                    return true;
                case 1:
                    sprite.sprite = front;
                    sprite.flipX = false;   // 정면은 대칭
                    return true;
                default:
                    sprite.sprite = quarter;
                    sprite.flipX = to < 0;
                    return true;
            }
        }
    }
}
