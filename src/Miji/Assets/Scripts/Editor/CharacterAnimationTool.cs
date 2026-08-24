using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace Miji.EditorTools
{
    /// <summary>
    /// A·B 캐릭터 애니메이션을 <b>원클릭으로 반입</b>하는 에디터 창.
    ///
    /// 여태 손으로 하던 공정 — ① 프레임 PNG를 Sprites 폴더에 복사 ② .meta 를
    /// 64px 단일 스프라이트로 재작성 ③ .anim 클립의 프레임 참조 갱신 — 을 한 번에 한다.
    /// 마스터 프레임은 보통 리포 밖(<c>docs/art/assets/…</c>)에 있으므로 <b>외부 폴더</b>를
    /// 소스로 받는다.
    ///
    /// ★ 클립은 <b>있으면 제자리 갱신</b>한다(GUID 보존) — 애니메이터 컨트롤러의 상태가
    ///   그대로 이 클립을 가리키므로 컨트롤러를 다시 배선할 필요가 없다.
    /// ★ 스프라이트도 <b>같은 파일명이면 GUID가 유지</b>된다 — 클립·씬의 기존 참조가
    ///   자동으로 새 그림을 가리킨다(idle/run 반입 때 쓰던 그 원리).
    ///
    /// 메뉴: <c>Miji ▸ Animation ▸ Character Animation Tool</c>
    /// </summary>
    public class CharacterAnimationTool : EditorWindow
    {
        enum Character { A, B }

        Character character = Character.A;
        string sourceFolder = "";
        string framePrefix = "";     // 비우면 폴더의 모든 png. "A_run" 처럼 주면 그것만
        string clipName = "";        // 예: A_Run  (비우면 클립 생성 생략, 프레임 임포트만)
        int fps = 12;
        bool loop = true;
        float pixelsPerUnit = 64f;
        bool buildClip = true;

        Vector2 scroll;
        readonly List<string> lastReport = new List<string>();
        List<string> scannedFrames = new List<string>();

        const string PrefKeyPrefix = "Miji.CharAnimTool.";

        [MenuItem("Miji/Animation/Character Animation Tool")]
        static void Open()
        {
            var win = GetWindow<CharacterAnimationTool>("A·B 애니 반입");
            win.minSize = new Vector2(460, 520);
        }

        void OnEnable()
        {
            sourceFolder = EditorPrefs.GetString(PrefKeyPrefix + "src", "");
            framePrefix = EditorPrefs.GetString(PrefKeyPrefix + "prefix", "");
        }

        void OnGUI()
        {
            scroll = EditorGUILayout.BeginScrollView(scroll);

            EditorGUILayout.LabelField("캐릭터 애니메이션 반입", EditorStyles.boldLabel);
            EditorGUILayout.HelpBox(
                "마스터 프레임 폴더를 골라 프레임을 캐릭터 Sprites 로 반입하고, 필요하면 .anim 클립까지 갱신합니다.\n" +
                "같은 파일명은 GUID가 유지되어 기존 클립·씬 참조가 자동으로 새 그림을 가리킵니다.",
                MessageType.Info);

            EditorGUILayout.Space();
            character = (Character)EditorGUILayout.EnumPopup("캐릭터", character);
            EditorGUILayout.LabelField("→ 대상", SpritesFolder(character));

            EditorGUILayout.Space();
            using (new EditorGUILayout.HorizontalScope())
            {
                sourceFolder = EditorGUILayout.TextField("소스 폴더(외부 가능)", sourceFolder);
                if (GUILayout.Button("찾기", GUILayout.Width(60)))
                {
                    string picked = EditorUtility.OpenFolderPanel("프레임 PNG 폴더 선택", DefaultBrowseRoot(), "");
                    if (!string.IsNullOrEmpty(picked)) { sourceFolder = picked; ScanFrames(); }
                }
            }
            framePrefix = EditorGUILayout.TextField(new GUIContent("프레임 접두어(선택)", "예: A_run — 비우면 폴더의 모든 png"), framePrefix);

            EditorGUILayout.Space();
            pixelsPerUnit = EditorGUILayout.FloatField(new GUIContent("PPU", "A·B 모두 64. 32px 자산이면 32."), pixelsPerUnit);

            EditorGUILayout.Space();
            buildClip = EditorGUILayout.ToggleLeft("애니메이션 클립도 만들기/갱신", buildClip);
            using (new EditorGUI.DisabledScope(!buildClip))
            {
                clipName = EditorGUILayout.TextField(new GUIContent("클립 이름", "예: A_Run — Animations/<이름>.anim"), clipName);
                fps = EditorGUILayout.IntField("FPS", Mathf.Max(1, fps));
                loop = EditorGUILayout.Toggle("루프", loop);
            }

            EditorGUILayout.Space();
            using (new EditorGUILayout.HorizontalScope())
            {
                if (GUILayout.Button("프레임 스캔")) ScanFrames();
                using (new EditorGUI.DisabledScope(scannedFrames.Count == 0))
                    if (GUILayout.Button("반입 실행", GUILayout.Height(24))) Apply();
            }

            if (scannedFrames.Count > 0)
            {
                EditorGUILayout.Space();
                EditorGUILayout.LabelField($"감지된 프레임 {scannedFrames.Count}개 (이 순서로 반입)", EditorStyles.boldLabel);
                for (int i = 0; i < scannedFrames.Count; i++)
                    EditorGUILayout.LabelField($"  [{i}] {Path.GetFileName(scannedFrames[i])}");
            }

            if (lastReport.Count > 0)
            {
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("결과", EditorStyles.boldLabel);
                foreach (var line in lastReport) EditorGUILayout.LabelField("  " + line);
            }

            EditorGUILayout.EndScrollView();
        }

        // ── 경로 헬퍼 ──────────────────────────────────────────────

        static string CharDir(Character c) => "Assets/Art/Characters/" + c;
        static string SpritesFolder(Character c) => CharDir(c) + "/Sprites";
        static string AnimationsFolder(Character c) => CharDir(c) + "/Animations";

        string DefaultBrowseRoot()
        {
            if (!string.IsNullOrEmpty(sourceFolder) && Directory.Exists(sourceFolder)) return sourceFolder;
            // 프로젝트 루트(…/src/Miji)에서 두 단계 위가 리포 루트. 마스터는 docs/art/assets 아래.
            string proj = Directory.GetParent(Application.dataPath)?.FullName ?? "";
            string repo = Directory.GetParent(Directory.GetParent(proj)?.FullName ?? proj)?.FullName ?? proj;
            string guess = Path.Combine(repo, "docs", "art", "assets");
            return Directory.Exists(guess) ? guess : proj;
        }

        // ── 스캔 ──────────────────────────────────────────────────

        void ScanFrames()
        {
            scannedFrames = CollectFrames();
            EditorPrefs.SetString(PrefKeyPrefix + "src", sourceFolder);
            EditorPrefs.SetString(PrefKeyPrefix + "prefix", framePrefix);
            lastReport.Clear();
            if (scannedFrames.Count == 0)
                lastReport.Add("⚠ 소스 폴더에서 조건에 맞는 png를 못 찾았습니다.");
        }

        List<string> CollectFrames()
        {
            var result = new List<string>();
            if (string.IsNullOrEmpty(sourceFolder) || !Directory.Exists(sourceFolder)) return result;

            var files = Directory.GetFiles(sourceFolder, "*.png", SearchOption.TopDirectoryOnly)
                .Where(f => string.IsNullOrEmpty(framePrefix) ||
                            Path.GetFileName(f).StartsWith(framePrefix, System.StringComparison.OrdinalIgnoreCase))
                // 시트·프리뷰 파일은 프레임이 아니다
                .Where(f => !Path.GetFileNameWithoutExtension(f).EndsWith("_frames") &&
                            !Path.GetFileNameWithoutExtension(f).EndsWith("_preview"));

            // 파일명 끝의 숫자로 자연 정렬 (A_run_0 … A_run_10)
            return files.OrderBy(TrailingNumber).ThenBy(f => f).ToList();
        }

        static int TrailingNumber(string path)
        {
            var m = Regex.Match(Path.GetFileNameWithoutExtension(path), @"(\d+)$");
            return m.Success ? int.Parse(m.Value) : int.MaxValue;
        }

        // ── 반입 ──────────────────────────────────────────────────

        void Apply()
        {
            var frames = scannedFrames;
            if (frames.Count == 0) { ScanFrames(); frames = scannedFrames; }
            if (frames.Count == 0) return;

            lastReport.Clear();
            string spritesDir = SpritesFolder(character);
            Directory.CreateDirectory(spritesDir);

            var spritePaths = new List<string>();
            try
            {
                AssetDatabase.StartAssetEditing();
                foreach (var srcPath in frames)
                {
                    string fileName = Path.GetFileName(srcPath);
                    string destAsset = spritesDir + "/" + fileName;                 // Assets/… 상대경로
                    string destAbs = Path.Combine(Application.dataPath, destAsset.Substring("Assets/".Length));

                    File.Copy(srcPath, destAbs, overwrite: true);
                    AssetDatabase.ImportAsset(destAsset, ImportAssetOptions.ForceUpdate);
                    ConfigureSprite(destAsset);
                    spritePaths.Add(destAsset);
                }
            }
            finally
            {
                AssetDatabase.StopAssetEditing();
                AssetDatabase.Refresh();
            }

            // 임포트 설정이 실제로 반영된 뒤 스프라이트 로드
            var sprites = spritePaths.Select(AssetDatabase.LoadAssetAtPath<Sprite>).ToList();
            int ok = sprites.Count(s => s != null);
            lastReport.Add($"프레임 {ok}/{spritePaths.Count} 반입 (PPU {pixelsPerUnit:0}, Point, 압축 없음)");

            if (buildClip && !string.IsNullOrEmpty(clipName))
            {
                if (sprites.Any(s => s == null))
                    lastReport.Add("⚠ 일부 스프라이트 로드 실패 — 클립을 만들지 않았습니다.");
                else
                    BuildOrUpdateClip(sprites);
            }
            else if (buildClip)
            {
                lastReport.Add("클립 이름이 비어 프레임만 반입했습니다.");
            }

            AssetDatabase.SaveAssets();
            Repaint();
        }

        void ConfigureSprite(string assetPath)
        {
            if (!(AssetImporter.GetAtPath(assetPath) is TextureImporter ti)) return;
            ti.textureType = TextureImporterType.Sprite;
            ti.spriteImportMode = SpriteImportMode.Single;   // 단일 스프라이트 = 풀 프레임 + 중심 피벗
            ti.spritePixelsPerUnit = pixelsPerUnit;
            ti.filterMode = FilterMode.Point;                // 픽셀 아트 — 블러 금지
            ti.mipmapEnabled = false;
            ti.alphaIsTransparency = true;
            ti.wrapMode = TextureWrapMode.Clamp;
            var settings = new TextureImporterPlatformSettings
            {
                overridden = true,
                format = TextureImporterFormat.RGBA32,       // 무압축 — 색 뭉갬 방지
                textureCompression = TextureImporterCompression.Uncompressed,
            };
            ti.SetPlatformTextureSettings(settings);
            ti.SaveAndReimport();
        }

        void BuildOrUpdateClip(List<Sprite> sprites)
        {
            string animDir = AnimationsFolder(character);
            Directory.CreateDirectory(animDir);
            string clipPath = animDir + "/" + clipName + ".anim";

            var clip = AssetDatabase.LoadAssetAtPath<AnimationClip>(clipPath);
            bool isNew = clip == null;
            if (isNew) clip = new AnimationClip { name = clipName };

            clip.frameRate = fps;

            var binding = EditorCurveBinding.PPtrCurve("", typeof(SpriteRenderer), "m_Sprite");
            var keys = new ObjectReferenceKeyframe[sprites.Count];
            for (int i = 0; i < sprites.Count; i++)
                keys[i] = new ObjectReferenceKeyframe { time = i / (float)fps, value = sprites[i] };
            AnimationUtility.SetObjectReferenceCurve(clip, binding, keys);

            var clipSettings = AnimationUtility.GetAnimationClipSettings(clip);
            clipSettings.loopTime = loop;
            AnimationUtility.SetAnimationClipSettings(clip, clipSettings);

            if (isNew) AssetDatabase.CreateAsset(clip, clipPath);
            else EditorUtility.SetDirty(clip);

            lastReport.Add($"{(isNew ? "클립 생성" : "클립 갱신")}: {clipName}.anim — {sprites.Count}프레임, {fps}fps, 루프 {(loop ? "O" : "X")}");
            if (isNew)
                lastReport.Add("※ 새 클립은 애니메이터 컨트롤러 상태에 직접 연결해야 합니다(기존 클립 갱신이면 불필요).");
        }
    }
}
