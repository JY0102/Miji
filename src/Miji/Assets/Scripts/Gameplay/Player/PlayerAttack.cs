using Miji.Core.Combat;
using Miji.Gameplay.Combat;
using UnityEngine;

namespace Miji.Gameplay.Player
{
    /// <summary>
    /// A의 근접 공격. FSM 상태가 아니라 <b>병행 컴포넌트</b>다 —
    /// 공격을 상태로 만들어 이동을 잠그면 그게 곧 응답 지연이고(조작감 척추 위반),
    /// HK도 이동 중 공격이 병행된다. 무게는 이동 잠금이 아니라 <b>히트스톱</b>이 진다.
    ///
    /// 선딜 → 활성 창(물리 스텝마다 Sweep) → 쿨다운. 활성 창 동안 히트박스가
    /// 바라보는 방향으로 배치되고, 그레이박스 이펙트(있으면)가 켜진다.
    /// Attack/Hurt 애니메이션은 아트가 나오면 Animator 트리거로 뒤따라 반입.
    /// </summary>
    [RequireComponent(typeof(PlayerController))]
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] Hitbox hitbox;

        [Header("타이밍 — 응답은 즉각, 무게는 히트스톱으로")]
        [Tooltip("누른 뒤 활성까지. 0이면 그 프레임에 바로 벤다.")]
        [SerializeField] float startup = 0.04f;
        [SerializeField] float activeDuration = 0.10f;
        [Tooltip("누름 → 다음 누름까지의 최소 간격.")]
        [SerializeField] float cooldown = 0.35f;

        [Header("히트스톱 — 맞힌 순간 공격자·피격자만 잠깐 멈춘다")]
        [SerializeField] HitStop hitStop;
        [SerializeField] float hitStopDuration = 0.05f;

        [Tooltip("활성 창 동안 켜지는 그레이박스 이펙트. 비우면 생략.")]
        [SerializeField] SpriteRenderer swingVisual;

        PlayerController controller;
        PlayerMotor motor;
        float cooldownLeft;
        float phaseLeft;
        Phase phase = Phase.Ready;
        float hitboxHomeX;

        enum Phase { Ready, Startup, Active }

        void Awake()
        {
            controller = GetComponent<PlayerController>();
            motor = controller.Motor != null ? controller.Motor : GetComponent<PlayerMotor>();

            if (hitbox == null) hitbox = GetComponentInChildren<Hitbox>();
            if (hitbox == null)
                Debug.LogError($"{nameof(PlayerAttack)}: Hitbox가 없다. 자식에 붙이거나 지정할 것.", this);
            else
                hitboxHomeX = Mathf.Abs(hitbox.transform.localPosition.x);

            if (hitStop == null) hitStop = GetComponent<HitStop>();
            if (swingVisual != null) swingVisual.enabled = false;
        }

        void OnEnable()
        {
            if (hitbox != null) hitbox.Landed += OnLanded;
        }

        void OnDisable()
        {
            if (hitbox != null) hitbox.Landed -= OnLanded;
            if (swingVisual != null) swingVisual.enabled = false;
            phase = Phase.Ready;
        }

        void Update()
        {
            cooldownLeft -= Time.deltaTime;

            if (phase == Phase.Ready && controller.Intent.AttackPressed && cooldownLeft <= 0f && hitbox != null)
            {
                cooldownLeft = cooldown;
                phase = Phase.Startup;
                phaseLeft = startup;

                // 방향은 누른 순간 고정 — 휘두르는 중에 뒤돌아도 칼은 그대로 간다.
                var local = hitbox.transform.localPosition;
                local.x = hitboxHomeX * motor.Facing;
                hitbox.transform.localPosition = local;
            }

            if (phase == Phase.Startup)
            {
                phaseLeft -= Time.deltaTime;
                if (phaseLeft <= 0f)
                {
                    phase = Phase.Active;
                    phaseLeft = activeDuration;
                    hitbox.BeginWindow();
                    if (swingVisual != null) swingVisual.enabled = true;
                }
            }
            else if (phase == Phase.Active)
            {
                phaseLeft -= Time.deltaTime;
                if (phaseLeft <= 0f)
                {
                    phase = Phase.Ready;
                    if (swingVisual != null) swingVisual.enabled = false;
                }
            }
        }

        void FixedUpdate()
        {
            if (phase == Phase.Active && hitbox != null) hitbox.Sweep();
        }

        void OnLanded(Hurtbox victim, DamageInfo info)
        {
            if (hitStop != null)
                hitStop.Freeze(hitStopDuration, gameObject, victim.transform.root.gameObject);
        }
    }
}
