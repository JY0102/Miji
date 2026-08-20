using UnityEngine;

namespace Miji.Gameplay.World
{
    /// <summary>
    /// 원경 레이어. 카메라 이동량의 일부만 따라가서 멀리 있는 것처럼 보이게 한다.
    /// followFactor 1 = 카메라에 완전히 붙음(무한 배경), 0 = 월드에 고정(전경과 동일).
    /// 원경일수록 1에 가깝게 둔다.
    /// </summary>
    public class ParallaxLayer : MonoBehaviour
    {
        [Range(0f, 1f)][SerializeField] float followFactor = 0.9f;
        [Tooltip("세로도 따라갈지. 낙하가 깊은 룸이 아니면 켜두는 쪽이 안전하다.")]
        [SerializeField] bool followY = true;

        Transform cam;
        Vector3 camStart;
        Vector3 selfStart;

        void Start()
        {
            cam = Camera.main != null ? Camera.main.transform : null;
            if (cam == null) { enabled = false; return; }
            camStart = cam.position;
            selfStart = transform.position;
        }

        void LateUpdate()
        {
            var delta = cam.position - camStart;
            var offset = new Vector3(delta.x * followFactor, followY ? delta.y * followFactor : 0f, 0f);
            transform.position = selfStart + offset;
        }
    }
}
