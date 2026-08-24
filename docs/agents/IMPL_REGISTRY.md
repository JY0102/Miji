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

---

## IMPL-005 — Core 1차 뼈대 (EventBus / InputReader / StateMachine / GameFlow)

- **상태**: 🟡 진행 중
- **시작일**: 2026-08-19
- **엔진**: Unity 6.3 / C# (Godot 코드 전부 폐기 후 첫 구현)
- **근거 스펙**: `specs/2026-08-19-implementation-core-architecture-design.md` (Core/Gameplay 이층 구조)
- **범위**: Core C1(EventBus) · C2(InputReader) · C3(StateMachine) · C6(GameFlow)
- **경로**: `src/Miji/Assets/Scripts/Core/`
- **설계 결정**:
  - **asmdef로 의존 방향을 컴파일러가 강제한다** — `Miji.Core`는 `Miji.Gameplay`를 참조하지 않으므로 Core에서 스토리 명사를 쓰는 실수가 빌드 에러가 된다. 문서 규칙이 아니라 빌드 규칙
  - **EventBus는 제네릭 pub/sub** — 게임플레이 고유 신호는 Gameplay 층에 정의된다. 새 이벤트가 생겨도 Core는 안 바뀐다
  - **InputReader + InputRouter 분리** — 입력 읽기와 「누가 조작되는가」를 나눈다. 2장 조작권 인계(여산→열하나)가 `InputRouter.Possess(actor)` 한 줄
  - IMPL-004 보류 항목 **「입력을 intent로 추상화 (컨트롤러 분리)」를 여기서 청산** (Godot 시절 Robot이 Input 직접 폴링 → 컷신 차단·AI 조작 불가였던 부채)
| 2026-08-19 | IMPLEMENTATION | C:\Work\Project\Game\Miji\src\Miji\Assets\Scripts\Core\Miji.Core.asmdef | [자동 기록] |
| 2026-08-19 | IMPLEMENTATION | C:\Work\Project\Game\Miji\src\Miji\Assets\Scripts\Core\Events\EventBus.cs | [자동 기록] |
| 2026-08-19 | IMPLEMENTATION | C:\Work\Project\Game\Miji\src\Miji\Assets\Scripts\Core\Input\InputIntent.cs | [자동 기록] |
| 2026-08-19 | IMPLEMENTATION | C:\Work\Project\Game\Miji\src\Miji\Assets\Scripts\Core\Input\InputReader.cs | [자동 기록] |
| 2026-08-19 | IMPLEMENTATION | C:\Work\Project\Game\Miji\src\Miji\Assets\Scripts\Core\Input\InputRouter.cs | [자동 기록] |
| 2026-08-19 | IMPLEMENTATION | C:\Work\Project\Game\Miji\src\Miji\Assets\Scripts\Core\StateMachines\StateMachine.cs | [자동 기록] |
| 2026-08-19 | IMPLEMENTATION | C:\Work\Project\Game\Miji\src\Miji\Assets\Scripts\Core\Flow\GameFlow.cs | [자동 기록] |
| 2026-08-19 | IMPLEMENTATION | C:\Work\Project\Game\Miji\src\Miji\Assets\Scripts\Gameplay\Miji.Gameplay.asmdef | [자동 기록] |
| 2026-08-19 | IMPLEMENTATION | C:\Work\Project\Game\Miji\src\Miji\Assets\Tests\EditMode\Miji.Core.Tests.asmdef | [자동 기록] |
| 2026-08-19 | IMPLEMENTATION | C:\Work\Project\Game\Miji\src\Miji\Assets\Tests\EditMode\EventBusTests.cs | [자동 기록] |
- **결과 (2026-08-19)**: ✅ 1차 뼈대 완료. `uloop compile` Success (에러 0 / 경고 0), `uloop run-tests` **19/19 통과**
  - `Core/Events/EventBus.cs` — 제네릭 pub/sub. 예외 격리(한 구독자의 예외가 나머지를 막지 않음), 발행 중 해제 안전(스냅샷 순회), `Clear()`
  - `Core/Input/InputIntent.cs` — 의도 구조체(`Move`/`JumpPressed`/`JumpHeld`/`Attack`/`Interact`/`Ability`) + `IPossessable`
  - `Core/Input/InputReader.cs` — Input System 유일 접점. 액션 **이름으로 조회**(생성 코드 비의존), `Blocked` 플래그
  - `Core/Input/InputRouter.cs` — `Possess(body)` / `Release()`. 대상 교체 시 직전 몸에 `InputIntent.None`을 먹여 관성 차단
  - `Core/StateMachines/StateMachine.cs` — `IState`/`StateBase`/`StateMachine<TKey>`. 전이 조건을 FSM에 넣지 않음(상태 추가 시 Core 불변)
  - `Core/Flow/GameFlow.cs` — `GameMode`(Playing/Cutscene/Paused/Loading), 입력 차단은 **모드에서 파생**, 씬 전환, `GameModeChanged` 신호
  - 테스트: `Assets/Tests/EditMode/` — EventBusTests 10 + StateMachineTests 9
- **미완**: 씬 배선(InputReader에 `InputSystem_Actions` 지정 + GameFlow 오브젝트 배치)은 G1 착수 시 함께
| 2026-08-19 | IMPLEMENTATION | C:\Work\Project\Game\Miji\src\Miji\ProjectSettings\TagManager.asset | [자동 기록] |
| 2026-08-19 | IMPLEMENTATION | C:\Work\Project\Game\Miji\src\Miji\Assets\Scripts\Gameplay\Player\PlayerController.cs | [자동 기록] |
| 2026-08-19 | IMPLEMENTATION | C:\Work\Project\Game\Miji\src\Miji\Assets\Scripts\Gameplay\StartupPossession.cs | [자동 기록] |

### 2차 — G1 A 컨트롤러 (2026-08-19) ✅

- **파일**: `Gameplay/Player/PlayerMotor.cs`, `Gameplay/Player/PlayerController.cs`, `Gameplay/StartupPossession.cs`
- **레이어 신설** (`ProjectSettings/TagManager.asset`): 6=Ground / 7=PlayerBody / 8=EnemyBody / 9=Hitbox / 10=Hurtbox
- **씬**: `Assets/Scenes/Greybox_Movement.unity` (dynamic-code로 생성 — 카메라·Systems·Player_A·발판 5개)
- **조작감 수치** — 확정 「묵직·기계적, 스프링 몸이 아니다」의 번역:
  - maxSpeed 5.5 / 지상 가감속 45·38 / **공중 가감속 26·10**(공중 제어를 일부러 둔하게)
  - jumpHeight **1.7**(낮게 — 위로 갈 여지를 남긴다) / jumpCut 0.45
  - gravityScale 3.6 / **fallGravityMultiplier 1.35**(낙하가 상승보다 빠르다) / maxFall 18
  - coyote 0.09 / jumpBuffer 0.11 (관용은 주되 조작감은 무겁게)
  - ⛔ 2단 점프 없음 — 「A는 스스로 높이를 얻지 못한다」. 두 번째 높이는 F2(B의 받침)
- **상태**: Grounded / Airborne 둘만. Idle·Move를 나누지 않은 것은 의도(처리가 같고 속도에서 파생되는 연출 문제). 의미 있는 상태(돌진·공격·피격)는 해당 기능과 함께 추가
- **검증 (ULoop)**:
  - `compile` Success 0/0 · `run-tests` **19/19**
  - 플레이모드 실측 — 정지 `y=0.51 Grounded` / 자유 이동 `vel=-5.12`(5.5로 가속 중) / 방향 전환·감속 정상 / 점프 `0.51→1.72` 상승 후 `Airborne`→착지 `Grounded`
  - 발판 측면 충돌로 정지 확인(물리 정상), 콘솔 에러 0
  - ★ **F2 게이트 수치 검증**: Ledge_Low(top 1.25) 도달 O / Ledge_Mid(2.45)는 Low를 밟고 도달(1.25+1.7) / **Ledge_TooHigh(3.65)는 어디서도 도달 X → F2 필요**. 「A는 높이를 못 얻는다」가 지형으로 성립
- **다음**: C7 CombatCore(Hitbox/Hurtbox/IDamageable) → G2 근접공격·상호작용
| 2026-08-19 | IMPLEMENTATION | C:\Work\Project\Game\Miji\src\Miji\Assets\Tests\PlayMode\Miji.Gameplay.PlayTests.asmdef | [자동 기록] |
| 2026-08-19 | IMPLEMENTATION | C:\Work\Project\Game\Miji\src\Miji\Assets\Tests\PlayMode\Miji.Gameplay.PlayTests.asmdef | [자동 기록] |

### 2차-b — 조작감 관용 기법 (2026-08-19) ✅ 사용자 요청

「코요테 타임 같은 유예 기법 + 홀로우나이트식 부드러운 조작감」 요청 반영. **무게를 줄이지 않고 「눌렀는데 안 됐다」만 없애는** 것이 기준.

| 기법 | 값 | 무엇을 해결하나 |
|---|---|---|
| 코요테 타임 (기존) | 0.09s | 발이 떨어진 직후의 점프 입력을 살린다 |
| 점프 버퍼 (기존) | 0.11s | 착지 직전에 누른 점프를 살린다 |
| 가변 점프 높이 (기존) | cut 0.45 | 짧게 누르면 짧게 뛴다 |
| **코너 보정** ★ 신규 | 0.22 유닛, 4단계 | 머리가 천장 모서리에 살짝 걸리면 **옆으로 밀어 통과**시킨다. HK 계열 핵심. 진행 방향 쪽을 먼저 시도 |
| **에이펙스 조정** 신규 | 임계 2.2 / 중력 ×0.65 / 제어 ×1.45 | 정점에서만 중력을 줄이고 공중 제어를 준다 → 착지 지점 조준 가능. 체공이 짧은 것을 그 순간만 보상 |
| **반전 스냅** 신규 | 감속 ×2.1 | 반대 방향 입력 시 더 빨리 꺾인다. 턴이 즉각 반응하되 무게는 유지 |
| **천장 범프** 신규 | — | 정말 막혔으면 상승 속도를 끊어 천장에 달라붙지 않게 |

**🐛 함께 고친 버그**: 점프 직후에도 발이 접지 판정에 걸려 있어 **코요테가 즉시 재충전 → 연타 시 이중 임펄스**가 가능했다. `jumpLockout` 0.08s로 차단(점프 직후에는 코요테를 재충전하지 않는다).

**검증 — PlayMode 테스트 신설** (`Assets/Tests/PlayMode/MotorFeelTests.cs`, 8건):
코요테 작동 / 코요테 만료 / 공중 2단 점프 불가 / 이중 임펄스 불가 / 오래된 점프 입력이 착지에 되살아나지 않음 / 코너 보정으로 모서리 통과 / 천장 범프로 상승 차단 / 반전이 단순 정지보다 빠름
- ⚠️ **PlayMode 테스트 asmdef는 `includePlatforms: []`여야 한다** — Editor 전용으로 두면 EditMode로 실행돼 물리가 스텝되지 않고 전부 실패한다(실제로 8/8 실패 후 원인 확인)
- 실행: `uloop run-tests --test-mode PlayMode` / `--test-mode EditMode` (기본값이 EditMode)

**최종**: compile 0/0 · **PlayMode 8/8 · EditMode 19/19 = 27/27 통과**
| 2026-08-19 | IMPLEMENTATION | C:\Work\Project\Game\Miji\src\Miji\Assets\Scripts\Core\Combat\Damage.cs | [자동 기록] |
| 2026-08-19 | IMPLEMENTATION | C:\Work\Project\Game\Miji\src\Miji\Assets\Scripts\Core\Combat\Health.cs | [자동 기록] |
| 2026-08-19 | IMPLEMENTATION | C:\Work\Project\Game\Miji\src\Miji\Assets\Scripts\Core\Combat\Hurtbox.cs | [자동 기록] |
| 2026-08-20 | IMPLEMENTATION | C:\Work\Project\Game\Miji\src\Miji\Assets\Scripts\Gameplay\Player\PlayerAnimator.cs | [자동 기록] |

### 3차 — A 스프라이트·애니메이션 적용 (2026-08-20) ✅

그레이박스 사각형 → 실제 픽셀 스프라이트. 생성 경위·프레임 선별·스타일 검토는 `docs/art/ART_LOG.md` 2026-08-20 항목이 원본.

- **에셋**: `Assets/Art/Player/` — 프레임 PNG 17장(idle 4 / run 8 / jump 3 / fall 2, 32x32·PPU 32·Point·무압축) + 클립 4종 + `A_Animator.controller`
- **코드**: `Gameplay/Player/PlayerAnimator.cs` 신설 — **뷰 전용.** Motor 상태(HorizontalSpeed/IsGrounded/Velocity.y) → Animator 파라미터, Facing → flipX. 물리·조작에 관여하지 않음(컴포넌트를 꺼도 게임 동작 동일)
- **애니메이터 구조**: Idle↔Run(Speed 0.1), Idle·Run→Jump(비접지+상승)·Fall(비접지+하강), Jump→Fall(하강 전환), Fall→Idle/Run(착지). 전환 duration 0(픽셀 아트 — 블렌딩 없음), Exit Time 없음
- **씬**: `Greybox_Movement.unity` Player_A에 Animator+PlayerAnimator 추가. 스프라이트 32px=PPU 32 → 월드 1유닛, 기존 1x1 콜라이더와 일치(콜라이더 무변경 — 물리 불변)
- **검증**: compile 0/0 · **EditMode 19/19 + PlayMode 8/8 유지** · 플레이 실측 — Idle 렌즈 명멸 / D 홀드 주행 / timeScale 0.15로 공중 낙하 프레임 캡처 확인
- **주의**: Idle 마지막 생성 프레임(렌즈 꺼짐)은 **의도적으로 제외** — A의 「꺼짐」은 서사 사건이라 아이들 루프에 쓰면 안 된다
- **다음**: C7 CombatCore 마무리(Hitbox 미작성) → G2 근접공격 때 Attack/Hurt 애니메이션 추가(PixelLab 잔여 3생성 고려)

**3차 추가 (2026-08-20) — 콜라이더를 그림에 맞춤 (사용자 지적):** 콜라이더가 1x1(캔버스 전체)이라 그림보다 사방 3px 커서 A가 땅에서 3px 떠 보였다. 전 프레임 불투명 픽셀 실측 후 **size (0.8125, 0.65625) / offset (0, -0.078125)** 로 조정 — 바닥·좌우는 그림 몸통에 정합(좌우 대칭이라 flipX 안전), **상단 스위치(폭 2~6px)는 충돌 제외**(시각 전용, 더듬이 관례). 점프 도달은 발 기준이라 F2 게이트 검증 불변. 실측 y=0.4212 접지(계산 0.4216 일치), PlayMode 8/8 유지.

### 4차 — 동굴 지형 타일 적용 (2026-08-20) ✅ (미커밋)

그레이박스 지형 5종을 타일 렌더로 전환. 타일 제작 경위(PixelLab 실패→수제작)는 `ART_LOG.md` 2026-08-20 2차 항목이 원본.

- **에셋**: `Assets/Art/Tiles/Tile_CaveTop.png`·`Tile_CaveFill.png` — 16x16 수제 도트(A 팔레트 샘플링), PPU 32·Point·**Repeat·FullRect**(Tiled draw mode 필수 설정)
- **씬 전환 방식**: 지형이 「1x1 스프라이트 × 스케일」이던 것을 「스케일 1 + SpriteRenderer.size(Tiled)」로 이관, **BoxCollider2D.size에 월드 크기를 넘겨 물리 불변**. sortingOrder 지형 -10/표면 -9(플레이어 0 앞)
- **분기 규칙**: 높이≤0.55=발판(이끼 상단 타일) / 세로>가로=벽(채움 — 이끼줄 반복 방지) / 그 외=지반(채움+상단 표면 스트립 자식 오브젝트)
- **적용 결과**: Ground_Main 30x1(채움+표면) / Ledge_Low·Mid·TooHigh 3x0.5(상단) / Wall_Right 1x6(채움) + 카메라 배경 #0E1017
- **검증**: 플레이 실측 — 타일 무이음·A 주행/착지 정상, PlayMode 8/8 유지
- **다음**: C7 CombatCore 마무리(Hitbox) → G2. 타일 확장(경사·모서리 전용 타일, Tilemap 이관)은 지리 설계 확정 후
| 2026-08-20 | IMPLEMENTATION | C:\Work\Project\Game\Miji\src\Miji\Assets\Scripts\Gameplay\Companion\CompanionFollower.cs | [자동 기록] |

### 5차 — B(무리비) 테스트용 추종 (2026-08-20) ✅ (미커밋)

- **코드**: `Gameplay/Companion/CompanionFollower.cs` 신설 — A 뒤 1.1유닛을 SmoothDamp(0.22s)로 추적, 8유닛 초과 시 연출 없이 즉시 스냅, 이동 중에만 사인 들썩임, 시선은 이동 방향(정지 시 A 쪽)
- **확정 설계 준수(이원 무브셋 3절 뼈대)**: 입력 없음·콜라이더 없음(**B 무적 — Health/Hurtbox 안 붙임**)·「멀어서 못 했다」 상황 원천 차단(스냅). F2/F5 협력 스냅 로직은 G6에서
- **씬**: `Companion_B` (SpriteRenderer sortingOrder -1 = A 바로 뒤 + CompanionFollower, target=Player_A Motor 배선)
- **검증**: compile 0/0 · 플레이 실측 — 우향 주행 시 좌측 후방 추종, 방향 전환 시 반대편 재정렬·시선 반전 확인
- **다음**: C7 CombatCore 마무리(Hitbox) → G2
| 2026-08-20 | IMPLEMENTATION | C:\Work\Project\Game\Miji\src\Miji\Assets\Scripts\Gameplay\World\ParallaxLayer.cs | [자동 기록] |
| 2026-08-20 | IMPLEMENTATION | C:\Work\Project\Game\Miji\src\Miji\Assets\Scripts\Gameplay\Companion\CompanionFollower.cs | [자동 기록] |
| 2026-08-20 | IMPLEMENTATION | C:\Work\Project\Game\Miji\src\Miji\Assets\Scripts\Gameplay\Companion\CompanionFollower.cs | [자동 기록] |
| 2026-08-20 | IMPLEMENTATION | C:\Work\Project\Game\Miji\src\Miji\Assets\Scripts\Gameplay\View\TurnView.cs | [자동 기록] |
| 2026-08-20 | IMPLEMENTATION | C:\Work\Project\Game\Miji\src\Miji\Assets\Scripts\Gameplay\Player\PlayerAnimator.cs | [자동 기록] |
| 2026-08-20 | IMPLEMENTATION | C:\Users\User\Game\src\miji\Assets\Tests\EditMode\Gameplay\Miji.Gameplay.Tests.asmdef | [자동 기록] |
| 2026-08-20 | IMPLEMENTATION | C:\Users\User\Game\src\miji\Assets\Tests\EditMode\Gameplay\TurnViewTests.cs | [자동 기록] |
| 2026-08-20 | IMPLEMENTATION | C:\Users\User\Game\src\Miji\Assets\Scripts\Editor\WovenNestSampleRoomBuilder.cs | [자동 기록] |
| 2026-08-20 | IMPLEMENTATION | C:\Users\User\Game\src\Miji\Assets\Scripts\Editor\WovenNestSampleRoomBuilder.cs | [자동 기록] |
| 2026-08-20 | IMPLEMENTATION | C:\Users\User\Game\src\Miji\Assets\Scripts\Editor\WovenNestSampleRoomBuilder.cs | [자동 기록] |
| 2026-08-20 | IMPLEMENTATION | C:\Users\User\Game\src\Miji\Assets\Scripts\Editor\WovenNestSampleRoomBuilder.cs | [자동 기록] |
| 2026-08-21 | IMPLEMENTATION | C:\Users\User\Game\src\Miji\Assets\Scripts\Editor\WovenNestSampleRoomBuilder.cs | [자동 기록] |
| 2026-08-21 | IMPLEMENTATION | C:\Users\User\Game\src\Miji\Assets\Scripts\Editor\WovenNestSampleRoomBuilder.cs | [자동 기록] |
| 2026-08-21 | IMPLEMENTATION | C:\Users\User\Game\src\Miji\Assets\Scripts\Editor\WovenNestSampleRoomBuilder.cs | [자동 기록] |
| 2026-08-21 | IMPLEMENTATION | C:\Users\User\Game\src\Miji\Assets\Scripts\Editor\WovenNestSampleRoomBuilder.cs | [자동 기록] |
| 2026-08-21 | IMPLEMENTATION | C:\Users\User\Game\src\Miji\Assets\Scripts\Editor\WovenNestSampleRoomBuilder.cs | [자동 기록] |

### 6차 — WovenNest 배경 패럴랙스 스택 세팅 (2026-08-21) ✅ (미커밋)

Codex(sprite-gen)가 뽑은 layer pass 01 PNG 7장을 인게임에 배치. 생성 경위는 `ART_LOG.md` 2026-08-21 항목이 원본.

- **코드**: `Assets/Scripts/Editor/WovenNestParallaxBuilder.cs` 신설 (`Miji.Editor`) — 메뉴 **`Miji/Background/Woven Nest 패럴랙스 배경 구성`**. 레이어 표(정렬순서·추종계수·틴트·on/off)를 코드에 두고 씬을 매번 새로 만든다. 몇 번을 돌려도 같은 결과가 된다
- **asmdef**: `Miji.Editor` 참조 `[]` → `["Miji.Core", "Miji.Gameplay"]` — `ParallaxLayer`를 붙이려면 필요하다. 방향은 Editor → Gameplay → Core 로 여전히 단방향
- ★ **뒷벽 타일맵이 배경을 통째로 가리고 있었다** — `Tilemap_BackWall`(order −60, 불투명)이 방 내부를 다 덮고 있어서 `BG_WovenNest.png` 는 **한 번도 화면에 나온 적이 없었다**(스크린샷으로 확인). 빌더가 이 렌더러를 끈다. 오브젝트는 남겨둬서 되돌리려면 렌더러만 켜면 된다. 뿌리다리 틈으로 「새어 보이는 것」이 이제는 배경이라 막을 이유가 없다
- ★ **배율은 1을 벗어나지 않는다** — 이전 한 장짜리 배경은 1.5였다. 688x384 / PPU 32 = 21.5 x 12u 이고 카메라(ortho 6, 16:9)가 21.33 x 12u 라 **배율 1에서 캔버스 = 화면**이다. 1.5로 늘리면 배경 픽셀이 타일(16px)보다 굵어져 한 화면에 픽셀 밀도가 두 종류가 된다
- **깊이 틴트 추가**(매니페스트에 없던 것) — 원경 0.50 → 근경 0.85 명도, 아주 살짝 푸르게. 틴트 없이 켜면 배경 대비가 지형과 같아서 **발판(노란 선)이 안 보인다**. 스크린샷 비교로 확정
- ⚠️ **L06_PropsLanterns 는 기본 off** — pass 01의 props 레이어는 사실상 「소품 시트」다. 등불이 낱개로 균등 배열돼 있고 크기가 A(1u)의 2~3배라 켜면 플레이 레인 한복판에 거대한 등불이 줄줄이 걸린다. pass 02에서 작은 덩어리로 쪼갤 때까지 꺼둔다
- **L07_GroundDressings 는 오프셋 0 유지** — 캔버스 아래쪽 뿌리 띠가 월드 y<0(바닥 타일 뒤)에 떨어져 거의 안 보이지만, 이 레이어만 위로 올리면 걷는 레인을 가로로 덮는다(+3.3 시험 후 되돌림)
- **검증**: compile 0/0 · **EditMode 25/25 + PlayMode 8/8 = 33/33 유지** · 플레이 실측 — 카메라 +2u 이동 시 레이어 x 이동량 1.92/1.84/1.72/1.60/1.44/1.10 = 추종계수와 정확히 일치
- ⚠️ **카메라 추종 스크립트가 아직 없다** — Main Camera는 Transform+Camera뿐이라 실제 플레이에서는 카메라가 고정이고, 따라서 **패럴랙스가 눈에 보이지 않는다.** 스택 자체는 정상 동작(위 실측)이며, 카메라 추종은 별건
- **다음**: C7 CombatCore 마무리(Hitbox 미작성) → G2

### 7차 — B 인게임 반입 + 🐛 타일 룸 콜라이더 소실 버그 (2026-08-21) ✅ (미커밋)

**① B(무리비)가 게임에 섰다.** 아트 판단(32px 다운스케일 폐기 → 64px 원화 유지)의 경위는 `ART_LOG.md` 2026-08-21 2차가 원본.
- **에셋**: `Art/Characters/B/Sprites/B_idle_0.png` — 64x64, **PPU 64**(= 1유닛, A와 동일), Point, 무압축
- **씬**: `Greybox_WovenNest` · `Greybox_Movement` 양쪽 `Companion_B` 의 SpriteRenderer 배선
- ⚠️ **Animator 를 껐다** — `B_Idle`/`B_Walk`/`B_Sleep` 클립이 8/20에 삭제된 스프라이트를 가리키는 죽은 참조다. 켜두면 `m_Sprite` 를 null 로 덮어써 **스프라이트를 물려도 B가 안 보인다.** `CompanionFollower` 는 애니메이터가 없으면 코드 들썩임으로 대체하도록 이미 설계돼 있어 그 경로를 쓴다. 클립·컨트롤러는 지우지 않고 남겼다
- **실측**: A y=0.421 접지 / B y=0.421, 스프라이트 하단 −0.079 (A와 같은 발높이)

**② 🐛 타일 룸에 충돌이 아예 없었다 — A가 바닥을 뚫고 떨어진다.**
- **증상**: 저장된 `Greybox_WovenNest` 를 열어 플레이하면 A가 `y=−119` 까지 낙하. `CompositeCollider2D.shapeCount` 가 **0**
- **원인**: `WovenNestSampleRoomBuilder.CreateLayer` 가 **`CompositeCollider2D` 를 `TilemapCollider2D` 보다 먼저** 붙였다. 이 순서로 만들면 `compositeOperation = Merge` 등록이 **씬 저장에 실려 나가지 않는다.** 만든 직후에는 메모리에 등록이 살아 있어 멀쩡히 플레이되므로 **8/21 오전 검증은 통과했고 버그는 그대로 커밋됐다**
- **격리 근거**: `compositeOperation` 을 None 으로 떼면 `TilemapCollider2D` 단독 도형이 **544개**(정상), 다시 Merge 로 붙이면 컴포지트 도형 **7개** 생성. 타일 에셋의 `colliderType` 도 전부 정상(Grid 12종)이었다 — 타일이 아니라 **컴포넌트 부착 순서**가 범인
- **수정**: 빌더에서 `TilemapCollider2D` → `CompositeCollider2D` 순으로 교체(주석에 함정 기록). 기존 씬은 컴포지트를 떼고 올바른 순서로 재생성해 저장 — **씬을 다시 열어도 도형이 유지됨을 확인**(Terrain 7 / Platforms 3)
- ★ **교훈: 「만든 직후 플레이 검증」은 씬 저장·재로드를 건너뛴다.** 빌더가 만든 씬은 반드시 **다시 열어서** 확인할 것

**검증**: compile 0/0 · **EditMode 25/25 + PlayMode 8/8 = 33/33 유지** · 플레이 실측 A·B 접지 정상
**다음**: C7 CombatCore 마무리(Hitbox 미작성) → G2. B는 walk/fall·턴 3프레임이 아직 없다
| 2026-08-21 | IMPLEMENTATION | C:\Users\User\Game\src\Miji\Assets\Scripts\Gameplay\View\BlinkView.cs | [자동 기록] |
| 2026-08-21 | IMPLEMENTATION | C:\Users\User\Game\src\Miji\Assets\Scripts\Editor\CompanionBAnimationBuilder.cs | [자동 기록] |
| 2026-08-21 | IMPLEMENTATION | C:\Users\User\Game\src\Miji\Assets\Scripts\Editor\CompanionBAnimationBuilder.cs | [자동 기록] |
| 2026-08-21 | IMPLEMENTATION | C:\Users\User\Game\src\Miji\Assets\Art\Characters\B\Animations\B_Idle.anim | [자동 기록] |
| 2026-08-24 | IMPLEMENTATION | C:\Users\User\Game\src\Miji\Assets\Art\Characters\A\Animations\A_Run.anim | [자동 기록] |
| 2026-08-24 | IMPLEMENTATION | C:\Users\User\Game\src\Miji\Assets\Art\Characters\A\Animations\A_Run.anim | [자동 기록] |
| 2026-08-24 | IMPLEMENTATION | C:\Users\User\Game\src\Miji\Assets\Art\Characters\A\Animations\A_Run.anim | [자동 기록] |
| 2026-08-24 | IMPLEMENTATION | C:\Users\User\Game\src\Miji\Assets\Scripts\Gameplay\Player\PlayerAnimator.cs | [자동 기록] |
| 2026-08-24 | IMPLEMENTATION | C:\Users\User\Game\src\Miji\Assets\Scenes\Greybox\Greybox_Movement.unity | [자동 기록] |
| 2026-08-24 | IMPLEMENTATION | C:\Users\User\Game\src\Miji\Assets\Scenes\Greybox\Greybox_WovenNest.unity | [자동 기록] |
| 2026-08-24 | IMPLEMENTATION | C:\Users\User\Game\src\Miji\Assets\Scenes\Greybox\Greybox_Movement.unity | [자동 기록] |
| 2026-08-24 | IMPLEMENTATION | C:\Users\User\Game\src\Miji\Assets\Scenes\Greybox\Greybox_WovenNest.unity | [자동 기록] |
| 2026-08-24 | IMPLEMENTATION | C:\Users\User\Game\src\Miji\Assets\Scripts\Gameplay\Player\PlayerAnimator.cs | [자동 기록] |
| 2026-08-24 | IMPLEMENTATION | C:\Users\User\Game\src\Miji\Assets\Scripts\Gameplay\Player\PlayerAnimator.cs | [자동 기록] |

### 8차 — A·B 애니메이션 대량 반입 (2026-08-24) ✅

- **A 64px 애니**: run(6f)/jump(3f)/fall(6f)/turn을 64px/PPU 64로 반입. `A_Run.anim` 6프레임 재작성, `A_run_6/7` 삭제
- **A fall 거리비례 틸트**: `PlayerAnimator.ApplyFallTilt()` — 하강거리를 `fallForMaxTilt`(1.5u)로 정규화해 fall_0~5 선택, LateUpdate 덮어쓰기(물리 불간섭). 두 씬 Player_A 배선
- **B jump/fall 신규**: PixelLab `animate_with_text_v3` 생성 → `B_Jump.anim`(비루프)/`B_Fall.anim`(루프) + `B_Animator` Jump/Fall 상태·전이·파라미터(Grounded/VSpeed)
- **CompanionFollower**: A의 실제 세로속도로 VSpeed 구동, `bGrounded = A접지 && B가 A높이 0.06u 이내`(🐛 A착지=B착지 버그 수정)
- **에디터 툴**: `Assets/Scripts/Editor/CharacterAnimationTool.cs` (`Miji ▸ Animation ▸ Character Animation Tool`) — A·B 애니 원클릭 반입
- 검증: compile 0/0 · EditMode 25 + PlayMode 8 = 33/33 · uloop 플레이 실측
