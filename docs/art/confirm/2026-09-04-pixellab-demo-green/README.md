# 데모맵 그린 톤 초안 — 2026-09-04

> 탐색용 초안. 사용자가 `2026-09-04-pixellab-hk-room-comps/green_hk_01_corridor.png`(잡 ab677735)를 방향 레퍼런스로 선택 →
> **그 이미지를 forced palette 앵커로 물려** 실크송 튜토리얼(이끼 동굴)·녹색거리 느낌의 데모 구역 룸 6종을 생성했다.
> 데모 스코프(깨어남→놀이→기습→암전, 한 구역+놀이터)의 비트 순서를 룸 유형에 대응시켰다.
> 생성: PixelLab `create_image_pixflux` + `color_image_url`(팔레트 강제), 344x192, 6 generations.

| 파일 | 데모 비트 | 룸 컨셉 | job id |
|---|---|---|---|
| demo_01_awakening_grotto | 깨어남 | 빛기둥 떨어지는 밀폐 이끼 동굴 (실크송 오프닝풍) | 62dd5e34 |
| demo_02_first_corridor | 첫 이동 | 평탄한 튜토리얼 통로, 좌우 출구 | da70cde3 |
| demo_03_playground | 놀이 | 개방 안전룸 + 매달린 둥지 포드, 낮은 선택 플랫폼 | d3313a1d |
| demo_04_gentle_shaft | 이동 학습 | 십자형 수직 룸, 완만한 등반 | 2cbb2072 |
| demo_05_ominous_turn | 기습 직전 | 갈대 우거진 어두워지는 통로 | 3e161321 |
| demo_06_vista_ledge | 세계 엿보기 | 절벽 개구부 + 좁은 길 | bc8fbf79 |

## 1차 검토 메모

- 팔레트 강제가 먹혀 6장 전부 같은 그린-틸 톤으로 묶임 — 이전 배치들의 장당 색 흔들림 문제 해소.
- demo_01·02·03이 비트 대응이 가장 정확. demo_03의 매달린 포드가 엮은 둥지 언어와의 연결 고리.
- demo_04는 십자 구조가 흥미로우나 플랫폼이 비어 있음(디테일 패스 필요). demo_06은 비스타보다 동굴 입구로 읽힘.
- 채택 시 다음 단계: 이 팔레트를 STYLE_GUIDE에 데모 구역 팔레트로 등록 + 정본 배경은 스타일 앵커 기반 재생성(688x384 레이어 분해).

## 연결 맵 컨펌 — 2026-09-04

- 위 룸들을 바탕으로 built-in ImageGen에서 만든 `tutorial_map_imagegen_v1.png`를 사용자가 **맵·타일 아트 레퍼런스로 승인**했다.
- 승인 정본은 `docs/art/assets/generated-concepts/maps/first-play-area/tutorial_map_green_reference_v1.png`로 이동했다.
- **승인 범위:** 타일 이음새가 드러나지 않는 표면 처리, 플레이면의 명료한 림라이트, 플레이 불가 지형의 곡선·비선형·비반복 실루엣.
- **미승인 범위:** 이미지의 정확한 룸 배치·게이팅·비율, 생산용 타일 에셋 여부, 구역 팔레트 정식 등록.
