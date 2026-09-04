# B Village Production Kit v1

세 장의 사용자 선택 이미지를 기준으로 만든 고밀도 픽셀 제작 키트 초안이다. built-in ImageGen으로 생성하고 결정론적 후처리로 팔레트·픽셀·알파를 정리했다.

## 공통 규격

- 공통 팔레트: 61 opaque colors, 디더링 없음
- 장면 네이티브: 832×468, 2× 미리보기 1664×936
- 타일 모듈 시트: 627×627 transparent PNG, 2× 미리보기 1254×1254
- 프롭 시트: 768×512 transparent PNG, 2× 미리보기 1536×1024
- 알파: 완전 투명/완전 불투명만 사용하며 반투명 픽셀은 없다.
- 플레이면: 착지 상단은 수평, 벽타기 면은 수직. 유기적 곡선은 천장·비등반 벽·발판 밑면에 사용한다.

## 파일

| 종류 | 네이티브/원본 | 미리보기 |
|---|---|---|
| 64색 팔레트 앵커 | `b_village_hd_palette_reference_64_v2_native_832x468.png` | `b_village_hd_palette_reference_64_v2_preview_2x.png` |
| 세 번째 이미지 고밀도 재렌더 | `b_village_room_23_hd_native_832x468.png` | `b_village_room_23_hd_preview_2x.png` |
| 지형 타일 모듈 컨셉 | `b_village_terrain_modules_native_627x627.png` | `b_village_terrain_modules_preview_2x.png` |
| 마을·숲 프롭 24종 | `b_village_props_native_768x512.png` | `b_village_props_preview_2x.png` |

ImageGen 원본은 `*_imagegen.png`로 함께 보존한다.

## 타일 모듈 시트 범위

수평 상단, 바크 필, 끝단·코너, 수직 벽, 벽-바닥/천장 접합, 뿌리 버트레스, 비플레이 천장, 유기적 밑면, 이끼·덩굴 오버레이 후보를 한 장에 모았다.

현재 시트는 **스타일과 모듈 후보를 고르는 컨셉 시트**다. ImageGen이 요청한 8×8 동일 셀 수를 정확히 지키지 않았으므로 Unity에서 바로 자동 슬라이스하는 정본 아틀라스가 아니다. 채택 모듈을 고른 뒤 32×32 셀 기준으로 다시 정렬·보정한다.

## 프롭 24종

- 조명: 매달린 포드 등불 2종, 스탠딩 등불 2종, 브래킷 등불, 앰버 창
- 건축: 주거 포드 2종, 타원형 문틀, 난간 2종, 지지 기둥
- 생활: 뿌리 벤치, 바구니·화분·공동 그릇, 도르래, 갈고리
- 식생: 이끼, 풀, 관목, 덩굴, 뿌리 커튼, 비대칭 뿌리 매듭

프롭 네이티브는 실제 투명 PNG이며 체크무늬 배경은 제거했다. 크기와 피벗은 채택 뒤 게임 내 B/A 스케일에 맞춰 개별 분리한다.
