using Miji.Core.Input;
using UnityEngine;

namespace Miji.Gameplay.Bootstrap
{
    /// <summary>
    /// 시작 시 조작권을 넘긴다.
    ///
    /// 씬에 「누가 조작되는가」를 하드코딩하지 않기 위한 얇은 배선 컴포넌트다.
    /// 2장의 조작권 인계(여산 → 열하나)도 결국 같은 <see cref="InputRouter.Possess"/>를
    /// 다른 시점에 부르는 것이라, 그 기능이 여기서 미리 검증된다.
    /// </summary>
    public class StartupPossession : MonoBehaviour
    {
        [SerializeField] InputRouter router;
        [Tooltip("IPossessable을 구현한 컴포넌트 (예: PlayerController)")]
        [SerializeField] MonoBehaviour body;

        void Start()
        {
            if (router == null || body == null)
            {
                Debug.LogError($"{nameof(StartupPossession)}: router 또는 body가 비어 있다.", this);
                return;
            }

            if (body is not IPossessable possessable)
            {
                Debug.LogError($"{nameof(StartupPossession)}: {body.GetType().Name}은 IPossessable이 아니다.", this);
                return;
            }

            router.Possess(possessable);
        }
    }
}
