# B 정착지(여관 허브) 초안 — 2026-09-04

> 탐색용 초안. **스펙에 확정된 것은 「여관(허브) + 여관 주인」**(MECHANIC_movement 5절)뿐이고 "B의 마을·고향"은 미설정이다.
> 이 초안은 여관 중심의 작은 비인간 정착지로 해석해 그렸다 — 채택하려면 Planning에서 정착지 설정(B의 고향인지, 길가 여관촌인지)을 결정해야 한다.
> 건축: 엮은 둥지 언어, B 종족 스케일(고양이 크기)의 원형 개구부. 팔레트: 데모 그린 앵커(green_hk_01, 잡 ab677735) 강제.
> 생성: PixelLab `create_image_pixflux` + forced palette, 344x192, 5 generations.

| 파일 | 컨셉 | job id |
|---|---|---|
| village_01_inn_exterior | 여관 외관 — 구형 엮은 포드, 앰버 창 | 50bb7d64 |
| village_02_inn_interior | 여관 내부 — 원형 화로·작은 좌석 | b70e9d91 |
| village_03_pod_lane | 거주 구역 — 줄에 매달린 포드 집들 | feddb33a |
| village_04_common_square | 공동 광장 — 우물·매듭 게시대 | d6943092 |
| village_05_forest_gate | 마을 끝 — 숲으로 나가는 길 | 3969f11d |
| village_06_tree_canopy_hub_imagegen_v1 | 거목 두 그루를 잇는 다층 포드 마을 허브 | built-in ImageGen |
| village_07_heart_tree_commons_imagegen_v1 | 심장나무 공동광장 — 중심 거목 랜드마크 | built-in ImageGen |
| village_08_mist_bridge_quarter_imagegen_v1 | 안개 현수교 지구 — 여백 많은 수평 탐색 | built-in ImageGen |
| village_09_rootwater_lane_imagegen_v1 | 뿌리물길 골목 — 수로와 저층 생활권 | built-in ImageGen |
| village_10_hanging_pod_hollow_imagegen_v1 | 매달린 포드 공동 — 수직 둥지 군락 | built-in ImageGen |
| village_11_rain_night_shelter_imagegen_v1 | 비 오는 밤 쉼터 — 우천 야간 생활권 | built-in ImageGen |
| village_07_old_root_commons_imagegen_v2 | 07 수정 — 하트 공동을 비대칭 뿌리 공동으로 교체 | built-in ImageGen |
| village_map_12_fallen_root_inn_spine_imagegen_v1 | 실제 룸 A — 쓰러진 뿌리 여관 척추 | built-in ImageGen |
| village_map_13_split_reed_vertical_quarter_imagegen_v1 | 실제 룸 B — 갈라진 갈대 수직 주거구 | built-in ImageGen |
| village_map_14_rootwater_service_loop_imagegen_v1 | 실제 룸 C — 뿌리 수로 하층 루프 | built-in ImageGen |

## 1차 검토 메모

- 01·03이 가장 강함 — 구형 포드 여관, 매달린 포드 집 모두 비인간·둥지 언어에 충실하고 스케일도 작은 종족에 맞음.
- 02·04·05는 바닥이 타원으로 그려져 **탑다운 기운**이 섞임 — 채택 시 엄격한 측면 시점으로 재생성 필요.
- 04의 게시물이 종이 쪽지+문자처럼 렌더됨 — 이 세계 표기는 매듭 끈이어야 하므로 수정 대상.
- 05 우하단에 워터마크성 글자 흔적 — 채택 불가, 재생성 필요.

## 2차 시안 — 사용자 제공 거목 마을 레퍼런스 3장

- **산출물:** `village_06_tree_canopy_hub_imagegen_v1.png` (1680×945).
- **직접 참조:** 사용자 제공 거목 주거·다층 현수교·나무 몸통 포드 마을 이미지 3장.
- **프로젝트 참조:** `docs/art/assets/generated-concepts/maps/first-play-area/tutorial_map_green_reference_v1.png`의 승인된 타일 격자 은폐·곡선형 비플레이 지형 처리.
- **강점:** 엄격한 측면 시점, 하층 광장·중층 주거·상층 다리의 플레이 동선, 청록 숲과 앰버 점광원 대비, 비반복 곡선 지형이 한 화면에서 읽힌다.
- **검토 지점:** 일부 지붕·문·바구니·통이 인간식 숲 마을 문법에 가까울 수 있다. 채택 시 이 정도 생활 소품을 허용할지, 포드·씨앗 껍질·매듭 구조로 더 비인간화할지 결정한다.
- **상태:** 컨펌 대기. B의 고향인지 여관 중심 정착지인지는 여전히 Planning 미확정이다.

## 3차 시안 — 공간 성격 5종 병렬 배치

공통 입력은 사용자 제공 거목 마을 레퍼런스 3장, `village_06`, 승인된 `tutorial_map_green_reference_v1`이다. 다섯 장 모두 `architecture-space`(case 331) 골격을 공유하고 구도·생활 기능·날씨만 분리했다.

| 번호 | 방향 | 강점 | 주의점 |
|---|---|---|---|
| 07 | 심장나무 공동광장 | 중심 랜드마크와 다층 순환 동선이 가장 강함 | 중앙 공동이 심장 모양으로 읽혀 상징이 과할 수 있음 |
| 08 | 안개 현수교 지구 | 플레이면과 수평 탐색 동선이 가장 명료함 | 주거 밀도가 낮아 마을보다 외곽 길목에 가까움 |
| 09 | 뿌리물길 골목 | 습윤한 생활감과 앰버 반사가 풍부함 | 전경 물길과 일부 주거가 인간식 판타지 마을에 가까움 |
| 10 | 매달린 포드 공동 | 엮은 둥지 서명과 수직 군집이 가장 직접적임 | 포드 수가 많아 반복감을 줄이는 변형 설계가 필요함 |
| 11 | 비 오는 밤 쉼터 | 정서·피난처 감각·lived-in 분위기가 가장 강함 | 중앙 쉼터와 해먹이 인간 생활시설처럼 읽힐 수 있음 |

전부 컨펌 대기다. 채택 전에는 B의 고향 설정, 정확한 마을 구조, 정식 팔레트로 승격하지 않는다.

## 4차 시안 — 07 수정 + 맵 우선 룸 3종

- **사용자 피드백:** 07의 하트 모양은 분위기보다 의도된 상징이 먼저 보여 가볍고 테마파크처럼 읽힌다.
- **07 v2:** 전체 구도와 기존 동선은 유지하고 중앙 하트 공동만 비대칭 뿌리 매듭·침식 공동으로 교체했다. v1은 반려 사례로 보존한다.
- **신규 제작 방식:** 사용자 제공 거목 마을 사진 3장은 직접 입력하지 않았다. 실제 플레이를 가정해 입구·출구·주동선·분기·재합류·비밀 힌트를 먼저 정하고, 승인된 그린 팔레트와 타일 격자 은폐 원칙만 텍스트로 계승했다.

| 룸 | 실제 맵 역할 | 주동선 | 검토 지점 |
|---|---|---|---|
| 12 쓰러진 뿌리 여관 척추 | 첫 도착·대화·기본 이동 연습 | 좌측 진입 → 여관 휴식면 → 우측 출구, 상단 분기 후 낙하 숏컷 | 여관 내부 생활 소품을 더 비인간화할 여지가 있음 |
| 13 갈라진 갈대 수직 주거구 | 수직 이동·양쪽 벽 활용 | 좌하단 진입 → 지그재그 상승 → 우상단 출구, 좌측 보조 루프 | 플레이어 실제 점프 높이에 맞춘 발판 간격 재조정 필요 |
| 14 뿌리 수로 하층 루프 | 낮은 위험 수로·재합류·미래 게이트 암시 | 좌상단 하강 → 중앙 휴식면 → 우상단 상승, 하층 수로가 지름길로 재합류 | 수면 판정과 부술 수 있는 바닥의 실제 메카닉 확정 필요 |

세 장은 한 마을의 인접 룸으로 연결할 수 있는 구조 초안이다. 이미지의 발판 간격을 곧바로 Unity 수치로 확정하지 않으며, 채택 후 캐릭터 1유닛 스케일로 블록아웃한다.

## 5차 시안 — 네이티브 픽셀 배경 5종

직전 12~14는 고해상도 회화에 픽셀 질감만 얹힌 결과라 픽셀 아트 판정에서 반려했다. 이번 배치는 ImageGen 생성 뒤 아래 규격으로 결정론적 픽셀 출력을 만들었다.

- **네이티브 마스터:** 416×234 PNG, 16:9, 32색 인덱스 팔레트, 디더링 없음.
- **확대 미리보기:** 1664×936 PNG, 네이티브 마스터의 nearest-neighbor 4배 확대.
- **픽셀 규칙:** 동일 크기 사각 픽셀, 안티앨리어싱·연속 그라데이션·소프트 글로우 없음. 광원은 제한된 명암 단으로 표현한다.
- **참조 분리:** 15만 사용자 선택 이미지 2장을 직접 참조했다. 16~19는 이전 구도 반복을 피하려고 직접 이미지 입력 없이 프로젝트 규칙만 사용했다.
- **후처리 도구:** `tools/pixelize-imagegen-background.mjs`.

| 번호 | 분위기·공간 | 네이티브 파일 | 4배 미리보기 |
|---|---|---|---|
| 15 | 청록 뿌리 여관 — 선택 레퍼런스의 따뜻한 정착지 감각 | `village_pixel_15_root_inn_teal_native_416x234.png` | `village_pixel_15_root_inn_teal_preview_4x.png` |
| 16 | 비바람 현수로 — 차가운 남청과 사선 횡단 | `village_pixel_16_rain_cord_canopy_native_416x234.png` | `village_pixel_16_rain_cord_canopy_preview_4x.png` |
| 17 | 포자우물의 아침 — 밝은 세이지 안개와 U자 공동 | `village_pixel_17_sporewell_dawn_native_416x234.png` | `village_pixel_17_sporewell_dawn_preview_4x.png` |
| 18 | 마른 씨앗껍질 단구 — 갈색·녹슨 주황의 늦가을 | `village_pixel_18_amber_husk_autumn_native_416x234.png` | `village_pixel_18_amber_husk_autumn_preview_4x.png` |
| 19 | 달빛 갈대 습지 — 저층 이중 동선과 남색 수면 | `village_pixel_19_moon_reed_marsh_native_416x234.png` | `village_pixel_19_moon_reed_marsh_preview_4x.png` |

다섯 장 모두 픽셀 규격은 충족하지만 정본 팔레트·확정 맵 구조는 아니다. 사용자 선택 후 선택안의 플레이면 대비와 비인간 생활 소품을 한 번 더 정리한다.

> ⚠️ **16~19 v1 반려:** “다르게”를 날씨·계절·색온도 변화로 잘못 해석했다. 사용자의 실제 의도는 **15의 색감·분위기는 고정하고 지형만 변경**하는 것이었다. 아래 v2가 대체한다.

## 6차 시안 — 15 팔레트 고정 지형 변형 4종

- **고정값:** 15의 청록 숲 공기, 이끼·세이지 플레이면, 희소한 앰버 조명, 건조한 황혼, 명암 대비, 416×234 네이티브 픽셀 규격.
- **변경값:** 대형 지형 실루엣, 입출구 높이, 주동선의 방향, 분기·재합류 구조만 변경했다.
- **팔레트 보증:** 각 v2의 모든 픽셀을 15 네이티브 마스터의 동일 32색 팔레트에 최근접 매핑했다. 장면별 독립 팔레트를 만들지 않았다.

| 번호 | 지형 변형 | 네이티브 파일 | 4배 미리보기 |
|---|---|---|---|
| 16 v2 | 뿌리 협곡 수직 상승 — 좌우 절벽과 중앙 샤프트 | `village_pixel_16_root_cleft_ascent_v2_native_416x234.png` | `village_pixel_16_root_cleft_ascent_v2_preview_4x.png` |
| 17 v2 | 뿌리수로 하층부 — 저층 수로와 상·하 이중 루프 | `village_pixel_17_rootwater_undercroft_v2_native_416x234.png` | `village_pixel_17_rootwater_undercroft_v2_preview_4x.png` |
| 18 v2 | 이끼 사면 스위치백 — 좌상단에서 우하단으로 내려가는 단구 | `village_pixel_18_moss_switchback_bank_v2_native_416x234.png` | `village_pixel_18_moss_switchback_bank_v2_preview_4x.png` |
| 19 v2 | 비대칭 공동 순환로 — 넓은 하층 척추와 상부 반원 루프 | `village_pixel_19_hollow_ring_crossroads_v2_native_416x234.png` | `village_pixel_19_hollow_ring_crossroads_v2_preview_4x.png` |

15와 16~19 v2는 동일 구역 후보 묶음이다. v1의 비·아침 안개·늦가을·달빛 변형은 다른 구역 탐색 자료로만 남긴다.

## 7차 시안 — 직선 플레이면 + 밝은 팔레트 5종

- **사용자 피드백:** 6차 시안은 플레이어가 밟는 면까지 곡선으로 처리되어 충돌 지형이 불명확했고, 색감도 목표 레퍼런스보다 어두웠다.
- **새 충돌 실루엣 규칙:** 걷기·착지 상단은 수평 직선, 벽타기 면은 수직 직선으로 고정한다. 곡선과 비선형 표현은 천장·비등반 벽·발판 아랫부분·전후경에만 둔다.
- **색감 앵커:** 사용자 제공 밝은 거목 다리 이미지에서 추출한 `village_pixel_bright_reference_palette_native_416x234.png`의 32색을 공통 팔레트로 사용했다. 밝은 세이지·셀라돈 안개, 짙은 청록 수목, 네이비 외곽, 소량의 주황 램프 비율을 다섯 장 모두 동일하게 유지한다.
- **픽셀 규격:** 네이티브 416×234, 32색, 디더링·안티앨리어싱 없음. 4배 미리보기는 nearest-neighbor 1664×936이다.
- **참조 역할 분리:** 사용자 이미지는 팔레트·명도 전용, `tutorial_map_green_reference_v1.png`는 비플레이 영역의 유기적 실루엣 전용으로 사용했으며 두 이미지의 맵 배치는 복제하지 않았다.

| 번호 | 실제 맵 역할 | 네이티브 파일 | 4배 미리보기 |
|---|---|---|---|
| 20 | 밝은 여관 훈련실 — 긴 안전 바닥과 상단 3단 착지 연습 | `village_pixel_20_bright_inn_training_hall_v3_native_416x234.png` | `village_pixel_20_bright_inn_training_hall_v3_preview_4x.png` |
| 21 | 수직 상승 뜰 — 중앙 샤프트와 교차 발판·수직 벽타기 | `village_pixel_21_bright_vertical_climb_court_v3_native_416x234.png` | `village_pixel_21_bright_vertical_climb_court_v3_preview_4x.png` |
| 22 | 중앙 다리 공동 — 긴 수평 주동선과 상·하 선택 발판 | `village_pixel_22_bright_bridge_commons_v3_native_416x234.png` | `village_pixel_22_bright_bridge_commons_v3_preview_4x.png` |
| 23 | 낙하 루프 하층부 — 상단 통로에서 하층으로 떨어져 재합류 | `village_pixel_23_bright_drop_loop_undercroft_v3_native_416x234.png` | `village_pixel_23_bright_drop_loop_undercroft_v3_preview_4x.png` |
| 24 | 층계 우물 교차로 — 넓은 하단 안전면과 우측 3단 상승 | `village_pixel_24_bright_stepwell_crossroads_v3_native_416x234.png` | `village_pixel_24_bright_stepwell_crossroads_v3_preview_4x.png` |

15와 16~19 v2는 이전 검토 자료로 보존한다. 이번 20~24가 “밟는 곳은 선형, 밟지 않는 곳은 비선형”이라는 최신 지형 표현 규칙을 반영한 후보 묶음이다.

## 8차 수정 — 타일·배경 재질 통합

- **번호 대응:** 사용자 피드백의 1~5는 7차 시안의 20~24 순서다.
- **20 수정:** 반복 석재 캡과 우측 ㄴ자 벽기둥을 제거했다. 충돌 높이는 유지하면서, 발판과 바닥을 주변 거목에서 이어지는 나무껍질·뿌리 섬유·이끼 덩어리로 다시 그렸다.
- **21 수정:** 하단 중앙의 뜬금없는 독립 공중 발판을 삭제하고 숲 배경과 여백으로 복원했다. 나머지 상승 동선은 유지했다.
- **22 유지:** 배경과 전체 구성이 좋다는 사용자 평가에 따라 7차 v3를 수정하지 않았다.
- **23·24 수정:** 공중에서 갑자기 시작하던 직사각형 벽 타일을 제거했다. 같은 수직 충돌면을 천장·바닥·거목에 연결된 넓은 뿌리 절벽의 안쪽 면으로 교체했다.
- **공통 불변값:** 걷기·착지 상단은 수평 직선, 벽타기 면은 수직 직선, 비플레이 밑면·천장은 비선형, 밝은 공통 32색 팔레트, 네이티브 416×234.

| 대응 | 상태 | 네이티브 파일 | 4배 미리보기 |
|---|---|---|---|
| 1 / 20 | 재질 통합 v4 | `village_pixel_20_integrated_inn_training_hall_v4_native_416x234.png` | `village_pixel_20_integrated_inn_training_hall_v4_preview_4x.png` |
| 2 / 21 | 하단 독립 발판 제거 v4 | `village_pixel_21_clean_vertical_climb_court_v4_native_416x234.png` | `village_pixel_21_clean_vertical_climb_court_v4_preview_4x.png` |
| 3 / 22 | v3 유지 | `village_pixel_22_bright_bridge_commons_v3_native_416x234.png` | `village_pixel_22_bright_bridge_commons_v3_preview_4x.png` |
| 4 / 23 | 뿌리 절벽 통합 v4 | `village_pixel_23_rooted_drop_loop_undercroft_v4_native_416x234.png` | `village_pixel_23_rooted_drop_loop_undercroft_v4_preview_4x.png` |
| 5 / 24 | 측벽 타일 제거 v4 | `village_pixel_24_rooted_stepwell_crossroads_v4_native_416x234.png` | `village_pixel_24_rooted_stepwell_crossroads_v4_preview_4x.png` |

이번 수정의 기준은 **“콜라이더는 직선이어도 아트는 독립 타일처럼 보이면 안 된다”**이다. 수직 벽과 수평 발판은 기능 실루엣만 정돈하고, 재질과 접합부는 주변 나무·뿌리 배경에 연속시킨다.

## 9차 — 고밀도 장면 + 타일·프롭 제작 키트

- **사용자 판정:** 직전 v4의 1·2번(`20_integrated`, `21_clean`)은 픽셀 밀도가 낮고 결과가 좋지 않아 후보에서 폐기한다. 파일은 실패 비교 기록으로만 보존한다.
- **해상도 교정:** 416×234·32색 후처리가 전체 화면 환경의 세부를 과도하게 뭉갠 것이 원인이었다. 세 번째 사용자 첨부 이미지는 단순 확대하지 않고 832×468 논리 해상도로 다시 픽셀링했으며, 2× 미리보기에서도 2×2 픽셀 셀로 읽히게 했다.
- **공통 기준:** 사용자 선택 이미지 3장의 밝은 세이지·셀라돈·청록·네이비·앰버 관계를 61색 공통 팔레트로 구성했다.
- **신규 키트:** 고밀도 장면 1장, 투명 지형 모듈 컨셉 시트 1장, 투명 마을·숲 프롭 24종 시트 1장.
- **위치:** `production-kit-v1/README.md` 참조.
- **주의:** 지형 시트는 ImageGen의 셀 배열 편차 때문에 현재 모듈 선택용 컨셉 시트다. 정본 선택 후 32×32 Unity 슬라이스 규격으로 다시 패킹한다.

## 10차 — 레퍼런스별 전용 타일·배경·프랍 키트 v2

- **요청 해석:** 세 이미지를 하나의 평균 스타일로 섞지 않고 각각의 지형 문법을 분리한다. 1번은 전용 지형 타일과 배경, 2번은 전용 지형 타일, 3번은 1·2번의 밝은 색감으로 교정한 장면과 그 장면 전용 지형·프랍을 제작한다.
- **해상도 유지:** 직전 저밀도 실패를 반복하지 않도록 장면은 832×468, 시트는 768×512에서 처리했다. 미리보기는 nearest-neighbor 2배다.
- **1번:** 뿌리·토양·이끼 기반의 지형 모듈 24종과 플랫폼·프랍이 없는 숲 배경 전용 플레이트를 분리했다.
- **2번:** 판재형 수평 발판, 거목 수직면, 판재-거목 접합, 로프·덩굴 하부 장식 24종을 분리했다.
- **3번:** 원본의 거대한 뿌리 여관 구조와 소품 배치를 유지하면서 1번의 밝은 세이지·셀라돈·청록 팔레트에 맞췄다. 이 보정본을 단일 기준으로 지형 24종과 등불·건축·생활·식생 프랍 24종을 파생했다.
- **투명도 보정:** 생성기에 의해 구워진 흰색/회색 체크 배경과 옅은 접촉 그림자를 제거하고, 타일·프랍 모두 0/255 알파로 정리했다.
- **검수:** 모든 네이티브 파일은 지정 크기, 최대 64 opaque colors, 반투명 픽셀 0개, 대응 팔레트 밖 색상 0개를 충족한다.
- **위치:** `production-kit-v2-by-reference/README.md` 참조.
- **상태:** 컨펌 대기. 시트는 6×4 고밀도 모듈 선택용이며, 채택 뒤 32×32 Unity 슬라이스 아틀라스로 별도 패킹한다.
