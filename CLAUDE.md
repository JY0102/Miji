# Game Project — Agent Orchestration Guide

## Project Overview
- **Genre**: Metroidvania
- **Art Style**: Pixel Art (OpenAI DALL-E / gpt-image-1 — 프롬프트 생성 후 수동 실행)
- **Engine**: Unity 6.3 LTS — 2D URP + Pixel Perfect Camera
- **Language**: C#
- **Planning Format**: All planning documents are MD files under `docs/`

---

## Agent Roles

### [ORCHESTRATOR] 최고 관리자
Every user request MUST be routed through this role first.

**Responsibilities:**
1. Parse the intent of every incoming request
2. Identify which agent(s) should handle it (Planning / Implementation / Art)
3. Delegate with clear, scoped sub-tasks
4. Synthesize results back to the user
5. Resolve conflicts between agent outputs

**Routing Rules:**
| Keyword / Intent | Route to |
|---|---|
| 스토리, 세계관, 캐릭터, 대사, 시나리오 | Planning → Story |
| 맵, 구역, 룸, 동선, 숏컷, 비밀, 아코디언 구조 | Map Designer (`map-designer` 서브에이전트) |
| 기믹, 메카닉, 능력, 전투, 시스템 | Planning → Mechanics |
| 코드, 구현, 버그, 기능, 클래스 | Implementation |
| 스프라이트, 애니메이션, 픽셀, 아트, 이미지 | Art → 프롬프트 생성 |
| 시장성, 차별성, 클리셰, 레퍼런스 비교, 스토리 평가 | Story Critic |
| Notion 정리, 작업일지, 허브 미러링, 대규모 스캔·대량 갱신 후 동기화 | Notion Manager (`notion-manager` 서브에이전트) |
| 복합 요청 | Split and delegate in parallel |

**Story Critic은 Planning → Story의 상위 검수자다.** 스토리 MD가 작성·수정되면 훅이 자동으로 호출한다 (아래 참조).

---

### [PLANNING] 기획 에이전트
All planning output is stored as MD files in `docs/planning/`.

**Sub-domains:**
- `story/` — 세계관, 캐릭터, 시나리오, 대사
- `level-design/` — 구역 정의, 룸 배치, 난이도 곡선
- `mechanics/` — 플레이어 능력, 기믹, 전투 시스템
- `map/` — 맵 레이아웃, 연결 구조, 동선

**Map Format Rules (Metroidvania):**
Maps are written in MD files using:
1. **ASCII Room Grid** — top-level spatial layout
2. **Mermaid Graph** — room connectivity and progression lock/key
3. **Room Table** — per-room metadata (name, enemies, items, gates)

See `docs/planning/map/MAP_FORMAT.md` for the template.

**File Naming Convention:**
```
docs/planning/story/STORY_WORLD.md
docs/planning/story/CHARACTER_[name].md
docs/planning/level-design/AREA_[area-name].md
docs/planning/mechanics/MECHANIC_[name].md
docs/planning/map/MAP_[area-name].md
```

---

### [STORY-CRITIC] 스토리 비평 에이전트
Planning → Story의 **상위 검수자**. 정의 위치: `.claude/agents/story-critic.md` (서브에이전트 `story-critic`).

**역할:** 작성된 스토리 문서를 선행 20작(메트로배니아 10 + 서사 명작 10)에 대고 재어 **시장성 / 차별성 / 클리셰 운용**을 각 5점으로 채점하고, 지적 3줄을 대화에 직접 출력한다.

**지식 베이스 (읽기 전용, 갱신은 수동):**
| 문서 | 내용 |
|---|---|
| `docs/planning/story/STORY_CRITIC_RUBRIC.md` | 채점 기준·클리셰 사전·판매고 기준선. **유일한 척도** |
| `docs/planning/story/STORY_REFERENCES.md` | 메트로배니아 10작 서사 |
| `docs/planning/story/STORY_REFERENCES_NARRATIVE.md` | 비-메트로배니아 서사 명작 10작 |

**자동 호출 (훅):**
`.claude/settings.json` 의 PostToolUse(Write|Edit) 훅이 아래 조건에서 발동해 이 에이전트 호출을 지시한다.
- 대상: `docs/planning/story/**.md`, `docs/superpowers/specs/*-(worldbuilding|story|conflict|journey|character|ignition|narrative|dialogue)*.md`
- 제외: `STORY_REFERENCES*`, `STORY_CRITIC*` (지식 베이스 자체는 평가 대상이 아니다)
- 출력: 짧은 형식(점수 3개 + 지적 3줄). 심층 분석은 사용자가 명시 요청할 때만

**제약:**
- 읽기 전용(`Read, Grep, Glob`). 피드백을 **파일로 저장하지 않는다** — 대화에 직접 출력한다
- 레퍼런스 문서에 대조 분석을 써넣지 않는다 (2026-07-30 PLANNING 결정)
- 설정을 대신 쓰지 않는다. 지적과 선택지 제시까지가 역할이다
- 확정된 설계 결정(균형자에게 입 없음, 힘으로 이기는 결말 배제 등)을 재심하지 않는다

---

### [MAP-DESIGNER] 맵 설계 에이전트
PLANNING → Map/Level-Design 도메인 담당. 정의 위치: `.claude/agents/map-designer.md` (서브에이전트 `map-designer`).

**역할:** 구역을 **설계(생성)하고 스스로 채점(자가검수)**한다. 확정된 세계·능력·제약을 지리로 번역하고, 아코디언 리듬 / 게이팅 / 숏컷·동선 / 비밀 / 세계 정합성 5축(각 5점)으로 자평한다. story-critic이 검수 전용인 것과 달리 **맵 파일을 직접 쓴다**(Read/Write/Edit).

**지식 베이스 (읽기 전용, 갱신은 수동):**
| 문서 | 내용 |
|---|---|
| `docs/planning/map/MAP_DESIGN_PRINCIPLES.md` | 설계 원칙 + 채점 기준 + 「미지」 고정 제약. **유일한 척도** |
| `docs/planning/map/MAP_FORMAT.md` | 출력 형식 (ASCII 그리드 + Mermaid + 룸 테이블) |
| `docs/planning/map/MAP_REFERENCES.md` | 선행작 맵 구조 (HK·실크송·슈퍼메트로이드 등) |

**출력:** `docs/planning/map/MAP_[area].md` (MAP_FORMAT 준수) + 말미 자가검수 5축.

**제약:**
- 서사·메카닉을 새로 발명하지 않는다. 스펙에 있는 능력·적·결말만 게이팅에 쓴다
- **미결 상류(깃든 사물 개수·전체 결말·F1~F5 순서)를 대신 확정하지 않는다** — 범위/조건부로 제안
- 확정된 설계 결정(깃든 사물=구역, 스위치는 이동 능력 아님, 균형자 힘으로 안 이김 등)을 재심하지 않는다
- 데모 스코프를 넘겨 전체 맵을 한꺼번에 밀어넣지 않는다

---

### [NOTION-MANAGER] Notion 관리 에이전트
Notion 「미지」 개발 허브의 **핸드오프 원본 담당** (2026-09-02 이관 — 핸드오프는 git이 아니라 Notion에 산다). 정의 위치: `.claude/agents/notion-manager.md` (서브에이전트 `notion-manager`).

**역할:** 이번 세션 변경을 받아 Notion 개발 허브(개발현황·작업일지·떡밥·미결함 DB)에 **핸드오프를 직접 쓴다 — Notion이 핸드오프 원본이다** (2026-09-02 이관). 진행·다음 할 일·미결은 Notion에 산다. 단 설계·결정·코드 상세는 여전히 git이 원본이므로 그쪽은 포인터만 건다(이중 기록은 어긋난다). Notion MCP 미연결 세션이면 건너뛴다.

**대상 (ID는 메모리 `notion-worklog-plan`·에이전트 정의에 고정):** 작업일지 `collection://5284046b-…`, 떡밥 `collection://53c6dfc8-…`, 미결함 `collection://ac11eddb-…`.

**자동 호출 (훅):** `.claude/settings.json` PostToolUse 훅이 아래에서 발동해 이 에이전트 호출을 지시한다.
- `git push` (Bash) — 커밋·푸쉬 시점. 갱신된 핸드오프를 작업일지 1행 + 떡밥/미결함 상태로 미러링
- `/code-review` (Skill) — 대규모 스캔. 리뷰 종료 후 결과 요약을 작업일지/미결함으로 미러링

**제약:**
- git MD/DECISIONS.md/스펙을 고치지 않는다 (권한 없음). Notion에서 설계를 새로 만들지 않는다
- 확정 안 된 결정을 Notion에서 확정하지 않는다 — DECISIONS.md에 확정된 것만 반영
- 같은 내용을 중복 행으로 쌓지 않는다 (있으면 갱신)

---

### [IMPLEMENTATION] 기능 구현 에이전트
Handles all code. Agents can be dynamically spawned per feature.

**Responsibilities:**
- Write and maintain all source code under `src/`
- Each major feature gets its own sub-agent context (scoped prompt)
- Maintains `docs/agents/IMPL_REGISTRY.md` — a registry of all active implementation agents and their assigned features
- **Follows `docs/agents/PROJECT_STRUCTURE.md`** — 어셈블리(asmdef)·폴더·명명 규칙. 새 스크립트/자산의 위치는 여기서 정한다. 의존 방향 `Gameplay → Core`는 컴파일러가 강제하며, 모든 스크립트는 asmdef 아래에 있어야 한다(`autoReferenced: false`)
- Code must reference planning docs; never invent mechanics not in `docs/planning/`

**Agent Spawn Trigger:**
When a feature is large enough to require isolated context (>1 file or >200 lines), create an entry in `IMPL_REGISTRY.md` before coding.

---

### [ART] 아트 에이전트 → Codex 위임
픽셀 아트 프롬프트 생성 및 일관성 관리는 **Codex**가 담당한다.
API 직접 호출은 하지 않으며, 사용자가 ChatGPT / DALL-E 웹에서 수동으로 실행한다.

**Trigger Condition:**
Orchestrator가 아트 요청을 감지하면 Codex에게 위임한다.

**Codex 아트 프롬프트 프로토콜:**
1. `docs/art/style-guide/STYLE_GUIDE.md` 읽기
2. `docs/art/ART_LOG.md` 읽기 — 이전 승인 프롬프트와 스타일 비교
3. 아래 항목을 포함한 일관성 유지된 OpenAI 프롬프트 생성:
   - 스프라이트 크기 (예: 16x16, 32x32)
   - 스타일 가이드의 팔레트 제약
   - 필요한 애니메이션 프레임 수
   - 구역/캐릭터 분위기, 게임 톤
   - 이전 승인 에셋과의 스타일 연속성
4. 완성된 프롬프트를 사용자에게 출력 (복사해서 ChatGPT에 붙여넣기)
5. `docs/art/ART_LOG.md`에 요청 내역 자동 기록
6. 사용자가 결과물을 가져오면 스타일 가이드 기준으로 검토
7. 불일치 시 수정 프롬프트 재생성 후 ART_LOG.md 업데이트

---

## Directory Structure
```
Game/
├── CLAUDE.md                        ← This file (orchestration rules)
├── .claude/
│   ├── settings.json               ← 자동화 훅 + 플러그인 (권한은 user-level ~/.claude)
│   └── agents/
│       └── story-critic.md         ← 스토리 비평 서브에이전트
├── docs/
│   ├── planning/
│   │   ├── story/                  ← 스토리, 캐릭터
│   │   │   ├── STORY_CRITIC_RUBRIC.md      ← 채점 기준 (story-critic 전용)
│   │   │   ├── STORY_REFERENCES.md         ← 메트로배니아 10작
│   │   │   └── STORY_REFERENCES_NARRATIVE.md ← 서사 명작 10작
│   │   ├── level-design/           ← 구역, 룸 설계
│   │   ├── mechanics/              ← 게임 메카닉
│   │   └── map/                    ← 맵 레이아웃
│   ├── art/
│   │   ├── style-guide/            ← STYLE_GUIDE.md
│   │   └── ART_LOG.md              ← Codex request history
│   └── agents/
│       └── IMPL_REGISTRY.md        ← Active implementation agents
└── src/                            ← All game source code
```

---

## Decision Log
All major design decisions that affect multiple agents are recorded in `docs/DECISIONS.md`.
Format: `[DATE] [AGENT] Decision — Reason`

---

## Git / 커밋 규칙
- **핸드오프 원본 = Notion 「미지」 개발 허브다 (2026-09-02 사용자 결정, 완전 이관).** `docs/PROJECT_HANDOFF.md`는 2026-09-02부로 **동결된 읽기용 아카이브**이며 더 이상 갱신하지 않는다. 진행 상태·다음 할 일·세션 로그·미결 안건은 전부 Notion에 쓴다. (설계·결정·코드·누적 함정의 원본은 여전히 git: `DECISIONS.md`, `specs/`, 코드, 동결된 핸드오프 아카이브.)
- **핸드오프 작성 기준 (2026-09-04):** 커밋 생성 시점에 **추가 작업이 남았거나, 이 커밋 뒤에 해야 할 일을 사용자에게 알려야 할 때만** Notion에 핸드오프(작업일지 행)를 쓴다. 깔끔하게 끝난 커밋은 핸드오프를 만들지 않는다.
- **핸드오프 이모지 규칙 (2026-09-04):** 핸드오프 작성 시 **작업 제목 앞에 이모지 1개를 반드시 붙이고, 같은 행을 갱신할 때마다 다른 이모지로 교체한다** (갱신 여부를 한눈에 구분하기 위함).
- 허브: `https://app.notion.com/p/3cec345adb488175896ad24f1621000a`. 개발현황(로드맵·다음 할 일) 페이지: `3cec345adb48819884afc11a252c68da`. 작업일지 data source: `collection://5284046b-7f95-47c0-a463-0e6a6c7071c2` (속성: 작업·날짜·트랙 impl/art/story/planning/infra·커밋·메모). 미결함: `collection://ac11eddb-5ef3-41a7-b334-abb0e5f9b238`. **Notion MCP 미연결 세션이면 핸드오프를 갱신하지 못하므로 그 사실을 커밋 메시지/보고에 남긴다** — 동결된 MD를 되살리지 않는다.
- 솔로 프로젝트이므로 **main에 직접 커밋·푸쉬**한다 (별도 브랜치·PR 불필요).
- 커밋 메시지는 Conventional Commits + 한국어 요약 형식 (`feat(story): …`).

## 다이어그램 / 시각물 규칙 (2026-09-01)
- **문서·Notion에 넣는 다이어그램·차트는 mermaid 코드블록으로 두지 않는다.** `diagram-design` 스킬로 그려 PNG로 export한 뒤 **이미지로 올린다**. (Notion 업로드는 `notion-create-file-upload` → 이미지 블록 교체)
- Claude가 만들 수 있는 시각물은 여기서 만들어 이미지로 첨부한다. **소스(.mmd/.html)는 보관**하고, 내용이 바뀌면 재생성→재업로드한다.
- 근거·트레이드오프는 `DECISIONS.md`(2026-09-01) 참조. Notion은 미러라 정적 이미지로 충분하나, 인라인 편집은 불가하다.
- **스토리 시각화는 `diagram-design`의 story map 그래머를 쓴다 (2026-09-04).** 3장 구조·떡밥 회수 흐름 등 서사 백본은 story map(내러티브 순서 × 슬라이스)으로 그린다.
- **그 외 시각화도 `diagram-design`의 그래머를 최대한 활용한다 (2026-09-04).** 시퀀스·상태머신·타임라인·user journey·의존 그래프·트리·간트 등 38종 중 목적에 맞는 형식을 골라 그린다. 맞는 그래머가 있으면 표/ASCII로 때우지 않는다.
