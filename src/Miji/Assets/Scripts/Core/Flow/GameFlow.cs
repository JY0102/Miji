using System.Collections;
using Miji.Core.Events;
using Miji.Core.Input;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Miji.Core.Flow
{
    public enum GameMode
    {
        /// <summary>정상 플레이. 입력이 몸에 전달된다.</summary>
        Playing,

        /// <summary>컷신·연출. 입력 차단. (데모 엔딩의 스위치·암전이 여기)</summary>
        Cutscene,

        /// <summary>일시정지·메뉴.</summary>
        Paused,

        /// <summary>씬 전환 중.</summary>
        Loading
    }

    /// <summary>모드가 바뀌었다. (이전, 다음)</summary>
    public readonly struct GameModeChanged
    {
        public readonly GameMode Previous;
        public readonly GameMode Current;
        public GameModeChanged(GameMode previous, GameMode current)
        {
            Previous = previous;
            Current = current;
        }
    }

    /// <summary>
    /// 진행의 「순서」를 쥔다. 데이터 보관은 하지 않는다.
    ///
    /// Godot 프로토타입의 교훈을 그대로 승계한다 —
    /// 「진행 순서 결정은 GameFlow, 데이터 보관은 상태 객체」(DECISIONS 2026-07-25).
    /// 체크포인트 순서가 씬 프롭에 흩어져 경계가 흐려진 적이 있다.
    /// </summary>
    public class GameFlow : MonoBehaviour
    {
        public static GameFlow Instance { get; private set; }

        [SerializeField] InputReader inputReader;

        public GameMode Mode { get; private set; } = GameMode.Playing;

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
            ApplyMode(Mode);
        }

        void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
                // 씬을 벗어난 구독자가 남아 다음 플레이에 유령 콜백이 되는 것을 막는다.
                EventBus.Clear();
            }
        }

        public void SetMode(GameMode next)
        {
            if (Mode == next) return;

            var previous = Mode;
            Mode = next;
            ApplyMode(next);
            EventBus.Publish(new GameModeChanged(previous, next));
        }

        void ApplyMode(GameMode mode)
        {
            // 입력 차단은 모드에서 파생된다 — 개별 시스템이 각자 끄지 않는다.
            if (inputReader != null)
                inputReader.Blocked = mode != GameMode.Playing;

            Time.timeScale = mode == GameMode.Paused ? 0f : 1f;
        }

        /// <summary>씬을 바꾼다. 전환 중에는 입력이 차단된다.</summary>
        public void LoadScene(string sceneName)
        {
            if (string.IsNullOrEmpty(sceneName))
            {
                Debug.LogError($"{nameof(GameFlow)}: 씬 이름이 비어 있다.", this);
                return;
            }

            StartCoroutine(LoadRoutine(sceneName));
        }

        IEnumerator LoadRoutine(string sceneName)
        {
            var previous = Mode;
            SetMode(GameMode.Loading);

            var op = SceneManager.LoadSceneAsync(sceneName);
            while (op != null && !op.isDone) yield return null;

            SetMode(previous == GameMode.Loading ? GameMode.Playing : previous);
        }
    }
}
