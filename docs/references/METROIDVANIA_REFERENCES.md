# 메트로배니아 제작 참고자료

**날짜:** 2026-08-10
**목적:** 「미지」 개발 시 참고할 선행작 포스트모템·설계 영상·조작감 코드 기법 모음.
**성격:** 외부 자료 링크집. 우리 설계 결정이 아니라 **참고용**이다. 구역 설계(핸드오프 병목 1번)·Unity 컨트롤러 구현 착수 시 여기서 꺼내 쓴다.

> ⚠️ 링크는 2026-08-10 기준. 끊기면 제목으로 재검색할 것.

---

## A. 포스트모템 · 위기 순간

### Hollow Knight (Team Cherry, 3인 코어) — 스코프 통제 실패 사례
메트로배니아 소규모 개발의 교과서적 위기.
- **원래 "아주 작은 게임"으로 기획됐다가 Kickstarter 스트레치 골로 스코프 폭발.**
  [Hollow Knight Was Originally Planned to Be a 'Very Small' Game (GameRant)](https://gamerant.com/hollow-knight-development-size-kickstarter-funding/)
- 후속작 Silksong는 DLC로 시작 → 7년짜리 정식 속편. 공동창업자 Ari Gibson: *"계속 그리다간 15년 걸리겠다 싶어 스케치를 멈춰야 했다."*
  [Silksong 7-year development (Game Developer)](https://www.gamedeveloper.com/business/hollow-knight-silksong-gets-a-release-date-after-7-years-in-development) ·
  [Bloomberg 인터뷰 요약 (Kotaku)](https://kotaku.com/silksong-interview-delay-hollow-knight-team-cherry-2000619168)

> **우리 프로젝트에 주는 교훈:** 데모(세로 슬라이스) 스코프를 못박아라. 「깃든 사물=구역」으로 구역 수를 통제하려는 것과 정확히 같은 문제다. 스케치를 언제 멈추느냐가 생사.

### Ori and the Blind Forest (Moon Studios) — 완전 원격 소규모 팀의 고품질 도전
- GDC 2015, James Benson: **제한된 인원·시간으로 지브리급 애니메이션 수천 장을 만든 방법** + 각 결정의 득실.
  [GDC Vault: Animation Bootcamp — The Animation Process of Ori](https://gdcvault.com/play/1021791/Animation-Bootcamp-The-Animation-Process) ·
  [해설 정리 (Game Developer)](https://www.gamedeveloper.com/art/video-deconstructing-the-animation-of-i-ori-and-the-blind-forest-i-)

### Axiom Verge (Thomas Happ, 완전 1인) — 솔로 개발의 현실
코드·아트·음악 전부 혼자, 자작 엔진(C#/MonoGame), 5년+ 부업으로 진행.
- [10 Years of Axiom Verge (GameRant)](https://gamerant.com/axiom-verge-10-year-anniversary-indie-metroidvanias-legacy/)

---

## B. 설계 영상 — GMTK 『Boss Keys』 시즌 2 (메트로배니아 월드 디자인)

능력↔장애물 연결로 세계를 여는 구조를 작품별로 해부. **우리 병목(지리·구역·진행 열쇠)에 직결.**
- [Super Metroid의 월드 디자인](https://www.youtube.com/watch?v=nn2MXwplMZA)
- [Metroid Prime의 월드 디자인](https://www.youtube.com/watch?v=zyoGD6uwCmk)
- [Metroid / Zero Mission의 월드 디자인](https://www.youtube.com/watch?v=kUT60DKaEGc)
- [The World Design of Hollow Knight (GMTK Substack)](https://gmtk.substack.com/p/the-world-design-of-hollow-knight) — 시리즈 최다 조회, 비선형 메트로배니아의 정석

---

## C. 조작감 · 코드 기법 (Unity 2D)

메트로배니아 "손맛"은 눈에 안 보이는 트릭에서 나온다. Super Meat Boy·Celeste·Dead Cells가 표준.

| 기법 | 내용 | 자료 |
|---|---|---|
| **코요테 타임** | 발판을 떠난 뒤 몇 ms 동안 점프 허용 | [Ketra Games 튜토리얼 (Unity C#)](https://www.ketra-games.com/2021/08/coyote-time-and-jump-buffering.html) |
| **점프 버퍼링** | 착지 직전 누른 점프 입력을 큐잉해 착지 즉시 발동 | 〃 · [Unity Discussions 스레드](https://forum.unity.com/threads/does-anyone-know-of-a-way-to-do-coyote-time-and-jump-buffering.871102/) |
| **코너 보정** | 점프 시 모서리에 걸리면 살짝 밀어 통과 | [2D game feel tips (Anchit)](https://anchitsh.github.io/platformer.html) |
| **서브픽셀 이동** | Celeste식 픽셀 단위 이동·충돌 처리 | Maddy Thorson, "Celeste and TowerFall Physics" (제목 검색) |

### Unity 특화 표준 도구 (튜토리얼 검색 키워드)
- **Cinemachine** — 룸 단위 카메라 프레이밍·데드존·Confiner. 메트로배니아 방 전환 표준
- **Tilemap + Composite Collider 2D** — 레벨 지오메트리. Rule Tile로 자동 타일링
- **Pixel Perfect Camera** (우리 스택) — 픽셀 아트 정수 스케일

---

## 관련 문서
- 조연·세계관: `docs/planning/story/` · `docs/superpowers/specs/`
- 이동·전투 메카닉: `docs/planning/mechanics/MECHANIC_movement.md`
- 우리 스토리 채점용 레퍼런스(별개): `docs/planning/story/STORY_REFERENCES.md`, `STORY_REFERENCES_NARRATIVE.md`
