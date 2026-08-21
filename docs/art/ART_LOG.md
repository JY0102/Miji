# Art Log — OpenAI Prompt Request History

All previous B-character art direction is superseded by the user-approved B concept sheet provided in the current chat on 2026-08-20.

This project still follows `docs/art/style-guide/STYLE_GUIDE.md`: 32x32 character sprites, dense indie pixel art, 1px dark outline, limited palette, no anti-aliasing, no soft glow, no direct copying of reference game IP.

---

## Active Request

### 2026-08-20 — B character concept reset

- **Purpose**: Lock B to the newly provided concept sheet and clear the older B sprite direction before regenerating the character from scratch.
- **Source reference**: User-provided chat attachment, a 4x4 pixel-art sheet of a small green leaf-eared creature with large glossy dark eyes, rounded belly, short limbs, tiny tail, and simple brown travel-wrap details.
- **Decision**: This B replaces the previous living-creature B direction, including the older `character_02_B_living_creature.png`, `character_B_standalone_pixel_art.png`, cute-variant attempts, and Unity B sprite frames.
- **Status**: Approved as the current B visual canon. Unity B sprite files were removed so new sprites can be generated cleanly. Older document-side B candidate folders remain until the user explicitly confirms deleting non-Unity concept/generation records.

### 2026-08-20 — B ear unification pixel edit

- **Purpose**: User circled B02, B07, and B15 as frames whose ears did not match the rest of the B concept sheet.
- **Method**: Manual pixel edit only; no AI regeneration. Desktop source was not overwritten. The clean 16-frame split set was recomposed, then B02/B07/B15 ear silhouettes were adjusted.
- **Edit memo**: B02 left ear changed to the broader leaf-fan read from the standing frames; B07 sleep pose kept while lower grass-like ear clutter was removed; B15 rear ears broadened to match the main leaf-ear silhouette.
- **Output**:
  - `docs/art/assets/b-current/B_concept_sheet_ears_unified.png`
  - `docs/art/assets/b-current/split-ears-unified/`
  - `docs/art/assets/b-current/B_concept_ears_unified_preview_numbered.png`

### 2026-08-20 — B ear unification redo from `reallast.png`

- **Purpose**: Reapply the ear-only correction using the user's clean final source image `C:/Users/User/Desktop/reallast.png`.
- **Method**: Manual pixel edit only; no AI regeneration. The desktop source was preserved and the project-side unified sheet/split outputs were regenerated.
- **Correction note**: B07 was restored to the clean `reallast.png` sleep frame because the previous edit read like a bitten/cut-out silhouette. Final pixel changes are limited to B02 and B15.
- **B02 face fix**: The B02 left face/body region was restored from `reallast.png` after the broad-ear patch had made the face read collapsed. The ear edit now stays outside the original face silhouette.
- **B02/B15 cleanup**: B02's left leaf-ear silhouette was cleaned up against the stable B01 ear read, and B15's neck-wrap/body join was restored from the preserved pre-ear-edit split frame to remove scarf-area distortion.
- **2026-08-21 follow-up cleanup**: B02's left ear was narrowed against the stable B05 left-ear read while restoring B02's cheek boundary, and B15's central back/head/neck-wrap area was restored from the preserved clean split frame while keeping the sheet composition intact.
- **2026-08-21 second follow-up cleanup**: B02's head/face roundness was restored from the preserved split frame, the extra right-side ear nub was removed so the right side reads as one upper leaf-ear, B07's floating/right-side second-ear read was cleared, and B15's side ear tips/roots were reinforced after the center scarf cleanup.
- **Preview note**: Full-sheet preview is now an unsegmented 3x scale image, with no grid bands, labels, or cell dividers.

### Visual DNA

- Small organic companion creature, cute but not mascot-like.
- Moss/dark olive head and back, lighter muted yellow-green belly and face accents.
- Leaf-like ear fins that create the main silhouette.
- Oversized black eyes with tiny pale highlights; no eyebrows, no detailed mouth, no human facial expression.
- Stubby arms and legs, short tail or leaf-tail read.
- Simple reddish-brown neck wrap, strap, or tiny travel pouch details are allowed, but clothing must stay minimal.
- Base B should not include the old gold crack motif. Save cracks, flowers, or special markings for story-state or special animation frames only.
- Transparent background for all usable sprites.

### Required Sprite Set For Next Generation

| Use | Frames | Notes |
|---|---:|---|
| Front / 3-4 / side / back references | 4 | Keep proportions consistent across views |
| Idle | 4 | Soft breathing, tiny ear-fin bounce, optional blink |
| Walk | 8 | Short-legged trot, stable head/eye shape, leaf ears trailing slightly |
| Sleep | 4-6 | Curled or low resting pose, closed eyes |
| Hurt / surprised | 2-3 | Small recoil, no exaggerated cartoon effects |

### Copy-Paste Prompt

```text
Pixel art sprite sheet, 32x32 pixels per frame, transparent background PNG.

Subject: B, a small organic companion creature for a melancholic metroidvania. Use the attached B concept sheet as the identity reference: a cute moss-green leaf-eared creature with a rounded body, lighter muted yellow-green belly, oversized glossy black eyes with tiny highlights, short limbs, tiny tail, and minimal reddish-brown travel-wrap or strap details.

Style: dense indie pixel art, readable at 32x32, 1px dark outline, limited muted palette, crisp pixel clusters, no anti-aliasing, no gradients except very limited dithering. The mood is quiet, curious, lived-in, and slightly fragile rather than bright comedy.

Animation/request: create [FRAME COUNT] frames for [ANIMATION NAME]. Keep the same body proportions, eye size, ear-fin shape, belly color, and strap placement across every frame. Motion should be subtle and game-ready: no squash-and-stretch extremes, no large pose drift, no extra props unless requested.

Palette: dark moss green, olive green, muted yellow-green belly, deep near-black eye color, tiny pale eye highlights, dark brown outline, subdued reddish-brown strap/wrap.

Do not include: human clothing, weapons, text, scenery, UI, shadows on the ground, eyebrows, detailed mouth, old gold crack motif, flower accessory in the base idle/walk set, extra characters, duplicate body parts, blurry pixels, painterly lighting, soft glow, or anti-aliased edges.
```

### Review Checklist

- [ ] 32x32 frame readability survives at 1x and 2x zoom.
- [ ] Leaf ears, large eyes, belly patch, and small companion silhouette remain consistent.
- [ ] Palette stays muted green/brown and does not become neon or candy-colored.
- [ ] No old B gold-crack motif appears in base sprites.
- [ ] No extra props or clothing are invented.
- [ ] Frame-to-frame body size and foot contact do not jitter.

### 2026-08-20 — Woven Nest tilemap generation

- **Purpose**: Generate a Unity-ready 16x16 tilemap set from the user-provided woven-root village reference.
- **Source reference**: User-provided chat image; same visual direction as `src/Miji/Assets/Art/Environment/Backgrounds/BG_WovenNest.png`.
- **Method**: Authored deterministic pixel-art tile modules, then imported the tile candidate folders through `sprite-gen unpack-atlas --pngs-dir` for curation/run provenance.
- **sprite-gen run**: `docs/art/assets/sprite-gen-runs/woven-nest-tilemap-curation/`
- **Art outputs**:
  - `docs/art/assets/tilemaps/woven-nest/Tile_WovenNest_Atlas.png`
  - `docs/art/assets/tilemaps/woven-nest/Tile_WovenNest_Tileset_Preview_x4.png`
  - `docs/art/assets/tilemaps/woven-nest/WovenNest_Tilemap_Mockup.png`
  - `docs/art/assets/tilemaps/woven-nest/WovenNest_Tilemap_Mockup_x2.png`
  - `docs/art/assets/tilemaps/woven-nest/WovenNest_Tilemap_Manifest.json`
- **Unity outputs**: `src/Miji/Assets/Art/Environment/Tiles/WovenNest/`
- **Tile count**: 31 usable 16x16 tiles plus one 128x64 atlas.
- **Visual DNA**: dark teal forest depth, woven reed/root caps, hanging underside roots, suspended lanterns, nest-wall lattice, root bridge pieces, cyan/amber folk-machine accents.
- **Importer contract**: Unity `.meta` files use PPU 32, Point filtering, no mipmaps, and 16x16 atlas slices.
- **Verification**: `sprite-gen inspect` and `sprite-gen compose-atlas` completed with `ok: true`; inspect warnings are expected because sprite-gen's dHash similarity checks are tuned for animation rows, not varied tile categories.

### 2026-08-21 — Woven Nest 타일셋 인게임 배치 (예시 룸)

- **목적**: Codex가 만든 타일 31종이 실제 Unity Tilemap에서 어떻게 읽히는지 확인.
- **결과물**: `src/Miji/Assets/Scenes/Greybox/Greybox_WovenNest.unity` (Greybox_Movement 복제본, 그레이박스 블록 제거).
- **툴**: `Assets/Scripts/Editor/WovenNestSampleRoomBuilder.cs` (메뉴 `Miji/Tilemap/Woven Nest 예시 룸 빌드`) — png → Tile 에셋 생성 + 도면대로 4레이어 페인트.
- **도면**: `Assets/Art/Environment/Tiles/WovenNest/SampleRoom/SampleRoom_{Terrain,Deco}.txt` (44x26 아스키, 1칸 = 0.5u). 도면만 고쳐 메뉴를 다시 눌러도 된다.
- **인게임 캡처**: `docs/art/assets/tilemaps/woven-nest/WovenNest_SampleRoom_InEngine.png`
- **본 것 (다음 타일 작업 입력)**:
  - 뒷벽과 지반 충전재의 명도·색상이 거의 같아 **바닥이 단단해 보이지 않는다**. 지반을 한 단계 어둡게/따뜻하게 내리는 게 첫 수정.
  - 뒷벽 A/B/C가 세로 줄무늬 한 종류로 수렴해 **44칸 폭에서 벽지처럼 반복**된다. 큰 단위 변주(덩어리·구멍·이끼)가 필요하다.
  - 랜턴 2종은 16px 안에서 사실상 안 보인다 — **Light2D 없이는 장식 효과가 없다**.
  - RootArch 3종은 지면에 놓으면 문이 아니라 **민둥 언덕**으로 읽힌다. 기둥과 세트로 써야 한다.
  - 뿌리다리(판자+아래 뿌리)와 GroundTop의 잔풀은 그대로 잘 읽힌다. **이 둘이 이 타일셋의 정체성.**


### 2026-08-21 — B 포즈·표정 시트 64x64 (higgsfield 경로)

- **목적**: 확정된 B 컨셉(`b-current/split-ears-unified/B_concept_01.png`, 64x64)을 기준으로 **성격이 읽히는 포즈·표정 세트**를 만든다. 인게임 스프라이트가 아니라 방향 확정용 컨셉이다.
- **경로**: 이 세션에는 **PixelLab MCP가 없어서** higgsfield `nano_banana_pro`(1k, 1:1)를 썼다. 레퍼런스는 원본을 x8 니어리스트 확대해 업로드(64px 원본은 모델이 픽셀 구조를 못 읽는다).
- **비용**: 2회 생성 × 2크레딧 = **4크레딧 사용, 잔액 1.35** (무료 플랜). 낱장 생성이 불가능해 **한 장에 9칸 시트**로 뽑고 잘라 쓰는 방식을 택했다.
- **후처리 (재사용 가능)**: 칸을 잘라 **최빈색(mode) 샘플링으로 64x64 다운스케일** + 최빈색 배경 투명화. 평균 다운스케일과 달리 픽셀아트의 평면 색과 1px 아웃라인이 뭉개지지 않는다. 스크립트는 `tools/sheet-to-sprites.ps1`로 일반화해 두었다(`-Cols/-Rows/-Size/-Inset/-Names`).
- **결과물**: `docs/art/assets/b-current/pose-sheet-64/` — 8포즈 64x64 PNG(배경 투명)
  - `01_sit_front` `02_peer_curious` `03_explain_point` `04_beckon_lookback` `05_startled` `06_delighted` `07_asleep` `08_glum_tired`
  - 미리보기: `docs/art/assets/b-current/B_pose_sheet_64_preview_x6.png`
  - ⚠ 원본 생성 시트와 탈락본은 **2026-08-21 정리에서 전부 삭제**했다(사용자 지시). 남은 것은 채택본뿐이다.
- **포즈 선정 근거**: 호기심(02)·설명(03)·재촉(04)은 `CHARACTER_B.md` 3.1~3.2의 성격축을 그대로 그림으로 옮긴 것이고, 잠(07)은 이미 구현된 Sleep 애니와 짝이 맞는다. **금빛 균열은 전 프레임에 없다**(4절 「기본 스프라이트에는 넣지 않는다」 준수).
- **실패·교훈 ★**:
  - **「흰 눈알 금지, 검은 둥근 눈 유지」를 두 번 명시해도 startled는 흰 눈알로 그린다.** 표정의 극단값은 생성으로 밀지 말고 기본 프레임에서 눈만 수작업 수정하는 쪽이 빠르다.
  - **칸 경계에 여백을 두라는 지시가 안 지켜진다** — 측면 착석 포즈는 두 판본 모두 머리 또는 하반신이 프레임 밖으로 잘려 **최종 세트에서 제외**했다. 크레딧이 있으면 포즈당 낱장 생성이 안전하다.
  - **소품 금지를 명시해도 「들여다보기」에는 받침대를 발명한다**(02의 회색 선반). 포즈 해부가 그 위에 얹혀 있어 지우면 자세가 무너지므로 남겼다 — 인게임 전환 시 제거 대상.
  - 처진 귀(08)는 「시든 잎귀」 지시가 **강아지 귀**로 번역됐다. 두 판본 다 같은 실패라 모델의 사전 문제로 본다.
  - ⚠ **한글 주석이 든 .ps1은 반드시 UTF-8 BOM으로 저장해야 한다.** BOM이 없으면 PowerShell 5.1이 시스템 코드페이지(CP949)로 읽어 한글 주석이 깨지고, **깨진 주석이 바로 다음 코드 줄을 삼킨다.** `tools/sheet-to-sprites.ps1`이 이 때문에 전 픽셀을 검게 뽑았다 — 문법 오류가 아니라 조용한 오작동이라 원인 찾기가 오래 걸린다.
- **크기 주의**: 64x64는 스타일 가이드상 **보스 크기**다. 이 세트를 그대로 인게임에 넣으면 PPU 32에서 B가 2유닛이 되어 A(32px, 1유닛)의 두 배가 된다. 인게임용으로 내리려면 32x32 재작업이 별도로 필요하다.

---

### 2026-08-21 (2차) — B 포즈 시트 검수 반영 + 컨셉 폴더 정리

사용자 검수 결과를 반영했다. **추가 생성은 없다**(크레딧 1.35, 1장에 2 필요) — 전부 수작업 픽셀 수정과 파일 정리다.

- **02 눈동자 수정**: 모델이 그린 흰자위 + 검은 동공을 레퍼런스 구조(**검은 구체 + 크림 하이라이트 + 아래쪽 갈색 크레센트**)로 다시 칠했다. 레퍼런스 `B_concept_01`의 눈 픽셀을 그대로 뜯어 색과 배치를 맞췄다.
- **03 입모양 수정**: 벌린 입 + 큰 혀(피카츄로 읽힘)를 **작고 둥근 입 + 혀 한 줄**로 바꿨다. 위쪽 옛 윤곽 잔재까지 지웠다.
- **04·06·09 삭제**(파일 자체를 지웠다). 번호는 재배열하지 않는다(검수 때 쓴 번호를 유지해야 대화가 어긋나지 않는다). 최종 채택은 **01·02·03·05·07·08 6종**.
- **05·08은 1차본으로 되돌렸다.** 사용자가 승인한 것은 1차 시트의 그림이고, 이전 세션에서 내가 2차 교정본으로 바꿔 둔 상태였다. 2차본은 정리 때 삭제했다.
- ★ **새 프롬프트 규칙 확정 → `STYLE_GUIDE.md` 「생성 프롬프트 금지 사항」 신설.** 핵심은 **평면적으로 보이는 각도를 요청하지 않는다**(3/4 앵글 기본). 폐기된 04·09가 그 사례다.

**컨셉 폴더 정리** — `split/`·`split-ears-unified/` 는 같은 16포즈의 두 판본이다. 사용자가 지정한 8종만 남기고 **나머지 8종은 삭제했다**. 파일명에 용도를 박았다.

| 파일 | 용도 |
|---|---|
| `B01_idle` | Idle |
| `B03_run` | 뛰기 |
| `B05_laugh` | 웃기 |
| `B06_sit` | 앉기 |
| `B09_handover` | 퀘스트 완료 / 의뢰 물품 건네주기 |
| `B10_eat` | 밥 |
| `B11_greet` | 인사 |
| `B12_fall` | Fall — ⚠ 입모양 추후 변경 예정 |

폴더 안내는 `docs/art/assets/b-current/README.md` 에 정리했다. 기준 판본은 **`split-ears-unified/`**(잎귀 통일본, 포즈 시트 생성 레퍼런스로 쓴 쪽).


## Cleanup Record

Completed on 2026-08-20:

- `src/Miji/Assets/Art/Characters/B/Sprites/*.png`
- `src/Miji/Assets/Art/Characters/B/Sprites/*.png.meta`

Completed on 2026-08-21 (사용자 지시 — 파일 삭제, 복구본 없음):

- `docs/art/assets/b-current/split/rejected/` (비채택 컨셉 8장)
- `docs/art/assets/b-current/split-ears-unified/rejected/` (비채택 컨셉 8장)
- `docs/art/assets/b-current/pose-sheet-64/rejected/` (탈락 포즈·수정 전 원본 12장)
- `docs/art/assets/b-current/pose-sheet-64/_source-sheets/` (생성 원본 시트 2장)
- `docs/art/assets/b-current/B_concept_*_preview*.png`, `B_concept_sheet_ears_unified.png` (폐기 포즈가 섞인 16종 미리보기 4장)

총 37개. 남은 것은 채택본 22개 + README + 채택본 미리보기 2장뿐이다.

Pending explicit confirmation because these include concept/generation records, not only Unity sprites:

- `docs/art/assets/b-variants/`
- `docs/art/assets/generated-concepts/characters/B/`

`balancer` assets are a separate character group and are intentionally not part of this B cleanup.
[2026-08-21 03:01] README.md 수정됨

### 2026-08-21 (3차) — sp/spe 병합 + Idle 눈 수정

`split`(sp) 과 `split-ears-unified`(spe) 를 **`b-current/poses/` 한 폴더로 병합**했다. 파일명 앞의 `sp`/`spe` 가 출처 표시다 — **귀 모양이 다른 두 판본이므로 출처를 지우면 안 된다.**

| 확정 | 파일 |
|---|---|
| Idle | `sp01_idle.png` ← **눈 수작업 수정 후 편입** |
| 앉기 | `spe01_sit.png` |
| 앉기+슬픔 | `spe06_sit_sad.png` |
| 물음표 | `spe12_question.png` (머리 위 `?` 를 그대로 쓴다) |

보류 5종(`spe03_run`·`spe05_laugh`·`spe09_handover`·`spe10_eat`·`spe11_greet`)은 이름만 유지한 미확정 상태다. **`fall`은 배정된 그림이 없다** — spe12가 question으로 재배정되면서 비었다.

- **sp01 눈 수정**: sp 판본의 눈은 검정이 아니라 **탁한 갈색 덩어리(#262012·#151D06)** 였고 아래 갈색 크레센트도 없었다. `spe01` 구조에 맞춰 **검은 구체 + 크림 하이라이트 + 갈색 크레센트**로 다시 칠했다. 눈 외에는 손대지 않았다(사용자 지시 「눈만」).
- ★ **눈 규격을 문서화했다** — `b-current/README.md` 3절. 기준 파일은 `spe01_sit.png`. 흰자위 분리도, 탁한 갈색 덩어리도 위반이다.
- **삭제**: sp 판본 01을 제외한 7장 + 빈 폴더 2개. 이전 정리(2026-08-21 2차)의 미리보기 `B_kept_poses_preview_x5.png` 도 구조가 바뀌어 폐기하고 `B_poses_preview_x6.png` 로 대체했다.

### 2026-08-21 (4차) — B 포즈 전부 `poses/` 통합 + B01 눈 크기 이식 + 합본 아트워크

- **이름 통일**: `sp`/`spe` 접두어를 버리고 **`B01`~`B15`** 로 재번호했다. `pose-sheet-64/` 도 같은 폴더로 흡수해 **B 포즈는 이제 `b-current/poses/` 한 곳뿐이다**(64x64 PNG, 배경 투명).
- **B01 눈 크기 수정 ★**: 색만 맞춘 3차 수정으로는 부족했다 — sp 판본의 눈은 **7x7** 로 기준(**9x10**)보다 작았다. 손으로 키우는 대신 **`B02_sit`(기준)의 눈 블록을 좌표 오프셋만 맞춰 통째로 이식**했다. 눈 크기까지 규격에 들어간다는 걸 문서화했다(`b-current/README.md` 3절).
  - 이식 방식이 손으로 그리는 것보다 안전하다: 크기·색·하이라이트·크레센트가 자동으로 기준과 동일해진다. 실루엣 밖으로는 칠하지 않도록 알파 검사를 걸었다.
- **합본 아트워크**: `b-current/B_artwork_all.png` — 15포즈를 x6로 배치하고 파일명·용도·확정/보류를 함께 찍었다.
- **남은 불균질 2건**(README에 명시): ① `B01` 만 구 split 판본이라 **귀가 갈라져 있다**(나머지는 잎귀 통일본), ② `B10`~`B15` 는 생성본이라 손그림 9종과 화풍이 미세하게 다르다. 인게임 반입 전에 통일 필요.

---

### 2026-08-21 — Woven Nest background layer pass 01 (sprite-gen)

- **Purpose**: Replace the weak 16x16 tilemap-only read with depth layering: collision/tilemap stays minimal, while background and dressing are split into parallax-ready visual PNG layers.
- **Source reference**: `src/Miji/Assets/Art/Environment/Backgrounds/BG_WovenNest.png`.
- **Method**: `sprite-gen gen --provider codex` with the current Woven Nest image as style/mood reference. Transparent layers were generated on magenta chroma, keyed by sprite-gen, normalized to 688x384, then locally cleaned to remove magenta/purple key remnants.
- **Run folder**: `docs/art/assets/sprite-gen-runs/woven-nest-layer-pass-01/`.
- **Unity outputs**: `src/Miji/Assets/Art/Environment/Backgrounds/WovenNest/Layers/`
  - `BG_WovenNest_01_FarFog.png` — opaque far fog / distant forest.
  - `BG_WovenNest_02_FarCanopy.png` — transparent far canopy.
  - `BG_WovenNest_03_MidRoots.png` — transparent mid root columns.
  - `BG_WovenNest_04_BackArchitecture.png` — transparent nest architecture.
  - `BG_WovenNest_05_HangingVines.png` — transparent top/foreground hanging vines.
  - `BG_WovenNest_06_PropsLanterns.png` — transparent lantern and folk-machine props.
  - `BG_WovenNest_07_GroundDressings.png` — transparent lower foreground roots and moss.
- **Preview outputs**:
  - `docs/art/assets/sprite-gen-runs/woven-nest-layer-pass-01/preview/WovenNest_LayerPass01_CompositePreview.png`
  - `docs/art/assets/sprite-gen-runs/woven-nest-layer-pass-01/preview/WovenNest_LayerPass01_LayerContactSheet.png`
- **Verification**: All Unity PNGs are 688x384. Transparent layers are RGBA with alpha coverage preserved; magenta-like opaque pixel check returned 0 after cleanup. Unity `.meta` files were generated from the existing `BG_WovenNest.png.meta` import contract: PPU 32, Point filter, no mipmaps.
- **Known issue / next direction**: The first pass is much richer but too busy as a full composite. Next pass should make `back_architecture` less central/noisy, split props into smaller moveable clusters, and keep the central play lane darker and calmer. Ground collision should still come from simple tilemaps or invisible physics, not from these visual layers.

**인게임 배치 결과 (2026-08-21, IMPLEMENTATION)** — `IMPL_REGISTRY.md` 6차가 원본. 아트 쪽으로 돌아오는 지적만 여기 적는다.

- ★ **배경이 지금까지 한 번도 화면에 나온 적이 없었다** — 뒷벽 타일맵(불투명, order −60)이 방 내부를 다 덮고 있었다. 즉 `BG_WovenNest.png` 의 품질 평가는 **아직 아무도 실제로 못 한 상태**였다. 이제 뒷벽을 끄고 스택을 깔았다
- ★ **props 레이어는 「레이어」가 아니라 「소품 시트」다** — 등불이 캔버스에 낱개로 균등 배열돼 있어 그대로 얹으면 화면 가로로 등불이 줄줄이 걸린다. 게다가 **등불 하나가 A(32px = 1u)의 2~3배** 크기라 배경이 아니라 전경 오브젝트로 읽힌다. **기본 off 로 두었다**
  - pass 02 요구: ① 소품을 작은 덩어리(2~3개씩)로 쪼개 개별 PNG로, ② 크기를 A 기준으로 명시(등불 ≤ 0.6u), ③ 균등 간격 금지
- ★ **캔버스 12u 중 4u가 지형에 가려진다** — 방의 열린 공간은 월드 y 0~8(8u)인데 캔버스는 12u다. 아래 1.7u(뿌리 띠)와 위 2.3u(캐노피 상단)가 지형 뒤로 들어가 버린다. **pass 02는 8u 개구부를 기준으로 프레이밍**하거나, 룸 높이를 키우는 쪽을 먼저 정해야 한다
- **틴트가 필요했다** — 원본 명도 그대로는 배경 대비가 지형과 같아서 발판이 안 보인다. 인게임에서 원경 0.50 → 근경 0.85 로 눌렀다. **생성 단계에서 원경을 더 어둡게/저채도로 뽑으면 이 보정이 필요 없다**
- **배율 1 고정** — 688x384 / PPU 32 가 카메라 화면(21.33 x 12u)과 1:1로 맞는다. 다음 배경도 **688x384 를 유지**하면 그대로 얹힌다

---

### 2026-08-21 (2차) — B 인게임 반입: 32px 다운스케일 시도 → 폐기, 64px 원화 유지

**발단**: `Companion_B` 의 스프라이트가 비어 있어 게임에서 B가 안 보이는 상태였다(8/20 폴더 재편에서 구 B 스프라이트를 지운 결과). 사용자 요청은 「B를 32x32로 다운스케일, 단 1장, 크레딧 최소화」.

**시도 — `tools/pose-to-sprite32.py` 신설(크레딧 0).** 4가지 방법을 만들어 비교했다.
- `nearest` — 눈 하이라이트가 날아가고 아웃라인이 끊긴다
- `lanczos` + 팔레트 스냅 — 눈은 크게 남지만 **1px 검은 아웃라인이 통째로 사라져** 실루엣이 물러진다
- **최빈색(mode) 블록 샘플링, 동률은 블록 평균에 가까운 색으로** — 평면 색과 눈 규격이 가장 잘 남는다. ★ 동률을 「가장 어두운 색」으로 깨면 아웃라인이 안쪽으로 번져 지저분해진다
- 위 + **아웃라인 복원**(실루엣 경계 픽셀을 팔레트 최암색으로 되돌림) ← 최선. A와 같은 「전둘레 1px 검은선」 규격이 된다

**결과 판정 — 사용자 「32픽셀로 줄이는건 별로네」.** 인게임에 A / B-32px / B-64px@PPU64 셋을 나란히 세워 실측했다.
- **확대(ortho 2.2)**: 다운스케일이 눈 크레센트·귀·아웃라인을 확연히 뭉갠다
- **플레이 배율(ortho 6)**: 차이가 크게 줄지만 여전히 64px 쪽이 깔끔하다
- ★ **64x64 원화를 PPU 64로 임포트하면 정확히 1유닛 = A와 같은 키다.** 크기 문제 없이 원화를 그대로 쓸 수 있다

**확정 — B는 64px 원화 + PPU 64.** `DECISIONS.md` 2026-08-21 · `STYLE_GUIDE.md` 픽셀 밀도 절에 예외로 기록.
- 대가: **B의 픽셀이 A·타일의 절반 크기.** 알고 받는 값이다
- 이득: 컨셉 15종이 전부 64px이라 **변환 공정이 통째로 사라진다**
- 뒤집을 경우의 경로: 다운스케일이 아니라 **32px 네이티브 재생성**(A가 그렇게 만들어졌다). 크레딧이 든다

**인게임 반입**: `Art/Characters/B/Sprites/B_idle_0.png` (B01_idle 원본, PPU 64 / Point / 무압축). `Greybox_WovenNest` · `Greybox_Movement` 두 씬의 `Companion_B` 에 배선.
- ⚠️ **B_Idle/B_Walk/B_Sleep 클립과 `B_Animator` 는 삭제된 스프라이트를 가리키는 죽은 참조다.** 켜두면 `m_Sprite` 를 null 로 덮어써 B가 다시 사라진다 → Animator 를 끄고 `CompanionFollower` 의 코드 들썩임 대체 경로를 쓴다(스크립트가 원래 지원하는 길). 클립은 지우지 않고 남겨뒀다
- **아직 idle 1장뿐이다.** walk/fall 프레임과 턴 3프레임(`turnQuarterSprite`·`turnFrontSprite`)은 비어 있다

**보류로 남은 것**: `B01_idle` 만 구 split 판본이라 **귀가 갈라져 있다**(나머지 14종은 잎귀 통일본). Idle이 가장 오래 보이는 프레임이라 통일 여부 결정이 필요하다.

**B01 수술 — 귀·배 통일 (2026-08-21, 사용자 지시)**

인게임 반입 직후 사용자 확인 요청으로 드러난 것: `B01_idle` 만 구 split 판본이라 **귀가 갈라져 있고 크림 배가 없다.** 나머지 14종은 잎귀 + 크림 배다. 하필 idle이라 화면에 가장 오래 뜬다. 상세 기록은 `b-current/README.md` 2절.

- ★ **`B01` 의 머리 = `B02` 의 머리를 x축 1px 옮긴 것** — 머리 영역 645px 중 **593px 완전 일치**. 두 그림은 머리를 공유하고 귀만 다르게 그린 것이었다. 그래서 귀는 **좌표만 맞춰 통째로 이식**했다(cols 0~27 / rows 0~32). 턱선은 포즈가 달라 손대지 않았다
- **배는 이식이 아니라 그려 넣었다** — `B01`은 서 있고 `B02`는 앉아 있어 몸통이 아예 다른 그림이다(서 있는 통일본 `B06`·`B09` 와도 일치율 16%). 몸통 초록 연결요소 ∩ 타원으로 칠하고 왼쪽 한 칸에 `#c1bc8d` 음영을 넣었다(B09와 같은 배치)
- ⚠️ **함정: 행마다 「몸통 중심을 지나는 구간」 하나만 남겨야 한다.** 안 그러면 크림이 팔·다리로 새고, **다리 사이 검은 선이 배 한복판에 구멍처럼 남는다.** 실제로 두 번 밟고 고쳤다
- ⚠️ **남은 차이 1건**: `B01`에만 갈색 주머니가 있다(몸통 왼쪽 아래). `B07_handover`가 같은 주머니를 손에 들고 있어 **소품일 가능성**이 있으므로 지우지 않고 남겼다 — 판단 필요
- **반영**: `b-current/poses/B01_idle.png` 갱신 + `Art/Characters/B/Sprites/B_idle_0.png` 동기화 후 재임포트. 인게임 실촬로 확인
