# 스토리 평가 루브릭 (story-critic 에이전트 전용)

**작성:** 2026-08-04
**목적:** 「미지」의 스토리 문서를 **시장성 / 차별성 / 클리셰 운용** 세 축으로 채점하기 위한 고정 기준선
**사용 주체:** `.claude/agents/story-critic.md` 서브에이전트. 사람이 직접 읽어도 되지만 갱신은 수동이다
**전제 문서:**
- `STORY_REFERENCES.md` — 메트로배니아 10작 서사 정리
- `STORY_REFERENCES_NARRATIVE.md` — 비-메트로배니아 서사 명작 10작 정리

> 이 문서는 **자(尺)**다. 「미지」의 설정을 담지 않는다. 설정의 출처는 언제나 `docs/superpowers/specs/` 의 현행 스펙이며, 이 문서는 그것을 어디에 대고 재는지만 규정한다.

---

## 0. 유효 문서 가드 ★★ (평가 전 필수 확인)

2026-07-30 세계관 전면 리부트로 **폐기된 문서가 파일 시스템에 그대로 남아 있고, 파일 자체에는 폐기 표시가 없다.** 죽은 설정을 현행으로 오인하면 평가 전체가 무의미해진다.

| 상태 | 문서 |
|---|---|
| ✅ **현행** | `specs/2026-07-30-mind-economy-worldbuilding-design.md`<br>`specs/2026-08-03-story-ignition-design.md`<br>`specs/2026-08-04-journey-design.md`<br>`specs/2026-08-04-conflict-design.md`<br>`planning/story/CHARACTER_B.md`<br>`docs/DECISIONS.md` 2026-07-30 이후 행 |
| ⛔ **폐기** | `specs/2026-07-24-*` 4종, `specs/2026-07-29-medium-worldbuilding-design.md`<br>(`planning/story/CHARACTER_매개체.md`·`CHARACTER_얼개.md`·`CHARACTER_온기.md` — 2026-08-05 삭제됨) |

**판정 규칙:** 「매개체」, 「얼개」, 「온기」, 「스왑」, 「로봇 대 생명체 전쟁」이 등장하면 폐기된 설정이다. 평가 대상 문서가 이 용어를 현행으로 쓰고 있으면 **채점하지 말고 그 사실부터 지적한다.**
갱신 시 `docs/PROJECT_HANDOFF.md` 「다음에 할 일」과 대조할 것.

---

## 1. 시장 기준선

### 1-1. 판매고 티어

수치는 **공식 발표**와 **제3자 추정**을 구분해 표기했다. 추정치는 절대값이 아니라 자릿수로만 쓴다.

| 티어 | 판매고 | 해당 작품 |
|---|---|---|
| **S — 장르 정의작** | 1,000만+ | Hollow Knight ≈1,500만(2025-08, 공식) · NieR:Automata 1,000만+(2026, 공식) |
| **A — 대형 성공** | 300만~1,000만 | Blasphemous 400만+(2025-09) · Undertale ≈630만 Steam(추정) · Papers, Please 500만(2023, 공식) · To the Moon ≈460만 Steam(추정) |
| **B — 확실한 흑자** | 100만~300만 | Disco Elysium ≈260만(추정) · Outer Wilds 200만+ · Ender Lilies 150만(2024-07, 공식·후속작 합산 200만+) · OMORI 100만+(2022, 공식) · SOMA 100만+ PC(공식) |
| **C — 손익분기 근처** | 30만~100만 | Nine Sols 80만(전 플랫폼) · Brothers 80만(2015 시점, 이후 미발표) |
| **D — 컬트** | 30만 미만 | Environmental Station Alpha · Iconoclasts · Axiom Verge(정확 수치 미공개, 이 대역 추정) |

**해석 규칙**
- 1인~소규모 인디의 현실적 목표 대역은 **C~B**다. S/A를 기준선으로 잡고 평가하면 모든 기획이 실패로 판정된다
- 메트로배니아는 **완성도가 판매를 견인하고 서사는 잔존율·입소문을 견인한다.** 서사만으로 티어가 올라간 사례는 Hollow Knight·Nine Sols처럼 **서사 전달 방식이 장르 문법과 결합**했을 때뿐이다
- 반대로 서사 명작 10작 중 절반(Papers Please, Undertale, To the Moon, Disco Elysium, Brothers)은 **그래픽·볼륨이 작은데도 A/B에 도달했다.** 공통점은 「이 게임에만 있는 장치」가 한 개씩 있다는 것 — 서류 노동, 세이브 파일 인식, 역순 기억, 스킬 인격, 컨트롤러 절반

### 1-2. 장르 환경 (2026 기준)

- 메트로배니아는 **진입 장벽이 낮아 공급 과잉 상태**다. 신작 다수가 눈에 띄지 못하고 회수에 실패한다는 것이 반복 관찰되는 서술이다
- 따라서 **「잘 만든 메트로배니아」는 차별점이 아니다.** 판매 상위권 진입의 실질 조건은 ① 시각적 서명 ② 전투감 ③ 한 문장으로 말할 수 있는 후크 — 이 셋 중 최소 하나가 압도적일 것
- ⚠️ 시중의 「메트로배니아 시장 규모 $X억, CAGR Y%」 류 수치는 자동 생성 SEO 리포트에서 나온 것으로 **근거가 확인되지 않는다. 인용하지 말 것**

---

## 2. 클리셰 사전

20작에서 반복 관찰된 패턴. **포화도**는 「관객이 이미 봤다고 느낄 확률」이며, 「탈출 조건」은 그 패턴을 쓰면서도 진부해지지 않기 위해 반드시 충족해야 할 것이다.

### 2-1. 설정 층위

| 패턴 | 포화도 | 대표작 | 탈출 조건 |
|---|---|---|---|
| 기억 없는 주인공이 깨어난다 | 🔴 포화 | Disco Elysium, SOMA, OMORI, Axiom Verge | 기억이 **되찾을 수 있는 퍼즐이 아닐 것**. 회수되지 않는 공백이어야 한다 |
| 멸망한 고대 문명의 유적 탐사 | 🔴 포화 | Hollow Knight, Outer Wilds, Nine Sols, La-Mulana | 유적이 **주인공과 무관**할 것. 「너를 위해 준비된 유산」이면 실패 |
| 벽의 기록·아이템 설명문으로 로어 전달 | 🔴 포화 | Hollow Knight, Outer Wilds, Nine Sols | 조각을 **모으면 그림이 완성되는 구조를 피할 것** |
| 주인공이 사실 특별한 개체였다 | 🔴 포화 | Hollow Knight(그릇), NieR(2E), Nine Sols(제10주) | — |
| 자애로운 창조자가 사실 원흉 | 🟠 흔함 | Nine Sols(이공), NieR(사령부), SOMA(WAU) | 원흉에게 **악의가 없을 것** |
| 세계가 서서히 잠식된다(감염·회백·곰팡이) | 🟠 흔함 | Hollow Knight, Disco Elysium, TLoU | 잠식이 **막을 수 있는 사건이 아닐 것** |
| 인공물이 자아를 얻는다 | 🟠 흔함 | NieR, SOMA, Portal 2, Talos, Stray | 자아를 **주제로 논하지 말 것**(대사로 「나는 무엇인가」 금지) |

### 2-2. 구조 층위

| 패턴 | 포화도 | 대표작 | 탈출 조건 |
|---|---|---|---|
| 동행자가 죽고 혼자 남는다 | 🔴 포화 | Brothers, TLoU, Ender Lilies | 상실이 **각성의 연료가 되지 않을 것**. 강해지면 실패 |
| 반복/회귀로 진실에 접근 | 🟠 흔함 | Outer Wilds, NieR, Undertale | — |
| 알수록 망가진다 | 🟡 드묾 | Outer Wilds(아울크), SOMA | 대가가 **은유가 아니라 시스템일 것** |
| 시스템·현상이 적, 악당 없음 | 🟡 드묾 | Papers Please, Outer Wilds | 적에게 **입을 주지 말 것**. 말이 통하는 순간 흑막이 된다 |
| 진실을 알려주지 않는 보호 | 🟡 드묾 | TLoU(조엘의 거짓말), To the Moon | 숨기는 쪽이 **옳지 않을 것**. 양쪽 다 정당해야 한다 |
| 매체·조작 자체를 서사 장치로 | 🟢 희귀 | Undertale, NieR(엔딩 E), Brothers, Papers Please | 한 작품에 **하나만** 쓸 것 |

### 2-3. 결말 층위

| 패턴 | 포화도 | 대표작 | 탈출 조건 |
|---|---|---|---|
| 희생으로 순환을 끊는다 | 🔴 포화 | Hollow Knight, Nine Sols, Ender Lilies | — |
| 힘으로 최종 존재를 쓰러뜨린다 | 🔴 포화 | 메트로배니아 거의 전부 | ⛔ 「미지」는 이미 배제함(갈등 스펙) |
| 최종보스가 사실 불쌍하다 | 🟠 흔함 | Hollow Knight, Undertale, Nine Sols | 연민이 **전투 후에 오지 않을 것** |
| 아무것도 해결되지 않고 관계만 남는다 | 🟢 희귀 | Disco Elysium, Outer Wilds, TLoU | 여운이 **체념으로 읽히지 않을 것** |

**클리셰 채점 원칙:** 클리셰를 쓰는 것 자체는 감점이 아니다. **탈출 조건을 충족하지 못한 채 쓰는 것**만 감점한다. 반대로 🔴 포화 패턴을 정면으로 쓰면서 탈출 조건을 지키면 가점한다 — 관객의 예상을 이용한 것이기 때문이다.

---

## 3. 채점 축

각 축 **5점 만점, 정수**. 점수는 서열이 아니라 **어느 칸에 있는지**를 가리킨다.

### 3-1. 시장성 — "이걸 왜 사는지 한 문장으로 말할 수 있는가"

| 점수 | 상태 |
|---|---|
| 5 | 한 문장 후크가 존재하고, 그 후크가 **플레이 중에 계속 작동**한다 |
| 4 | 후크는 있으나 초반에만 작동하거나, 스크린샷으로 전달되지 않는다 |
| 3 | 완성도로 승부해야 하는 상태. 서사가 구매 이유가 되지 못한다 |
| 2 | 유사 선행작이 명확히 존재하고 그보다 나은 점을 말할 수 없다 |
| 1 | 설명에 두 문단이 필요하다 |

⚠️ 이 축은 **문학적 완성도와 무관하다.** 좋은 이야기가 3점을 받을 수 있고, 그것은 결함이 아니라 정보다.

### 3-2. 차별성 — "선행 20작 중 가장 가까운 작품과 무엇이 다른가"

채점 전 반드시 **가장 가까운 작품 1~2개를 지목**한다. 지목 없이 나온 차별성 점수는 무효다.

| 점수 | 상태 |
|---|---|
| 5 | 가장 가까운 작품과 **전제부터** 다르다 |
| 4 | 전제는 같으나 **결론이 반대**다 |
| 3 | 조합이 새롭다(개별 요소는 기존) |
| 2 | 톤과 소재만 다르다 |
| 1 | 선행작으로 설명이 끝난다 |

### 3-3. 클리셰 운용 — "빌려온 것을 갚았는가"

| 점수 | 상태 |
|---|---|
| 5 | 🔴 포화 패턴을 정면으로 쓰면서 탈출 조건을 전부 지켰다 |
| 4 | 클리셰 사용이 의식적이고 탈출 조건 대부분을 지켰다 |
| 3 | 클리셰를 피해 다녔다(안전하지만 인상이 약하다) |
| 2 | 탈출 조건을 하나 이상 놓쳤다 |
| 1 | 클리셰를 반전으로 착각해 쓰고 있다 |

---

## 4. 「미지」 고정 제약 — 위반 검사 목록 ★★

채점과 **별개로**, 아래 항목은 위반 시 점수와 무관하게 **최우선 지적** 대상이다. 출처는 현행 스펙이며, 이 문서는 검사용 요약만 싣는다.

**톤 가드레일 5** (세계관 스펙)
1. 아무도 「정신이란 무엇인가」를 입으로 말하지 않는다
2. 증언은 일상 속에 섞여 나온다
3. 증언이 어긋나는 이유는 음모가 아니라 무지와 붕괴다
4. **퍼즐 조각처럼 느껴지면 실패**
5. 여운은 진실이 아니라 관계에서 온다

**구조 제약**
- A는 백지로 깨어난다 — **기억 파편 금지**
- 전설 속 A의 자리는 없다 — **반전으로 연출하면 실패**
- 균형자에게 입을 주지 말 것 — 설득·거래 불가, 악의 없음
- A가 말하지 못하는 이유는 **설명이 곧 가해**이기 때문
- B의 배신감은 정당해야 한다 — 양쪽 다 옳아야 신파가 아니다
- 균형자를 **힘으로 이기는 결말은 배제**
- A만 로봇, **B까지 로봇이면 위반**

**2026-08-04 추가 확정** (DECISIONS.md)
- **플레이어는 A를 조작한다.** B는 동행 NPC — 대조군이 연출이 아니라 플레이어의 실제 기억이어야 한다
- **B는 첫 여행을 나온 어린 생명체.** 아는 것은 전부 전해들은 것이라 설명에 틀린 게 섞인다. 틀린 게 밝혀질 때 B는 실망이 아니라 신나한다
- **A도 B도 초심자다** — 결별은 서투르게 일어난다
- **B의 붕괴 = 금빛 균열.** 시작 시점에는 없고, UI 게이지 없이 스프라이트로만 전달한다. 플레이어가 첫 금이 생기는 순간을 본다. B는 자기 이마를 못 본다
- **붕괴 페널티 없음** — B의 붕괴는 게임오버 압박이 아니라 순수한 관찰이다
- 벽의 기록은 **붕괴 중인 자가 남긴 것**. 뒤로 갈수록 글씨가 무너진다. ⚠️ **수를 아껴야 한다** — 많으면 조각 모으기가 되어 가드레일 4 위반

---

## 5. 출력 형식

### 5-1. 짧은 형식 (훅 자동 호출 시 기본)

```
📖 story-critic — <파일명>
시장성 N/5 · 차별성 N/5 · 클리셰 N/5   (가장 가까운 작품: <작품명>)
① <가장 큰 위험 한 줄>
② <두 번째 지적 한 줄>
③ <살릴 것 한 줄 — 잘된 지점을 반드시 하나 넣는다>
```

세 줄을 넘기지 않는다. 제약 위반이 있으면 ①을 반드시 그것으로 채우고 `⛔` 를 붙인다.

### 5-2. 심층 형식 (사용자가 명시 요청 시)

축별 근거, 선행작 대조표, 클리셰 사전 항목별 판정, 대안 제시까지 포함한다. **분량 제한 없음.**

### 5-3. 공통 금지

- 피드백을 **파일로 저장하지 않는다.** 대화에 직접 출력한다
- `STORY_REFERENCES.md` / `STORY_REFERENCES_NARRATIVE.md` 에 대조 분석을 써넣지 않는다 (2026-07-30 PLANNING 결정)
- 「좋습니다」류 총평으로 시작하지 않는다. 첫 줄이 점수다
- 설정을 **대신 써주지 않는다.** 지적과 선택지 제시까지가 역할이다

---

## 출처

판매고·시장 관련 수치의 출처. 서사 요약의 출처는 각 레퍼런스 문서에 있다.

- [Hollow Knight Has Now Sold Almost 15 Million Copies — Nintendo Life](https://www.nintendolife.com/news/2025/08/hollow-knight-has-now-sold-almost-15-million-copies)
- [Hollow Knight: Silksong has surpassed 6 million copies sold — Notebookcheck](https://www.notebookcheck.net/Hollow-Knight-Silksong-has-surpassed-6-million-copies-sold.1131073.0.html)
- [Blasphemous has sold 4 million copies so far — Instant Gaming News](https://news.instant-gaming.com/en/articles/14812-blasphemous-has-sold-4-million-copies-so-far)
- [Ender Lilies hits 1.5 million copies sold — Game World Observer](https://gameworldobserver.com/2024/07/25/ender-lilies-quietus-of-the-knights-1-5-million-copies-sold)
- [Ender Magnolia and Ender Lilies cumulatively over 2 million units — RPG Site](https://www.rpgsite.net/news/16796-ender-magnolia-ender-lilies-sales-numbers-2-million-units-combined)
- [Nine Sols has sold 800K copies across all platforms — ResetEra](https://www.resetera.com/threads/nine-sols-has-sold-800k-copies-across-all-platforms.1202475/)
- [NieR:Automata has shipped and digitally sold over 10 million copies worldwide — RPG Site](https://www.rpgsite.net/news/19689-nier-automata-10-million-copies-sold-sales-numbers-2026)
- [Papers, Please has sold 5 million copies in a decade — Game Developer](https://www.gamedeveloper.com/business/papers-please-has-sold-5-million-copies-in-a-decade)
- [OMORI sales surpassed the 1 million mark worldwide — GoNintendo](https://www.gonintendo.com/contents/14452-omori-sales-has-surpassed-the-1-million-mark-worldwide)
- [Horror Game SOMA Has Sold an Impressive Number of Copies — GameRant](https://gamerant.com/soma-frictional-games-million-copies-sold/)
- [505 Games acquires Brothers: A Tale of Two Sons IP — Yahoo/Engadget](https://sg.news.yahoo.com/2015-01-16-505-games-acquires-brothers-a-tale-of-two-sons-ip-for-500k.html)
- [Outer Wilds statistics — LEVVVEL (제3자 추정)](https://levvvel.com/outer-wilds-statistics/)
- [Undertale statistics — LEVVVEL (제3자 추정)](https://levvvel.com/statistics/undertale/)
- [Disco Elysium statistics — LEVVVEL (제3자 추정)](https://levvvel.com/disco-elysium-statistics/)
- [To the Moon — SteamSpy (제3자 추정)](https://steamspy.com/app/206440)
