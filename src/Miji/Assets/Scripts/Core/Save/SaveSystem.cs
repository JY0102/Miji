using System;
using System.Collections.Generic;
using System.IO;
using Miji.Core.Progression;
using UnityEngine;

namespace Miji.Core.Save
{
    /// <summary>
    /// JSON 세이브 — Godot Phase 7 검증 설계의 재이식.
    /// <see cref="ProgressionState"/>를 통째로 쓰고 통째로 읽는다.
    /// JsonUtility는 딕셔너리를 못 다루므로 평행 배열 DTO를 경유한다.
    /// </summary>
    public static class SaveSystem
    {
        [Serializable]
        class SaveData
        {
            public string[] abilityIds = Array.Empty<string>();
            public int[] abilityStates = Array.Empty<int>();
            public string[] flags = Array.Empty<string>();
            public string checkpointRoomId = "";
            public float checkpointX;
            public float checkpointY;
        }

        public static string DefaultPath => Path.Combine(Application.persistentDataPath, "save.json");

        public static void Save(ProgressionState state, string path = null)
        {
            path ??= DefaultPath;

            var ids = new List<string>();
            var states = new List<int>();
            foreach (var pair in state.Abilities)
            {
                ids.Add(pair.Key);
                states.Add((int)pair.Value);
            }

            var data = new SaveData
            {
                abilityIds = ids.ToArray(),
                abilityStates = states.ToArray(),
                flags = new List<string>(state.Flags).ToArray(),
                checkpointRoomId = state.CheckpointRoomId ?? "",
                checkpointX = state.CheckpointX,
                checkpointY = state.CheckpointY
            };

            File.WriteAllText(path, JsonUtility.ToJson(data, prettyPrint: true));
        }

        /// <summary>없거나 깨졌으면 null — 호출자가 새 게임으로 시작한다.</summary>
        public static ProgressionState Load(string path = null)
        {
            path ??= DefaultPath;
            if (!File.Exists(path)) return null;

            SaveData data;
            try
            {
                data = JsonUtility.FromJson<SaveData>(File.ReadAllText(path));
            }
            catch (Exception e)
            {
                Debug.LogWarning($"{nameof(SaveSystem)}: 세이브를 읽지 못했다 — {e.Message}");
                return null;
            }
            if (data == null) return null;

            var state = new ProgressionState
            {
                CheckpointRoomId = data.checkpointRoomId,
                CheckpointX = data.checkpointX,
                CheckpointY = data.checkpointY
            };

            var count = Mathf.Min(data.abilityIds.Length, data.abilityStates.Length);
            for (var i = 0; i < count; i++)
                state.RestoreAbility(data.abilityIds[i], (AbilityState)data.abilityStates[i]);

            foreach (var flag in data.flags)
                state.RestoreFlag(flag);

            return state;
        }

        public static void Delete(string path = null)
        {
            path ??= DefaultPath;
            if (File.Exists(path)) File.Delete(path);
        }
    }
}
