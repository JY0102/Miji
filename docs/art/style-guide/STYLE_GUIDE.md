# Art Style Guide — Pixel Art

> Codex가 프롬프트를 생성할 때 반드시 이 문서를 참조합니다.
> 스타일 결정이 변경될 경우 이 파일을 먼저 수정하고, ART_LOG.md에 기록합니다.

---

## 기본 사양

| 항목 | 값 |
|------|----|
| 스타일 | 픽셀 아트 (Pixel Art) |
| 기본 스프라이트 크기 | 16x16 px (캐릭터), 16x16 px (타일) |
| 보스 스프라이트 크기 | 32x32 ~ 64x64 px |
| 스케일 배율 | x3 또는 x4 (렌더링 시) |
| 팔레트 | 미정 (첫 구역 확정 시 설정 예정) |
| 아웃라인 | 1px 다크 아웃라인 |
| 디더링 | 제한적 사용 (그라디언트 표현 시만) |

---

## 팔레트 (미정 — 구역별로 확장 예정)

> 첫 구역이 확정되면 이 섹션에 HEX 코드를 추가합니다.

```
[BASE PALETTE — TBD]
배경:    #??????
플랫폼:  #??????
적:      #??????
플레이어: #??????
UI:      #??????
```

---

## 캐릭터 스프라이트 애니메이션 프레임 기준

| 애니메이션 | 최소 프레임 | 권장 프레임 |
|-----------|------------|------------|
| Idle      | 2          | 4          |
| Walk/Run  | 4          | 6          |
| Jump      | 2          | 3          |
| Fall      | 1          | 2          |
| Attack    | 3          | 5          |
| Hurt      | 2          | 3          |
| Death     | 4          | 6          |
| Special   | 4          | 8          |

---

## Codex 요청 프롬프트 템플릿

```
Pixel art sprite sheet, [WIDTH]x[HEIGHT] pixels per frame, [N] frames.
Subject: [캐릭터/오브젝트 설명]
Animation: [애니메이션 유형]
Style: retro game pixel art, 1px dark outline, limited palette
Palette: [팔레트 제약 — 색상 수, 주요 색상]
Mood/Theme: [분위기 — 예: dark gothic dungeon, melancholic]
No anti-aliasing, no gradients (except limited dithering).
Transparent background (PNG).
```

---

## 스타일 일관성 체크리스트

Codex로부터 받은 스프라이트를 검토할 때 확인합니다:

- [ ] 아웃라인이 1px dark인가?
- [ ] 팔레트 범위를 벗어난 색이 없는가?
- [ ] 안티앨리어싱이 없는가?
- [ ] 스프라이트 크기가 사양과 일치하는가?
- [ ] 게임의 분위기(Mood)와 시각적으로 맞는가?
- [ ] 애니메이션 프레임 수가 최소 기준 이상인가?

---

## 구역별 아트 디렉션 (추가 예정)

> 각 구역이 기획 확정되면 여기에 구역별 색조 및 분위기 지침을 추가합니다.

| 구역 | 주 색조 | 분위기 키워드 | 비고 |
|------|--------|-------------|------|
| (미정) | — | — | — |
