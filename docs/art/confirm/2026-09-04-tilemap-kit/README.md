# 타일맵 + 프랍 키트 (PixelLab) — 2026-09-04

> 사용자 제공 ImageGen 레퍼런스 4장(`production-kit-v2-by-reference/`의 imagegen 원본들, 4번째 = 배경 레퍼런스) 기반.
> 배경 레퍼런스에서 8x8 팔레트 스와치를 추출해 프랍 생성에 강제 팔레트로 사용. 타일셋 도구는 팔레트 강제 미지원이라 프롬프트로 색을 유도.

## 구성

| 파일 | 내용 | 출처 |
|---|---|---|
| tileset_mossy_roots_32.png/.json | v1 Wang 16타일 (32px) — 이끼+돌, 채도 높음 | tileset 9cea8118 |
| tileset_mossy_roots_32_v2.png/.json | v2 — 무채도 유도, 뿌리 흙 + 이끼 탑 | tileset b9554e35 |
| props/prop_lantern_post.png | 랜턴 기둥 48x80 | 02e09340 |
| props/prop_hanging_pod.png | 매달린 둥지 포드 64x80 | 4518af28 |
| props/prop_bush_cluster.png | 덤불 80x40 | 44f27509 |
| props/prop_hanging_vines.png | 매달린 덩굴 64x96 | ba3eba87 |
| props/prop_hollow_door.png | 나무 구멍 문 64x64 | 66a7abca |
| mockup_placed_832x468.png (+2x) | v1 타일 배치 목업 — 배경 ref01_background_native 위 | 합성 |
| mockup_placed_v2_832x468.png (+2x) | v2 타일 배치 목업 | 합성 |

배치: Wang 코너 규칙으로 지면 스트립(26칸) + 부유 플랫폼 3개(모서리 캡 포함) + 프랍 7점.
합성 스크립트: 세션 스크래치패드 `compose_mockup_v2.ps1` (메타데이터 JSON에서 타일 위치 동적 매핑).

## 검토 메모

- 타일 이음새는 양쪽 버전 모두 깨끗함 (Wang 세트 정상 동작). Unity Rule Tile로 그대로 옮길 수 있는 구조.
- **남은 문제 = 톤 매칭.** v1은 보라 돌+네온 이끼로 배경과 충돌, v2는 개선됐으나 여전히 배경(안개 낀 세이지 톤)보다 따뜻하고 채도 높음.
  후속 선택지: ① 팔레트 확정 후 타일셋 재생성 반복 ② Unity에서 타일맵 틴트/컬러 그레이딩 ③ 채택 레퍼런스로 base_tile 체이닝.
- 프랍은 팔레트 강제 덕에 배경과 잘 붙음. 덤불은 둔덕처럼 읽혀 실루엣 개선 여지.
- 배경은 사용자 ImageGen 레퍼런스의 native 다운스케일(832x468)을 그대로 사용 — 정본 아님, 컨펌 대기.
