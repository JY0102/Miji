# Implementation Agent Registry

> ## ⛔ 2026-08-10 — 아래 IMPL-001~004 전부 무효 (엔진 Unity 이관)
> IMPL-001~004는 **Godot 4 프로젝트** 기록이다. `src/miji/`는 삭제됐고 엔진은 **Unity 6 / C#**으로 바뀌었다(`DECISIONS.md` 2026-08-10).
> 아래 기록·자동 로그는 **참고용 이력**으로만 남긴다. Unity 첫 기능부터 IMPL-005로 새로 등록한다.

---

기능 구현 에이전트 목록입니다.
새로운 기능 구현을 시작할 때 여기에 먼저 등록합니다.

---

## 등록 형식

```
### [IMPL-XXX] [기능 이름]
- **상태**: 계획 중 / 진행 중 / 완료 / 보류
- **담당 파일**: `src/...`
- **연관 기획 문서**: `docs/planning/...`
- **설명**: (이 에이전트가 구현하는 기능 요약)
- **시작일**: YYYY-MM-DD
- **완료일**: YYYY-MM-DD 또는 미정
```

---

## 활성 에이전트

> 등록된 구현 에이전트가 없습니다.
> 기능 구현 시작 시 이 파일에 먼저 항목을 추가합니다.

---

## 완료된 에이전트

### [IMPL-001] Godot 프로젝트 초기 세팅
- **상태**: 완료
- **담당 파일**: `src/miji/project.godot`, `src/miji/scenes/`, `src/miji/scripts/`, `src/miji/assets/`
- **연관 기획 문서**: `docs/superpowers/specs/2026-07-24-core-concept-design.md` (픽셀 아트 16x16 기준)
- **설명**: 픽셀 아트 렌더링 설정(Nearest 필터, 정수 배율 뷰포트 스케일) 적용, scenes/scripts/assets 기본 폴더 구조 생성, 최소 Main 씬 등록
- **시작일**: 2026-07-24
- **완료일**: 2026-07-24
| 2026-07-24 | IMPLEMENTATION | C:\Users\User\Game\src\miji\project.godot | [자동 기록] |
| 2026-07-24 | IMPLEMENTATION | C:\Users\User\Game\src\miji\scenes\.gitkeep | [자동 기록] |
| 2026-07-24 | IMPLEMENTATION | C:\Users\User\Game\src\miji\scripts\.gitkeep | [자동 기록] |
| 2026-07-24 | IMPLEMENTATION | C:\Users\User\Game\src\miji\assets\sprites\.gitkeep | [자동 기록] |
| 2026-07-24 | IMPLEMENTATION | C:\Users\User\Game\src\miji\scenes\Main.tscn | [자동 기록] |
| 2026-07-24 | IMPLEMENTATION | C:\Users\User\Game\src\miji\project.godot | [자동 기록] |
| 2026-07-24 | IMPLEMENTATION | C:\Users\User\Game\src\miji\scripts\robot.gd | [자동 기록] |
| 2026-07-24 | IMPLEMENTATION | C:\Users\User\Game\src\miji\scripts\medium_manager.gd | [자동 기록] |
| 2026-07-24 | IMPLEMENTATION | C:\Users\User\Game\src\miji\scripts\camera_follow.gd | [자동 기록] |
| 2026-07-24 | IMPLEMENTATION | C:\Users\User\Game\src\miji\scenes\Robot.tscn | [자동 기록] |
| 2026-07-24 | IMPLEMENTATION | C:\Users\User\Game\src\miji\scenes\Main.tscn | [자동 기록] |
| 2026-07-24 | IMPLEMENTATION | C:\Users\User\Game\src\miji\project.godot | [자동 기록] |

### [IMPL-002] 로봇 컨트롤러 + 스왑 메카닉 (Phase 1-2)
- **상태**: 완료
- **담당 파일**: `src/miji/scripts/robot.gd`, `src/miji/scripts/medium_manager.gd`(Autoload: Medium), `src/miji/scripts/camera_follow.gd`, `src/miji/scenes/Robot.tscn`, `src/miji/scenes/Main.tscn`
- **연관 기획 문서**: `docs/superpowers/specs/2026-07-24-core-concept-design.md` (§3 스왑 메카닉), `docs/superpowers/specs/2026-07-24-core-mechanics-design.md` (§4 이동 & 메트로배니아 게이트)
- **설명**: CharacterBody2D 기반 이동/점프, 얼개·온기 2개체 그레이박스 배치, 매개체(Medium) 싱글톤이 활성 로봇 추적 및 스왑 처리(즉시 조작권 전환 + ~0.5초 이동 연출 + 이전 로봇 취약→완전 정지), 카메라 팔로우. 전투/HP는 Phase 3에서 추가 예정
- **입력키(임시)**: 이동 A/D, 점프 Space, 스왑 Q
- **시작일**: 2026-07-24
- **완료일**: 2026-07-24
| 2026-07-24 | IMPLEMENTATION | C:\Users\User\Game\src\miji\scripts\robot.gd | [자동 기록] |
| 2026-07-24 | IMPLEMENTATION | C:\Users\User\Game\src\miji\scripts\hurtbox.gd | [자동 기록] |
| 2026-07-24 | IMPLEMENTATION | C:\Users\User\Game\src\miji\scripts\attack_hitbox.gd | [자동 기록] |
| 2026-07-24 | IMPLEMENTATION | C:\Users\User\Game\src\miji\scripts\skill.gd | [자동 기록] |
| 2026-07-24 | IMPLEMENTATION | C:\Users\User\Game\src\miji\scripts\run_state.gd | [자동 기록] |
| 2026-07-24 | IMPLEMENTATION | C:\Users\User\Game\src\miji\scripts\game_flow.gd | [자동 기록] |
| 2026-07-24 | IMPLEMENTATION | C:\Users\User\Game\src\miji\scripts\checkpoint.gd | [자동 기록] |
| 2026-07-24 | IMPLEMENTATION | C:\Users\User\Game\src\miji\scripts\upgrade_station.gd | [자동 기록] |
| 2026-07-24 | IMPLEMENTATION | C:\Users\User\Game\src\miji\scripts\hud.gd | [자동 기록] |
| 2026-07-24 | IMPLEMENTATION | C:\Users\User\Game\src\miji\scripts\training_dummy.gd | [자동 기록] |
| 2026-07-24 | IMPLEMENTATION | C:\Users\User\Game\src\miji\scripts\hazard.gd | [자동 기록] |
| 2026-07-24 | IMPLEMENTATION | C:\Users\User\Game\src\miji\scenes\Robot.tscn | [자동 기록] |
| 2026-07-24 | IMPLEMENTATION | C:\Users\User\Game\src\miji\scenes\Checkpoint.tscn | [자동 기록] |
| 2026-07-24 | IMPLEMENTATION | C:\Users\User\Game\src\miji\scenes\Hazard.tscn | [자동 기록] |
| 2026-07-24 | IMPLEMENTATION | C:\Users\User\Game\src\miji\scenes\TrainingDummy.tscn | [자동 기록] |
| 2026-07-24 | IMPLEMENTATION | C:\Users\User\Game\src\miji\scenes\UpgradeStation.tscn | [자동 기록] |
| 2026-07-24 | IMPLEMENTATION | C:\Users\User\Game\src\miji\scenes\HUD.tscn | [자동 기록] |
| 2026-07-24 | IMPLEMENTATION | C:\Users\User\Game\src\miji\scenes\Main.tscn | [자동 기록] |
| 2026-07-24 | IMPLEMENTATION | C:\Users\User\Game\src\miji\scenes\Main.tscn | [자동 기록] |
| 2026-07-24 | IMPLEMENTATION | C:\Users\User\Game\src\miji\project.godot | [자동 기록] |
| 2026-07-24 | IMPLEMENTATION | C:\Users\User\Game\src\miji\scripts\_debug_driver.gd | [자동 기록] |
| 2026-07-24 | IMPLEMENTATION | C:\Users\User\Game\src\miji\project.godot | [자동 기록] |
| 2026-07-24 | IMPLEMENTATION | C:\Users\User\Game\src\miji\scripts\_debug_driver.gd | [자동 기록] |
| 2026-07-24 | IMPLEMENTATION | C:\Users\User\Game\src\miji\scripts\_debug_driver.gd | [자동 기록] |
| 2026-07-24 | IMPLEMENTATION | C:\Users\User\Game\src\miji\scripts\_debug_driver.gd | [자동 기록] |
| 2026-07-24 | IMPLEMENTATION | C:\Users\User\Game\src\miji\scripts\game_flow.gd | [자동 기록] |
| 2026-07-24 | IMPLEMENTATION | C:\Users\User\Game\src\miji\scripts\_debug_driver.gd | [자동 기록] |
| 2026-07-24 | IMPLEMENTATION | C:\Users\User\Game\src\miji\scripts\_debug_driver.gd | [자동 기록] |
| 2026-07-24 | IMPLEMENTATION | C:\Users\User\Game\src\miji\scripts\_debug_driver.gd | [자동 기록] |
| 2026-07-24 | IMPLEMENTATION | C:\Users\User\Game\src\miji\project.godot | [자동 기록] |

### [IMPL-003] 전투/자원/스킬프레임워크/코어게이트/체크포인트/재화/업그레이드 (Phase 3-9)
- **상태**: 완료
- **담당 파일**: `src/miji/scripts/robot.gd`(대폭 확장), `hurtbox.gd`, `attack_hitbox.gd`, `skill.gd`, `run_state.gd`(Autoload: RunState), `game_flow.gd`(Autoload: GameFlow), `checkpoint.gd`, `upgrade_station.gd`, `hud.gd`, `training_dummy.gd`, `hazard.gd`, `medium_manager.gd`(입력 바인딩 추가), `scenes/Robot.tscn`, `scenes/Checkpoint.tscn`, `scenes/Hazard.tscn`, `scenes/TrainingDummy.tscn`, `scenes/UpgradeStation.tscn`, `scenes/HUD.tscn`, `scenes/Main.tscn`
- **연관 기획 문서**: `2026-07-24-core-mechanics-design.md` §1~5, `2026-07-24-core-concept-design.md` §7
- **설명**:
  - Phase 3 전투: 근접 히트박스/허트박스(Area2D, 자기 자신 피해 방지 가드), 로봇별 독립 HP, 사망 시 died 시그널
  - Phase 4 자원: 히트 성공 시 충전, 스킬 소모, 체크포인트에서 풀충전 — **로봇별 독립 자원으로 구현** (스펙에 공유/개별 여부 명시 안 돼있어 판단, 패시브가 로봇별 독립 장착이라는 점에 맞춤)
  - Phase 5 스킬 슬롯: 액티브(1~2)/패시브(1~4) 장착·발동 구조만 (구체 스킬 없음, Skill 리소스 베이스클래스만)
  - Phase 6 코어 게이트: 대쉬/이중점프/벽타기 구현. **매개체 안정화는 unlock 플래그만 존재, 실제 효과 미구현** — "설정 연계"로만 적혀있어 구체 메카닉이 planning 문서에 없음 (임의로 발명하지 않음)
  - Phase 7 체크포인트/세이브: HP·자원 풀회복, user://save.json 저장/불러오기
  - Phase 8 재화: 공유 풀, 사망 시 드롭, 재방문 회수, 미회수 재사망 시 영구 소실
  - Phase 9 업그레이드 스테이션: 공격력/공속/이속/체력 4종, 로봇별 개별 레벨
  - HUD(로봇별 HP/자원 바, 재화, 활성 로봇 하이라이트) — Phase 13 예정이었으나 의존성 없어 앞당겨 포함
  - **"게임 오버" 해석**: 스펙상 "어느 쪽이든 사망 시 게임 오버"를 죽음=재화 드롭+체크포인트 리스폰(Hollow Knight식)으로 해석해 구현. 완전한 런 종료(타이틀로)가 아님 — 스토리 기획 시 재확인 필요
  - **TEMP**: RunState의 코어 능력 전부 기본 해금 상태 (실제 게이트 콘텐츠 없어서). 진짜 세이브는 전부 잠금 시작해야 함
  - 헤드리스 자동 입력 시뮬레이션으로 전체 흐름(공격→자원충전→스왑→체크포인트→사망→드롭→리스폰→업그레이드구매→대쉬) 검증 완료
- **시작일**: 2026-07-24
- **완료일**: 2026-07-24
| 2026-07-25 | IMPLEMENTATION | C:\Users\User\Game\src\miji\project.godot | [자동 기록] |
| 2026-07-25 | IMPLEMENTATION | C:\Users\User\Game\src\miji\scripts\run_state.gd | [자동 기록] |
| 2026-07-25 | IMPLEMENTATION | C:\Users\User\Game\src\miji\scripts\attack_hitbox.gd | [자동 기록] |
| 2026-07-25 | IMPLEMENTATION | C:\Users\User\Game\src\miji\scripts\hurtbox.gd | [자동 기록] |
| 2026-07-25 | IMPLEMENTATION | C:\Users\User\Game\src\miji\scripts\game_flow.gd | [자동 기록] |
| 2026-07-25 | IMPLEMENTATION | C:\Users\User\Game\src\miji\scripts\checkpoint.gd | [자동 기록] |
| 2026-07-25 | IMPLEMENTATION | C:\Users\User\Game\src\miji\scripts\camera_follow.gd | [자동 기록] |
| 2026-07-25 | IMPLEMENTATION | C:\Users\User\Game\src\miji\scripts\hud.gd | [자동 기록] |
| 2026-07-25 | IMPLEMENTATION | C:\Users\User\Game\src\miji\scripts\training_dummy.gd | [자동 기록] |
| 2026-07-25 | IMPLEMENTATION | C:\Users\User\Game\src\miji\scenes\Robot.tscn | [자동 기록] |
| 2026-07-25 | IMPLEMENTATION | C:\Users\User\Game\src\miji\scenes\Robot.tscn | [자동 기록] |
| 2026-07-25 | IMPLEMENTATION | C:\Users\User\Game\src\miji\scenes\TrainingDummy.tscn | [자동 기록] |

### [IMPL-004] 코드 품질 리팩토링 (Phase 1-9 전체)
- **상태**: 완료
- **담당 파일**: `src/miji/scripts/*.gd` 전체, `src/miji/project.godot`, `scenes/Robot.tscn`, `scenes/TrainingDummy.tscn`
- **연관 기획 문서**: 없음 (기존 구현 정리 — 기능 추가/변경 없음)
- **설명**: 재사용 / 단순화 / 효율 / 추상화 계층 4개 관점으로 리뷰 후 정리. **동작 변경 없음** — 리팩토링 전 헤드리스 실측값 57개 항목이 전부 동일함을 확인(신규 검증 69개 추가, 총 126 PASS / 0 FAIL)
  - 스탯 4종 병렬 필드 + `match` 2개 → `stat_levels` 딕셔너리 + `Robot.STAT_KEYS` 테이블. `max_hp`/`max_energy`는 레벨에서 파생되는 게터로 변경(동기화 누락 불가)
  - `robot.gd::_physics_process` 55줄 단일 함수 → 이름 붙은 단계별 헬퍼 + `move_and_slide()` 호출 1곳 (기존 실행 순서 그대로 유지)
  - 입력 액션 11개를 코드 등록(`_bind`)에서 `project.godot [input]`으로 이전
  - 콜리전 레이어 분리(hurtbox=2 / hitbox=3) + `class_name Hurtbox`/`AttackHitbox` 타입 체크로 그룹 문자열 제거
  - HUD의 A/B 병렬 멤버·분기 3곳 → 패널 배열 + 슬롯 번호를 연결 시점에 고정(`find()` 매 시그널 호출 제거)
  - 업그레이드 스테이션: 매 프레임 문자열 생성(`_process`) → 시그널 기반 갱신, 스탯 4분기 → 테이블 순회
  - `AttackHitbox.strike()`가 자기 콜라이더 수명을 직접 관리 (공격마다 `create_timer` 할당 제거)
  - 스왑 오브를 매번 생성/해제하지 않고 하나를 숨겨뒀다 재사용
  - 체력/자원 변경을 `_set_hp`/`_set_energy` 2곳으로 집중 (수동 emit 7곳 제거)
  - `RunState`: 능력 키 상수화, `Vector2` 직렬화 헬퍼, `FileAccess.open` 실패 가드, JSON→int 명시 변환
  - 체크포인트 진행 순서를 씬 프롭에서 `GameFlow.activate_checkpoint()`로 이동, `save_game()` 호출 명시화
- **줄 수**: 705 → 821줄 (주석 제외 512 → 555). 중복은 줄었으나 이름 붙은 상수·주석·실패 가드가 늘어 총량은 증가
- **시작일**: 2026-07-25
- **완료일**: 2026-07-25

---

## 리팩토링에서 보류한 항목 (IMPL-004)

리뷰에서 지적됐지만 **의도적으로 적용하지 않은** 것들입니다.
대부분 리팩토링 범위를 넘는 설계 작업이라 해당 Phase에서 처리해야 합니다.

| 항목 | 보류 이유 | 처리 시점 |
|---|---|---|
| `Damageable`/`Combatant` 공용 베이스 추출 | 현재 `Robot`과 더미가 HP/사망을 각각 구현. `hazard.gd`가 `body is Robot`으로 체크해 적을 때릴 수 없는 구조 | **Phase 11 (적/보스) 착수 직전 — 세 번째 복사본이 생기기 전에** |
| 체크포인트/재화 드롭을 룸 단위로 저장 | 지금은 월드 좌표 + 반경 32 비교. 룸 전환이 생기면 다른 룸의 같은 좌표에서 회수되고, 리스폰 시 룸 복원 불가 | **Phase 10 (룸 전환)** |
| 입력을 intent로 추상화 (컨트롤러 분리) | `Robot`이 `Input`을 직접 폴링. 대화/컷신에서 조작을 막거나 AI로 로봇을 움직일 방법이 없음 | Phase 12 (NPC/대화) |
| HUD 슬롯 동적 생성 + 로봇 identity 리소스 | 로봇 이름이 HUD 씬의 정적 Label 텍스트에 박혀 있음. 스토리상 이름 획득이 중간 이벤트라 부적절 | Phase 12 |
| 업그레이드 카탈로그를 Resource로 분리 | 비용 곡선이 씬 프롭 안에 있음. 두 번째 스테이션이나 메뉴 미리보기가 생기면 재파생 필요 | 업그레이드 UI 확정 시 |
| 스왑 오브를 월드 씬으로 이동 | 지금은 Medium 오토로드의 자식이라 룸 밖에 있음 (카메라/패럴랙스 미적용) | Phase 10 또는 아트 확정 시 |
| `State` enum → bool 축소 | `TRANSITIONING_OUT`이 스펙의 "이전 로봇 취약→완전 정지" 개념이라 제거하면 설계 의도 손실 | 적용 안 함 |
| 스킬 슬롯 프레임워크 삭제 | 미사용이지만 Phase 5 스캐폴딩으로 의도된 것 | 적용 안 함 |
| `camera_follow`를 엔진 `position_smoothing`으로 교체 | 스크립트가 Phase 10 룸 프레이밍/데드존 작업의 이음새 역할 | 적용 안 함 |
| `dropped_currency_valid` 제거 | `dropped_currency > 0`로 파생 가능하나 세이브 포맷 변경 대비 이득이 적음 | 적용 안 함 |
| 2026-08-10 | IMPLEMENTATION | C:\Work\Project\Game\Miji\src\Miji\Packages\manifest.json | [자동 기록] |
| 2026-08-10 | IMPLEMENTATION | C:\Work\Project\Game\Miji\src\Miji\.gitattributes | [자동 기록] |
