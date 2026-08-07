# 세계 기술 수준 비교 — 컨셉 프롬프트 3종

**날짜:** 2026-08-07
**목적:** 세계의 기술 수준을 정하기 위해 **같은 장면을 기술 수준만 바꿔** 3장 생성, 눈으로 비교한다.
**사용법:** 아래 각 프롬프트를 ChatGPT / DALL-E 이미지 생성에 그대로 붙여넣기 → 3장을 나란히 놓고 비교.
**연관:** `STYLE_GUIDE.md`, `ART_LOG.md`(2026-08-04 리부트 컨셉과 A/B 실루엣 연속성 유지)

---

## 비교의 규칙 — 변수는 "기술 수준" 하나만

세 프롬프트는 **장면·구도·톤·A/B 실루엣·픽셀 스타일을 전부 고정**하고, 오직 **"기계가 이 세계에서 어떻게 읽히는가"만** 바꾼다. 그래서 나온 차이는 순수하게 기술 수준의 차이다.

**고정 요소 (셋 다 동일):**
- 장면: 쓸쓸한 여행길가의 여관 앞 (여관 주인 = 초반 튜토리얼 인물). 측면 메트로배니아 구도
- A/B 스케일 실루엣: A = 시안 렌즈 하나 + 스위치 + 저상 크롤러/롤러 몸통 / B = 잎귀 + 새챌을 멘 작고 호기심 많은 어린 생명체
- 픽셀 아트 · 1px 다크 아웃라인 · 저채도 소수 팔레트 · 16x16 타일 언어 · 텍스트/UI/워터마크 없음

**판단 포인트:** 세 장을 보며 물어라 — *"A(작은 기계) 하나가 이 세계에서 시시한 물건으로 보이나(㉮), 신비한 유물로 보이나(㉯), 흔한 기술 중 하나로 묻히나(㉰)?"* 우리 설정(`전설 속 A의 자리 = 없다`, `무의미를 반전으로 연출 금지`)은 **A가 시시해야** 지켜진다.

---

## ㉮ 소박한 기계 + 느린 여행 공존  (권고)

> **한 줄:** 걸어서 여행하는 전근대 세계인데, 간단한 생활 기계는 그냥 일상 물건이다.
> **볼 것:** A 같은 기계가 나무·돌·천 사이에 자연스럽게 섞여 "당연한 도구"로 읽히는가. 낡았지만 작동하고, 아무도 신기해하지 않는가.

```text
Pixel art Metroidvania area screenshot reference, side view. A quiet wayside inn on a lonely travel road at dusk. The world is pre-modern and slow: people travel on foot, an inn with a hanging lantern and a wooden signpost, a stone well, stacked firewood, cloth awnings. BUT simple everyday utility machines are an ordinary, unremarkable part of daily life, blending naturally with the wood, stone and cloth: a worn mechanical hand-pump at the well, a small clockwork lantern, a battered little household maintenance machine doing chores near the door, a simple mechanical signboard. The machines look mundane, well-used, lived-in — ordinary tools, not miracles and not ruins; nobody treats them as special. Include tiny readable scale-cue silhouettes only: A, a small compact switch-bearing robot with one round cyan lens and a low crawler/roller base, and B, a small curious young organic wanderer with leaf-like ear fins and a satchel, passing by the inn. Melancholic, lonely, lived-in mood. Crisp retro pixel art, 16x16 tile language, 1px dark outline, limited low-saturation palette, no text, no labels, no UI, no minimap, no blur, no watermark.
```

---

## ㉯ 몰락한 문명의 잔재 (기계 = 유물)

> **한 줄:** 전근대 사람들이 무너진 선진 문명의 뼈대 사이에서 산다. 기계는 아무도 이해 못 하는 유물.
> **볼 것:** 분위기는 세지만 — A 같은 기계가 **신비한 유물/성물**로 보이지 않는가? 그러면 "A는 왜 이런 기능이 있지"가 떡밥이 되어 `무의미 반전 금지`를 위반하고, 루브릭 `멸망한 고대 문명 유적 🔴포화` 클리셰에 걸린다.

```text
Pixel art Metroidvania area screenshot reference, side view. A quiet wayside inn built into the bones of a collapsed advanced civilization. The living people are pre-modern folk with lanterns, cloth and foot-travel, dwarfed by huge broken machine-ruins: dead metal towers, toppled pylons, half-buried mysterious devices no one understands anymore. A single still-working machine is set apart like a strange relic or roadside shrine, roped off and revered. Overgrown rusted technology reclaimed by moss and stone, a mood of awe and unease before a lost golden age. Include tiny readable scale-cue silhouettes only: A, a small compact switch-bearing robot with one round cyan lens and a low crawler/roller base, and B, a small curious young organic wanderer with leaf-like ear fins and a satchel, dwarfed by the ruins. Melancholic, haunted mood. Crisp retro pixel art, 16x16 tile language, 1px dark outline, limited low-saturation palette, no text, no labels, no UI, no minimap, no blur, no watermark.
```

---

## ㉰ 첨단 SF 사회

> **한 줄:** 기술이 보편·발전한 사회. 로봇·패널·인공조명이 흔하다.
> **볼 것:** 정신경제·전설·여관·도보 여행의 쓸쓸한 톤과 **부딪히지 않는가?** A가 흔한 기술 중 하나로 묻혀 특별한 무게가 사라지지 않는가.

```text
Pixel art Metroidvania area screenshot reference, side view. A high-tech waystation on a travel route in an advanced, comfortable society. Technology is pervasive and modern: glowing info panels, common service robots, powered signboards, clean functional machinery everywhere, artificial lighting strips, smooth engineered surfaces. People and machines mix seamlessly in a bright functional sci-fi settlement; machines are advanced, common and completely taken for granted. Include tiny readable scale-cue silhouettes only: A, a small compact switch-bearing robot with one round cyan lens and a low crawler/roller base, and B, a small curious young organic wanderer with leaf-like ear fins and a satchel, among the crowd of tech. Clean, engineered, busy mood. Crisp retro pixel art, 16x16 tile language, 1px dark outline, limited low-saturation palette, no text, no labels, no UI, no minimap, no blur, no watermark.
```

---

## 비교 표 (생성 후 채워보기)

| | 한 줄 | A가 어떻게 읽히나 | 설계 위험 | 봤을 때 느낌 |
|---|---|---|---|---|
| ㉮ | 소박한 기계 일상 | 시시한 물건 ✅ | 없음 (권고) | (채우기) |
| ㉯ | 문명 유물 | 신비한 유물 ⚠️ | 무의미 반전·고대문명 클리셰 | (채우기) |
| ㉰ | 첨단 SF | 흔한 기술에 묻힘 | 톤 충돌 | (채우기) |

**결정 기준:** 세 장 중 *"A가 시시한 물건으로 보이면서 세계가 쓸쓸한"* 것을 고르면 된다. 이론상 ㉮가 그 자리지만, 눈으로 보고 마음이 가는 쪽을 고르면 그게 답이다.

> 결정되면 세계관 스펙(`specs/`)에 기술 수준 한 절을 추가하고 DECISIONS.md에 기록한다. 균형자 형태는 별도로 "행동만, 형태는 아트 때" 유보 상태.

---

# 라운드 2 — ㉮ 확정 후: 비인간 건축 서명 3종

**날짜:** 2026-08-07
**배경:** 1라운드 결과 ㉰(첨단 SF)는 분위기가 깨져 탈락, 기술 수준은 **㉮(소박한 기계·비SF)로 수렴**. 단 사용자 피드백:
> "집 모양이 너무 사람이 만든 것 같음. 생명체가 애초에 다른데 할로우나이트처럼 조금 색다른 부분이 있었으면. 너무 SF로 가면 분위기가 깨짐."

**뽑아낸 원칙:**
1. **건축이 인간식이면 안 된다.** B의 종이 비인간 생명체이므로 목조 집·사각 문/창·인간 비율 금지. 할로우나이트가 벌레 문명이라 껍질·균사 건축인 것처럼, **비인간 종이 만든 유기적 건축 서명**이 필요
2. **기계는 소박한 민속 기술.** 매끈한 SF ❌, 손때 묻은 낡은 도구 ✅ (㉮ 유지)
3. 톤·A/B 실루엣·픽셀 스타일은 1라운드와 동일 고정

**변수는 "비인간 건축 서명" 하나만.** 어떤 생물학적 모티프의 건축이 가장 색다르면서 분위기에 맞는지 비교한다.

**판단 포인트:** *"이게 사람이 아니라 다른 생명체가 지은 곳으로 보이나? 할로우나이트처럼 낯설고 고유한가? 그러면서도 소박·쓸쓸한 톤이 사나?"*

---

## 2-A 껍질·굴 건축 (carved-shell / burrow)

> **한 줄:** 거대한 나선 껍질과 키틴판을 파내고 흙 둔덕에 굴을 뚫어 만든 쉼터. 문 대신 둥근 유기적 구멍.
> **가장 할로우나이트에 가까운 방향** (벌레 문명의 껍질 건축 계열).

```text
Pixel art Metroidvania area screenshot reference, side view. A quiet wayside rest-stop for travelers on a lonely road at dusk, built by a small non-human species — NOT human architecture: no human-style timber houses, no rectangular doors or windows, no human proportions. The shelters are hollowed out of giant spiral shells and chitin plates and dug into rounded earth mounds, with smooth organic openings and short burrow tunnels instead of doors, curved carapace roofs, no straight edges. Simple humble everyday machines are an ordinary part of this world and blend into the organic structures — a worn hand-cranked mechanical pump, a small clockwork lantern, a battered little maintenance machine doing chores — but they look folk-made, hand-worn and low-tech, NOT sleek or sci-fi. Include tiny readable scale-cue silhouettes only: A, a small compact switch-bearing robot with one round cyan lens and a low crawler/roller base, and B, a small curious young organic wanderer with leaf-like ear fins and a satchel. Melancholic, lonely, lived-in mood. Crisp retro pixel art, 16x16 tile language, 1px dark outline, limited low-saturation palette, no text, no labels, no UI, no minimap, no blur, no watermark.
```

## 2-B 균사·버섯 건축 (fungal / grown)

> **한 줄:** 거대 균류에서 자라난 쉼터 — 넓은 버섯 갓 지붕, 발광하는 주름, 자루 기둥, 포자 등불. 지은 게 아니라 **자란** 마을.

```text
Pixel art Metroidvania area screenshot reference, side view. A quiet wayside rest-stop for travelers on a lonely road at dusk, built by a small non-human species — NOT human architecture: no human-style timber houses, no rectangular doors or windows, no human proportions. The shelters are grown from giant fungi rather than built: broad mushroom-cap roofs, soft glowing gills, thick stalk-pillars, spore-lantern lights, organic rounded hollows for dwellings. Simple humble everyday machines are an ordinary part of this world and blend into the organic growth — a worn hand-cranked mechanical pump, a small clockwork lantern, a battered little maintenance machine doing chores — but they look folk-made, hand-worn and low-tech, NOT sleek or sci-fi. Include tiny readable scale-cue silhouettes only: A, a small compact switch-bearing robot with one round cyan lens and a low crawler/roller base, and B, a small curious young organic wanderer with leaf-like ear fins and a satchel. Melancholic, lonely, lived-in mood. Crisp retro pixel art, 16x16 tile language, 1px dark outline, limited low-saturation palette, no text, no labels, no UI, no minimap, no blur, no watermark.
```

## 2-C 엮은 둥지 건축 (woven nest / plant-silk)

> **한 줄:** 뿌리·갈대·창백한 실로 엮어 매단 둥지 포드와 매듭 아치. 매달리고 동여맨 유기적 쉼터.

```text
Pixel art Metroidvania area screenshot reference, side view. A quiet wayside rest-stop for travelers on a lonely road at dusk, built by a small non-human species — NOT human architecture: no human-style timber houses, no rectangular doors or windows, no human proportions. The shelters are woven from roots, reeds and pale silk into hanging nest-pods and knotted arches, suspended and lashed together into an organic woven waystation, soft rounded woven forms, no straight edges. Simple humble everyday machines are an ordinary part of this world and blend into the woven structures — a worn hand-cranked mechanical pump, a small clockwork lantern, a battered little maintenance machine doing chores — but they look folk-made, hand-worn and low-tech, NOT sleek or sci-fi. Include tiny readable scale-cue silhouettes only: A, a small compact switch-bearing robot with one round cyan lens and a low crawler/roller base, and B, a small curious young organic wanderer with leaf-like ear fins and a satchel. Melancholic, lonely, lived-in mood. Crisp retro pixel art, 16x16 tile language, 1px dark outline, limited low-saturation palette, no text, no labels, no UI, no minimap, no blur, no watermark.
```

---

## 라운드 2 비교 표 (생성 후 채워보기)

| | 건축 서명 | 할로우나이트식 낯섦 | 톤 | 봤을 때 느낌 |
|---|---|---|---|---|
| 2-A | 껍질·굴 | 최강 (벌레 문명 계열) | 단단·쓸쓸 | (채우기) |
| 2-B | 균사·버섯 | 강 (자라난 마을) | 습하고 몽환 | (채우기) |
| 2-C | 엮은 둥지 | 중 (수공예 유기) | 연약·따뜻 | (채우기) |

**세 개 다 인간 건축을 배제하고 소박한 기계를 유지한다.** 어느 생물학적 서명이 "낯설면서 우리 톤"인지 눈으로 고르면, 그게 이 세계의 건축 언어가 된다.

---

## ✅ 결론 (2026-08-07)

- **기술 수준 = ㉮ 확정** (소박한 기계 + 느린 여행 공존). ㉯·㉰ 탈락
- **건축 = 비인간·유기적 확정** (인간 건축 배제)
- **3종 서명 전부 채택 → 구역별 건축 언어로 사용** (사용자: "셋 다 맘에든다"). 구역-서명 배정은 지리 설계 때
- **Codex 픽셀 질감 = 아트 파이프라인 기준**
- 반영: `DECISIONS.md` 2026-08-07 행, `STYLE_GUIDE.md` 세계 아트 디렉션·구역별 서명 표
