# 애니메이션·픽셀 아트 레퍼런스 분석

> 목적: 「미지」의 그래픽 스타일과 애니메이션 부드러움을 정할 때 참고할 선행작 분석.
> 대상: Primal Planet · Rusted Moss · Katana Zero · Lone Chef · Hollow Knight (5작).
> 작성: 2026-08-27, 웹 1차/2차 자료 + Rusted Moss는 개발자 공개 소스코드 실측.

## 표기 규칙
- **[사실]** = 1차/2차 출처로 확인됨
- **[실측]** = 개발자 공개 소스코드에서 직접 추출 (Rusted Moss 한정)
- **[추정]** = 공개 자료 없음, 정황 추론
- **[미공개]** = 개발사가 밝힌 적 없음

---

## 한 줄 결론

**"부드러움"은 하나의 기술이 아니라 여러 갈래다. 뭉뚱그리면 잘못된 교훈을 얻는다.**

1. **손으로 프레임을 때려박기** (Katana Zero, Primal Planet) — 비싸다, 노동집약
2. **코드 절차 애니 + 물리** (Rusted Moss) — 소규모 팀에 현실적, 우리에게 가장 가까움
3. **연출로 속이기** (Lone Chef) — 공격 트레일·전환 모션·조명 변화로 적은 프레임을 매끄럽게 보이게
4. **리미티드 손그림 + 게임필** (Hollow Knight) — 프레임은 오히려 적다(10~12fps). 전통 애니메이션 기법 + Unity 런타임 연출로 부드러움을 만든다. **스켈레탈도 프레임 보간도 아니다.**

---

## 비교표

| 항목 | Primal Planet | Rusted Moss | Katana Zero | Lone Chef | Hollow Knight |
|---|---|---|---|---|---|
| 엔진 | Godot [사실] | GameMaker Studio 2 [사실] | GameMaker Studio 2 [사실] | 미공개(Unity 추정) | **Unity** [사실] |
| 부드러움 정체 | 고프레임 손그림 | 코드 절차 애니 + verlet 물리 | 고밀도 손그림 + 스매어 프레임 | 공격 트레일·전환모션·조명 | 리미티드 손그림 + 게임필 |
| 아트 형식 | 픽셀 | 픽셀 | 픽셀 | 픽셀 | **비-픽셀 손그림** |
| 캐릭터 규격 | ~48–64px대 [추정] | 조각 19–22px / 합성 ~24–28px [실측] | 몸체 ~30–40px [추정] | 32–64px [추정] | 고해상 손그림(격자 없음) [미공개] |
| 프레임 방식 | 프레임 다수 손작화 | 조각 소량 + 런타임 변형 | 액션당 다수 프레임 전량 손작화 | 미공개(트레일로 보완) | **frame-by-frame, 트윈/본 없음** |
| 팔레트/톤 | 다채·생물발광 | 어둡고 낮은 채도(우울) | 80s 네온 느와르 | 밝은 카툰(역발상) | 어둡고 병든 수채 |
| 개발 규모 | 1인 (Flash 애니 출신) | 3인 (소스 공개) | 1인 프로그래밍 + 영입 아트팀 | 한국 스튜디오 4→12인 | 3인 (전통 애니 출신 Gibson) |
| 개발 비용 | 5년 | — | 6년·약 $60k | 진행 중, 2026 하반기 출시 | ~3년 |

> ⚠️ **수치 신뢰도**: Rusted Moss만 개발자 공개 소스(`github.com/faxdoc/RM_multiplayer`)에서 실측. 나머지 셋은 하드 스펙(px·프레임 수)을 개발사가 공개한 적이 없어 정성 평가 + 추정이다. 레퍼런스로 인용할 때 수치는 신뢰하지 말 것.

---

## ① Katana Zero — 손으로 프레임을 때려박은 부드러움

- **전 프레임 손그림(frame-by-frame)** 을 Aseprite로 작화. [사실]
- 빠른 검격에 **스매어 프레임**(중간 프레임을 일부러 왜곡·늘여 손그림 모션블러 효과) 사용 → 눈이 빠른 동작을 연속으로 읽음. [사실/일반론]
- **60fps 고정.** 애니를 게임보다 낮은 fps로 돌린다거나 프레임 보간을 쓴다는 근거는 **없음** — 그냥 프레임을 많이, 잘 그렸다. [사실]
- 환경: 80s 네온 느와르 + **VHS 셰이더**(GMS2 커스텀 셰이더로 라인스윕·색수차 실시간). 주변광 반응 다층 셰이딩. [사실]
- **비용**: 6년, 아티스트 여럿이 중도 이탈할 만큼 노동집약. 가장 비싼 방식. [사실]

**교훈**: 최고 품질이지만 재현 비용이 극단적. 소규모/절차 파이프라인이 그대로 따라갈 길은 아니다.

## ② Rusted Moss — 코드로 만든 부드러움 (우리에게 가장 중요)

개발자 공개 소스에서 직접 확인한 사실:

- 캐릭터 "Fern"은 **단일 스프라이트가 아니라 조각을 런타임에 코드로 합성·회전**. [실측]
  - `splayer_body` 19×19px·14프레임 / `splayer_legs` 22×15px·2프레임 / 합성 캔버스 64×64px
  - 팔·눈은 런타임 회전으로 얹음, 360° 조준은 팔 스프라이트 on-the-fly 회전
  - 3D 느낌은 **sprite stacking**(2D 이미지를 층층이 쌓아 각도별 스프라이트 없이 회전)
- 손 프레임은 다리 8프레임 수준으로 **적다.** 개발자 명시: *"코드로 애니메이트해 고통스러운 수작업 애니를 피했다."* [사실]
- 이동의 부드러움 = 애니가 아니라 **verlet 물리**. [실측]
  - 로프 시뮬레이션 상수: `grav=0.125, ground_frc=0.8, universal_frc=0.91, bounce=0.87`
  - 그래플이 고정 앵커로 당기는 게 아니라 **고무줄처럼 운동량 보존·전달** → 스윙이 물리적으로 연속
  - 머리카락·풀도 같은 verlet 천 시뮬레이션
- 프레임 재생 속도(소스 실측):

  | 사이클 | 프레임 | playbackSpeed |
  |---|---|---|
  | Idle (다리) | 2 | 30 |
  | Run (다리) | 8 | 7 |
  | Jump (다리) | 4 | 30 |
  | Body | 14 | 30 |

**교훈**: 24~28px 조각으로 Katana Zero급 유동감을 낸다. **부드러움 ≠ 프레임 수.** 런타임 변형 + 물리 연속성이 핵심. 소규모 팀 + 절차 생성 파이프라인과 정확히 맞는 유일한 레퍼런스이며, **소스가 공개돼 있어 직접 뜯어볼 수 있다.**

## ③ Primal Planet — Flash 애니 출신의 고프레임 감각

- 개발자(Albert van Zyl / Seethingswarm)가 **Flash 벡터 애니메이터 출신** → 고프레임·이징·스쿼시&스트레치·세컨더리 모션 감각을 픽셀아트에 이식. [사실]
- 언론 평: "일반적 2D 기대치를 상회하는 애니." 프레임 수·px는 [미공개].
- **씬 전체의 세컨더리 모션**: 배경 공룡·물 반짝임·반딧불 등 앰비언트 애니가 화면 전체에 깔려 "살아있다"고 느껴지게 함. [사실]
- 엔진 Godot, 1인, 5년 개발. [사실]

**교훈**: 캐릭터 프레임만이 아니라 **씬 전체의 세컨더리 모션**이 체감 부드러움에 크게 기여한다. 투자 대비 효과가 큰 지점.

## ④ Lone Chef — 연출로 부드럽게 보이게 (한국 스튜디오)

- 개발사 **프로젝트모름**(한국, 4→12인), 퍼블리셔 컴투스홀딩스, 2026 하반기 출시 예정. [사실]
- 정량 스펙(엔진·px·프레임·팔레트) **전부 미공개.** 인용 시 정성 평가만.
- 확인되는 애니메이션 기법(데모 패치노트): [사실]
  - **회전/전환(turning) 모션 별도 제작** — 방향 전환 시 스냅이 아니라 중간 프레임
  - **공격 궤적(attack trail)** — 잔상으로 적은 프레임에서도 스윙이 매끄럽게 읽힘(모션블러 대체)
  - **day-night 사이클** — 시간대별 색·조명 변화로 생동감
- 아트 디렉션: 포스트 아포칼립스인데 회색 대신 **밝은 카툰 톤 역발상** + **웹툰 원화 / 인게임 도트 하이브리드**. [사실]

**교훈**: 트레일·전환 모션·조명 변화는 **적은 프레임으로 부드러운 인상을 사는 값싼 기법.** 우리 전투 연출에 즉시 이식 가능.

## ⑤ Hollow Knight — 손그림 리미티드 애니 + Unity 게임필 (엔진이 우리와 동일)

> **주의: 유일한 비-픽셀 레퍼런스.** 그런데 엔진이 Unity라 기술 스택 교훈은 5작 중 가장 직접적이다.

- **엔진 Unity.** 개발 중반 Stencyl → Unity로 교체(2015). 커스텀 엔진 없이 **Unity 기본 2D 스택 + 에셋스토어**로만 완성. [사실]
  - 확인된 스택: **2D Toolkit**(스프라이트/아틀라스), **Playmaker**(적·기믹 비주얼 스크립팅), **Sprite Packer**(아틀라스), 기본 sprite 셰이더 소폭 수정
  - **스켈레탈 리깅 툴(Spine/Anima2D) 안 씀.** "Anima2D 사용설"은 근거 없는 오해. [사실]
- **애니메이션 = frame-by-frame 손그림, 트윈(보간)·본 없음.** [사실, 정황 종합 높은 신뢰도]
  - Ari Gibson이 **Photoshop에서 프레임을 한 장씩 그려 PNG로 저장** → Unity 반입
  - 물증: Spriters Resource에 프레임별 통짜 스프라이트 시트 존재(스켈레탈은 런타임 조립이라 이런 시트가 안 나옴)
- **프레임은 오히려 적다.** 커뮤니티 추정 idle ~100ms/프레임, 액션 ~80ms/프레임 → **10~12fps대 리미티드 애니메이션.** [추정] 풀 애니(24fps)보다 적은데도 부드럽게 느껴지는 게 핵심.
- **왜 부드러운가** — 프레임 매수가 아니라 결합이다:
  1. **전통 애니메이터의 기법**: Gibson은 애니 스튜디오 출신. 스쿼시&스트레치·예비/후속동작·세컨더리 모션(망토·더듬이)을 소수 프레임에 압축
  2. **단순 실루엣(벌레)**로 프레임 예산을 아껴 중요 순간(공격·피격)에 몰아줌 → 1인이 적 150종 감당
  3. **Unity 런타임 연출**: 이동/피격 반응·카메라 추적·셰이크가 스프라이트 위에 얹혀 손맛 보강(스프라이트 보간이 아니라 트랜스폼 레벨 움직임)
  4. **다층 패럴랙스**: 2D 에셋을 실제 **Z축 3D 공간**에 배치해 카메라 앵글에 맞춰 정렬
- 파이프라인 요체: **"복잡한 커스텀 기술 배제(keep it simple) + 단순 실루엣 + frame-by-frame PNG + Unity 기본기"** 로 1인 아티스트가 방대한 분량을 3년에 소화. [사실]

**교훈(우리와 직결)**: 우리도 Unity다. Hollow Knight는 **"엔진 특수 기술 없이 기본 2D 스택으로도 최상급 유동감이 가능"** 함을 증명한다. 부드러움의 원천이 **프레임 수도 스켈레탈도 아니라 (a) 애니메이터의 기본기 + (b) 런타임 게임필**이라는 점이 5작 중 가장 명료하게 드러난다. 단, 저 "기본기"가 곧 진입장벽 — 전통 애니 감각 없이는 재현 난이도가 높다.

## ⑤-2 Hollow Knight 환경/타일 구성 — 왜 격자 티가 안 나는가

> 우리 프로젝트가 지금 "타일 룸 콜라이더"를 다루는 중이라 가장 직접적으로 걸리는 분석.

### 핵심: 게임플레이 격자는 유지, 비주얼은 격자에서 해방
Hollow Knight가 자연스러운 이유는 **"타일을 버려서"가 아니라, 게임플레이용 격자 규율은 지키되 그 위에 덮는 그림을 격자에서 풀어줬기** 때문이다. 이 둘을 분리해야 한다.

- **비주얼 = 오토타일 아님, 손배치.** Team Cherry는 "손그림 2D 에셋을 3D 공간에 레이어링"해 레벨을 만든다. HK 스타일을 실제 재현한 아티스트 증언: *"모든 걸 손으로 배치했고 타일맵은 전혀 안 썼다."* → 16/32px 타일이 규칙 반복될 때 생기는 **격자 무늬가 원천적으로 없음.** [사실]
- **게임플레이 격자는 의도적으로 살아있음.** Ari Gibson: HK는 Faxanadu 같은 **"명백히 타일 기반"** 게임의 특질을 의도했다. 플랫폼 두께·점프 높이 등 일정한 리듬 + 가속 없는 디지털 이동(메가맨식)으로 **거리 판단·가독성(legibility)** 확보. [사실]
- 정리: **"게임플레이 그리드는 있으나 비주얼 타일맵은 없다."** 이게 정밀 조작감 + 유기적 외관을 동시에 얻는 비결.

### 콜리전과 아트의 분리 (데이터마이닝 확인)
- 지형 충돌은 **`EdgeCollider2D` / `PolygonCollider2D`로 별도 오브젝트, layer 8(terrain)** 에 존재. [사실]
- 장식 스프라이트는 `SpritePatcher` 컴포넌트를 가진 **다른 오브젝트 트리**. → **그림과 충돌 지오메트리가 완전히 별개.** [사실]
- 아티스트는 바위 실루엣을 자유롭게 그리고, 그 밑에 게임플레이가 필요한 만큼만 **더 단순한** 콜라이더를 따로 깐다. "유기적 외관 + 예측 가능한 충돌"의 표준 기법. [사실+정황]

### 이음새(seam)를 감추는 3중 장치
1. **손배치 = 반복 없음.** 대형 비반복 손그림 위에 버섯·이끼·돌기 장식 스프라이트를 겹쳐 실루엣을 깨뜨림. [사실/추정]
2. **강한 대비 + 두꺼운 외곽선.** 미들그라운드(플레이 공간)를 배경과 분리 → 시선이 타일 경계가 아니라 실루엣으로 감. [사실]
3. **반투명 조명·안개 오버레이.** "부드러운 반투명 도형"으로 표면을 덮어 반복을 흐림. [사실]

### 레이어 3층
- **foreground(어둡게) / middle-ground(플레이 공간, 최대 대비·굵은 외곽선) / background(구역 예고·원경 흐림).** 손그림 2D를 실제 Z축 3D 공간에 배치해 서로 다른 속도로 팬 → 패럴랙스. 원경은 `BlurPlane`으로 흐림. [사실]

### 타 타일게임과의 대비
| 항목 | 전형적 그리드 타일게임 | Hollow Knight |
|---|---|---|
| 지형 | 16/32px 타일 오토타일 격자 스냅 | 손그림 조각 자유 배치 |
| 반복 | 같은 타일 규칙 반복 → 격자 무늬 보임 | 비반복 대형 손그림 + 장식으로 실루엣 파괴 |
| 콜리전 | 타일 = 콜리전 (한 몸) | **콜리전/그림 분리** (layer 8 별도 폴리곤) |
| 경계 | 정해진 엣지·코너 타일 세트 | 그림은 격자 무시, 게임플레이 리듬만 격자 준수 |

**우리 적용**: HK식 룩을 원하면 ① 게임플레이 격자 리듬은 코드로 유지, ② 비주얼은 오토타일 대신 대형 비반복 손그림 + 장식 손배치, ③ **콜라이더를 그림과 별도 오브젝트로 분리**(PolygonCollider2D/EdgeCollider2D), ④ 3층 레이어 + 패럴랙스 + 반투명 안개 오버레이. 특히 ③은 지금 우리 타일 룸 콜라이더 구조와 직결 — **아트 스프라이트에 콜라이더를 묶지 말고 분리**하는 게 HK식 유기적 지형의 전제다.

> ⚠️ 우리는 픽셀 아트라 HK의 "손그림 대형 피스"를 그대로는 못 쓴다. 하지만 **콜리전/아트 분리, 3층 레이어, 장식 스프라이트 손배치로 반복 깨기**는 픽셀 타일에도 그대로 적용된다.

---

## 「미지」 적용 함의

현재 파이프라인: **64px / PPU 64 + 프레임 절차 생성** (최근 커밋 기준).

1. **"부드러움 = 프레임 수" 전제를 버려라.** Rusted Moss가 24~28px 조각으로, Hollow Knight가 10~12fps 리미티드 애니로 각각 반증한다. 프레임 폭증은 Katana Zero처럼 6년 갈 각오가 있을 때만.
2. **우리 길은 Rusted Moss형(②).** 조각 스프라이트 + 런타임 변형(회전·오프셋·이징). 소규모 팀 + 절차 생성과 정확히 맞음. 소스 공개돼 직접 참고 가능.
3. **씬 전체 세컨더리 모션(③)** + **공격 트레일·전환 모션(④)** 은 값싸고 효과 큰 즉시 적용 대상.
4. **런타임 게임필(⑤)이 스프라이트만큼 중요.** Hollow Knight는 프레임이 적어도 카메라 추적·셰이크·피격 반응 등 Unity 트랜스폼 레벨 연출로 부드러움을 만든다. 우리도 Unity라 이 교훈이 가장 직접적 — **애니 프레임에만 매달리지 말고 게임필 연출에 예산을 배분하라.**
5. **64px가 과할 수 있다.** 픽셀 레퍼런스 중 확인된 둘이 30px대 이하. 단 이건 아트 방향 결정이라 확정 아님 — 제기만.

---

## 확인 실패 / 추가 조사 경로

정량 수치가 필요하면 자동 페치로 접근 실패한 아래를 수동 확인:
- **Katana Zero px**: Spriters Resource Zero 시트(asset 187355)를 브라우저로 직접 열어 캔버스 실측
- **Primal Planet 프레임**: 개발자 X(@SeethingSwarm) 타임랩스 영상
- **Lone Chef**: Steam 데모가 공개돼 있으니 에셋 직접 추출이 가장 빠름

## 출처

**Primal Planet**
- Steam: https://store.steampowered.com/app/2350270/Primal_Planet/
- PC Gamer: https://www.pcgamer.com/games/action/primal-planet-is-my-favorite-new-metroidvania-because-it-has-friendly-dinosaurs-gorgeous-pixel-art-and-you-can-hug-your-wife/
- Godot 개발 일지: https://www.gamedevjourney.co.uk/home/developer-diaries/godot-diaries/primal-planet
- 개발자 X: https://x.com/SeethingSwarm

**Rusted Moss**
- 소스 코드(1차, 실측): https://github.com/faxdoc/RM_multiplayer
- PS Blog(verlet 그래플): https://blog.playstation.com/2024/06/18/how-rusted-moss-devs-teamed-up-to-create-physics-based-grappling-hook-action/
- 80.lv 절차 애니 튜토리얼: https://80.lv/articles/tutorial-procedural-2d-aim-animation-in-gamemaker
- 80.lv sprite stacking: https://80.lv/articles/how-to-implement-3d-aiming-using-sprite-stacking-in-gamemaker

**Katana Zero**
- Wikipedia: https://en.wikipedia.org/wiki/Katana_Zero
- 기술 아트 정리(Aseprite+GMS2, VHS 셰이더): https://foro3d.com/en/2026/mayo/katana-zero-pixel-art-neon-y-distorsion-vhs-en-gamemaker-studio-2.html
- GameRevolution 인터뷰: https://www.gamerevolution.com/originals/520685-katana-zero-interview
- 스프라이트(치수 확인용): https://www.spriters-resource.com/pc_computer/katanazero/asset/187355/

**Lone Chef**
- Steam: https://store.steampowered.com/app/3280150/Lone_Chef/
- Steam 뉴스(패치노트): https://store.steampowered.com/feeds/news/app/3280150/
- 개발사: https://www.projectmoreum.com/way_page/lone_chef.php
- Niche Gamer(fluid animation): https://nichegamer.com/lone-chefs-distinct-cuisine-combat-gets-demo/
- 인벤(웹툰 원화+도트): https://m.inven.co.kr/webzine/wznews.php?site=78&idx=303183

**Hollow Knight**
- Unity 공식 Made-with(1차, 스택): https://unity.com/made-with-unity/hollow-knight
- Educademy(2D Toolkit·Playmaker·셰이더 재인용): https://www.educademy.co.uk/blog/games-made-in-unity-hollow-knight-by-team-cherry
- Wikipedia(Stencyl→Unity, 스캔 반입): https://en.wikipedia.org/wiki/Hollow_Knight
- PC Gamer(Photoshop·PNG·손그림): https://www.pcgamer.com/hollow-knights-charming-art-sets-the-bar-for-hand-drawn-games/
- Spriters Resource(프레임 시트 = frame-by-frame 물증): https://www.spriters-resource.com/pc_computer/hollowknight/
- 80.lv(Z축 3D 레이어 패럴랙스): https://80.lv/articles/hollow-knight-silksong-s-game-world-isn-t-actually-2d
- HK Modding API(런타임 씬 구조: layer 8 terrain, EdgeCollider2D/PolygonCollider2D, SpritePatcher, BlurPlane): https://radiance.synthagen.net/apidocs/NewScene.html
- Source Gaming Team Cherry 인터뷰(Faxanadu 타일 기반 영향·가독성): https://sourcegaming.info/2025/04/09/straight-from-the-source-team-cherry/
- 80.lv 아트 브레이크다운(3층 레이어·대비·외곽선): https://80.lv/articles/breakdown-hollow-knights-art-style
- Terresquall HK 재현 튜토리얼(콜라이더 분리·Z 레이어·안개): https://blog.terresquall.com/2023/06/creating-a-metroidvania-like-hollow-knight-part-5/
