using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Tilemaps;
using Miji.Gameplay.World;

namespace Miji.EditorTools
{
    /// <summary>
    /// WovenNest 배경을 「한 장짜리 스프라이트」에서 <b>7장 패럴랙스 스택</b>으로 갈아끼우는 빌더.
    ///
    /// 소스는 Codex(sprite-gen) layer pass 01. 정렬순서·추종계수는
    /// docs/art/assets/sprite-gen-runs/woven-nest-layer-pass-01/layer-manifest.json 값 그대로다.
    /// 고칠 때는 매니페스트와 아래 표를 <b>같이</b> 고친다.
    ///
    /// ★ 이 빌더는 Tilemap_BackWall 렌더러를 끈다.
    ///   뒷벽 타일맵은 「패럴랙스가 새어 보이는 것」을 막으려고 깔았던 것인데,
    ///   이제 새어 보이는 쪽이 배경이다. 불투명 뒷벽(order -60)을 켜두면
    ///   01~04 레이어가 통째로 가려져 방이 다시 평평해진다(실제로 그 상태였다).
    /// </summary>
    public static class WovenNestParallaxBuilder
    {
        const string TargetScene = "Greybox_WovenNest";
        const string LayerFolder = "Assets/Art/Environment/Backgrounds/WovenNest/Layers";
        const string RootName = "Background_WovenNest";
        const string BackWallName = "Tilemap_BackWall";

        /// <summary>
        /// 스택 전체의 기준점. 캔버스 688x384 / PPU 32 = 21.5 x 12u 이고
        /// 카메라(ortho 6, 16:9)가 21.33 x 12u 라서 <b>배율 1에서 캔버스 = 화면</b>이 된다.
        /// y=2 는 카메라 y와 같은 값 — 즉 그림 한 장이 화면에 1:1로 얹힌다.
        /// </summary>
        public static readonly Vector3 BackgroundAnchor = new Vector3(0f, 2f, 0f);

        /// <summary>
        /// 1을 벗어나지 않는다. 1.5로 늘리면 배경 픽셀이 타일(16px)보다 굵어져
        /// 한 화면에 픽셀 밀도가 두 종류로 보인다(이전 한 장짜리 배경이 1.5였다).
        /// </summary>
        const float LayerScale = 1f;

        struct LayerDef
        {
            public string File;      // png 파일명(확장자 제외)
            public string Node;      // 씬 하이라키 이름
            public int Order;        // sorting order
            public float Follow;     // ParallaxLayer.followFactor
            public float Tint;       // 명도 배수 — 원경일수록 어둡게 눌러 플레이 레인을 띄운다
            public bool Active;      // 꺼둔 채로 태어나는 레이어

            public LayerDef(string file, string node, int order, float follow, float tint, bool active = true)
            {
                File = file; Node = node; Order = order; Follow = follow; Tint = tint; Active = active;
            }
        }

        // 원경(추종 1에 가까움) → 근경. 참조 지형: Terrain -10 / Platforms -9 / Deco -5 / Player 10.
        static readonly LayerDef[] Layers =
        {
            new LayerDef("BG_WovenNest_01_FarFog",           "L01_FarFog",           -90, 0.96f, 0.50f),
            new LayerDef("BG_WovenNest_02_FarCanopy",        "L02_FarCanopy",        -80, 0.92f, 0.52f),
            new LayerDef("BG_WovenNest_03_MidRoots",         "L03_MidRoots",         -70, 0.86f, 0.58f),
            new LayerDef("BG_WovenNest_04_BackArchitecture", "L04_BackArchitecture", -62, 0.80f, 0.60f),
            new LayerDef("BG_WovenNest_05_HangingVines",     "L05_HangingVines",     -45, 0.72f, 0.70f),

            // ⚠️ 기본 off. pass 01의 props 레이어는 「소품 시트」에 가깝다 —
            //   등불이 낱개로 균등 배열돼 있고 크기가 A(1u)의 2~3배라, 켜면
            //   플레이 레인 한복판에 거대한 등불이 줄줄이 걸린다. 스크린샷으로 확인함.
            //   pass 02에서 작은 덩어리로 쪼개 다시 뽑을 때까지 꺼둔다(ART_LOG 2026-08-21 「next direction」).
            new LayerDef("BG_WovenNest_06_PropsLanterns",    "L06_PropsLanterns",    -35, 0.65f, 0.72f, false),

            // 캔버스 아래쪽 뿌리 띠는 월드 y<0 (바닥 타일 뒤)에 떨어져 거의 안 보인다.
            // 이 레이어만 위로 올리면 걷는 레인을 가로로 덮어버려서(테스트함) 그대로 둔다.
            new LayerDef("BG_WovenNest_07_GroundDressings",  "L07_GroundDressings",  -15, 0.55f, 0.85f),
        };

        [MenuItem("Miji/Background/Woven Nest 패럴랙스 배경 구성")]
        public static void Build()
        {
            var scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
            if (scene.name != TargetScene)
            {
                Debug.LogError("[WovenNest/BG] 활성 씬이 " + TargetScene + " 이 아니다 (현재: " + scene.name
                    + "). 씬을 연 뒤 다시 실행한다 — 열려 있는 씬의 미저장 변경을 말없이 날리지 않으려고 자동으로 열지 않는다.");
                return;
            }

            // 기존 배경 루트(한 장짜리든 이전 스택이든) 통째로 걷어낸다 — 몇 번을 돌려도 같은 결과가 되도록
            foreach (var root in scene.GetRootGameObjects())
                if (root.name == RootName)
                    Object.DestroyImmediate(root);

            var parent = new GameObject(RootName);
            parent.transform.position = BackgroundAnchor;

            int built = 0;
            foreach (var def in Layers)
            {
                var path = LayerFolder + "/" + def.File + ".png";
                var sprite = AssetDatabase.LoadAssetAtPath<Sprite>(path);
                if (sprite == null)
                {
                    Debug.LogError("[WovenNest/BG] 스프라이트를 찾지 못했다: " + path);
                    continue;
                }

                var go = new GameObject(def.Node);
                go.transform.SetParent(parent.transform, false);
                go.transform.localPosition = Vector3.zero;
                go.transform.localScale = new Vector3(LayerScale, LayerScale, 1f);

                var renderer = go.AddComponent<SpriteRenderer>();
                renderer.sprite = sprite;
                renderer.sortingOrder = def.Order;
                renderer.color = DepthTint(def.Tint);

                // followFactor 는 [SerializeField] private 이다.
                // 에디터 편의 때문에 런타임 API를 public 으로 벌리지 않고 SerializedObject 로 넣는다.
                var parallax = go.AddComponent<ParallaxLayer>();
                var so = new SerializedObject(parallax);
                so.FindProperty("followFactor").floatValue = def.Follow;
                so.ApplyModifiedPropertiesWithoutUndo();

                go.SetActive(def.Active);
                built++;
            }

            int walls = DisableBackWall();

            EditorSceneManager.MarkSceneDirty(scene);
            EditorSceneManager.SaveScene(scene);
            Debug.Log("[WovenNest/BG] 패럴랙스 스택 " + built + "장 구성 (기준점 " + BackgroundAnchor
                + ", 배율 " + LayerScale + "). 뒷벽 타일맵 렌더러 " + walls + "개 off.");
        }

        /// <summary>깊이 틴트. 명도를 누르면서 아주 살짝 푸르게 — 멀수록 공기가 낀 것처럼.</summary>
        static Color DepthTint(float t)
        {
            return new Color(t, Mathf.Min(1f, t * 1.02f), Mathf.Min(1f, t * 1.08f), 1f);
        }

        /// <summary>불투명 뒷벽을 끈다. 오브젝트는 남긴다 — 되돌리려면 렌더러만 다시 켜면 된다.</summary>
        static int DisableBackWall()
        {
            int count = 0;
            foreach (var renderer in Object.FindObjectsByType<TilemapRenderer>(FindObjectsInactive.Include, FindObjectsSortMode.None))
            {
                if (renderer.gameObject.name != BackWallName || !renderer.enabled)
                    continue;
                renderer.enabled = false;
                EditorUtility.SetDirty(renderer);
                count++;
            }

            return count;
        }
    }
}
