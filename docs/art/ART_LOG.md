# Art Log — OpenAI Prompt Request History

All previous B-character art direction is superseded by the user-approved B concept sheet provided in the current chat on 2026-08-20.

This project still follows `docs/art/style-guide/STYLE_GUIDE.md`: 16x16 tiles, 32x32 general characters, A/B hero artwork at 64x64 with PPU 64, dense indie pixel art, 1px dark outline, limited palette, no anti-aliasing, no soft glow, no direct copying of reference game IP.

---

## Active Request

### 2026-09-03 — Session-wide artwork verdict

- **Sole positive result**: `docs/art/assets/generated-concepts/key-art/2026-09-03-concept-art-pass/05_old_song_reunion.png` is the only image from this session that the user considers a useful visual direction.
- **Rejected results**: every other image generated during this session is considered visually unsuccessful. This includes the other four narrative frames, all three painterly panorama images, all four Woven Nest map/prop exploration images, and the already-deleted three-map strict-composition pass.
- **Reuse rule**: rejected results must not be treated as canon, style anchors, palette anchors, composition references, or positive prompt examples unless the user explicitly re-approves one later. This verdict supersedes the earlier favorable review notes below.
- **Retention**: all rejected image files were deleted from the project at the user's request to avoid increasing GitHub repository size. Only `05_old_song_reunion.png` remains. Small README prompt records remain as failure documentation. The strict-composition pass and its documentation had already been deleted separately.
- **Status**: `05_old_song_reunion.png` is a preferred direction reference, not automatically an approved final asset or canon scene layout.

### 2026-09-03 — Painterly panoramic artwork direction test (`gpt-image-2-style-library` + built-in ImageGen)

- **Purpose**: Translate three user-provided landscape references into an original Miji concept-art direction: immense environments, very small travelers, painterly value masses, selective drawn contours, and strong atmospheric depth.
- **Reference boundaries**: references were used only for general mood, scale, lighting, composition language, and medium. Their locations, characters, buildings, logos, text, red-eye motifs, and proprietary designs were excluded.
- **Templates / cases**: `illustration-art-style` (case 346) as primary, `scene-storytelling` (case 330) for narrative framing, and `architecture-space` (case 331) for environment hierarchy. ImageGen taxonomy: `stylized-concept`.
- **Project continuity**: A and B identity references remained authoritative. Miji's non-human organic architecture rules were preserved. This pass intentionally uses high-resolution painterly concept art and does not alter the in-game pixel-art standard.
- **Outputs**: the three generated PNGs were rejected and deleted. `docs/art/assets/generated-concepts/key-art/2026-09-03-painterly-artwork-pass/README.md` remains only as a failure/prompt record.
- **Review**: output 1 best matches the requested contour-rich bright landscape feeling; output 2 is the strongest world-overview image; output 3 is the strongest tonal match for Miji's melancholy. A/B remain readable at small scale. Floating geography, settlements, and the extinguished bell vessel remain exploratory.
- **Status**: Rejected by the user's later session-wide verdict. Do not reuse these images as positive references.

### 2026-09-03 — Five-scene narrative concept-art pass (`gpt-image-2-style-library` + built-in ImageGen)

- **Purpose**: Test the game's current story and art direction as a coherent five-frame visual arc rather than isolated assets.
- **Routing**: ORCHESTRATOR → Planning/Story scene selection → Art generation. Scene selection used only current confirmed material from `PROJECT_HANDOFF.md`, `PROSE_MIDPOINT_DRAFT.md`, `CHARACTER_B.md`, and `DECISIONS.md`; no new lore was promoted.
- **Templates / cases**: primary `scene-storytelling` (case 330), supported by `illustration-art-style` (case 346). Project `STYLE_GUIDE.md` and this log remained authoritative.
- **References**: 64px A idle identity, B01 idle identity, the preferred Woven Nest mood/palette concept, the refined Balancer identity, and the first generated frame as the cross-image style anchor.
- **Outputs**: only `docs/art/assets/generated-concepts/key-art/2026-09-03-concept-art-pass/05_old_song_reunion.png` remains. Images 01–04 were rejected and deleted. Prompt framework and historical QA notes remain in the folder `README.md`.
- **Review**: visual continuity passed for A/B identity, scale, palette, pixel density, organic framing, and restrained cyan/amber lighting. Known exploratory deviations: scene 1 switch contact is ambiguous; scene 2 spoon is readability-scaled; scene 3 wall handle is subtle; scene 4's first crack is slightly stronger than prose; scene 5 uses sound ticks as cinematic shorthand.
- **Status**: `05_old_song_reunion.png` is the sole preferred direction reference from this pass. Images 01–04 are rejected and must not be reused as positive references. Image 05 is not automatically a final/canon asset.

### 2026-09-03 — Woven Nest map + prop exploration pass (`gpt-image-2-style-library` + built-in ImageGen)

- **Purpose**: Test the newly integrated prompt-selection workflow against Miji's existing pixel-art canon by generating two playable-room concepts and two separated prop sheets.
- **Templates / cases**: maps use `architecture-space` + `scene-storytelling` (case 331, 330); props use the separation/layout rules from `concept-product-breakdown` (case 370, 361).
- **References**: approved Woven Nest composite, Woven Nest in-engine sample room, the 64px A idle scale anchor, and the older oversized lantern/folk-machine sheet. References were used for style, palette, density, and scale only.
- **Outputs**: all four generated PNGs were rejected and deleted. `docs/art/assets/generated-concepts/woven-nest/2026-09-03-prompt-library-pass/README.md` remains only as a failure/prompt record.
- **Map review**: `Root-Loom Crossing` has the richer architecture but remains busy; `Lantern-Silt Well` has stronger negative space and more readable traversal edges. Neither is a collision map or final room layout.
- **Prop review**: object counts and cell separation passed. Scale remains concept-level and must be normalized during extraction. Both outputs failed real-alpha generation: the checkerboard is baked into `Format24bppRgb`, and a targeted background-removal retry failed the same check. Files are explicitly named `rgb-checkerboard` and must not be imported directly into Unity.
- **Status**: All four images were rejected by the user's later session-wide verdict. Do not use them as style or palette anchors, despite the earlier provisional mood note below.
- **사용자 의견 (2026-09-03)**: 이 PNG 4장은 **에셋 확정이 아니라 "이런 분위기·느낌을 선호한다"는 방향 레퍼런스**다. 픽셀 하나하나가 아니라 톤(차분한 멜랑콜리), 팔레트(딥 틸 + 앰버/시안 소액센트), 유기적 비인간 건축, 밀도감을 참고한다. 이후 아트 프롬프트는 이 무드를 기준선으로 삼되, 여기 그려진 개별 오브젝트·룸 레이아웃은 정본으로 인용하지 않는다.

### 2026-09-03 — GPT-Image2 Style Library workflow integration

- **Purpose**: Add the external `gpt-image-2-style-library` as a structured prompt-selection aid without changing Miji's approved visual canon or manual image-generation workflow.
- **Source**: `freestylefly/awesome-gpt-image-2`, skill path `agents/skills/gpt-image-2-style-library` (MIT). Installed through Codex's audited `skill-installer`; the package's own overwrite installer was not run.
- **Precedence**: `STYLE_GUIDE.md` > user-approved canon and failure records in `ART_LOG.md` > external style-library templates and example cases.
- **Preferred template mapping**: `character-design-sheet` for characters/sprites; `scene-storytelling` or `architecture-space` for area concepts. These templates provide structure only and do not override pixel size, PPU, frame count, palette, transparency, or prohibited-detail rules.
- **Output rule**: Keep the selected template name and case IDs outside the copyable generation prompt and record them here as metadata.
- **Status**: Workflow integration approved. No art asset, palette, character identity, or world-art decision changed.

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

**B idle 1차 실패 — 「목인사」 (2026-08-21)**

`animate_image` 계열에 「Gentle idle breathing… body rises and falls by 1 pixel」을 넣었더니 **눈을 크게 감았다 뜨는 동작**이 나왔고, 큰 눈 + 1px 상하 바브가 겹쳐 **목인사로 읽혔다**(사용자 지적).

- **원인 (사용자 재지적으로 정정)**: 문제는 깜빡임 자체가 아니라 **감고 있는 시간**이었다. `A_Idle` 규격인 **4프레임/6fps** 에 깜빡임을 프레임 하나로 넣으면 **0.167초를 0.67초마다** = 전체의 **25%를 감고 있고 초당 1.5회** 깜빡인다. 실제 깜빡임은 **0.1초를 2~5초에 한 번**이다 — 빈도 20배, 지속 2배
- ★ **이건 프롬프트가 아니라 클립 타이밍 문제다.** 생성 도구는 프레임만 주고, **각 프레임의 노출 시간은 `.anim` 이 정한다.** 프롬프트를 고쳐도 균등 분할이면 결과는 같다
- **개정 3점**: ① 눈·얼굴·머리를 **전 프레임 레퍼런스 픽셀 복사**로 못 박음 ② 숨쉬기 주체를 **몸통·귀·꼬리**로 한정(머리는 움직이는 주체에서 제외) ③ **프레임 1~4에 각각 할 일 지정**
- **negative 추가**: blinking / closed eyes / squinting / eyelids / nodding / bowing / head dip / looking down / greeting motion
- **개정 2 (채택)**: 5프레임 요구 — 호흡 4장(눈 고정 개방) + **깜빡임 1장**. 타이밍은 Unity에서 잡는다: 호흡은 6fps 루프, 깜빡임은 **0.1초를 3~6초 랜덤 간격**으로 위에 덮는다(`TurnView` 식 LateUpdate 덮어쓰기 — 랜덤 간격은 클립에 구우면 주기가 기계적으로 반복돼 티가 난다)
- ★ **「감은 눈」만 쓰면 모델은 웃는 눈(위로 휜 ^ 아치)을 가져온다** — `B06_laugh` 가 정확히 그 형태다. 중립 깜빡임은 **평평하거나 살짝 아래로 휜 직선**임을 명시해야 깜빡일 때마다 B가 웃지 않는다
- sit·question·laugh 계열은 여전히 **원샷 이모트**다(이건 지속 시간과 무관한 별건)
- `STYLE_GUIDE.md` 「생성 프롬프트 금지 사항」에 규칙으로 추가
[2026-08-21 15:37] B_Idle.anim 수정됨

### 2026-08-21 (5차) — B Idle 애니메이션 확정 ★ 생성 크레딧 0

**결론: 시안 F 채택.** 4프레임 호흡 + 귀 흔들림 + 깜빡임 2장. 사용자 컨펌 완료.

★ **AI 생성을 쓰지 않았다.** `B01_idle.png` 의 픽셀을 좌표만 옮겨서 만들었다 — **크레딧 소모 0**.
근거는 1차 실패(같은 날, 위 항목)와 `b-current/README.md` 3절이다: 생성 모델은 B의 눈 규격(흰자위 금지)과 잎귀를 반복해서 깨뜨렸고, **idle은 화면에 가장 오래 떠 있어** 여기서 정체성이 흔들리면 포즈 전환마다 얼굴이 바뀐다. 눈·귀·목도리·꼬리는 **1픽셀도 새로 그리지 않았다**(깜빡임의 감은 눈만 예외).

**★ 레이어 분해 — 스프라이트가 y축으로 깨끗하게 갈린다**

| 행 | 내용 |
|---|---|
| `y 0~11` | 귀 끝만 (머리 정수리는 y=12부터) |
| `y 0~33` | 귀 + 머리 **전부**. 목도리·몸통이 한 픽셀도 안 섞인다 |
| `y 34~54` | 목도리 + 몸통 + 꼬리 + 팔 |
| `y 55~60` | 다리·발 (두 다리는 y=55부터 갈라진다) |

**★ 함정 1 — 아래로만 움직여야 한다.** 레이어를 위로 올리면 **목에 1px 구멍**이 뚫린다(머리 밑변 y=33, 몸통 윗변 y=34라 사이가 없다). 그래서 `머리 이동량 >= 몸통 이동량` 을 코드에 잠갔다. 이 제약 때문에 **머리가 몸통보다 늦게 따라오는(lag) 체인은 불가능**하고, 대신 **머리를 더 크게 움직이는(리드) 체인**으로 갔다.

**★ 함정 2 — 몸통을 내리면 다리가 뭉갠다** (수정 1차에서 실제로 밟음).
몸통 아랫줄이 다리를 덮는데, 덮이는 `y=55~56` 이 하필 **두 다리 사이가 갈라져 있는 유일한 줄**이다. 덮이면 다리가 통짜 덩어리가 되고 발이 사라진 것처럼 읽힌다.
→ **덮지 말고 정강이 행을 빼서 압축한다.** 다리 구간은 `55=56`, `57=58`, `59=60` 으로 **같은 줄이 두 번씩** 반복되므로 중복 줄만 빼면 실루엣이 안 깨진다.
- 몸 1px ↓ → `56` 삭제 / 몸 2px ↓ → `56`·`58` 삭제
- **발(59~60)과 다리 갈라짐(55)은 어떤 프레임에서도 안 없어진다**

**채택 F — 프레임별 이동량 (px, 64px 기준)**

| 부위 | f0 | f1 | f2 | f3 |
|---|--:|--:|--:|--:|
| 머리+귀 (y0~33) | 0 | 1 | 2 | 1 |
| 귀 끝 x축 (y0~11) | 0 | 0 | -1 | -1 |
| 몸통+꼬리 (y34~54) | 0 | 1 | 1 | 0 |
| 꼬리 끝 추가 (y≤44 ∩ x≤20) | 0 | 0 | +1 | +1 |
| 다리 | 몸통만큼 압축 | | | |

- 검토 후 탈락: **E**(몸통 `0,1,2,1` + 머리 `0,2,3,1`) — 진폭이 과했다. 사용자 판정
- 깜빡임: 눈 검은 덩어리를 **연결요소로 뽑아** 바로 위 머리 초록으로 메우고, 높이 55% 지점에 2px 눈꺼풀 선(`#151d06`, 양끝 1px `#263515`)을 얹었다. **웃는 눈(위로 휜 아치)이 아니라 평평한 직선** — 1차 실패 때의 `B06_laugh` 형태를 피하는 것이 규격

**산출물**
- `b-current/anim/idle/B_idle_0~3.png` + `B_idle_blink_1~2.png` + `B_idle_preview.gif`
- 인게임: `Art/Characters/B/Sprites/B_idle_1~3.png`, `B_idle_blink_1~2.png` (PPU 64 / Point / 무압축, `B_idle_0` 은 기존 것이 그대로 f0)
- 재생성 도구: `tools/b-anim/build-idle.mjs` + `imglib.mjs` — **의존성 0**(Node 내장 zlib만 사용). PNG 디코드/인코드 + GIF89a 인코더 자작. 파라미터 표만 고치면 walk/sleep 에도 같은 공정을 쓸 수 있다

**Unity 배선 — B_Idle.anim 죽은 참조 복구**
위 「2차」 항목에서 「삭제된 스프라이트를 가리키는 죽은 참조」로 기록된 클립 중 **B_Idle 만 복구했다.** 12키프레임(호흡 3사이클, 3번째에 깜빡임), `m_SampleRate: 100`, 루프 1.54초.

- ⚠️ **`B_Walk` · `B_Sleep` 은 여전히 죽은 참조다.** 그래서 씬의 Animator 는 **아직 `m_Enabled: 0` 그대로 뒀다** — 켜면 `IsAsleep` 전이로 Sleep 에 들어가 `m_Sprite` 가 null 이 되고 B가 사라진다(2차 항목에 기록된 실패 그대로). Idle 만으로는 Animator 를 못 켠다
- ⚠️ **깜빡임을 클립에 구웠다 — 위 1차 실패 항목의 「랜덤 간격 덮어쓰기」 결정과 충돌한다.** 사용자가 컨펌한 GIF가 구운 버전(1.54초 주기)이라 그대로 반영했다. 기록된 기준은 **0.1초를 3~6초 랜덤**이며, 전환하려면 클립을 호흡 4프레임만 남기고 `TurnView` 식 LateUpdate 덮어쓰기로 깜빡임을 옮기면 된다(스프라이트는 이미 분리돼 있다). **판단 필요**
[2026-08-21 15:40] README.md 수정됨
[2026-08-21 19:03] README.md 수정됨
[2026-08-21 19:08] README.md 수정됨

### 2026-08-21 (6차) — B Walk 애니메이션 확정 ★ PixelLab 초안 채택 (1생성)

**결론: PixelLab `animate_image` 초안 채택.** 수제 픽셀 이동 시안(4프레임)과 나란히 컨펌에 올려 사용자 판정 — 「pixellab이 좀 더 맘에드네」.

- **구독 전환 확인**: 트라이얼 소진(8/20 기록)이 아니라 **Tier 2 Pixel Artisan 구독 활성** — 잔여 4,390/5,000, 리셋 9/20. 이번 걷기 초안에 1생성 사용
- **새 워크플로 (사용자 지시)**: ① 애니메이션 초안은 PixelLab 로 먼저 뽑고 보고 결정한다 ② 컨펌 대기 아트는 `docs/art/confirm/` 대기함에 미리보기만 모은다 (규칙: 그 폴더 README)
- **생성 설정**: B01_idle 을 first_frame 으로 `animate_image` 8프레임, action 에 「quadruped trot, 다리 교차, 1px 바운스, 눈 뜬 상태 고정」 명시. 64px 8프레임 = 1생성. 큐 혼잡 시 10분+ 걸린다
- **검수**: 8프레임 전부 눈 규격(검은 구체+크림 하이라이트+갈색 크레센트, 흰자위 없음)·잎귀·목도리·크림 배·주머니 유지 — 과거 실패(흰자위·강아지 귀)와 달리 이번엔 정체성이 살아남았다. 귀·꼬리 두께가 프레임별로 1px씩 출렁이는 건 재생 시 자연스러운 수준
- **산출물**: `b-current/anim/walk/B_walk_0~7.png` (draft_1~8 승격, draft_0=원본 사본은 폐기) + `B_walk_preview.gif`. 수제 시안은 기각 — 빌더는 `tools/b-anim/build-walk.mjs` 에 handmade 접두어로 남김
- **Unity 반입**: `Art/Characters/B/Sprites/B_walk_0~7.png` — B_idle 과 동일 임포트 설정(.meta 를 B_idle_1 템플릿에서 GUID·spriteID 만 새로 발급해 직접 생성). **`B_Walk.anim` 죽은 참조 8건을 새 GUID 로 교체** (기존 12fps·0.667초 루프 타이밍 유지)
- ⚠️ **Animator 는 여전히 꺼 둔다** — 남은 죽은 참조는 `B_Sleep` 하나. IsAsleep 전이가 살아 있어 켜면 B 가 사라진다. B_Sleep 복구(후보: B14_asleep 1프레임 클립)가 Animator 활성화의 마지막 블로커

### 2026-08-21 (7차) — A 64px 업스케일 탐색: PixelLab 초안 3종 (3생성) — 컨펌 대기

**발단**: 사용자 「A캐릭터를 B캐릭터처럼 64픽셀로 업스케일링 하고싶은데 예시 뽑아줘」. B가 64px/PPU 64로 확정된 뒤 A(32px/PPU 32)와의 픽셀 밀도 차이를 없애려는 탐색.

- ⚠️ **STYLE_GUIDE 결정과 충돌 예약**: 「64px 예외는 B에만, 다른 캐릭터는 32x32/PPU 32 유지」(2026-08-21 확정)가 걸려 있다. A 64px을 채택하면 이 결정을 개정해야 한다 — **채택 시 STYLE_GUIDE.md + DECISIONS.md 갱신 필수**
- **방식**: 단순 확대(니어리스트 x2)는 밀도가 안 늘어나므로 **64px 네이티브 재생성**. `A_idle_0.png`(32px)을 x2 니어리스트 확대해 init_image로 쓰고 pixflux img2img 2종 + pixen 텍스트 1종
  - **v1** img2img strength 300 (보수적) — 원본에 가장 충실, 밀도 증가는 미미
  - **v2** img2img strength 150 (본편집) — ★ 패널 라인·리벳·베이지 스위치(컨셉 원화 색 복원)·렌즈 링이 살아난 진짜 64px 밀도. 실루엣 유지
  - **v3** pixen 텍스트 전용 — 오프모델(둥근 머리, 스위치가 안테나화). 실루엣이 깨져 참고용
- **산출물**: `assets/a-64px/A64_v1~v3.png`(원본) + `confirm/A_64px_upscale_pixellab_preview.png`(원본 A 대비 비교 시트)
- **채택 시 남는 일**: idle 4 + run 8 + jump 3 + fall 2 + front + turn45 = **18장 전부 64px 재생성** 필요(이번엔 idle 1장만). B walk처럼 `animate_image`에 채택본을 first_frame으로 넣는 공정이 유력

**2차 라운드 — v1~v3 전부 기각, 모스 라운드 (3생성)**

**사용자 판정 「다 별로, 기존에 있던 이끼 낀 거 같은 느낌도 없음」** + 새 요구 2건:
- ★ **파츠 분리 애니메이션 전제**: ① 점프 — 몸체와 바퀴(다리)가 벌어지고 그 사이에 얇은 실선 ② 방향전환 — **바퀴는 가만히, 몸체만 회전**. → 64px 스프라이트는 **몸체와 궤도 섀시 사이에 깨끗한 수평 이음새**가 있어야 y축 레이어 분해(B idle 공정)로 자를 수 있다
- **개정 3점**: ① 프롬프트에 moss/grime 명시 ② **`color_image` 팔레트 강제**(원본 A_idle_0 색만 쓰도록 — 1차에서 이끼색이 날아간 원인 차단) ③ 몸체-섀시 분리선(dark horizontal seam) 명시
- **v4** s280 — 이끼 유지 + 원본 충실 + 패널 밀도. 몸체/롤러 경계 실선 존재
- **v5** s200 — 이끼가 가장 짙다(카모 수준). 렌즈가 커지고 섀시가 몸체에서 확실히 떨어져 보이나 롤러가 흩어짐
- **v6** s150 — 스위치가 변형(파란 점 노브)되고 이끼가 오히려 줄어 기각 후보
- 산출물은 `assets/a-64px/A64_v4~v6.png`, 비교 시트 갱신. **컨펌 대기**

**3차 라운드 — v4 채택 방향, 바퀴 개선 2종 (`edit_image`, ~40생성)**

사용자 판정 「v4 나쁘지 않은데 바퀴 구분감/퀄리티를 올려 달라」. v4를 베이스로 **`edit_image`**(몸체 보존, 지정 부위만 수정 — img2img 재생성과 달리 채택된 부분이 안 흔들린다. 단 건당 ~20생성으로 20배 비싸다) 2건:
- **v7 구분감** — 몸체 밑변과 섀시 윗변 사이에 **투명 2px 갭** + 양쪽 폐곡선 아웃라인. 파츠 분리(점프 벌어짐/방향전환 몸체 회전)에 바로 쓸 수 있는 형태
- **v8 바퀴 퀄리티** — 허브 박힌 원형 로드휠 + 트레드 링크 세그먼트 + 림 하이라이트. 갭은 없음
- 몸체(이끼·렌즈·스위치)는 두 버전 다 v4 그대로 유지됨. `assets/a-64px/A64_v7_gap.png`, `A64_v8_wheels.png`. **컨펌 대기**

**4차 — V8 채택 + Idle 초안 (`animate_image`, 1생성)**

사용자 「V8 베스트, 그걸로 Idle 뽑아 줘」. V8을 first_frame으로 `animate_image` 4프레임(A_Idle 규격 = 4프레임/6fps 그대로 대응).
- ⚠️ **base64 전송 truncation** — V8(3364자) 인라인 base64가 MCP 전송 중 잘려 「broken data stream」. **PixelLab 다운로드 URL(`first_frame_url`)로 우회**해서 성공. 64px여도 인라인은 위험, URL이 안전
- **큐 혼잡 극심** — 5%→18%(감소)→54%(정체)→90% 로 진행률이 오르내리며 **~50분** 소요. ART_LOG 기록대로 큐 혼잡 시 매우 느리다. 취소 시 1생성 손실이라 대기
- **action 지시**: 하부 궤도 섀시 완전 고정(바퀴 상하 이동 금지) + 상체만 1px 호흡 + 스위치 1px 흔들림 + 렌즈는 뜬 상태 유지하며 광량만 미세 펄스. 나중 파츠 분리(점프 벌어짐/방향전환 몸체 회전) 의도와 일관되게 「바퀴 고정」을 명시
- **결과 판정**: 바퀴는 대체로 고정, 몸체 호흡·스위치 흔들림 OK. ★ **약점 2건** — ① 렌즈 광량 펄스가 과함(f2~f3 코어가 거의 흰색까지 뜸) ② **f4에서 렌즈가 가로 슬릿으로 수축**해 깜빡임/눈감음처럼 읽힌다(원래 렌즈는 항상 원형이어야 함). 재롤 or f4 교체 후보
- 산출물: `assets/a-64px/anim-idle/A64_idle_0~4.png`(0=원본 V8, 1~4=생성 사이클) + `A64_idle_preview.gif` + `_frames.png`. 컨펌: `confirm/A_64px_idle_pixellab_preview.gif`·`_frames_preview.png`

**4차-b — AI idle 폐기, 절차적 「위아래 이동만」 재작성 ★ 크레딧 0**

사용자 「렌즈 테두리가 움직이는 건 너무 짜치고, 그냥 위아래 움직이는 것만 추가해 줘」. AI `animate_image`가 프레임마다 렌즈·이끼·패널을 다시 그려 생긴 잔떨림(특히 f4 렌즈 슬릿·광량 과펄스)이 근본 원인 → **AI 4프레임 전부 폐기.**
- ★ **B idle 공정과 동일**: V8 원본 픽셀을 **평행이동만** 시켜 만든다 → 렌즈·테두리·이끼가 1픽셀도 안 변하고 세로 움직임만 생긴다. `tools/b-anim/imglib.mjs` 재사용, **생성 0**
- **V8 세로 실측**: content rows 10~63, **위 여백 10px / 아래 여백 0**(바퀴가 캔버스 맨 밑 붙음). → **위로만 이동 가능**(아래로 가면 바퀴 클리핑)
- **강체 전체 이동 채택** — 몸체만 올리면 몸체-섀시 경계(row 51/52)에 1px 갭이 깜빡여 또 짜친다. 로봇 전체를 통으로 올리는 게 갭 0. 바퀴 1px 부양 < 허리 갭 깜빡임, 이쪽이 덜 거슬린다는 판정
- **채택: 4프레임 삼각파 offsets `[0,1,2,1]` (위로 2px 진폭), 6fps.** 0.667초 루프. A_Idle.anim 규격(4키/6fps) 그대로 대응
- 빌더 `scratchpad/.../build-idle-bob.mjs` — 파라미터(offsets)만 고치면 진폭·프레임 조정 가능. 인게임 반입 확정 시 `tools/b-anim/` 로 승격 예정
- 산출물 동일 경로로 덮어씀(0~3 절차판, AI f4 삭제)

**4차-c — 「공중부양」 정정: 강체 전체 이동 → 바퀴 고정 + 몸체만 이동**

사용자 「GIF로 보니 공중부양하는 것 같다. 바퀴는 가만히 있고 몸체만 움직이게」. 4차-b의 강체 전체 이동은 바퀴까지 떠서 부양처럼 읽혔다.
- ★ **V8 하단 구조 실측**(`map-v8.mjs` ASCII 맵): 둥근 트레드 바퀴 = **rows 54~63**, 그 위(rows 10~53)가 몸체. → **바퀴(54~63) 고정, 몸체(0~53)만 세로 이동**
- **방향 트레이드오프 검토**(`cand_compare.png`): 몸체 **위로** = 허리에 갭이 벌어져 몸체가 떨어져 보임 / 몸체 **아래로** = 바퀴에 1px 겹치지만 갭 없음. 2px는 둘 다 과함
- **채택: 아래로 1px, `[0,1,1,0]`** — 갭 0이라 부양 느낌이 완전히 사라진다. 몸체가 바퀴 위로 살짝 눌러앉았다 뜨는 호흡. 바퀴는 4프레임 내내 동일 위치. 렌즈는 평행이동만(모양 불변)
- 대안 보관: 위로 1px `cand_up1_*`(허리 갭 버전) — `confirm/A_64px_idle_ALT_up_preview.gif`
- 빌더 `build-idle-final.mjs`(CHASSIS_TOP=54, offsets 파라미터). 인게임 확정 시 `tools/b-anim/`로 승격
- 산출물 `A64_idle_0~3.png` 아래방향판으로 덮어씀 + `_preview.gif` + `_frames.png`

**4차-d — 절차판도 반려(바퀴 테두리 딸려올라감) → PixelLab 재초안 2종 (2생성)**

사용자 「바퀴 테두리가 같이 딸려 올라간다. PixelLab에서 일단 초안 다시 만들어 줘」. 절차판의 CHASSIS_TOP=54 컷이 **둥근 바퀴 테두리 상단을 몸체로 잘못 포함**해 같이 움직였다(실측 경계가 직선이 아니라 곡선이라 수평 컷으로는 안 갈림).
- PixelLab `animate_image` 4프레임, V8 URL first_frame, 시드 1111/2222 2종. action에 「바퀴·테두리·트레드 100% 고정 + 렌즈 고정 원형(깜빡임/광량변화 없음) + 몸체만 1px 호흡」 강하게 명시
- 큐 혼잡 재현 — 2종 병렬 ~40분
- ⚠️ **AI 재초안의 한계 재확인**: `animate_image`는 매 프레임 전체를 다시 그려 **바퀴 트레드·스위치 레버가 프레임마다 미세하게 흔들리고**(완전 고정 지시에도), seed B는 렌즈 무늬까지 프레임별로 바뀐다. seed A(1111)가 렌즈가 더 안정적. 근본적으로 「바퀴 완전 고정」은 AI로는 안 나온다 — 절차적 방식이라야 픽셀 고정이 보장됨
- 산출물: `scratchpad/.../draft2/s1_*, s2_*` + `confirm/A_64px_idle_pixellab_v2_seedA.gif`·`_seedB.gif`·`_frames.png`

**4차-e — seedA 채택 + 인게임 반입 (사용자 「이걸로 ㄱ」)**

사용자가 seedA(1111) GIF를 첨부하며 확정. STYLE_GUIDE·DECISIONS 개정(2026-08-22) 후 인게임 반입.
- **프레임 매핑**: seedA 생성 사이클 `s1_1~s1_4`(index0=원본 V8 제외) → `A/Sprites/A_idle_0~3.png` (확정 GIF 재생 순서 유지). `docs/art/assets/a-64px/anim-idle/A_idle_0~3.png` 에도 정본 사본
- ★ **`.meta` 재작성으로 `A_Idle.anim` 무수정 반입** — 기존 A idle .meta는 **32px 타이트 rect(x2,y2,27×29)** 라 64px PNG를 그냥 넣으면 27×29만 크롭돼 깨진다. B_idle_1.meta(64px 정상본)를 템플릿으로 **PPU 64 + 풀프레임 + 중심피벗 + spriteSheet 비움**으로 재작성하되 **각 파일의 원래 GUID를 유지**. GUID·주 스프라이트 fileID(21300000)가 그대로라 `A_Idle.anim` 4키(4/6fps)가 자동으로 새 스프라이트를 가리킨다 — anim 무수정
- ⚠️ **정렬 리스크(검증 필요)**: 구 스프라이트는 27×29 rect 중심피벗 → 발이 피벗 아래 ~0.45u. 신 스프라이트는 64px 풀캔버스 중심피벗 + 콘텐츠가 rows 10~63(위 10px 여백)이라 발(맨아래)이 피벗 아래 0.5u. **발 위치가 ~0.05u(≈3px) 낮아질 수 있음** → 인게임 실측 후 SpriteRenderer 트랜스폼 Y 또는 피벗 보정 필요할 수 있음
- ⚠️ **밀도 튐**: idle만 64px, run/jump/fall/turn은 32px. 크기(1u)는 같지만 전환 시 해상도가 튄다 — 나머지 14장 64px 재생성이 남은 부채

**8차 — A 나머지 애니(run/jump/fall/turn) 64px 초안. 성격별로 공정 분리**

사용자 「나머지도 64px로 뽑아줘. turn·jump는 바퀴 움직임 생각해서」. 자연 모션은 AI, 바퀴 구조 거동은 절차/뷰생성으로 나눔.
- ⚠️ **V8 호스팅 URL 만료(404)** — 이전 job 이미지가 시간 지나 내려감. 로컬 V8을 base64로 넘기려니 64px 트루컬러가 전송 truncation. **PIL로 64색 인덱스 PNG(865B)로 줄여** base64 1156자 → 통과. 이후 A 애니 생성의 표준 first_frame
- **run (PixelLab animate_image 8프레임, seed 3131)** — 트레드 롤. 몸체 유지, 트레드/바퀴 회전. 렌즈 원형 유지. 큐가 이번엔 빨랐음(수분)
- **fall (PixelLab 4프레임, seed 4242)** — 낙하 기울임. 인게임 `A_Fall.anim`은 2프레임/6fps라 4장 중 2장 선택 필요
- **jump (절차, 크레딧 0)** — ★ 사용자 요구 「몸체와 다리가 벌어지며 사이 얇은 실선」 직접 구현. V8 몸체(rows 0~53)를 위로 dy=[2,5,8] 띄우고 바퀴(54~63) 고정, 벌어진 틈에 중앙 x=32 세로 2px 스트럿(다크 아웃라인+메탈 하이라이트)을 그림. `build-jump.mjs`. AI로는 이 구조 분리가 안 나오므로 절차 채택
- **turn (PixelLab 6프레임 스윙, seed 5353)** — `TurnView.cs`가 45°+정면 2장을 덮어쓰는 방식(측면→정면→반전). action에 「트레드 베이스 측면 고정, 상체만 터릿처럼 스윙, 중점서 정면」 명시. 결과 idx0=원본→idx6=정면으로 렌즈가 중앙으로 이동. **제안: `A_turn45`=idx2(3/4), `A_front`=idx6(정면)**. 베이스가 완벽히 고정되진 않음(AI 한계)
- 산출물: `confirm/A_64px_{run,fall,jump,turn}_*` (프리뷰+시트), 원본 `scratchpad/.../anim2/`(run/fall/turn) + `assets/a-64px/anim-jump/`(jump). **4종 모두 컨펌 대기** — 확정 시 각 .anim에 GUID 유지 방식으로 반입(idle과 동일 공정), fall은 2장·turn은 2장 선택

**8차-b — 사용자 1차 피드백 반영**

「jump 이 느낌 OK / turn 바퀴 움직였지만 일단 쓸 수 있으니 남김 / run 바퀴쪽 흰 픽셀 + 렌즈 테두리 들썩임 고쳐라」
- **jump·turn**: 유지(변경 없음). turn 바퀴 미고정은 AI 한계로 알려진 값, 사용자가 현 상태 수용
- **run 보정 (절차, `build-run-fix.mjs`, 크레딧 0)**: AI 매프레임 재그리기가 원인. ① **렌즈+테두리 rect(x35~50,y31~48)를 V8에서 전 프레임 wholesale 덮어씀** → 잔떨림 완전 제거(몸체가 run에서 거의 안 움직여 고정 렌즈로 정렬 OK) ② **전 픽셀 V8 팔레트 스냅**(full 29색), **바퀴 영역(rows≥54)은 바퀴 팔레트(23색·전부 어두움)로만 스냅** → 오프팔레트 밝은 픽셀 제거. 트레드 롤 변화는 유지. 산출 `run_fix_1~8` → `confirm/A_64px_run_{preview.gif,frames.png}` 갱신

**8차-c — 2차 피드백: run 재제작 / jump PixelLab / turn 바퀴 해결**

「run 엉망 아예 다시 / jump 픽셀 잘림, PixelLab로 / turn 알아서 해결」. 8차-b의 run 전체 팔레트 스냅이 몸체까지 뭉갬(엉망 원인). jump 절차판은 픽셀 잘림.
- **run 재제작 (PixelLab seed 7272, 6프레임 + `build-run2-fix.mjs`)**: 프롬프트에 「렌즈 고정·오프팔레트 금지」 강화. 보정은 **가볍게** — 렌즈 rect만 V8 고정, **바퀴 영역만** 바퀴 팔레트 스냅, **몸체는 PixelLab 원본 그대로**(8차-b의 전체 스냅 뭉갬을 회피). 결과 깨끗: 렌즈 무떨림·튄픽셀 제거·몸체 디테일 유지·바퀴 미세 회전
- **jump 재제작 (PixelLab seed 6161, `animate_image` 4프레임+원본=5)**: ★ 절차판을 폐기하고 PixelLab로. 이번엔 AI가 요구 형태를 냄 — 몸체가 상승하며 바퀴가 아래 남고 **얇은 스트럿이 틈을 잇는다**(idx0 지상→idx4 최고점). 픽셀 잘림 없음. A_Jump 3프레임 → idx2·3·4 추천. (구 절차판 `assets/a-64px/anim-jump/`는 폐기 예정)
- **turn 바퀴 고정 해결 (절차, `build-turn-fix.mjs`, 크레딧 0)**: AI 스윙 프레임의 **바퀴 영역(rows≥54)만 V8로 덮어 고정**. 몸체 스윙(측면 idx0→정면 idx6)은 AI 그대로, 베이스는 7프레임 동일 = 원래 요구「바퀴 고정·몸체 회전」 충족. `A_turn45`=idx2, `A_front`=idx6
- 전 산출 `confirm/A_64px_{run,jump,turn}_*` 갱신. **컨펌 대기**

**8차-d — 3차 피드백**

「run 렌즈 테두리·내부 따로 논다 / jump 몸체 하단 픽셀 튐 어색 / turn 그냥 몸 전체 회전으로, 근데 왜 삭제했냐」
- **turn 원본 복원**: 8차-c의 바퀴고정판을 사용자가 반려, **원본 full-body 스윙(turn_0~6)으로 되돌림**. ⚠️ 원본 프레임은 삭제된 적 없음(scratchpad 유지) — confirm 프리뷰만 덮어썼던 것. 교훈: **컨펌 프리뷰를 대체할 때 이전 버전을 지우지 말고 접미어로 남길 것**
- **run 렌즈 일체화**: 8차-c 핀 rect(x35~50,y31~48)가 하우징 링 바깥(x34/51, y30/49)을 못 덮어 **테두리는 AI(흔들림)·내부는 고정**으로 분리됐음. V8 실측 하우징 x34~51·y30~49 → 핀 rect를 **x33~52·y29~50**으로 넓혀 링 전체 일체 고정. `run3_*`
- **jump 잔조각 정리**: PixelLab 연결부가 끊긴 조각들로 흩어져 어색. **연결요소 디스페클(≥14px만 유지)로 떠다니는 잔픽셀 제거 + 몸체 최하단 중앙↔바퀴 최상단을 깨끗한 2px 스트럿(다크+메탈)으로 재연결**. `build-jump-clean.mjs` → `jump3_*`. 결과: 몸체 상승 + 단일 실선 연결, 잔조각 없음
- 갱신 `confirm/A_64px_{run,jump,turn}_*`. **컨펌 대기**

**8차-e — 4차 피드백: run 원복 / jump 손잡이 제거**

「run 내 피드백 다 빼고 최초 run으로 / jump 시작 프레임부터 몸체 하단 손잡이 같은 픽셀 제거」
- **run 완전 원복**: 8차-b/c/d의 모든 보정(팔레트 스냅·렌즈 핀) 폐기. **최초 PixelLab 원본**(job 2d374429, seed 3131, run_1~8) 그대로 confirm 복원. 사용자가 raw 원본을 선호
- **jump 손잡이 제거**: PixelLab jump(jump2)의 몸체 하단 중앙에 **U자 손잡이형 돌기**(AI 연결부 잔재, 몸체에 붙어 despeckle로 안 지워짐)가 시작 프레임부터 보임. → **PixelLab 폐기, 절차 재제작(jump5)**: V8 깨끗한 몸체(손잡이 없음)를 AI 측정 상승 arc(topRow로 잰 dy≈[2,8,10,8] → 채택 `[3,6,9,7]`, V8 top=10이라 clip 없음)에 맞춰 올리고 고정 바퀴 + 2px 스트럿. 손잡이·잔떨림 원천 제거. `build-jump5.mjs`
- 갱신 `confirm/A_64px_{run,jump}_*`. **컨펌 대기**

**8차-f — 5차 피드백: run 렌즈 몸체와 따로 삐짐 / jump 바퀴 떨어짐→PixelLab**

「run 렌즈가 몸체와 같이 안 움직이고 따로 삐져나감 / jump 바퀴 떨어지는 느낌, PixelLab로」
- **run 렌즈 몸체 고정 (`build-run6.mjs`)**: 측정 결과 렌즈X는 42.5 고정인데 **frame8만 44.5(우측 2px 삐짐)**, 세로는 몸체 bob과 대체로 동기(오프셋 ~29.5 일정)이나 1px 편차. 원인=AI가 렌즈를 몸체와 독립 재그림. 최소 수정: 원본 프레임 유지 + **V8 렌즈 하우징(x33~52,y29~50)을 각 프레임 몸체 top(=bodyTopY-10 만큼 세로 시프트)에 맞춰 얹음** → 렌즈가 몸체에 잠김. 팔레트 스냅 등 다른 처리 없음(사용자가 raw 몸체 선호)
- **jump PixelLab 재제작 (seed 8484)**: 절차판(jump5)의 「바퀴 떨어지는 느낌」(몸체가 뜨며 바퀴만 남아 스트럿에 매달린 read) 반려. action에 「바퀴는 하나의 솔리드 베이스로 붙어 있음·흩어짐 금지·손잡이/잔조각 금지」 명시. 결과: 바퀴 베이스 유지 + 몸체 상승 + 얇은 스트럿, 손잡이 없음. ⚠️ 상승 프레임서 렌즈가 약간 불규칙(블롭) — jump는 0.25s 단발이라 허용, 필요시 run처럼 렌즈 핀 가능
- 갱신 `confirm/A_64px_{run,jump}_*`. **컨펌 대기**

**8차-g — jump 채택 / run 합성 재제작**

「jump ok / run 다시」. run은 5회 손봐도 PixelLab 몸체 재그리기 잔떨림이 남아 반복 반려.
- **jump ✅ 채택**: 8차-f PixelLab jump(seed 8484) 확정
- **run 합성 (`build-run7.mjs`)**: ★ 근본 해결 — **몸체·렌즈(rows 0~53)는 V8 고정**(8프레임 완전 동일 → 잔떨림·렌즈 삐짐 원천 소멸) + **바퀴(rows 54~63)만 AI 원본 롤 유지**(바퀴 팔레트 스냅으로 튄픽셀 제거). 안정된 몸체 + 구르는 바퀴. run 불만(흰픽셀·엉망·렌즈삐짐) 일괄 해소. 몸체 bob 없음(원하면 추가 가능). `run7_1~8`
- **run 컨펌 대기, idle/jump 확정**

**8차-h — run: 합성/보정 다 버리고 PixelLab 신규 생성 2종 (사용자 「다시 만들라고」)**

사용자가 합성(run7)도 반려 — 「그냥 PixelLab로 다시 뽑아라」. 보정 접근 전면 중단.
- `animate_image` 6프레임 신규 2종: **seedA 9091**(몸체 안정), **seedB 2027**(미세 bob). raw 그대로, 어떤 후처리도 안 함
- 큐 혼잡 ~40분. `confirm/A_64px_run_seedA_9091.gif`·`_seedB_2027.gif`·`_AB_frames.png`. **하나 선택 or 재롤 대기**
- 원본 `scratchpad/.../anim2/runA_*`(9091), `runB_*`(2027)

**8차-i — run seedA 채택 / fall 대각선 기울기 재제작**

「seedA 괜찮네 / fall은 떨어질 때 바퀴가 살짝 대각선으로 기울게」
- **run ✅ seedA(9091) 확정** → `assets/a-64px/anim-run/A_run_0~5.png`(6프레임)
- **fall 재제작 (PixelLab seed 3690)**: action에 「낙하하며 트레드 베이스가 살짝 대각선으로 pitch」 명시. idx0 수평→idx2~4 기울기. A_Fall 2프레임 → 추천 idx2·4. `fall2_*`
- confirm 갱신. **fall·turn 프레임 확정 남음 → 확정 시 run/jump/fall/turn 4종 인게임 반입**

**8차-j — fall tilt: 수제 회전 폐기 → PixelLab 재생성 채택 / A 애니 4종 결정 확정 (2026-08-24)**

「픽셀랩에서 fall 이미지를 기울여서 만드는 걸로 다시 제작」 → 이후 「픽셀렙에서 뽑으라고」 / 「낙하 거리가 클수록 최대 기울기까지」
- **1차 시도(폐기)**: `build-fall.mjs`로 `A_idle_0`을 바퀴 밑변 피벗 **NN 회전**(fwd 7°→14°). 각도가 어긋난 픽셀이 **짤려 뭉개짐** — 사용자 지적("픽셀 짤리는데? 니가 만든거 아님?"). 수제 회전은 픽셀아트에 부적합. **폐기**
- **채택: PixelLab `animate_image` 재생성** (seed 1440, 64px 8프레임, 1 generation). first_frame=`A_idle_0`, action=「몸체가 앞으로 대각선 pitch되며 낙하, 리지드」. 각 각도를 **새로 렌더**해 짤림 없음. get_image → 9프레임(idx0=직립 … idx8)
- **사용자 결정: 낙하 거리 구동**. 하강 거리에 비례해 기울기 증가, **멀수록 최대 기울기까지**. 채택 램프 **idx0~5**(직립→앞으로 다이빙). **idx6~8은 텀블링(뒹굴기)이라 미채택** → `anim-fall/overshoot/` 보관
- **구현 방식(설계 확정, 반입은 보류)**: `PlayerAnimator`(뷰)가 `transform.y`로 하강 거리 추적 → `fallForMaxTilt`(≈4u) 기준 정규화 → `Sprite[] fallTiltFrames` 인덱스 선택 후 LateUpdate에서 덮어쓰기(turn 오버라이드와 동일 패턴). **물리 불간섭** 원칙 유지. ⚠️ 이번 세션엔 **Unity 미적용**(사용자 지시) — 코드·씬 와이어링은 다음 세션
- 마스터: `assets/a-64px/anim-fall/A_fall_0~5.png` + preview/frames

**A 64px 애니 4종 결정 확정 (2026-08-24) — Unity 반입은 별도 세션**
- **run** = seedA(9091) 6프레임 → `anim-run/A_run_0~5.png`
- **jump** = PixelLab seed8484, idx1·2·3 → `anim-jump/A_jump_0~2.png`
- **turn** = 몸 전체 회전(PixelLab), idx2=turn45 / idx6=front → `anim-turn/A_turn_frames.png`(+preview)
- **fall** = 위 8차-j (PixelLab seed1440 tilt idx0~5, 거리 구동)
- confirm/ 대기 큐 비움. 마스터 전부 `assets/a-64px/anim-*/`로 정렬
[2026-08-22 01:02] STYLE_GUIDE.md 수정됨
[2026-08-22 01:02] STYLE_GUIDE.md 수정됨
[2026-08-22 01:03] README.md 수정됨
[2026-08-24 16:09] A_Run.anim 수정됨
[2026-08-24 16:09] A_Run.anim 수정됨
[2026-08-24 16:09] A_Run.anim 수정됨

### 2026-08-24 — A 64px 애니 전면 반입 + B jump/fall 생성

- **A**: run/jump/fall/turn 64px 반입(idle 방식 = .meta 64px 단일화 + GUID 유지). fall은 거리비례 틸트 코드로 구동(fall_0 직립→fall_5 다이빙). turn은 프리뷰 시트 다운스케일 임시본(네이티브 재생성 필요)
- **B jump/fall**: PixelLab `animate_with_text_v3`, first_frame=B_idle_0. 1차 초안 2종이 idle성 흔들림으로 구분 안 돼 **과장·6프레임 재생성**(seed 7133/4288). 정점서 40%+ 차이 확보, 온모델 유지. 컨펌본 `confirm/b-jump-fall/`(프레임+시트+루프 GIF)
- ★ **GIF 인코딩 교훈**: 수제 GIF89a LZW 인코더는 버그로 디코더 거부(업로드 400) → **WPF `GifBitmapEncoder` + NETSCAPE 루프/딜레이 바이트 패치**가 안전한 경로
- PixelLab Tier 2, 이번 세션 4생성 사용(잔여 ~4,325)
[2026-09-03 13:46] ART_LOG.md 수정됨
[2026-09-04 14:15] README.md 수정됨
[2026-09-04 14:31] README.md 수정됨
[2026-09-04 14:49] README.md 수정됨

[2026-09-04 14:54] README.md 수정됨
[2026-09-04 15:00] README.md 수정됨

### 2026-09-04 — 그린 톤 튜토리얼 연결 맵 v1 (built-in ImageGen)

- **요청:** `docs/art/confirm/2026-09-04-pixellab-demo-green/`의 이미지와 README를 레퍼런스로 실제 튜토리얼 맵 이미지를 생성한다. 사용자의 명시적 정정에 따라 프롬프트 전달만 하지 않고 built-in ImageGen을 실행했다.
- **라우팅:** ORCHESTRATOR → Art. 기존 6개 룸의 비트를 한 장의 연결된 횡스크롤 컷어웨이 레벨로 통합했다.
- **템플릿 / 사례:** `architecture-space` (case 331) 주 템플릿 + `scene-storytelling` (case 330) 보조. ImageGen 분류 `stylized-concept`.
- **입력:** 도구의 5장 제한 때문에 `demo_01`, `demo_03`, `demo_04`, `demo_05`, `demo_06`을 직접 참조했다. `demo_02_first_corridor`의 평탄 바닥·낮은 턱·좌우 출구는 프롬프트 구조 지시로 반영했다.
- **프롬프트 핵심:** 개별 룸 보드나 콜라주가 아닌 하나의 실제 연결 지형. 깨어남 방 → 첫 통로 → 포드 놀이터 → 완만한 수직방·상단 숏컷 → 갈대 통로 → 밝은 거짓 출구. 하드 엣지 픽셀 아트, forced green-teal palette, 플레이면 세이지 림라이트, 비인간 유기 건축, 텍스트·UI·캐릭터 없음.
- **산출물:** `docs/art/assets/generated-concepts/maps/first-play-area/tutorial_map_green_reference_v1.png` (1680×945 PNG). 컨펌 후 대기함에서 승인 레퍼런스 위치로 이동했다.
- **사용자 승인:** ★ 맵·타일 아트 레퍼런스로 채택. 특히 「타일이 타일처럼 안 보이는」 이음새 은폐와, 플레이어가 타지 못하는 부분의 곡선·비선형 실루엣을 긍정했다.
- **승인 범위:** 플레이 가능 면은 명료하게 정돈하고, 비플레이 지형은 타일 격자를 무시한 큰 유기적 곡선·비반복 덩어리로 처리한다. 정확한 룸 배치·게이팅·비율과 팔레트 정식 등록은 아직 확정하지 않는다.
- **상태:** ✅ 스타일·표면 처리 정본 레퍼런스. 생산용 타일 에셋이나 확정 맵 구조는 아님.
[2026-09-04 15:14] README.md 수정됨

### 2026-09-04 — B 정착지 거목 캐노피 허브 v1 (built-in ImageGen)

- **요청:** 사용자 제공 이미지 3장을 B가 사는 마을 느낌의 레퍼런스로 삼아 실제 환경 이미지를 제작한다.
- **라우팅:** ORCHESTRATOR → Art. 기존 `2026-09-04-pixellab-b-village`의 실패 기록을 적용해 탑다운 타원 바닥·종이 표지판·문자·워터마크를 금지했다.
- **템플릿 / 사례:** `architecture-space` (case 331) 주 템플릿 + `scene-storytelling` (case 330) 보조. ImageGen 분류 `stylized-concept`.
- **입력:** 사용자 제공 거목 주거 3장 + 승인된 `tutorial_map_green_reference_v1.png`를 지형 표현 앵커로 사용했다.
- **프롬프트 핵심:** 거대한 살아 있는 나무 두 그루가 만든 엄격한 측면 시점 허브. 하층 공동 광장, 중층 포드 주거, 상층 엮은 현수 통로, 좌우 숲 출구. 플레이면은 명료한 림라이트, 비플레이 지형은 곡선·비선형·비반복 덩어리. 깊은 청록·이끼색에 앰버 점광원 10% 미만.
- **산출물:** `docs/art/confirm/2026-09-04-pixellab-b-village/village_06_tree_canopy_hub_imagegen_v1.png` (1680×945 PNG).
- **1차 검수:** 측면 시점·다층 동선·타일 격자 은폐는 성공. 일부 지붕·문·바구니·통은 인간식 숲 마을 문법에 가까워, 사용자 채택 시 허용 범위를 확인하거나 포드·씨앗 껍질·매듭 구조로 재생성한다.
- **상태:** 컨펌 대기. B의 고향/여관촌 설정과 정본 팔레트는 자동 확정하지 않는다.
[2026-09-04 15:20] README.md 수정됨

### 2026-09-04 — B 정착지 분위기 5종 병렬 탐색 (built-in ImageGen)

- **요청:** `village_06` 방향을 유지하면서 서로 다른 느낌의 B 마을 이미지 5장을 동시에 제작한다.
- **방식:** built-in ImageGen 5회 병렬 호출. `architecture-space` (case 331) 주 템플릿 + `scene-storytelling` (case 330) 보조, 분류 `stylized-concept`.
- **공통 입력:** 사용자 제공 거목 마을 레퍼런스 3장 + `village_06_tree_canopy_hub_imagegen_v1.png` + 승인된 `tutorial_map_green_reference_v1.png`.
- **공통 고정:** 엄격한 측면 시점, 캐릭터 없음, 플레이면 가독성, 비플레이 곡선 덩어리, 타일 격자 은폐, 청록·이끼색 + 앰버 10% 미만, 비인간 엮은 둥지 건축.
- **07 심장나무 공동광장:** `docs/art/confirm/2026-09-04-pixellab-b-village/village_07_heart_tree_commons_imagegen_v1.png` — 중심 거목 랜드마크와 다층 순환.
- **08 안개 현수교 지구:** `docs/art/confirm/2026-09-04-pixellab-b-village/village_08_mist_bridge_quarter_imagegen_v1.png` — 여백·안개·수평 탐색.
- **09 뿌리물길 골목:** `docs/art/confirm/2026-09-04-pixellab-b-village/village_09_rootwater_lane_imagegen_v1.png` — 습윤 수로·저층 생활권.
- **10 매달린 포드 공동:** `docs/art/confirm/2026-09-04-pixellab-b-village/village_10_hanging_pod_hollow_imagegen_v1.png` — 수직 둥지 군락과 상하 이동.
- **11 비 오는 밤 쉼터:** `docs/art/confirm/2026-09-04-pixellab-b-village/village_11_rain_night_shelter_imagegen_v1.png` — 비·어둠·따뜻한 피난처.
- **1차 판정:** 08은 동선 가독성, 10은 비인간 둥지 언어, 11은 정서가 특히 강하다. 07의 심장형 공동과 09·11의 인간식 생활 소품은 채택 시 조정 후보.
- **상태:** 5장 모두 컨펌 대기. 어떤 방향도 정본으로 자동 승격하지 않는다.

### 2026-09-04 — 07 하트 제거 + B 정착지 맵 우선 룸 3종 (built-in ImageGen)

- **사용자 피드백:** “하트는 좀 짜친다.” 07의 자연 공동이 문자 그대로의 하트 아이콘으로 굳어 공간보다 상징이 먼저 읽힌 것이 실패 원인이다.
- **가드레일 추가:** 나무 구멍·뿌리 아치·암벽 공동을 하트·얼굴·문장처럼 정면 대칭 상징으로 만들지 않는다. 비대칭 침식·성장 흔적으로 처리한다.
- **07 수정 방식:** `precise-object-edit`. v1의 구도·동선·조명·주거는 유지하고 중앙 하트 공동만 비대칭 뿌리 매듭과 자연 공동으로 교체했다.
- **07 수정 산출물:** `docs/art/confirm/2026-09-04-pixellab-b-village/village_07_old_root_commons_imagegen_v2.png` (1672×941 PNG). v1은 반려 사례로 보존한다.
- **신규 요청 해석:** 레퍼런스 일러스트와 닮은 분위기 변주가 아니라, 실제 게임에서 연결 가능한 맵 룸을 설계한다.
- **템플릿 / 사례:** `architecture-space` (case 331) + 맵 우선 공간 설계. ImageGen 분류는 신규 룸 3종 `stylized-concept`, 07 수정 `precise-object-edit`.
- **입력 분리:** 신규 룸 3종에는 사용자 제공 거목 레퍼런스와 기존 마을 이미지를 직접 입력하지 않았다. 프로젝트 STYLE_GUIDE의 그린 톤·비인간 유기 건축·타일 격자 은폐 규칙만 텍스트로 계승했다.
- **12 여관 척추:** `docs/art/confirm/2026-09-04-pixellab-b-village/village_map_12_fallen_root_inn_spine_imagegen_v1.png` — 낮고 긴 좌우 척추, 중앙 여관 대화면, 상단 분기와 하층 비밀.
- **13 수직 주거구:** `docs/art/confirm/2026-09-04-pixellab-b-village/village_map_13_split_reed_vertical_quarter_imagegen_v1.png` — 좌하단에서 우상단으로 상승하는 지그재그 주동선과 좌측 보조 루프.
- **14 수로 루프:** `docs/art/confirm/2026-09-04-pixellab-b-village/village_map_14_rootwater_service_loop_imagegen_v1.png` — 좌우 고지대를 중앙 저지대와 하층 수로가 잇는 U자 동선.
- **1차 검수:** 세 장의 대형 실루엣과 진행 축은 서로 충분히 다르다. 12는 여관 소품 비인간화, 13은 실제 점프 간격, 14는 물 위험·부서지는 바닥 메카닉을 블록아웃 단계에서 재검증한다.
- **상태:** 4장 모두 컨펌 대기. 이미지상의 발판 간격과 룸 연결은 생산 수치로 자동 확정하지 않는다.

### 2026-09-04 — B 정착지 네이티브 픽셀 배경 5종 (built-in ImageGen + deterministic pixel export)

- **사용자 피드백:** 직전 결과는 픽셀 아트가 아니라 고해상도 디지털 페인팅에 픽셀 질감만 얹힌 것으로 보인다. 12~14는 구도 참고용으로만 남기고 픽셀 결과물 판정에서는 반려한다.
- **요청:** 사용자가 선택한 뿌리 여관·거목 정착지 이미지 2장의 감각을 진짜 픽셀 배경으로 1장 만들고, 분위기가 다른 픽셀 배경 4장을 추가한다.
- **라우팅:** ORCHESTRATOR → Art. built-in ImageGen을 자산별 1회씩 사용했다.
- **템플릿 / 사례:** `architecture-space` (case 331) 주 템플릿 + `scene-storytelling` (case 330) 보조. 15는 `style-transfer`, 16~19는 `stylized-concept`.
- **직접 참조:** 15만 사용자 제공 이미지 2장을 참조했다. 16~19는 구도 복제를 피하려 직접 이미지 입력 없이 STYLE_GUIDE의 비인간 유기 건축·측면 시점·타일 격자 은폐 규칙을 프롬프트로 전달했다.
- **생성 프롬프트 픽셀 규칙:** 416×234 논리 캔버스, 24~32색, 1px 다크 아웃라인, 계단식 곡선, 하드 픽셀 클러스터, 광원 3~4단. 안티앨리어싱·연속 그라데이션·소프트 글로우·회화 브러시·고해상도 미세 질감 금지.
- **결정론적 출력:** built-in 원본을 중앙 1664×936으로 크롭한 뒤 nearest-neighbor로 416×234에 축소하고, 32색·디더링 없음의 PNG로 저장했다. 이 네이티브 파일을 nearest-neighbor 4배로 확대한 1664×936 미리보기도 함께 저장했다.
- **도구:** `tools/pixelize-imagegen-background.mjs` (bundled Sharp 사용).
- **15 청록 뿌리 여관:** `village_pixel_15_root_inn_teal_native_416x234.png` / `village_pixel_15_root_inn_teal_preview_4x.png`.
- **16 비바람 현수로:** `village_pixel_16_rain_cord_canopy_native_416x234.png` / `village_pixel_16_rain_cord_canopy_preview_4x.png`.
- **17 포자우물의 아침:** `village_pixel_17_sporewell_dawn_native_416x234.png` / `village_pixel_17_sporewell_dawn_preview_4x.png`.
- **18 마른 씨앗껍질 단구:** `village_pixel_18_amber_husk_autumn_native_416x234.png` / `village_pixel_18_amber_husk_autumn_preview_4x.png`.
- **19 달빛 갈대 습지:** `village_pixel_19_moon_reed_marsh_native_416x234.png` / `village_pixel_19_moon_reed_marsh_preview_4x.png`.
- **1차 검수:** 5장 모두 동일 크기 픽셀·무안티앨리어싱·제한 팔레트 조건을 충족한다. 15는 기존 정착지 정서, 16은 사선 동선, 17은 밝은 안개, 18은 건조한 계절색, 19는 저층 수로 실루엣으로 분리된다.
- **상태:** 컨펌 대기. 선택 전에는 416×234와 개별 팔레트를 프로젝트 정본으로 승격하지 않는다.

### 2026-09-04 — B 정착지 동일 팔레트 지형 변형 4종 v2

- **사용자 정정:** 이전 요청의 “다른 분위기”는 팔레트·날씨·계절을 다르게 하라는 뜻이 아니었다. 15의 색감과 지역 분위기는 유지하고 지형을 다르게 구성하는 것이 의도였다.
- **오류 판정:** 16~19 v1은 픽셀 규격은 맞지만 비·옅은 아침·늦가을·달빛으로 색온도와 조명을 바꿔 같은 구역 묶음으로 실패했다. 삭제하지 않고 반려 탐색 자료로 보존한다.
- **재생성 고정값:** `village_pixel_15_root_inn_teal_preview_4x.png`를 네 장 모두의 유일한 스타일·팔레트·조명 레퍼런스로 입력했다. 건조한 황혼, 청록 배경, 이끼·세이지 지형, 희소한 앰버 조명은 고정했다.
- **재생성 변경값:** 지형과 동선만 수직 협곡 / 저층 수로 / 사선 스위치백 / 비대칭 돔형 공동으로 분리했다.
- **템플릿 / 사례:** `architecture-space` (case 331) 주 템플릿 + `scene-storytelling` (case 330) 보조. ImageGen 분류 `style-transfer`.
- **후처리:** `tools/pixelize-imagegen-background.mjs`에 선택 팔레트 입력 기능을 추가했다. 네 v2의 모든 출력색을 15 네이티브 마스터의 동일 32색에 최근접 매핑하고 416×234 네이티브 + 1664×936 nearest-neighbor 미리보기로 저장했다.
- **16 v2:** `village_pixel_16_root_cleft_ascent_v2_native_416x234.png` / `village_pixel_16_root_cleft_ascent_v2_preview_4x.png`.
- **17 v2:** `village_pixel_17_rootwater_undercroft_v2_native_416x234.png` / `village_pixel_17_rootwater_undercroft_v2_preview_4x.png`.
- **18 v2:** `village_pixel_18_moss_switchback_bank_v2_native_416x234.png` / `village_pixel_18_moss_switchback_bank_v2_preview_4x.png`.
- **19 v2:** `village_pixel_19_hollow_ring_crossroads_v2_native_416x234.png` / `village_pixel_19_hollow_ring_crossroads_v2_preview_4x.png`.
- **1차 검수:** 15와 네 v2는 동일 팔레트·조명 묶음으로 읽힌다. 차이는 대형 지형 실루엣과 동선 방향에 한정된다.
- **상태:** 컨펌 대기. 15 팔레트를 프로젝트 정본으로 자동 승격하지 않는다.

### 2026-09-04 — B 정착지 직선 플레이면 + 밝은 팔레트 5종 v3

- **사용자 정정:** 플레이어가 밟는 면까지 곡선이면 실제 충돌 지형과 착지 가능 구간이 모호하다. 걷기·착지 상단은 선형으로 두고, 비선형 실루엣은 천장·벽타기가 불가능한 벽·타일 아랫부분에만 사용한다. 색감은 새로 제공한 밝은 거목 다리 이미지에 맞춘다.
- **라우팅:** ORCHESTRATOR → Art. built-in ImageGen으로 5개 맵 룸을 생성하고 21번의 세로 비율 및 처진 로프 다리만 추가 교정했다.
- **템플릿 / 사례:** `architecture-space` (case 331) 주 템플릿 + `scene-storytelling` (case 330) 보조. ImageGen 분류 `style-transfer`; 21의 로프 제거는 `precise-object-edit`.
- **입력 역할:** 사용자 제공 밝은 거목 다리 이미지는 팔레트·명도·조명 전용, `docs/art/assets/generated-concepts/maps/first-play-area/tutorial_map_green_reference_v1.png`는 비플레이 지형의 유기적 경계 처리 전용으로 사용했다. 두 입력의 룸 토폴로지는 복제하지 않았다.
- **충돌면 가드레일:** 모든 걷기·착지 상단은 수평 직선, 모든 벽타기 면은 수직 직선, 90도 턱을 갖는다. 경사·아치·처진 로프·곡선 뿌리 상단은 플레이 경로로 금지한다. 곡선은 천장·비등반 오버행·발판 아랫부분·전후경에만 허용한다.
- **공통 팔레트:** 사용자 이미지에서 32색 앵커 `village_pixel_bright_reference_palette_native_416x234.png`를 만들고, 20~24의 모든 픽셀을 이 팔레트에 최근접 매핑했다. 밝은 세이지·셀라돈 안개, 깊은 청록, 네이비 외곽, 희소한 주황 조명이 전 장면에 동일하다.
- **결정론적 출력:** 각 원본을 중앙 1664×936으로 크롭한 뒤 nearest-neighbor로 416×234에 축소하고, 동일 32색·디더링 없음으로 저장했다. 4배 미리보기는 1664×936이다.
- **20 밝은 여관 훈련실:** `village_pixel_20_bright_inn_training_hall_v3_native_416x234.png` / `village_pixel_20_bright_inn_training_hall_v3_preview_4x.png`.
- **21 수직 상승 뜰:** `village_pixel_21_bright_vertical_climb_court_v3_native_416x234.png` / `village_pixel_21_bright_vertical_climb_court_v3_preview_4x.png`.
- **22 중앙 다리 공동:** `village_pixel_22_bright_bridge_commons_v3_native_416x234.png` / `village_pixel_22_bright_bridge_commons_v3_preview_4x.png`.
- **23 낙하 루프 하층부:** `village_pixel_23_bright_drop_loop_undercroft_v3_native_416x234.png` / `village_pixel_23_bright_drop_loop_undercroft_v3_preview_4x.png`.
- **24 층계 우물 교차로:** `village_pixel_24_bright_stepwell_crossroads_v3_native_416x234.png` / `village_pixel_24_bright_stepwell_crossroads_v3_preview_4x.png`.
- **검수:** 다섯 네이티브 모두 416×234, 32색, 공통 팔레트 밖 색상 0개. 플레이면 직선과 비플레이 유기적 밑면이 분리되어 읽힌다.
- **상태:** 컨펌 대기. 사용자 승인 전까지 밝은 32색 앵커와 20~24의 맵 구조를 프로젝트 정본으로 자동 승격하지 않는다.

### 2026-09-04 — B 정착지 타일·배경 통합 수정 v4

- **사용자 피드백:** 20의 타일과 배경이 분리되어 보이고 ㄴ자 타일이 돌출된 것처럼 읽힌다. 21의 하단 독립 공중 발판은 다소 뜬금없다. 22의 배경과 전반 구성은 매우 좋다. 23·24의 수직 벽 타일은 맥락 없이 갑자기 생긴 오류처럼 보인다.
- **수정 범위:** 20·21·23·24만 built-in ImageGen `precise-object-edit`로 수정하고, 승인된 22 v3는 그대로 유지했다.
- **템플릿 / 사례:** `architecture-space` (case 331)로 지형의 물리적 연결과 공간 기능을 고정하고, `scene-storytelling` (case 330)로 생활감 있는 배경 불변값을 유지했다.
- **프롬프트 세트 핵심:** 20은 반복 석재 캡과 ㄴ자 기둥을 연속된 거목·뿌리 재질로 교체. 21은 하단 중앙 독립 발판만 삭제. 23은 좌측·중앙 수직 벽을 상·하 지형에 고정된 뿌리 버트레스로 교체. 24는 양쪽 회색 벽 타일 스트립을 천장·바닥·측면 거목에서 이어지는 뿌리 절벽으로 교체.
- **새 가드레일:** 직선 충돌면을 표현하기 위해 반복 사각 캡·회색 벽 블록·독립 ㄴ자 기둥을 노출하지 않는다. 나무결·이끼·크랙은 가상의 타일 경계를 가로지르고, 수직 벽은 반드시 천장·바닥·거목에 시각적으로 고정한다.
- **불변값:** 기존 룸 토폴로지, 밝은 세이지·청록·주황 팔레트, 416×234 네이티브 픽셀 규격, 수평 착지면, 수직 벽타기 면, 비선형 비플레이 밑면.
- **20 v4:** `village_pixel_20_integrated_inn_training_hall_v4_native_416x234.png` / `village_pixel_20_integrated_inn_training_hall_v4_preview_4x.png`.
- **21 v4:** `village_pixel_21_clean_vertical_climb_court_v4_native_416x234.png` / `village_pixel_21_clean_vertical_climb_court_v4_preview_4x.png`.
- **22:** `village_pixel_22_bright_bridge_commons_v3_native_416x234.png` / `village_pixel_22_bright_bridge_commons_v3_preview_4x.png` 유지.
- **23 v4:** `village_pixel_23_rooted_drop_loop_undercroft_v4_native_416x234.png` / `village_pixel_23_rooted_drop_loop_undercroft_v4_preview_4x.png`.
- **24 v4:** `village_pixel_24_rooted_stepwell_crossroads_v4_native_416x234.png` / `village_pixel_24_rooted_stepwell_crossroads_v4_preview_4x.png`.
- **검수:** 수정된 네 장 모두 네이티브 416×234, 동일 32색 앵커 밖 색상 0개. 20의 ㄴ자 기둥, 21의 하단 독립 발판, 23·24의 독립 벽 타일이 제거되었고 기능 충돌면은 유지됐다.
- **상태:** 컨펌 대기. 22의 배경 방향은 사용자 긍정 평가로 유지하지만, 세트 전체를 정본으로 확정한 것은 아니다.

### 2026-09-04 — B 정착지 고밀도 픽셀·타일·프롭 제작 키트 v1

- **사용자 판정:** 직전 수정본의 1·2번은 품질이 낮아 폐기. 416×234·32색 강제 후처리 이후 픽셀 덩어리가 커지고 디테일이 뭉개진 점을 실패 원인으로 확정했다. `village_pixel_20_integrated_inn_training_hall_v4_*`, `village_pixel_21_clean_vertical_climb_court_v4_*`는 반려 기록으로만 보존한다.
- **요청:** 새로 첨부한 세 장을 스타일 기준으로 삼아 세 번째 이미지를 고밀도 픽셀로 업스케일하고, 타일맵 지형 모듈과 프롭을 제작한다.
- **라우팅:** ORCHESTRATOR → Art. built-in ImageGen 3회 병렬 실행.
- **템플릿 / 사례:** `architecture-space` (case 331) 주 템플릿 + `scene-storytelling` (case 330) 보조. 장면·아틀라스 모두 ImageGen 분류 `style-transfer`.
- **입력 역할:** 사용자 이미지 3은 장면 업스케일의 구도·토폴로지 편집 대상. 이미지 1은 높은 픽셀 밀도·등불·정착지 프롭 기준, 이미지 2는 수평 발판·거목 프레임·밝은 숲 배경 기준. 신규 타일·프롭에는 세 장 모두를 공통 스타일 입력으로 사용했다.
- **장면 프롬프트 핵심:** 이미지 3의 지형·출구·등불 위치를 유지하고 논리 832×468에서 모든 윤곽을 재픽셀링. 작은 나무결·이끼·잔뿌리·금속 픽셀을 추가하되 안티앨리어싱과 회화식 스무딩은 금지.
- **타일 프롬프트 핵심:** 수평 플레이 캡, 바크 필, 끝단·코너, 수직 벽, 벽-바닥/천장 접합, 뿌리 버트레스, 비플레이 천장·밑면, 데코 오버레이를 투명 시트로 생성. 반복 석재 캡·독립 ㄴ자 벽·회색 벽 블록 금지.
- **프롭 프롬프트 핵심:** 6×4 배열에 등불·포드·문틀·난간·생활 소품·식생 24종을 격리. 인간식 가구·문자·종이 표지·하트·얼굴 금지.
- **고밀도 후처리:** `tools/pixelize-imagegen-asset-hd.mjs`를 추가했다. 임의 네이티브 크기, 실제 색상 수 검증, 참조 팔레트 매핑, 밝은 중성 체크무늬 제거, 0/255 알파 고정, nearest-neighbor 미리보기를 지원한다.
- **팔레트:** `production-kit-v1/b_village_hd_palette_reference_64_v2_native_832x468.png` — 실제 opaque 61색.
- **고밀도 장면:** `production-kit-v1/b_village_room_23_hd_native_832x468.png` / `production-kit-v1/b_village_room_23_hd_preview_2x.png`.
- **지형 모듈:** `production-kit-v1/b_village_terrain_modules_native_627x627.png` / `production-kit-v1/b_village_terrain_modules_preview_2x.png`.
- **프롭:** `production-kit-v1/b_village_props_native_768x512.png` / `production-kit-v1/b_village_props_preview_2x.png`.
- **검수:** 장면 832×468·61색. 지형 시트 627×627·59색·투명 픽셀 206,961개. 프롭 시트 768×512·58색·투명 픽셀 322,643개. 세 결과 모두 공통 팔레트 밖 색상 0개, 반투명 픽셀 0개.
- **제한:** 지형 시트의 개별 모듈은 유용하지만 ImageGen이 요청한 정확한 8×8 셀 수를 지키지 않았다. 현 단계는 모듈 선택용 컨셉 시트이며 Unity 자동 슬라이스 정본으로 표기하지 않는다.
- **상태:** 컨펌 대기. 고밀도 832×468·61색 규격과 개별 타일/프롭은 사용자 승인 후에만 정본으로 승격한다.

### 2026-09-04 — B 정착지 레퍼런스별 제작 키트 v2 (built-in ImageGen)

- **요청:** 사용자 제공 세 이미지를 기준으로 전용 타일을 제작한다. 1번에는 배경을 추가하고, 3번은 먼저 1·2번의 밝은 색감에 맞춘 다음 타일과 프랍을 제작한다.
- **라우팅:** ORCHESTRATOR → Art. built-in ImageGen 6회 사용: 1번 타일, 1번 배경, 2번 타일, 3번 색감 교정, 3번 타일, 3번 프랍.
- **템플릿 / 사례:** `architecture-space` (case 331) 주 템플릿 + `scene-storytelling` (case 330) 보조.
- **ImageGen 분류:** 타일·프랍 `style-transfer`, 1번 배경 `background-extraction`, 3번 색감 교정 `lighting-weather`.
- **참조 역할:** 1번은 뿌리·토양·이끼 지형과 안개 숲 배경, 2번은 나무 판재·거목 기둥·로프 하부 구조, 3번은 거대한 뿌리 여관의 구조·생활 소품 원본으로 분리했다. 세 구조를 한 시트에 혼합하지 않았다.
- **3번 색감 프롬프트 핵심:** 원래 룸 토폴로지, 출구, 가구, 화로와 등불 위치를 고정하고 어두운 청록·황금색 대비를 밝은 세이지·셀라돈 안개와 깊은 청록으로 이동한다. 따뜻한 앰버는 소형 광원에만 남긴다.
- **타일 프롬프트 핵심:** 각 시트 6×4, 24종. 걷기·착지 상단은 수평, 벽타기 면은 수직, 유기적 곡선은 밑면·천장·비등반 외벽·장식에만 허용한다. 반복 가능한 접합선, 독립 모듈 간 간격, 투명 배경을 요구했다.
- **프랍 프롬프트 핵심:** 3번 보정본에서 등불 6종, 건축 6종, 생활 6종, 식생·장식 6종을 분리했다. 문자·캐릭터·하트·얼굴·현대 물건은 금지했다.
- **후처리:** `tools/pixelize-imagegen-asset-hd.mjs`의 중성 배경 제거 범위를 옅은 접촉 그림자까지 확장했다. `tools/inspect-png-asset.mjs`를 추가해 크기, opaque 색상 수, 0/255 알파, 참조 팔레트 밖 색상을 검증했다.
- **1번 산출물:** `production-kit-v2-by-reference/ref01_terrain_tiles_native_768x512.png`, `ref01_background_native_832x468.png`.
- **2번 산출물:** `production-kit-v2-by-reference/ref02_terrain_tiles_native_768x512.png`.
- **3번 산출물:** `production-kit-v2-by-reference/ref03_color_matched_master_native_832x468.png`, `ref03_terrain_tiles_native_768x512.png`, `ref03_props_native_768x512.png`.
- **검수:** 1번 타일 64색·투명 289,186px, 1번 배경 39색, 2번 타일 58색·투명 292,899px, 3번 보정본 64색, 3번 타일 64색·투명 281,890px, 3번 프랍 64색·투명 299,214px. 전 파일 반투명 픽셀 0개, 대응 팔레트 밖 색상 0개.
- **제한:** 6×4 배열은 모듈 선택에 충분히 명확하지만 Unity용 32×32 자동 슬라이스 정본은 아니다. 채택 모듈은 셀 크기·피벗·심리스 접합·콜라이더를 후속 패킹 단계에서 보정한다.
- **상태:** 컨펌 대기. 세 레퍼런스의 전용 지형 문법과 3번의 색감 교정 방향은 사용자 승인 후 정본 후보로 승격한다.
[2026-09-04 17:39] README.md 수정됨
[2026-09-04 17:51] README.md 수정됨
[2026-09-04 17:58] README.md 수정됨
