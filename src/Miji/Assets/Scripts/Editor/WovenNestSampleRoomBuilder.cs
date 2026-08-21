using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Miji.EditorTools
{
    /// <summary>
    /// WovenNest 타일셋(Codex 제작)을 실제 Tilemap으로 배치해 보는 예시 룸 빌더.
    /// 1) 타일 png들로 Tile 에셋을 만들고
    /// 2) Greybox_Movement 씬을 복제해 그레이박스 블록을 걷어낸 뒤
    /// 3) SampleRoom_*.txt 도면대로 4개 타일맵 레이어를 칠한다.
    ///
    /// 도면(txt)만 고쳐 다시 실행하면 방이 새로 그려진다.
    /// </summary>
    public static class WovenNestSampleRoomBuilder
    {
        const string TileFolder = "Assets/Art/Environment/Tiles/WovenNest";
        const string TileAssetFolder = TileFolder + "/Tiles";
        const string LayoutFolder = TileFolder + "/SampleRoom";
        const string SourceScene = "Assets/Scenes/Greybox/Greybox_Movement.unity";
        const string TargetScene = "Assets/Scenes/Greybox/Greybox_WovenNest.unity";

        // TagManager: Ground — PlayerMotor.groundLayers(m_Bits 64)와 맞춘다
        const int GroundLayer = 6;

        // 도면 좌하단이 놓일 타일 좌표. 셀 0.5u 이므로 (-22,-8) = 월드 (-11,-4).
        // 바닥 윗면이 월드 y=0 이 되도록 맞춘 값이다(그레이박스와 동일한 지면 높이).
        static readonly Vector3Int Origin = new Vector3Int(-22, -8, 0);

        // 그레이박스 검증용 블록들 — 타일 룸에서는 지형이 대체한다
        static readonly string[] GreyboxObjects =
        {
            "Ground_Main", "Surface", "Ledge_Low", "Ledge_Mid", "Ledge_TooHigh", "Wall_Right"
        };

        [MenuItem("Miji/Tilemap/Woven Nest 예시 룸 빌드")]
        public static void Build()
        {
            if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                return;

            if (AssetDatabase.LoadAssetAtPath<SceneAsset>(TargetScene) == null)
            {
                if (!AssetDatabase.CopyAsset(SourceScene, TargetScene))
                {
                    Debug.LogError("씬 복제 실패: " + SourceScene + " -> " + TargetScene);
                    return;
                }
            }

            var scene = EditorSceneManager.OpenScene(TargetScene, OpenSceneMode.Single);

            // ★ 순서 주의: 씬을 연 뒤에 에셋을 읽는다.
            //   OpenScene은 미사용 에셋을 언로드하므로, 먼저 읽어둔 Tile은 여기서 전부 죽는다
            //   (C# 참조는 남고 네이티브 객체만 파괴돼 SetTile이 조용히 무시된다 — 반나절 짜리 함정).
            var tiles = EnsureTileAssets();
            var terrain = ReadLayout("SampleRoom_Terrain.txt");
            var deco = ReadLayout("SampleRoom_Deco.txt");
            if (terrain == null || deco == null)
                return;

            foreach (var root in scene.GetRootGameObjects())
            {
                if (System.Array.IndexOf(GreyboxObjects, root.name) >= 0 || root.name == "TileRoom")
                    Object.DestroyImmediate(root);
            }

            var room = new GameObject("TileRoom");
            var grid = room.AddComponent<Grid>();
            grid.cellSize = new Vector3(0.5f, 0.5f, 0f); // 16px 타일 / PPU 32

            // ★ 뒷벽은 만들되, 패럴랙스 스택을 쓰는 방에서는 WovenNestParallaxBuilder 가 이 렌더러를 끈다.
            //   방을 다시 빌드하면 새 뒷벽이 켜진 채로 생기므로 「패럴랙스 배경 구성」도 다시 돌려야 한다.
            var backWall = CreateLayer(room, "Tilemap_BackWall", -60, false);
            var terrainMap = CreateLayer(room, "Tilemap_Terrain", -10, true);
            var platformMap = CreateLayer(room, "Tilemap_Platforms", -9, true);
            var decoMap = CreateLayer(room, "Tilemap_Deco", -5, false);

            int painted = PaintTerrain(tiles, terrain, terrainMap, platformMap, backWall);
            PaintDeco(tiles, deco, decoMap);

            terrainMap.GetComponent<CompositeCollider2D>().GenerateGeometry();
            platformMap.GetComponent<CompositeCollider2D>().GenerateGeometry();

            PlaceActors(scene);

            EditorSceneManager.MarkSceneDirty(scene);
            EditorSceneManager.SaveScene(scene, TargetScene);
            Debug.Log("[WovenNest] 예시 룸 완성 -> " + TargetScene
                + " (타일 " + tiles.Count + "종, 칸 " + painted + "개)");
        }

        // 타일 에셋 ------------------------------------------------

        static Dictionary<string, TileBase> EnsureTileAssets()
        {
            if (!AssetDatabase.IsValidFolder(TileAssetFolder))
                AssetDatabase.CreateFolder(TileFolder, "Tiles");

            var paths = new Dictionary<string, string>();
            foreach (var path in Directory.GetFiles(TileFolder, "*.png"))
            {
                var file = Path.GetFileNameWithoutExtension(path);
                if (file.EndsWith("_Atlas") || file.Contains("Preview"))
                    continue;

                var key = file.Replace("Tile_WovenNest_", "");
                var assetPath = TileAssetFolder + "/T_" + key + ".asset";
                paths[key] = assetPath;

                var tile = AssetDatabase.LoadAssetAtPath<Tile>(assetPath);
                if (tile == null)
                {
                    tile = ScriptableObject.CreateInstance<Tile>();
                    AssetDatabase.CreateAsset(tile, assetPath);
                }

                tile.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(TileFolder + "/" + file + ".png");
                // 지형은 셀 전체 충돌(그림 실루엣이 아니라 격자) — 잔풀·뿌리 끝에 걸리지 않게
                tile.colliderType = IsSolid(key) ? Tile.ColliderType.Grid : Tile.ColliderType.None;
                EditorUtility.SetDirty(tile);
            }

            AssetDatabase.SaveAssets();

            // ★ SaveAssets가 방금 만든 에셋을 재임포트하면서 위에서 들고 있던 인스턴스를 죽인다.
            //   반드시 저장 뒤에 디스크에서 다시 읽어야 SetTile이 먹는다.
            var result = new Dictionary<string, TileBase>();
            foreach (var pair in paths)
            {
                var tile = AssetDatabase.LoadAssetAtPath<Tile>(pair.Value);
                if (tile == null)
                    Debug.LogError("타일 에셋을 다시 읽지 못했다: " + pair.Value);
                else
                    result[pair.Key] = tile;
            }

            return result;
        }

        static bool IsSolid(string key)
        {
            return key.StartsWith("Ground") || key.StartsWith("NestWall")
                || key.StartsWith("Edge") || key.StartsWith("RootBridge");
        }

        // 레이어 ---------------------------------------------------

        static Tilemap CreateLayer(GameObject parent, string name, int order, bool collide)
        {
            var go = new GameObject(name);
            go.transform.SetParent(parent.transform, false);
            var map = go.AddComponent<Tilemap>();
            var tilemapRenderer = go.AddComponent<TilemapRenderer>();
            tilemapRenderer.sortingOrder = order;

            if (collide)
            {
                go.layer = GroundLayer;
                var body = go.AddComponent<Rigidbody2D>();
                body.bodyType = RigidbodyType2D.Static;
                var composite = go.AddComponent<CompositeCollider2D>();
                composite.geometryType = CompositeCollider2D.GeometryType.Polygons;
                var tileCollider = go.AddComponent<TilemapCollider2D>();
                tileCollider.compositeOperation = Collider2D.CompositeOperation.Merge;
            }

            return map;
        }

        // 칠하기 ---------------------------------------------------

        static int PaintTerrain(Dictionary<string, TileBase> tiles, string[] rows,
            Tilemap terrain, Tilemap platforms, Tilemap backWall)
        {
            int painted = 0;
            for (int r = 0; r < rows.Length; r++)
            {
                var line = rows[r];
                for (int c = 0; c < line.Length; c++)
                {
                    var pos = CellOf(rows.Length, r, c);
                    var ch = line[c];

                    switch (ch)
                    {
                        case '#': terrain.SetTile(pos, Pick(tiles, pos, "GroundFill_A", "GroundFill_B", "GroundFill_C")); break;
                        case 'T': terrain.SetTile(pos, Pick(tiles, pos, "GroundTop_A", "GroundTop_B", "GroundTop_C")); break;
                        case 'N': terrain.SetTile(pos, Pick(tiles, pos, "NestWall_A", "NestWall_B")); break;
                        case '<': terrain.SetTile(pos, tiles["EdgeLeft"]); break;
                        case '>': terrain.SetTile(pos, tiles["EdgeRight"]); break;
                        case '[': platforms.SetTile(pos, tiles["RootBridge_Left"]); break;
                        case ']': platforms.SetTile(pos, tiles["RootBridge_Right"]); break;
                        case 'b': platforms.SetTile(pos, Pick(tiles, pos, "RootBridge_A", "RootBridge_B")); break;
                    }

                    // 뒷벽은 「빈 칸」이 아니라 「막히지 않은 칸」에 깐다.
                    // 뿌리다리는 판자 사이가 비어 있어서 뒷벽이 없으면 패럴랙스 배경이 그대로 새어 보인다.
                    bool opaque = ch == '#' || ch == 'T' || ch == 'N' || ch == '<' || ch == '>';
                    bool interior = r >= 2 && r <= rows.Length - 8 && c >= 2 && c <= line.Length - 3;
                    if (!opaque && interior)
                        backWall.SetTile(pos, Pick(tiles, pos, "BackWall_A", "BackWall_B", "BackWall_C"));

                    if (ch != '.' || interior)
                        painted++;
                }
            }

            return painted;
        }

        static void PaintDeco(Dictionary<string, TileBase> tiles, string[] rows, Tilemap deco)
        {
            for (int r = 0; r < rows.Length; r++)
            {
                var line = rows[r];
                for (int c = 0; c < line.Length; c++)
                {
                    var pos = CellOf(rows.Length, r, c);
                    switch (line[c])
                    {
                        case 'u': deco.SetTile(pos, Pick(tiles, pos, "UndersideRoots_A", "UndersideRoots_B", "UndersideRoots_C")); break;
                        case 'v': deco.SetTile(pos, Pick(tiles, pos, "VineOverlay_A", "VineOverlay_B", "VineOverlay_C")); break;
                        case 'l': deco.SetTile(pos, tiles["HangingLanternOverlay"]); break;
                        case 'c': deco.SetTile(pos, tiles["CyanLanternOverlay"]); break;
                        case 'p': deco.SetTile(pos, Pick(tiles, pos, "PillarRoot", "PillarRoot_B")); break;
                        case 'm': deco.SetTile(pos, tiles["MachineStub"]); break;
                        case '(': deco.SetTile(pos, tiles["RootArch_Left"]); break;
                        case '=': deco.SetTile(pos, tiles["RootArch_Mid"]); break;
                        case ')': deco.SetTile(pos, tiles["RootArch_Right"]); break;
                    }
                }
            }
        }

        static Vector3Int CellOf(int rowCount, int row, int col)
        {
            return new Vector3Int(Origin.x + col, Origin.y + (rowCount - 1 - row), 0);
        }

        /// <summary>위치 해시로 변종을 고른다 — 실행할 때마다 무늬가 바뀌지 않도록.</summary>
        static TileBase Pick(Dictionary<string, TileBase> tiles, Vector3Int pos, params string[] keys)
        {
            int h = (pos.x * 73856093) ^ (pos.y * 19349663);
            return tiles[keys[Mathf.Abs(h) % keys.Length]];
        }

        // 배우 배치 -------------------------------------------------

        static void PlaceActors(UnityEngine.SceneManagement.Scene scene)
        {
            foreach (var root in scene.GetRootGameObjects())
            {
                switch (root.name)
                {
                    case "Player_A": root.transform.position = new Vector3(-9.5f, 1f, 0f); break;
                    case "Companion_B": root.transform.position = new Vector3(-10.3f, 1f, 0f); break;
                    case "Background_WovenNest": root.transform.position = WovenNestParallaxBuilder.BackgroundAnchor; break;
                    case "Main Camera": root.transform.position = new Vector3(0f, 2f, -10f); break;
                }
            }
        }

        static string[] ReadLayout(string file)
        {
            var asset = AssetDatabase.LoadAssetAtPath<TextAsset>(LayoutFolder + "/" + file);
            if (asset == null)
            {
                Debug.LogError("도면을 찾지 못했다: " + LayoutFolder + "/" + file);
                return null;
            }

            return asset.text.Replace("\r", "").TrimEnd('\n').Split('\n');
        }
    }
}
