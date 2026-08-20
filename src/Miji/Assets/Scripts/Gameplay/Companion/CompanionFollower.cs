using Miji.Gameplay.Player;
using UnityEngine;

namespace Miji.Gameplay.Companion
{
    /// <summary>
    /// B(무리비)의 테스트용 추종 — A의 바로 뒤를 따라다니는 것이 전부다.
    ///
    /// 확정 설계(이원 무브셋 3절)의 뼈대를 미리 지킨다:
    /// - 플레이어는 B를 관리하지 않는다 — 입력도 콜라이더도 없다
    /// - 너무 처지면 연출 없이 즉시 복귀한다 (「B가 멀어서 못 했다」는 상황 금지)
    /// - B는 무적이므로 Health/Hurtbox를 붙이지 않는다 (붕괴는 전투 피해가 아니다)
    ///
    /// F2 받침·F5 투척의 협력 스냅 로직은 여기가 아니라 G6(B 협력)에서 붙는다.
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer))]
    public class CompanionFollower : MonoBehaviour
    {
        [Tooltip("따라다닐 대상(A). 비우면 씬에서 PlayerMotor를 찾는다.")]
        [SerializeField] PlayerMotor target;

        [Tooltip("A가 보는 방향의 반대쪽으로 이만큼 떨어져 선다.")]
        [SerializeField] float followDistance = 1.1f;

        [Tooltip("목표 지점을 향한 감쇠 추적 시간. 클수록 굼뜨다.")]
        [SerializeField] float smoothTime = 0.22f;

        [Tooltip("이보다 멀어지면 연출 없이 즉시 복귀한다.")]
        [SerializeField] float snapDistance = 8f;

        [Header("걸음 들썩임 — 생물이라는 최소한의 표시")]
        [SerializeField] float bobAmplitude = 0.05f;
        [SerializeField] float bobFrequency = 9f;

        SpriteRenderer sprite;
        Vector3 basePosition;   // 들썩임을 뺀 실제 추적 위치
        Vector3 velocity;
        float bobPhase;

        void Awake()
        {
            sprite = GetComponent<SpriteRenderer>();
            if (target == null) target = FindFirstObjectByType<PlayerMotor>();
            basePosition = transform.position;
        }

        void LateUpdate()
        {
            if (target == null) return;

            var anchor = target.transform.position
                         + new Vector3(-target.Facing * followDistance, 0f, 0f);

            if ((anchor - basePosition).sqrMagnitude > snapDistance * snapDistance)
            {
                basePosition = anchor;
                velocity = Vector3.zero;
            }
            else
            {
                basePosition = Vector3.SmoothDamp(basePosition, anchor, ref velocity, smoothTime);
            }

            // 움직일 때만 들썩인다. 서 있으면 조용히 선다.
            var moving = Mathf.Clamp01(new Vector2(velocity.x, velocity.y).magnitude / 2f);
            bobPhase += Time.deltaTime * bobFrequency * Mathf.Max(moving, 0.001f);
            var bob = Mathf.Abs(Mathf.Sin(bobPhase)) * bobAmplitude * moving;

            transform.position = basePosition + new Vector3(0f, bob, 0f);

            // 가는 방향을 본다. 멈춰 있으면 A 쪽을 본다.
            var face = Mathf.Abs(velocity.x) > 0.05f
                ? velocity.x
                : target.transform.position.x - basePosition.x;
            if (!Mathf.Approximately(face, 0f)) sprite.flipX = face < 0f;
        }
    }
}
