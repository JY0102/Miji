# Game Project — Agent Orchestration Guide

## Project Overview
- **Genre**: Metroidvania
- **Art Style**: Pixel Art (OpenAI DALL-E / gpt-image-1 — 프롬프트 생성 후 수동 실행)
- **Engine**: Godot 4
- **Language**: GDScript
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
| 맵, 구역, 룸, 동선, 체크포인트 | Planning → Level Design |
| 기믹, 메카닉, 능력, 전투, 시스템 | Planning → Mechanics |
| 코드, 구현, 버그, 기능, 클래스 | Implementation |
| 스프라이트, 애니메이션, 픽셀, 아트, 이미지 | Art → 프롬프트 생성 |
| 시장성, 차별성, 클리셰, 레퍼런스 비교, 스토리 평가 | Story Critic |
| 복합 요청 | Split and delegate in parallel |

**Story Critic은 Planning → Story의 상위 검수자다.** Claude Code 측에서는 `.claude/agents/story-critic.md` 서브에이전트로 정의되어 있고, 스토리 MD 작성 시 훅이 자동 호출한다. 채점 기준은 `docs/planning/story/STORY_CRITIC_RUBRIC.md`, 대조 자료는 `STORY_REFERENCES.md`(메트로배니아 10작)와 `STORY_REFERENCES_NARRATIVE.md`(서사 명작 10작)이다. 시장성 / 차별성 / 클리셰 운용을 각 5점으로 채점한다.

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

### [IMPLEMENTATION] 기능 구현 에이전트
Handles all code. Agents can be dynamically spawned per feature.

**Responsibilities:**
- Write and maintain all source code under `src/`
- Each major feature gets its own sub-agent context (scoped prompt)
- Maintains `docs/agents/IMPL_REGISTRY.md` — a registry of all active implementation agents and their assigned features
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
├── AGENTS.md                        ← This file (orchestration rules)
├── .Codex/
│   └── settings.json               ← Permissions
├── docs/
│   ├── planning/
│   │   ├── story/                  ← 스토리, 캐릭터
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
