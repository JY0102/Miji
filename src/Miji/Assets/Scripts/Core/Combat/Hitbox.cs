using System;
using System.Collections.Generic;
using UnityEngine;

namespace Miji.Core.Combat
{
    /// <summary>
    /// 때리는 판정 범위. 물리 콜백이 아니라 <b>능동 쿼리</b>로 동작한다 —
    /// 공격의 활성 창 동안 <see cref="Sweep"/>을 호출하면 허트박스 레이어만 훑어서
    /// <see cref="Hurtbox.Receive"/>로 넘긴다.
    ///
    /// 쿼리 기반인 이유: 히트박스는 공격하는 짧은 창에만 살아 있으면 되고,
    /// 명시적 레이어 마스크 쿼리는 콜리전 매트릭스와 무관하므로
    /// 9(Hitbox)·10(Hurtbox) 층의 물리 접촉을 통째로 꺼도 된다(접촉 쌍 생성 비용 0).
    ///
    /// 자기피해 가드 2중: ⑴ 편(<see cref="FactionRules"/>) ⑵ <see cref="owner"/> 루트 아래의
    /// 허트박스는 건너뛴다 — Hazard처럼 편 규칙이 「아무나 때린다」인 경우까지 막는다.
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class Hitbox : MonoBehaviour
    {
        [SerializeField] Faction faction = Faction.Player;
        [SerializeField] int damage = 1;

        [Tooltip("맞은 쪽을 밀어내는 세기. 방향은 자동(공격자 → 피격자 수평 + 살짝 위).")]
        [SerializeField] float knockbackStrength = 5f;

        [Tooltip("강공격이면 켠다. 카메라 흔들림 같은 전역 효과가 이 플래그에만 반응한다.")]
        [SerializeField] bool strong;

        [Tooltip("훑을 허트박스 레이어. 기본 10(Hurtbox).")]
        [SerializeField] LayerMask hurtboxLayers = 1 << 10;

        [Tooltip("이 루트 아래의 허트박스는 때리지 않는다(자기피해 가드). 비우면 자기 루트.")]
        [SerializeField] Transform owner;

        Collider2D shape;
        ContactFilter2D filter;
        readonly List<Collider2D> results = new(8);
        readonly HashSet<Hurtbox> hitThisWindow = new();

        /// <summary>이번 창에서 한 명이라도 맞혔을 때, 맞힌 대상마다 1회.</summary>
        public event Action<Hurtbox, DamageInfo> Landed;

        public Faction Faction => faction;

        void Awake()
        {
            shape = GetComponent<Collider2D>();
            shape.isTrigger = true;

            if (owner == null) owner = transform.root;

            filter = new ContactFilter2D { useLayerMask = true, layerMask = hurtboxLayers, useTriggers = true };
        }

        /// <summary>공격 활성 창의 시작. 같은 창에서 같은 대상을 두 번 때리지 않기 위한 리셋.</summary>
        public void BeginWindow() => hitThisWindow.Clear();

        /// <summary>
        /// 지금 겹쳐 있는 허트박스를 때린다. 활성 창 동안 물리 스텝마다 불러도
        /// 창 내 중복 히트는 걸러진다. 반환값은 이번 호출에서 새로 맞힌 수.
        /// </summary>
        public int Sweep()
        {
            var found = shape.Overlap(filter, results);
            var landed = 0;

            for (var i = 0; i < found; i++)
            {
                var hurtbox = results[i].GetComponent<Hurtbox>();
                if (hurtbox == null || hitThisWindow.Contains(hurtbox)) continue;
                if (owner != null && hurtbox.transform.IsChildOf(owner)) continue; // 자기피해 가드

                var info = BuildInfo(hurtbox);
                if (!hurtbox.Receive(info)) continue;

                hitThisWindow.Add(hurtbox);
                landed++;
                Landed?.Invoke(hurtbox, info);
            }

            return landed;
        }

        DamageInfo BuildInfo(Hurtbox victim)
        {
            var origin = owner != null ? (Vector2)owner.position : (Vector2)transform.position;
            var victimPos = (Vector2)victim.transform.position;

            var dir = victimPos.x >= origin.x ? 1f : -1f;
            var knockback = new Vector2(dir, 0.35f).normalized * knockbackStrength;

            return new DamageInfo(damage, faction, victim.GetComponent<Collider2D>().ClosestPoint(origin),
                                  knockback, owner != null ? owner.gameObject : gameObject, strong);
        }
    }
}
