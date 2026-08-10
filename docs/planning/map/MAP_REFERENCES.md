# 선행작 맵 구조 레퍼런스 (map-designer 전용)

**작성:** 2026-08-10
**목적:** 메트로배니아 맵 구조의 **선행작 분석**. `MAP_DESIGN_PRINCIPLES.md`가 척도라면 이 문서는 대조 자료다.
**작성 원칙 (2026-07-30 PLANNING 결정 준용):** 각 작품의 **구조 자체를 요약**한다. 「미지」와의 대조·차용 분석은 넣지 않는다 — 그래야 자료로 재사용된다.

> 링크는 2026-08-10 기준. STORY_REFERENCES와 별개 문서다(저건 서사, 이건 지리).

---

## 1. Super Metroid (1994) — 장르의 원형

- **저작된 단일 맵 + 우아한 파워 커브.** 한 장의 연속된 행성(제베스)이 층으로 나뉘고, 능력이 늘수록 접근 범위가 계단식으로 넓어진다
- **능력=열쇠 / 지형=자물쇠**의 원형. 모프볼·하이점프·그래플·스페이스점프가 각각 새 지형 유형을 연다
- **은닉 요소** — 부술 수 있는 블록(폭탄·슈퍼미사일·파워봄), 벽 뒤 통로. 스캔 없이 실험으로 발견
- **동선** — 명시적 지시가 적고 지형·적 배치로 방향을 암시(환경적 유도). 되돌아가기가 잦지만 능력이 왕복을 단축
- 참고: [How to design a great Metroidvania map — PC Gamer](https://www.pcgamer.com/how-to-design-a-great-metroidvania-map/)

## 2. Hollow Knight (2017) — 손으로 그린 거대 네트워크

- **스프라울링(sprawling) 상호연결망.** 허브(교차로/Dirtmouth)에서 사방으로 뻗은 대형 유기적 맵
- **Souls식 숏컷** — 한쪽에서만 열리고, 열면 양방향이 되는 지름길이 다수. 탐험의 위험을 감수한 뒤 안전을 보상
- **비밀** — 부술 수 있는 벽, 전경 오브젝트에 가려진 통로. **발견 전엔 맵에 안 뜬다**
- **맵 시스템** — 지도공(Cornifer)에게 구역 지도를 사야 하고, 그 전엔 맵이 비어 있어 방향 감각과 긴장을 준다
- **루프** — 양방향 통행 루프가 많아, 다른 입구로 먼저 들어가면 다른 경로가 닫히는 식으로 유기적 전진 유도
- **백트래킹** — 메인은 전진 위주지만, 맵 암기와 철저 탐험이 숙련자에게 이점을 준다
- 참고: [The World Design of Hollow Knight — GMTK](https://gmtk.substack.com/p/the-world-design-of-hollow-knight) · [Hollow Knight Critique — RSD](https://rosodudemods.wordpress.com/2020/01/14/hollow-knight-critique/)

## 3. Hollow Knight: Silksong (2025) — 척추형 + 점진적 개방

Mark Brown(GMTK)의 분석 기준.

- **거대한 U자 벤드** — 파를룸을 관통해 성채(Citadel)를 향해 **일관되게 위로** 올라가는 척추. 1막은 강한 전진 관성, 2막은 성채에서 진짜 비선형 개방
- **열쇠-자물쇠** — 초반엔 "열쇠 하나가 자물쇠 몇 개만" 열고, 진행 자물쇠는 늘 **열쇠 코앞**에 있다. **Cling Grip** 능력이 맵 전역의 여러 방을 **한 번에** 열어 팽창을 폭발시킨다
- **비밀** — 50개 넘는 무너지는 벽·은닉 통로가 **발견 전엔 맵에 안 뜬다**. Bilewater·Putrified Ducts 같은 구역은 존재를 몰라도 클리어 가능
- **백트래킹** — 메인 진행에서 되돌아가기를 **거의 강요하지 않는다**. 대신 자발적 역추적을 선택 보스·아이템으로 보상
- **맵 포킹** — 구역 지도가 미방문 연결을 미리 보여줘, 플레이어가 지도를 훑어 진행로를 스스로 찾게 한다
- **복수 경로** — Clawline 능력에 이르는 길이 여럿(중복 설계) → 자유도·발견 확률 ↑. 멜로디 조각 3개는 순서 무관 수집
- **선택 아이템 4대 출처** — 역추적 / 비밀벽 / 상점 / 소원(wish)
- 참고: [The World Design of Hollow Knight: Silksong — GMTK](https://gmtk.substack.com/p/the-world-design-of-hollow-knight)

## 4. 그 외 (고수준 특징 — 상세는 필요 시 보강)

> 아래는 일반적으로 알려진 구조 특징이다. 세부는 미검증이므로 설계 근거로 쓸 땐 확인 요망.

- **Metroid Prime (2002)** — 3D 1인칭이지만 메트로배니아 구조. 바이저/빔 = 열쇠, 스캔 로어로 세계 전달. 허브(탤런 IV) 중심 방사형
- **Ori 시리즈 (2015·2020)** — 능력 중심 정밀 플랫포밍. 「탈출 시퀀스」(강제 스크롤 추격) 구간이 척추의 긴장 포인트. 2편은 더 개방적 맵 + 퀘스트 허브
- **Nine Sols (2024)** — 거점(Root) 중심 방사형. Sekiro식 패링 전투 + 상대적으로 압축된 맵. 팽창보다 밀도
- **Blasphemous (2019)** — 종교적 테마의 상호연결 대륙. 워프 지점으로 후반 이동 단축, NPC 퀘스트가 역추적 동선을 만든다

참고: [Metroidvania, explained — allthings.how](https://allthings.how/metroidvania-explained-design-pillars-history-scope/) · [Making Sense of Metroidvania Game Design — Game Developer](https://www.gamedeveloper.com/design/making-sense-of-metroidvania-game-design)

---

## 출처
- [The World Design of Hollow Knight / Silksong — GMTK (Mark Brown)](https://gmtk.substack.com/p/the-world-design-of-hollow-knight)
- [How to design a great Metroidvania map — PC Gamer](https://www.pcgamer.com/how-to-design-a-great-metroidvania-map/)
- [Making Sense of Metroidvania Game Design — Game Developer](https://www.gamedeveloper.com/design/making-sense-of-metroidvania-game-design)
- [Metroidvania, explained — allthings.how](https://allthings.how/metroidvania-explained-design-pillars-history-scope/)
- GMTK 『Boss Keys』 영상 시리즈 — `docs/references/METROIDVANIA_REFERENCES.md` B절
