using UnityEngine;

namespace Miji.Core.Input
{
    /// <summary>
    /// 읽은 의도를 「지금 조작되는 몸」에 흘려보낸다.
    ///
    /// 입력을 읽는 일(<see cref="InputReader"/>)과 누가 조작되는지를 나눠둔 이유:
    /// 2장에서 조작권이 여산 → 열하나로 넘어가는 것이 <see cref="Possess"/> 한 줄이 된다.
    /// 몸 쪽 코드는 자기가 조작되는지 여부를 몰라도 된다.
    /// </summary>
    [RequireComponent(typeof(InputReader))]
    public class InputRouter : MonoBehaviour
    {
        InputReader reader;

        /// <summary>지금 조작되는 몸. 없으면 아무 데도 안 보낸다.</summary>
        public IPossessable Current { get; private set; }

        void Awake() => reader = GetComponent<InputReader>();

        /// <summary>조작 대상을 바꾼다. 직전 대상에게는 의도 없음을 먹여 관성을 끊는다.</summary>
        public void Possess(IPossessable body)
        {
            if (ReferenceEquals(Current, body)) return;

            Current?.SetIntent(InputIntent.None);
            Current = body;
        }

        public void Release() => Possess(null);

        void Update()
        {
            Current?.SetIntent(reader.Current);
        }
    }
}
