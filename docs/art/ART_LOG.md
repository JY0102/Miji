# Art Log — OpenAI 프롬프트 요청 이력

생성된 모든 OpenAI 프롬프트와 그 결과를 기록합니다.
프롬프트는 ChatGPT / DALL-E 웹에서 수동으로 실행합니다.

---

## 로그 형식

```
### [날짜] — [에셋 이름]
- **목적**: [왜 이 에셋이 필요한가]
- **생성 프롬프트**: (Claude가 생성한 OpenAI 프롬프트 전문)
- **결과**: 승인 / 재요청 / 수정 중
- **검토 메모**: (스타일 가이드 체크리스트 결과)
- **파일 경로**: (에셋이 저장된 경로, 예: docs/art/assets/slime_idle.png)
```

---

## 요청 기록

### 2026-07-25 — 주인공 2인 (얼개 / 온기) 컨셉 레퍼런스

- **목적**: 주인공 로봇 2체의 실루엣 대비 + 팔레트 방향 확정. 스왑 메카닉상 두 로봇이 한눈에 구분돼야 하므로 한 장에 나란히 배치해 대비를 함께 검증.
- **생성 경로**: Higgsfield MCP (`generate_image`) — 수동 ChatGPT 실행 아님
- **모델**: `z_image` (Tongyi-MAI)
  - `recraft_v4_1`을 1순위로 시도 → **403 `job_minimum_basic_plan_required`** (유료 Basic 플랜 전용). free 플랜에서 사용 불가.
  - `recraft_v4_1`은 `colors` 파라미터로 팔레트를 모델 레벨에서 강제할 수 있어 이 프로젝트에 이상적 — **유료 전환 시 1순위 재시도 대상**.
- **비용**: 0.15 크레딧/장 × 3장 = 0.45 크레딧 (`recraft_v4_1`은 1.25 크레딧/장)
- **파라미터**: `aspect_ratio: 4:3`, `count: 4` (3장 반환), 출력 2048x1536

**제안 팔레트 (미확정 — STYLE_GUIDE.md는 여전히 TBD)**

캐릭터 설정에서 도출: 얼개=기계적 명료함(냉색), 온기=생물체적 온기(난색).

```
#12131A  다크 아웃라인
#2A2E3D  그림자
#4A5164  중간 메탈
#7C8598  밝은 메탈
#C3CBD8  하이라이트
#2E6F8E  얼개 스틸블루
#5FC9E8  얼개 시안 발광
#9B4A22  온기 러스트
#F2903C  온기 앰버
#F6D26A  난색 하이라이트
배경:    #1B1E28
```

- **결과**: **컨셉 레퍼런스로는 승인 / 스프라이트로는 사용 불가**

- **검토 메모** (STYLE_GUIDE 체크리스트):
  | 항목 | 결과 |
  |------|------|
  | 1px 다크 아웃라인 | ✗ 두껍고 굵기가 일정하지 않음 |
  | 팔레트 범위 준수 | ✗ 지정 10색을 크게 벗어남 (소프트 셰이딩·블룸) |
  | 안티앨리어싱 없음 | ✗ AA·글로우 블룸 다수 |
  | 스프라이트 크기 사양(16x16) | ✗ 2048x1536 일러스트 |
  | 게임 분위기 부합 | ✓ 폐기물·풍화된 기계, 쓸쓸한 톤 잘 맞음 |
  | 애니메이션 프레임 | N/A (단일 idle 레퍼런스) |

  **결론**: 3장 모두 "픽셀 아트 *풍* 일러스트"이지 진짜 픽셀 아트가 아님. 텍스트→이미지 모델의 구조적 한계 — 게임에 바로 넣을 16x16 스프라이트는 이 경로로 안 나옴. **디자인 레퍼런스로 확정하고, 실제 스프라이트는 이걸 보고 도트를 찍는 용도**로 사용.

  개별 평:
  - **A** (`2af468a3`): 두 캐릭터 대비가 가장 명확. 온기 머리가 둥근 돔(용접 마스크형). 온기의 "앞으로 기운 자세"는 거의 반영 안 됨.
  - **B** (`dd688e84`): 온기 머리가 각진 기계형, 몸통이 가장 묵직. **다운스케일 후에도 실루엣이 살아남을 가능성이 가장 높음.**
  - **C** (`46a97de8`): 케이블 다발·풍화 표현 등 캐릭터 설정 반영도가 가장 높고 온기가 앞으로 웅크린 자세. 다만 **머리가 몸에 비해 과대 → 16x16에서 가분수로 읽힐 위험**.

- **미해결 이슈**:
  - 얼개 쪽은 3장 모두 거의 동일 — 관찰자의 "분석적/철학적" 성격을 실루엣으로 더 밀어낼 여지 있음
  - 온기 머리 크기가 저해상도에서 실루엣 가독성을 해칠 수 있음 → 스프라이트화 시 두상 비율 축소 필요
  - 배경 투명 PNG 아님 (플랫 `#1B1E28`). 컷아웃 필요 시 `remove_background` 툴로 후처리 가능

- **파일 경로**: 미저장 (아래 CDN 원본만 존재, 만료 가능성 있음 — 채택본은 리포로 내려받을 것)
  - A: `hf_20260725_122716_2af468a3-fc0f-4acc-9ed6-ffba168e699e.png`
  - B: `hf_20260725_122716_dd688e84-7dd1-424c-9e49-6ea51b232736.png`
  - C: `hf_20260725_122716_46a97de8-8274-46d6-a28a-842aa2061d54.png`

- **생성 프롬프트**:
```
Pixel art character reference sheet, retro 16-bit game sprite style. Two humanoid
robot characters standing side by side, full body, front-facing idle pose, clearly
separated with empty space between them, on a flat solid dark background (#1B1E28).

LEFT ROBOT (the Observer): tall and thin, angular geometric frame, sharp rectangular
plating, exposed segmented joints, a single large round glowing cyan lens as its
head-eye, one narrow antenna. Cold steel-blue and cyan color scheme (#2E6F8E,
#5FC9E8). Posture perfectly upright, still, watchful and analytical.

RIGHT ROBOT (the Doer): shorter and stockier, rounded dented chassis, thick sturdy
limbs, a horizontal visor slit face glowing warm amber, frayed cable tufts at the
shoulders. Rust-orange and amber color scheme (#9B4A22, #F2903C). Posture leaning
forward with weight on the front foot, restless and eager to move.

Both are scavenged weathered post-apocalyptic machines, with a faint warm glow
leaking through the seams of the chest plate. Melancholic lonely tone.

Strict limited palette, only these colors: #12131A dark outline, #2A2E3D shadow,
#4A5164 mid metal, #7C8598 light metal, #C3CBD8 highlight, #2E6F8E steel blue,
#5FC9E8 cyan glow, #9B4A22 rust, #F2903C amber, #F6D26A warm highlight.

Style: crisp pixel art, hard-edged blocky pixels, 1px dark outline around every
silhouette, flat color fills only, no anti-aliasing, no gradients, no blur, no soft
shading, no text, no labels, no watermark. Clean readable silhouettes.
```
