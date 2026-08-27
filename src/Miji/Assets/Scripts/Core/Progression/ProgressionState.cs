using System.Collections.Generic;
using Miji.Core.Events;
using UnityEngine;

namespace Miji.Core.Progression
{
    /// <summary>
    /// 능력의 세 상태. 결별 메카닉의 토대 —
    /// 확정 설계 「지우지 않고 잠근다」가 처음부터 3상태를 요구한다(아키텍처 스펙 0절).
    /// </summary>
    public enum AbilityState
    {
        NotAcquired,
        Unlocked,

        /// <summary>획득했지만 지금은 못 쓴다(결별 시 F2·F5). 재획득이 아니라 해제로 돌아온다.</summary>
        Locked
    }

    /// <summary>능력 상태가 바뀌었다. 게이트·UI·B 협력 로직이 구독한다.</summary>
    public readonly struct AbilityChangedSignal
    {
        public readonly string Id;
        public readonly AbilityState State;

        public AbilityChangedSignal(string id, AbilityState state)
        {
            Id = id;
            State = state;
        }
    }

    /// <summary>
    /// 진행 상태의 유일한 원장 — 능력 3상태 + 진행 플래그 + 체크포인트.
    /// Core는 능력의 의미를 모른다. "F1"이 돌진인 것은 Gameplay가 안다.
    ///
    /// 세이브 대상이다(<see cref="Miji.Core.Save.SaveSystem"/>).
    /// </summary>
    public class ProgressionState
    {
        /// <summary>씬 전체가 공유하는 현재 진행.</summary>
        public static ProgressionState Current { get; set; } = new();

        // 이 프로젝트는 Enter Play Mode 도메인 리로드가 꺼져 있다(EditorSettings) —
        // static이 플레이 간에 살아남으므로 진입마다 명시적으로 비운다. 빌드에서는 시작 시 1회.
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        static void ResetOnEnterPlay() => Current = new ProgressionState();

        readonly Dictionary<string, AbilityState> abilities = new();
        readonly HashSet<string> flags = new();

        /// <summary>마지막 체크포인트의 룸 ID. 룸 단위 저장(Godot 보류 항목 청산)의 앵커.</summary>
        public string CheckpointRoomId = "";
        public float CheckpointX;
        public float CheckpointY;

        public AbilityState GetAbility(string id) =>
            abilities.TryGetValue(id, out var state) ? state : AbilityState.NotAcquired;

        /// <summary>지금 쓸 수 있나. 잠김(Locked)은 획득했어도 false다.</summary>
        public bool IsUsable(string id) => GetAbility(id) == AbilityState.Unlocked;

        public void SetAbility(string id, AbilityState state)
        {
            if (GetAbility(id) == state) return;
            abilities[id] = state;
            EventBus.Publish(new AbilityChangedSignal(id, state));
        }

        public bool HasFlag(string flag) => flags.Contains(flag);
        public void SetFlag(string flag) => flags.Add(flag);

        // ── 직렬화 경계 (SaveSystem 전용) ──

        public IReadOnlyDictionary<string, AbilityState> Abilities => abilities;
        public IReadOnlyCollection<string> Flags => flags;

        /// <summary>로드 복원용 — 신호를 쏘지 않고 조용히 채운다.</summary>
        public void RestoreAbility(string id, AbilityState state) => abilities[id] = state;
        public void RestoreFlag(string flag) => flags.Add(flag);
    }
}
