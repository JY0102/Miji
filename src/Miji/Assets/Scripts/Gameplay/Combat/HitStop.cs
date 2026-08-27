using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Miji.Gameplay.Combat
{
    /// <summary>
    /// 국소 히트스톱. `Time.timeScale`을 만지지 않고 지정한 루트들의
    /// Animator 속도와 Rigidbody2D 시뮬레이션만 잠깐 동결한다(§9-3 #2 —
    /// 카메라·패럴랙스·다른 엔티티는 계속 돈다).
    ///
    /// 무게는 응답 지연이 아니라 히트스톱이 진다(2026-08-21 조작감 척추).
    /// </summary>
    public class HitStop : MonoBehaviour
    {
        // ponytail: 동결 중 새 요청은 무시(전역 1슬롯) — 다중 동시 히트스톱이 필요해지면 대상별 카운팅으로 승격
        bool frozen;

        public bool IsFrozen => frozen;

        public void Freeze(float duration, params GameObject[] roots)
        {
            if (frozen || duration <= 0f || roots == null) return;
            StartCoroutine(Run(duration, roots));
        }

        IEnumerator Run(float duration, GameObject[] roots)
        {
            frozen = true;

            var animators = new List<(Animator anim, float speed)>();
            var bodies = new List<(Rigidbody2D body, Vector2 velocity)>();

            foreach (var root in roots)
            {
                if (root == null) continue;

                foreach (var anim in root.GetComponentsInChildren<Animator>())
                {
                    if (!anim.enabled) continue;
                    animators.Add((anim, anim.speed));
                    anim.speed = 0f;
                }

                foreach (var body in root.GetComponentsInChildren<Rigidbody2D>())
                {
                    bodies.Add((body, body.linearVelocity));
                    body.simulated = false;
                }
            }

            yield return new WaitForSeconds(duration);

            foreach (var (anim, speed) in animators)
                if (anim != null) anim.speed = speed;

            foreach (var (body, velocity) in bodies)
            {
                if (body == null) continue;
                body.simulated = true;
                body.linearVelocity = velocity;
            }

            frozen = false;
        }
    }
}
