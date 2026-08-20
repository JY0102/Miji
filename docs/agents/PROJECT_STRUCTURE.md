# 프로젝트 구조 규칙 — 어셈블리 · 폴더 · 명명

**대상:** `src/Miji/` (Unity 6.3, 2D URP)
**확정:** 2026-08-20 (구조 감사 + 정리 작업 시)
**상위 근거:** `docs/superpowers/specs/2026-08-19-implementation-core-architecture-design.md` 0절

> 이 문서는 **어디에 무엇을 두는가**만 정한다. 무엇을 만드는가는 위 스펙과 `IMPL_REGISTRY.md`가 정한다.

---

## 0. 한 줄 원칙

> **Core는 서사가 바뀌어도 안 바뀐다. Gameplay는 기획 문서를 안다.**
> 의존은 단방향 `Gameplay → Core`이며, **문서 규칙이 아니라 컴파일 규칙**이다.

---

## 1. 어셈블리 (asmdef)

| 어셈블리 | 위치 | 참조 | 플랫폼 |
|---|---|---|---|
| `Miji.Core` | `Assets/Scripts/Core/` | Unity.InputSystem | 전체 |
| `Miji.Gameplay` | `Assets/Scripts/Gameplay/` | **Miji.Core**, Unity.InputSystem | 전체 |
| `Miji.Core.Tests` | `Assets/Tests/EditMode/Core/` | Miji.Core | Editor |
| `Miji.Gameplay.Tests` | `Assets/Tests/EditMode/Gameplay/` | Miji.Core, Miji.Gameplay | Editor |
| `Miji.Gameplay.PlayTests` | `Assets/Tests/PlayMode/` | Miji.Core, Miji.Gameplay | **전체 (`includePlatforms: []`)** |
| `Miji.Editor` | `Assets/Scripts/Editor/` | 필요 시 | Editor | ⬜ **예약** — 에디터 툴이 처음 생길 때 만든다 |

### 어겨서는 안 되는 것

1. **`Miji.Core`는 `Miji.Gameplay`를 참조하지 않는다.** 역방향 통신은 `EventBus`로만 한다.
2. **Core의 식별자(클래스·필드·enum·네임스페이스)에 스토리 명사를 쓰지 않는다** — 딸각·무리비·균형자·열하나·여산 등. 컴파일러가 잡아주지 않는 층이므로 여기서만 사람이 지킨다.
   - **주석은 예외다.** 「왜 이렇게 설계했나」를 설명하려고 고유명사를 드는 것은 허용한다 (예: `Health.cs`의 "B(무리비)에게는 붙지 않는다"). 설계 근거를 지우면 다음 사람이 같은 실수를 한다.
   - `Faction.Player` 같은 **장르 일반 용어는 스토리 명사가 아니다.**
3. **`autoReferenced`는 전부 `false`다.** 켜두면 asmdef 밖에 떨어진 스크립트(Assembly-CSharp)가 Core·Gameplay를 **양쪽 다** 참조할 수 있어 단방향 규칙에 우회로가 생긴다.
   → **따라서 모든 게임 스크립트는 반드시 asmdef 아래에 있어야 한다.** `Assets/` 아무 데나 .cs를 떨구면 Core를 못 본다. 이건 버그가 아니라 의도다.
4. **PlayMode 테스트 asmdef는 `includePlatforms: []`** 를 유지한다. `["Editor"]`로 바꾸면 EditMode로 돌아 물리가 안 돌고 전부 실패한다 (2026-08-19에 한 번 밟은 함정).

### 테스트를 어디에 쓰는가

| 대상 | 어디 |
|---|---|
| 순수 계산·상태기계·이벤트 (물리·프레임 불필요) | **EditMode** — `Core/` 또는 `Gameplay/` |
| 중력·충돌·조작감처럼 **프레임이 지나야** 재는 것 | **PlayMode** |

「Gameplay 코드라서 PlayMode」가 아니다. `TurnView`처럼 Gameplay에 있어도 순수 계산이면 EditMode가 맞다 — 그래서 `Miji.Gameplay.Tests`가 있다.

---

## 2. 폴더

```
Assets/
├── Art/
│   ├── Characters/
│   │   ├── A/  ├── Sprites/       프레임 png
│   │   │       └── Animations/    .anim + .controller
│   │   └── B/  (동일 구조)
│   ├── Environment/
│   │   ├── Backgrounds/           패럴랙스 배경
│   │   └── Tiles/                 타일맵 소스
│   └── UI/                        ⬜ 예약
├── Scenes/
│   ├── Greybox/                   실험·조작감 검증 씬
│   └── Demo/                      ⬜ 예약 — 데모 조립 씬
├── Scripts/
│   ├── Core/         (Miji.Core)
│   │   ├── Combat/ Events/ Flow/ Input/ StateMachines/
│   │   └── Progression/ Save/ Rooms/     ⬜ 예약 (C4·C5·C8)
│   ├── Gameplay/     (Miji.Gameplay)
│   │   └── Bootstrap/ Player/ Companion/ View/ World/
│   └── Editor/                    ⬜ 예약 (Miji.Editor)
├── Settings/                      URP·렌더러 — Unity 기본 위치, 옮기지 않는다
└── Tests/
    ├── EditMode/ Core/ Gameplay/
    └── PlayMode/
```

### 아트는 **역할이 아니라 정체성**으로 가른다 ★

`Art/Player/`가 아니라 `Art/Characters/A/`인 이유:

> **2장에서 조작권이 인계된다.** A는 1·3장에서 플레이어지만 2장에서는 아니고, 그때 조작되는 것은 열하나다.
> 역할(Player/NPC)로 폴더를 가르면 확정된 설계가 폴더를 깨뜨린다. 정체성으로 가르면 안 깨진다.

**스크립트는 반대로 역할로 가른다** (`Gameplay/Player/`, `Gameplay/Companion/`). 스크립트가 다루는 것은 「이 몸이 지금 무슨 일을 하는가」이기 때문이다. 층이 다르므로 기준이 달라도 된다.

### 예약 폴더는 미리 만들지 않는다

⬜ 표시는 **이름만 정해둔 것**이다. 빈 폴더나 빈 어셈블리를 미리 만들지 않는다 — Unity가 빈 .meta를 남기고 리포에 노이즈가 된다. 필요해지는 순간 이 표의 이름으로 만든다.

---

## 3. 명명

| 대상 | 규칙 | 예 |
|---|---|---|
| 네임스페이스 | **폴더 경로와 1:1** | `Scripts/Gameplay/View/` → `Miji.Gameplay.View` |
| 어셈블리 | `Miji.<층>[.<용도>]` | `Miji.Gameplay.PlayTests` |
| 캐릭터 스프라이트 | `<캐릭터>_<동작>_<번호>.png` (동작은 소문자) | `A_run_3.png`, `B_sleep_0.png` |
| 애니 클립 | `<캐릭터>_<동작>.anim` (동작은 파스칼) | `A_Idle.anim` |
| 애니메이터 | `<캐릭터>_Animator.controller` | `B_Animator.controller` |
| 씬 | `<구분>_<내용>.unity` | `Greybox_Movement.unity` |
| 타일 | `Tile_<지형><부위>.png` | `Tile_CaveTop.png` |

**네임스페이스가 폴더와 어긋나면 폴더가 아니라 네임스페이스를 고친다.** 파일을 옮겼으면 네임스페이스도 옮긴다 — 씬의 `m_EditorClassIdentifier`도 같이 갱신해야 한다(GUID가 본체라 안 해도 돌아가지만, 텍스트 diff가 거짓말을 하게 된다).

---

## 4. 자산을 옮길 때

Unity 자산은 **`.meta`가 본체**다. GUID가 거기 들어 있고, 씬·클립·프리팹은 전부 GUID로 서로를 가리킨다.

1. **파일과 `.meta`를 항상 함께 옮긴다.** 하나만 옮기면 참조가 통째로 끊긴다
2. `git mv`를 쓴다. **경로 표기는 `src/Miji`(대문자 M)** 로 통일한다 — git 인덱스·커밋 트리·어셈블리 이름(`Miji.*`)이 전부 대문자다
3. 옮긴 뒤 **`uloop compile` → `run-tests`**로 확인한다. `npx --yes uloop-cli@2.2.0` 로 부르면 전역 설치가 필요 없다
4. 빈 폴더가 남으면 폴더와 그 `.meta`를 함께 지운다. 지우기 전에 **폴더 GUID를 참조하는 곳이 없는지** 확인한다

---

## 5. 알려진 미해결

| | 내용 | 조치 |
|---|---|---|
| ✅ | ~~`src/miji`(디스크) vs `src/Miji`(git 인덱스) 대소문자 불일치~~ | **2026-08-20 해소** — 디스크를 `src/Miji`로 개명. 방치했으면 이날 만든 미추적 `.meta`들이 소문자 경로로 커밋되어 리포 트리에 `src/Miji`·`src/miji` 두 갈래가 생길 뻔했다. ⚠️ **역사 문서의 `src/miji/` 표기는 손대지 말 것** — 삭제된 Godot 프로젝트의 실제 경로이며 이 건과 무관하다 |
| ✅ | ~~빌드 세팅에 `SampleScene`만 등록돼 있다~~ | **2026-08-20 해소** — 사용자 지시로 `SampleScene` 삭제, 빌드 세팅을 `Scenes/Greybox/Greybox_Movement.unity`로 교체. 데모 씬이 생기면 그때 다시 교체한다 |
| ⬜ | `Assets/` 루트에 `DefaultVolumeProfile` · `InputSystem_Actions` · `UniversalRenderPipelineGlobalSettings` 3종이 흩어져 있다 | Unity 기본 생성물이라 옮기면 ProjectSettings 경로 참조가 깨질 수 있다. **이득 대비 위험이 커서 두기로 결정** |
