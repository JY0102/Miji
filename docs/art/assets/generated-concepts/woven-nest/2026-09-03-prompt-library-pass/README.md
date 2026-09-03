# Woven Nest Prompt-Library Pass — 2026-09-03

> 탐색용 콘셉트다. 사용자 승인 전까지 정본·Unity-ready 에셋이 아니다.

## 생성 메타데이터

| 결과물 | GPT-Image2 Style Library 템플릿 | 예시 case | 상태 |
|---|---|---|---|
| `map_root_loom_crossing_v1.png` | `architecture-space` + `scene-storytelling` | case 331, case 330 | 룸 구도 탐색본 |
| `map_lantern_silt_well_v1.png` | `architecture-space` + `scene-storytelling` | case 331, case 330 | 룸 구도 탐색본 |
| `props_everyday_16_v1_rgb-checkerboard.png` | `concept-product-breakdown`의 분리·배열 규칙 | case 370, case 361 | 소품 콘셉트 시트, RGB 체크무늬 배경 |
| `props_folk_machines_12_v1_rgb-checkerboard.png` | `concept-product-breakdown`의 분리·배열 규칙 | case 370, case 361 | 소품 콘셉트 시트, RGB 체크무늬 배경 |

## 공통 기준

- 스타일 우선순위: `STYLE_GUIDE.md` > `ART_LOG.md` 승인·실패 기록 > 외부 템플릿.
- 참조 이미지는 스타일·팔레트·밀도·스케일 앵커로만 사용했다. 구도는 새로 생성했다.
- 맵은 엄격한 횡스크롤 측면 시점, 유기적 비인간 건축, 단순 민속 기계, 차분한 중앙 플레이 레인을 요구했다.
- prop은 한 시트에 개별 오브젝트를 분리하고 A 64px = 1유닛을 스케일 앵커로 삼았다.
- 두 prop 시트는 실제 알파 요청과 배경 제거 재시도에도 `Format24bppRgb` 체크무늬로 저장됐다. 투명 배경이 아니므로 그대로 Unity에 임포트하지 않는다.

## Prompt 1 — Root-Loom Crossing

```text
Use case: stylized-concept
Asset type: original 2D metroidvania playable room concept, Woven Nest exploration draft
Input images: Image 1 is the approved Woven Nest palette, materials, pixel density, organic woven-root architecture, and melancholy mood reference. Image 2 is the gameplay scale and platform-readability reference. Use both only as style and scale references; do not copy either composition.

Primary request: Create an original side-view room called Root-Loom Crossing: a wide traversal chamber built by a non-human species from interlaced roots, reeds, knotted fibers, hollow seed shells, and a few hand-worn brass fittings. The room should feel inhabited but quiet and abandoned recently, never like a human house.
Scene/backdrop: deep teal forest-cavern depth behind suspended woven nest pods and broad root arches. A low main ground path crosses the room; two short optional upper branches and one small lower nook create an accordion-like expand-and-return exploration rhythm.
Subject: clearly readable playable terrain: one continuous grounded route, three short root-bridge platforms, one tall woven support mass near an outer third, and a subtle one-way-looking return shortcut implied by a descending root loop. Decorative hanging lanterns and cords must be sparse and irregular, not evenly spaced.
Style/medium: crisp dense indie pixel art matching the reference images; hard pixel clusters; 1-pixel dark outline language; limited muted palette; no anti-aliasing; only restrained dithering. This is a finished environment concept frame, not a painting and not a greybox.
Composition/framing: strict orthographic side view, 16:9 landscape, single-room game-camera framing comparable to about 688x384 at PPU 32. Keep the central play lane calm and uncluttered. Separate planes clearly: distant background darkest and desaturated, mid architecture subdued, interactive ground and platform rims slightly warmer and brighter.
Lighting/mood: melancholic, hushed, lived-in; dim cyan ambient depth with a few restrained amber lantern accents. No bloom or soft glow; any light stays inside crisp pixel clusters.
Color palette: dark teal, blue-black, muted olive, weathered root brown, dusty brass, tiny cyan and amber accents.
Materials/textures: braided root caps, reed lattice, tied fibers, old brass collars, sparse moss. Favor large readable shapes over noisy micro-detail.
Constraints: original design; readable silhouettes; collision surfaces visually cleaner than decoration; props scaled below the player-height reference; no text, labels, UI, characters, logo, or watermark.
Avoid: human wooden houses, square doors or windows, gothic cathedral motifs, advanced sci-fi, sleek machinery, neon, magical crystal ruins, symmetrical decoration, repeated wallpaper patterns, evenly spaced lantern rows, busy center lane, painterly blur, gradients, smooth vector edges, anti-aliasing, excessive particles, weapons, gore.
```

## Prompt 2 — Lantern-Silt Well

```text
Use case: stylized-concept
Asset type: original 2D metroidvania playable room concept, Woven Nest exploration draft
Input images: Image 1 is the freshly generated Woven Nest style anchor for palette, materials, organic construction, and pixel density. Image 2 is the gameplay-scale and platform-readability reference. Create a distinct new room; do not copy either layout.

Primary request: Create an original side-view room called Lantern-Silt Well: a tall-feeling chamber inside the Woven Nest, framed within one 16:9 game screen. A shallow dark pool or damp silt bed occupies the lowest band, while curved root ledges rise in a loose zigzag toward a high exit. The architecture is woven by a non-human species from roots, reeds, knotted cord, seed-shell floats, and weathered folk-machine fittings.
Scene/backdrop: deep teal forest hollow with large quiet negative-space pockets. One distant hollow trunk silhouette, a few suspended nest pods, and faint vertical root strands establish depth without filling the screen.
Subject: readable traversal geometry: solid lower entrance ledge, three ascending platforms of different lengths, one broad mid-level rest platform, and one return ledge that visually reconnects toward the entrance. Platform tops must be clean and strongly readable. Keep hazardous-looking water/silt entirely below the play lane and do not invent a game mechanic.
Style/medium: crisp dense indie pixel art matching Image 1; hard pixel clusters, 1-pixel dark outline language, limited muted palette, no anti-aliasing, restrained dithering only. Finished environment concept frame, not painterly concept art and not a schematic.
Composition/framing: strict orthographic side view, 16:9 landscape, single-room camera. Let 35-45% of the traversable chamber remain visually calm. Put detailed architecture near the edges and behind the route; keep the center route readable. Distant background darkest/desaturated, midground subdued, playable edges warmer and brighter.
Lighting/mood: quiet damp dusk, melancholy and lived-in. One cyan utility lantern low in the room and two small amber lamps at different heights, irregular spacing. No bloom; crisp local light pixels only.
Color palette: blue-black, dark teal, muted moss olive, root brown, aged brass, tiny cyan and amber accents.
Materials/textures: woven reed lattice, damp roots, tied fiber, old copper/brass collars, sparse moss and water-darkened wood-like organic surfaces.
Constraints: original layout; no characters; no text; no UI; no logo; no watermark; props smaller than player height; clear collision silhouettes; asymmetrical composition.
Avoid: human architecture, square doors/windows, gothic cathedral arches, advanced sci-fi, magic crystals, neon, symmetrical tower layout, evenly spaced lanterns, wallpaper repetition, busy central silhouette, soft blur, smooth painted gradients, vector edges, anti-aliasing, excessive particles, weapons, gore.
```

## Prompt 3 — Everyday Props ×16

```text
Use case: stylized-concept
Asset type: 2D pixel-art game prop exploration sheet with genuinely transparent background
Input images: Image 1 is the approved Woven Nest environment style anchor. Image 2 is the 64x64 hero scale anchor: one hero equals 1 world unit. Image 3 is an older prop-direction reference; retain its rustic material vocabulary but correct its oversized proportions and keep every new prop isolated.

Primary request: Generate exactly 16 distinct small everyday Woven Nest environmental props, arranged in a clean 4-by-4 grid with wide fully transparent gutters. The objects are: (1) tiny amber hanging lantern, (2) tiny cyan maintenance lantern, (3) tied reed basket, (4) shallow woven seed tray, (5) hollow seed-shell jar, (6) coiled fiber rope, (7) wooden-root cord spool, (8) bundled drying reeds, (9) knotted route charm, (10) small shell wind chime, (11) mossy low woven rest pad, (12) root peg cluster, (13) folded leaf rain flap, (14) seed-shell drinking vessel, (15) compact repair pouch with brass clasp, (16) small irregular nest-wall patch.
Scene/backdrop: no scene and no floor; actual transparent alpha behind and between all props.
Subject: each prop is a separate reusable sprite concept with a complete silhouette and no contact with neighboring cells.
Style/medium: crisp dense indie pixel art matching Image 1; hard pixel clusters; 1-pixel dark outline language; limited muted palette; no anti-aliasing; no soft shading; only minimal dithering.
Composition/framing: strict 4x4 evenly sized cells, one object centered per cell, generous transparent margin. Do not draw visible grid lines, labels, numbers, or cell backgrounds.
Scale: most props should read as 16x16 or 32x32 logical-pixel assets at the project's density. No prop may exceed about 0.6 of Image 2's character height; the small wind chime may be slightly taller only because of its string. Keep pixel scale consistent across the sheet.
Lighting/mood: subdued, hand-worn, quiet and lived-in. Amber/cyan light appears only as a few crisp emissive pixels inside the two lanterns, with no halo.
Color palette: dark brown outline, muted root brown, olive moss, dull reed tan, oxidized brass, tiny cyan and amber accents.
Materials/textures: braided fibers, woven reeds, hollow seed husks, weathered brass fasteners, sparse moss.
Constraints: exactly 16 props; each visually distinct; actual transparent background; clean separation; original designs; no text; no logo; no watermark; no characters; no cast shadows.
Avoid: oversized props, human furniture, modern manufactured goods, advanced sci-fi, polished steel, magic crystals, neon, weapons, identical lantern variants, extra objects, decorative frames, inventory UI, labels, numbers, checkerboard transparency pattern, black or white background, glow bloom, gradients, smooth vector edges, anti-aliasing, blurry pixels.
```

## Prompt 4 — Folk Machines ×12

```text
Use case: stylized-concept
Asset type: 2D pixel-art game prop exploration sheet with genuinely transparent background
Input images: Image 1 is the approved Woven Nest environment style anchor. Image 2 is the companion everyday-prop sheet whose pixel scale, outline, palette, and clean spacing must be matched. Image 3 is the 64x64 hero scale anchor: one hero equals 1 world unit.

Primary request: Generate exactly 12 distinct small rustic folk-machine environmental props for the Woven Nest, arranged in a clean 4-column by 3-row grid with wide fully transparent gutters. These are visual set dressing and do not establish new gameplay mechanics. The objects are: (1) compact root-and-brass hand pump, (2) small rope winch, (3) low crank post, (4) knotted wooden pulley block, (5) seed-shell counterweight, (6) root-bound pipe elbow, (7) capped pipe vent, (8) compact weathered pressure vessel, (9) hook-and-ring hanger, (10) woven cable junction, (11) cracked brass dial gauge, (12) folded reed maintenance barrier.
Scene/backdrop: no scene and no floor; actual transparent alpha behind and between every prop.
Subject: every device is a separate reusable prop concept with a complete readable silhouette. Construction should look repaired repeatedly by small non-human inhabitants using cord, roots, reed bands, aged brass, ceramic or seed-shell bodies.
Style/medium: crisp dense indie pixel art exactly consistent with Image 2; hard pixel clusters; 1-pixel dark outline language; limited muted palette; no anti-aliasing; restrained dithering only.
Composition/framing: strict 4x3 cells, one object centered per cell, generous transparent margins. Do not draw visible grid lines, labels, numbers, or cell backgrounds.
Scale: each prop should read as a 24x24 to 48x48 logical-pixel asset at the project's density. Most must remain below 0.75 of Image 3's character height; only the narrow capped vent may approach that height. Keep pixel scale consistent across all 12.
Lighting/mood: old, practical, quiet, hand-worn. At most two devices may contain one or two crisp cyan or amber indicator pixels; no halos.
Color palette: blue-black and dark-brown outlines, muted root brown, dull olive, oxidized brass, smoky ceramic, tiny cyan/amber accents.
Materials/textures: braided fiber lashings, carved roots, dented brass collars, patched seed-shell vessels, sparse moss and grime.
Constraints: exactly 12 isolated props; actual transparent background; visually distinct silhouettes; original designs; no text; no logo; no watermark; no characters; no cast shadows.
Avoid: advanced sci-fi, sleek machinery, steampunk ornament overload, human industrial factory equipment, polished steel, clean modern gauges, magic crystals, neon, weapons, extra objects, repeated variants, decorative frames, inventory UI, labels, numbers, checkerboard pattern baked into the image, black or white background, glow bloom, smooth gradients, vector edges, anti-aliasing, blurry pixels.
```

