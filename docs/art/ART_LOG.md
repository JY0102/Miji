# Art Log — OpenAI 프롬프트 요청 이력

생성된 모든 OpenAI 프롬프트와 그 결과를 기록합니다.
프롬프트는 ChatGPT / DALL-E 웹에서 수동으로 실행합니다.

---

## 로그 형식

```
### [날짜] — [에셋 이름]
- **목적**: [왜 이 에셋이 필요한가]
- **생성 프롬프트**: (Claude가 생성한 OpenAI 프롬프트 전문)
- **결과**: 승인 / 재요청 / 수정 중
- **검토 메모**: (스타일 가이드 체크리스트 결과)
- **파일 경로**: (에셋이 저장된 경로, 예: docs/art/assets/slime_idle.png)
```

---

## 요청 기록

### 2026-07-25 (7차) — 신규 5방향 ★I안 / K안 유력

- **목적**: 방향 추가 탐색 5종. 6차에서 배경에 지형이 생긴 문제를 프롬프트로 차단 (`no scenery, no ground plane, no horizon`) — **성공, 5개 모두 플랫 배경**.
- **모델**: `z_image` / **비용**: 0.15 × 8장 = 1.2 크레딧
- **설계**: 이동 방식 5종 전부 상이 (견인 / 외바퀴 / 자벌레 / 무한궤도 / 구르기), 얼개는 규칙 ①로 고정

| 안 | 팔레트 | 온기 이동 | 결과 |
|---|---|---|---|
| **G** 심해 열수구 | 슬레이트+자홍 | 견인(갈고리) | △ 갈고리+바퀴 혼종. 둘 다 회색 드럼형이라 대비 약함 |
| **H** 설림 | 냉회색+주황 | 외바퀴 | ✗ **최약.** 외바퀴가 이륜 수레로 나왔고, 몸통이 얼개와 거의 동일 |
| **I** 산성 늪 | 황동+라임 | 자벌레(풀무) | ★ **실루엣 대비 최강.** 얼개=컴팩트 구형 / 온기=길게 늘어난 발광 마디 |
| **J** 잿더미 | 목탄+용암 | 무한궤도 | ○ 기계다움·픽셀 품질 우수. 단 둘 다 "회색 상자+주황 눈" |
| **K** 청동 신전 | 청동+녹청+흰빛 | 구르기(장갑판) | ★ **온기 매력 최고.** 녹청 낀 얼개도 우수. 대비 양호 |

**I안** (`f3757eaf`): 얼개=황동 구체 포드+라임 렌즈, 온기=풀무로 늘어나는 자벌레. 두 개체의 **가로세로 비율 자체가 달라** 저해상도에서도 확실히 구분됨. 단 라임 발광부가 과채도라 규칙 ②에서 다소 이탈.

**K안** (`490f10d2`): 얼개=녹청 청동 포드+흰 렌즈(부유·그림자 정상), 온기=장갑판을 두른 아르마딜로. 온기가 다소 유기체로 읽히나 판 구조가 명확해 F안 두꺼비보다는 기계에 가까움.

---

#### ★ 발견 #4 수정 — 이동 방식만 다르면 부족하다. **몸통 비율**이 달라야 한다

6차에서 *"두 개체의 이동 방식을 서로 간섭하지 않는 축으로 설계"* 라고 적었으나, 7차에서 **이동 방식을 5개 다 다르게 했는데도 G·H·J가 실패**했다.

원인: 세 안 모두 얼개의 껍데기와 온기의 몸통을 **비슷한 명사**(rounded shell / broad drum / low slab)로 지정 → 부속만 다른 같은 덩어리가 됨.
성공한 I·K는 온기에 **근본적으로 다른 비율**을 준 경우:
- I: 길게 늘어난 가로형 마디 몸통 (얼개=정사각 구형)
- K: 반원형 장갑 셸 + 다리 (얼개=컴팩트 박스)

→ **규칙 갱신: 얼개와 온기는 이동 방식이 아니라 몸통의 종횡비로 구분할 것.** 부속(바퀴·궤도·갈고리)만 바꾸면 실루엣이 수렴한다.

#### ★ 발견 #5 — 배경 오염은 명시적 부정으로 차단 가능
`no scenery, no ground plane, no horizon, no <테마명사>` 를 배경 지시에 붙이자 8장 전부 플랫 배경 유지. 6차 E안의 사막 지형 생성 문제 해소.

- **파일 경로**: 미저장 (CDN 원본만)
  - G `424a76a1` / H `b3e14974` / **I `f3757eaf` ★** / J `b3260c90` / **K `490f10d2` ★**

- **탐색 팔레트** (미확정)
```
G 열수구: #06100E #12242A #24404A #3E6470 #6E8E96 #B6CBCB #E8F0EC #5A1240 #C2308A #F27ACA
H 설림:   #0C1016 #1C2430 #333E4C #55637A #8A97A8 #C6D2DE #F0F6FA #6B2A0E #D9642A #F5A65E
I 산성늪: #0E1108 #1E2612 #36421E #5A6B2E #8A9A52 #C2C88E #EAEEC4 #3A5A0A #8ED12A #C8F26A
J 잿더미: #0A0A0C #1A1A1E #2E2E34 #4E4E56 #7A7A84 #ADADB6 #E0E0E4 #6B1A08 #D93E12 #FF9A3C
K 청동:   #0F0B08 #241A10 #4A3418 #7A5A26 #A8863E #4E7A62 #86B49A #D8C89A #F0EDE0 #FFFFFF
```

---

### 2026-07-25 (6차) — 신규 3방향 (팔레트 + 형태 + 이동방식 동시 변경)

- **목적**: 색다른 방향 추가 탐색. 5차까지 누적된 규칙 3개를 전부 적용한 첫 라운드.
- **적용한 규칙**: ① 얼개 = `pod` 명사 + 렌즈 + `hovering, casting a shadow` (사물 명사 금지) ② 어둡고 저채도 팔레트 ③ 온기는 큰 덩어리 위주 (`few large shapes rather than many small details` 명시)
- **추가 변수**: 이동 방식을 셋 다 다르게 설계 (활주 / 구름 / 도약)
- **모델**: `z_image` / **비용**: 0.15 × 6장 = 0.9 크레딧
- **검토 범위**: 각 방향의 대표 1컷씩 상세 검토 (`_1` 계열)

**D — 얼음 동굴 / 민트, 온기=썰매 활주형** (`70bae89d`, `05e9f409`) — 최약
- ✗ **얼개가 부유하지 않고 스키를 달고 접지함.** `hovering` 지시가 온기의 활주부에 오염된 것으로 보임
- ✗ 결과적으로 **둘 다 타원 몸통 + 활주부**라 실루엣 구분이 이번 라운드 최악. 스왑 가독성 불충족
- 교훈: 두 개체에 유사한 이동 부속을 지정하면 형태가 서로 수렴한다

**E — 사막 토기 / 청록, 온기=드럼 롤러형** (`193441c2`, `d6d3238a`) ★**이번 라운드 최선**
- ✓ 얼개: 끈으로 감긴 둥근 토기 포드, 큰 청록 렌즈, **부유 + 그림자 명확**. 규칙 ①이 처음으로 완벽하게 작동
- ✓ 온기: 옆으로 누운 드럼통 롤러 + 짧은 안정각. **큰 단일 덩어리 → 16px 생존 확실**
- ✓ 이동 방식(구름)이 얼개(부유)와 확실히 대비됨
- △ 온기의 발광하는 드럼 단면이 측면을 향한 "얼굴"로 읽혀 진행 방향이 모호
- △ 배경에 사막 지형·바위가 생성됨 (플랫 배경 지시 무시). 에셋화 시 크롭 필요

**F — 곰팡이 / 탁한 노랑, 온기=두꺼비 도약형** (`0620fd6d`, `1232f6f2`)
- ✓ **얼개가 이번 라운드에서 가장 사랑스러움** — 버섯 갓을 쓴 부유 포드, 큰 연노랑 렌즈, 부유·그림자 정상
- ✗ **온기가 완전한 유기체 두꺼비로 나옴.** 발톱 달린 발까지 생성 — 기계 요소 전무. "매개체 로봇" 설정과 불일치
- 활용안: 얼개만 따로 채택하고 온기는 다른 방향에서 가져오는 조합 가능

---

#### ★ 발견 #4 — 두 개체에 유사한 이동 부속을 주면 형태가 수렴한다
D안에서 온기에 활주부(runners)를 지정하자 얼개까지 스키를 달고 접지함. 얼개의 `hovering` 지시를 덮어씀.
→ **두 개체의 이동 방식은 서로 간섭하지 않는 축으로 설계할 것** (부유 vs 접지처럼). 유사 계열(활주 vs 활주)은 금지.

#### ★ 규칙 ①~③ 검증 결과
- **규칙 ① (pod + 렌즈 + hovering) — 유효.** E·F에서 얼개가 처음으로 안정적으로 "부유하는 생물"로 생성됨. 단 D처럼 다른 지시와 충돌하면 깨진다.
- **규칙 ② (어두운 저채도 팔레트) — 유효.** 3라운드 연속 확인.
- **규칙 ③ (큰 덩어리) — 유효.** E의 드럼, F의 두꺼비 모두 16px 생존 가능한 단순 실루엣.

- **파일 경로**: 미저장 (CDN 원본만 존재)
  - D: `hf_20260725_133854_70bae89d-...png`, `hf_20260725_133854_05e9f409-...png`
  - E: `hf_20260725_133924_193441c2-...png` ★, `hf_20260725_133924_d6d3238a-...png`
  - F: `hf_20260725_133954_0620fd6d-...png`, `hf_20260725_133954_1232f6f2-...png`

- **탐색 팔레트** (모두 미확정)
```
D 얼음:   #080D14 #16222E #2A3C4E #4A6478 #7E97A6 #C4D6DC #EAF4F2 #1E5A50 #4FBFA0 #A8E8D0
E 토기:   #140E0A #2E1C12 #6B3A22 #A8552E #D08A52 #E8C48E #F4E4C4 #0E4A4A #2FA8A0 #7FE0D4
F 곰팡이: #120A12 #2A1620 #4A2836 #7A4A5C #A87A8C #D8BCC0 #F0E4DC #6B5A18 #D8C24A #F4EBA8
```

---

### 2026-07-25 (5차) — 조합 / 리컬러 / 신규탐색 3종

- **목적**: 4차에서 나온 세 갈래를 동시 검증. 1번과 2번은 얼개·팔레트를 고정하고 **온기 형태만 교체**한 통제 비교.
- **모델**: `z_image` / **비용**: 0.15 × 4장 = 0.6 크레딧 (429 rate limit 1회, 실패분 과금 없음)

**1번 — 조합안** (`4078d6ba`): 얼개=3차 H형 부유 포드 + 온기=4차 B형 촉수 블롭 + A 이끼 팔레트
- ✓ 얼개 우수 — 이끼 낀 둥근 포드, 보라 렌즈, 조작 팔, 착지 프롱, 부유 그림자 명확
- ✗ **온기의 촉수 링이 저해상도에서 뭉갠다.** 작고 반복되는 형상이 12개 → 16x16에서 죽 형태로 뭉개질 위험 큼
- ✗ 온기 얼굴이 점 하나뿐이라 캐릭터성 약함

**2번 — H형 리컬러** (`f5973ff5`) ★**전체 라운드 최선**
- 얼개=1번과 동일 부유 포드(각진 버전) + 온기=다리형 크롤러 + A 이끼 팔레트
- ✓ **둘 다 명확히 살아 움직이는 생물로 읽힘** (이번 전체에서 이게 성립한 유일한 조합)
- ✓ 온기 등껍질이 넓고 평평 → **§3 발판 메카닉 충족**
- ✓ 온기에 눈+입이 생겨 캐릭터성 최고, 크기 균형도 양호
- △ 온기가 6족이 아닌 **4족 거북**으로 나옴 (스펙 드리프트). 기계보다 유기체로 읽힘 — 세계관상 "매개체 로봇"인지 재검토 필요

**3번 — 신규탐색: 밤/등불 (남색+뼈색+금색)** (`b6201ad6`, `890ebf05`)
- ✓ **픽셀 아트 품질 전체 최고.** 플랫 컬러·아웃라인·해상감 모두 지금까지 중 가장 우수
- ✓ 온기(다절 애벌레)가 생물로 잘 읽히고 귀여움
- ✗ **얼개가 또 천장 사슬에 매달림** — 2장 모두. 주인공으로 사용 불가

---

#### ★ 4차 발견 #1 수정 — "렌즈만으로는 부족하다"

4차에서 *"부유형 관찰자는 렌즈(눈)가 없으면 사물이 된다"* 고 적었으나, **이번 3번에서 렌즈를 넣었는데도 천장 설비가 나옴** (실패 4연속: 3차 I안 → 4차 B안 → 4차 C안 → 5차 3번).

수정된 규칙:
- **렌즈는 필요조건이지 충분조건이 아니다.**
- 진짜 원인은 **명사 선택**. `lantern`, `obelisk`, `lamp` 처럼 실세계 사물 명사를 쓰면 렌즈가 있어도 그 사물의 관습적 배치(매달림·받침대)를 따라간다.
- **성공한 케이스는 모두 `pod`(3차 H, 5차 1·2번)** — 사물 명사가 아닌 중립적 형태 명사.
- → 얼개 프롬프트에는 **`pod` 계열 명사 + 렌즈 + `hovering ... casting a shadow beneath it`** 를 함께 쓸 것. 사물 명사는 피한다.

#### ★ 발견 #2 — 팔레트가 픽셀 아트 충실도를 좌우한다 (4차 발견 #2 재확인)
어둡고 채도 낮은 소수 색 팔레트(A 이끼 / 3번 남색)에서 플랫 컬러가 확연히 잘 나옴. 1·2차의 밝은 러스트/스틸 팔레트 대비 명백한 차이. **팔레트 확정 시 이 특성을 우선 고려할 것.**

#### ★ 발견 #3 — 저해상도 뭉개짐은 형태 단계에서 걸러야 한다
1번의 촉수 12개처럼 **작고 반복되는 요소가 많은 형태는 16x16에서 죽이 된다.** 컨셉 단계에서 "이 형태가 16px로 줄었을 때 남는가"를 판정 기준에 포함할 것.

- **파일 경로**: 미저장 (CDN 원본만 존재)
  - 1번: `hf_20260725_133114_4078d6ba-3fc7-4116-bd1f-04e359b9aae8.png`
  - 2번: `hf_20260725_133153_f5973ff5-d030-4602-bc74-1ebaf987ef2b.png` ★
  - 3번a: `hf_20260725_133224_b6201ad6-9be5-490d-9b51-952c9cee529b.png`
  - 3번b: `hf_20260725_133224_890ebf05-167c-4e97-a2f2-5cea29ee0bbb.png`

- **3번 탐색 팔레트** (밤/등불, 미확정)
```
#0D1020 #1A2038 #2E3A5C #55638C #8A94B0 #E4DECB #FFF6DE #8A5A1E #E0A03C #FFD98A
```

---

### 2026-07-25 (4차) — 색 + 형태 동시 탐색 3방향

- **목적**: 3차 비인간형 방향 유지하되, 색과 형태 언어를 함께 바꿔 대안 탐색. 3차에서 지적한 크기 불균형도 프롬프트에 반영 (`ROUGHLY THE SAME SIZE`).
- **모델**: `z_image` / **비용**: 0.15 × 4장 = 0.6 크레딧 (429 rate limit 2회 발생, 실패분 과금 없음)

**A — 이끼 폐허 / 보라 발광** (`965a6f6d`)
- 얼개 = 몸통 없는 부유 고리(이끼 낀 돌 토러스, 중앙에 보라 코어 부유) / 온기 = 이끼 낀 장갑 공벌레
- ✓ **지금까지 전 배치 중 픽셀 아트 완성도 최고.** 플랫 컬러 적중, 팔레트 준수 확연히 우수
- ✓ 얼개 실루엣이 전체 통틀어 가장 독창적
- ✗ **고리 가운데가 뚫려 있어 발판으로 사용 불가** — 코어 컨셉 §3 퍼즐 메카닉과 정면 충돌
- ✗ 고리는 정면/측면 구분이 없어 **시선 방향을 알 수 없음**

**B — 심해 도자기 / 산호 발광** (`855ac388`, `aaa0e4aa`)
- 얼개 = 금 간 도자기 오벨리스크(킨츠기) / 온기 = 촉수로 기는 물렁이
- ✓ **온기(문어 블롭)가 전체 라운드 통틀어 가장 귀여운 단일 캐릭터**
- ✓ 크기 균형 양호 — "비슷한 크기" 지시가 처음으로 먹힘
- ✗ **얼개가 기념비/화병으로 읽힘.** 2번째 컷은 받침대까지 생겨 더 악화

**C — 잿빛 + 주홍 단색 강조** (`42e6b239`) ✗ **실패**
- 의도: 얼개 = 종이등 큐브 / 온기 = 3족 스프링 호퍼
- ✗ **온기가 인간형으로 회귀.** "NO head, NO arms, exactly THREE legs" 명시했으나 큐브 머리 + 스프링 팔 2개(손 포함) + 다리 2개 생성. 지시 완전 무시
- ✗ 얼개도 램프 오브젝트로 읽힘

---

#### ★ 이번 라운드 핵심 발견 (다음 생성에 반드시 반영)

1. **부유형 관찰자는 "눈"이 없으면 사물이 된다.**
   3차 I안(천장 설비) → 4차 B안(기념비) → 4차 C안(램프)로 3연속 동일 실패.
   반대로 캐릭터로 읽힌 유일한 케이스는 **3차 H안이며, 차이는 렌즈(눈)의 유무**.
   → 얼개에는 시선을 지시하는 요소(렌즈/단안)가 **필수**. 추상 형태만으로는 캐릭터성이 성립하지 않음.

2. **어둡고 채도 낮은 소수 색 팔레트일수록 플랫 컬러가 잘 나온다.**
   A안이 전 배치 중 가장 픽셀 아트다웠던 이유로 추정. 향후 팔레트 설계 시 활용.

3. **형태 지시가 강할수록 인간형으로 회귀할 위험이 있다.**
   C안에서 대문자 부정형 지시(NO head / NO arms / exactly THREE legs)를 무시하고 기본값인 인간형 생성.
   → 부정형 나열보다 **긍정형 형태 묘사**(예: "a pod resting on three coiled springs")가 안전할 것으로 추정. 미검증.

4. **`ROUGHLY THE SAME SIZE` 지시는 유효.** 3차의 크기 불균형이 B안에서 해소됨.

- **파일 경로**: 미저장 (CDN 원본만 존재)
  - A: `hf_20260725_124051_965a6f6d-016c-4091-83fd-885e19a5f2c5.png`
  - B1: `hf_20260725_124136_855ac388-468c-4e4a-bc0d-02850c199735.png`
  - B2: `hf_20260725_124136_aaa0e4aa-105d-42cb-ac88-a49153e40857.png`
  - C: `hf_20260725_124319_42e6b239-4ecf-41fa-9809-ccf34f3f5d32.png`

- **탐색 팔레트** (모두 미확정, 참고용)
```
A 이끼:   #14180F #2C3A22 #5A7247 #9BB07A #D9E0C4 #3A2E44 #6B4E8C #B48AE8 #E8D9A0 #C4643C
B 도자기: #101A22 #1E3038 #35555E #6E9099 #E8E0D0 #FFF4E0 #F2C4C0 #E8746A #B8A890 #C9A24B
C 잿빛:   #0E0E10 #1E1E22 #3A3A42 #6A6A74 #A8A8B0 #E4E4E8 #7A1E12 #D93B22 #F27A45 #FFD9A0
```

---

### 2026-07-25 (3차) — 비인간형 재설계

- **목적**: 1·2차가 모두 인간형(머리-몸통-팔-다리)에 갇혀 있다는 피드백. 기획 문서 어디에도 인간형 제약이 없음을 확인하고 형태 전면 재설계.
- **설계 근거 — 코어 컨셉 §3**: *"정지 로봇은 발판·스위치 트리거 등 퍼즐 요소로 기능"*. 스왑 후 남겨진 몸이 **밟고 올라설 발판**이 돼야 하므로, 좁고 불안정한 인간형은 이 메카닉에 부적합. 넓고 낮은 형태가 유리.
- **모델**: `z_image` / **비용**: 0.15 × 4장 = 0.6 크레딧
- **형태 방향**:
  - **얼개(관찰자)** — 다리 없는 부유 센서 포드. 세로 캡슐, 큰 시안 렌즈 1개, 아래로 늘어진 탐침 다발. 정지 시 그 자리 고정 → **공중 발판**
  - **온기(행동가)** — 6족 저상 크롤러. 머리·팔 없이 둥근 껍데기를 짧은 다리들이 떠받침. 판 이음새로 앰버 발광, 뒤로 늘어진 케이블 꼬리
  - 부유/세로 vs 접지/가로 → 실루엣 대비 최대화

- **결과**: **H안 채택 권장 (미확정 — 사용자 선택 대기)**

- **검토 메모**:
  - **H** (`2be81710`) ★: 얼개에 **작은 조작 팔 2개 + 착지 프롱 3개**, 부유 그림자 명확. 팔이 있어 스위치 조작 연출이 자연스러움. 온기는 **등이 넓고 평평한 6족 크롤러 → 정지 시 발판으로 즉시 읽힘**. §3 퍼즐 메카닉 적합도 최고.
  - **F** (`b9053907`): 귀여움 최고(온기가 거북/딱정벌레형). 등 평평해 발판 OK. 얼개가 산만하고 **늘어진 탐침이 다리로 오독될 여지** 있음.
  - **G** (`20717b75`): 무난하나 온기 실루엣이 식빵 덩어리에 가까워 특징 최약.
  - **I** (`541ef46d`) ✗: **얼개가 천장 파이프에 매달린 설비로 읽힘** — 자율 이동 캐릭터가 아니라 고정 장치. 주인공으로 사용 불가. 온기도 다리가 바퀴로 보이고 눈이 없어 캐릭터성 최약.

- **4장 공통 문제 — 다음 생성 시 반드시 반영**:
  - **온기가 얼개보다 과도하게 큼 (4장 전부).** 둘 다 매개체가 갈아타는 몸이고 코-옵에서는 각 플레이어가 하나씩 전담하므로, 이 정도 질량 차는 화면 점유·히트박스·카메라 프레이밍을 모두 왜곡. 프롬프트에 "두 개체 크기 비슷하게" 명시 필요.
  - 기술적 한계는 1·2차와 동일 (소프트 셰이딩·글로우·AA) — 여전히 도트 작업용 레퍼런스

- **파일 경로**: 미저장 (CDN 원본만 존재)
  - F: `hf_20260725_123649_b9053907-3a38-4561-95a7-366b8732445b.png`
  - G: `hf_20260725_123649_20717b75-671f-441f-ab7d-88048f3bdf25.png`
  - H: `hf_20260725_123649_2be81710-d4b5-4bc3-8fcf-361cfcb9616a.png` ★
  - I: `hf_20260725_123649_541ef46d-170f-4def-90a4-1b88df42e1cd.png`

- **생성 프롬프트 핵심 블록** (팔레트/스타일 지시문은 1차와 동일)
```
Two NON-HUMANOID robot creatures side by side [...]

CRITICAL: neither robot is humanoid. No human body plan, no head-on-torso, no
arms, no hands, no two-legged standing figure. They are machine creatures, not
little people.

LEFT (the Observer): a legless floating sensor pod, hovering above the ground. A
tall vertical capsule shell like a hanging lantern, dominated by one huge round
glowing cyan lens set into its front. A cluster of thin limp probe cables dangles
beneath it. A faint hover shimmer under its base. [...] Body is wide and flat on top.

RIGHT (the Doer): a squat low crawler that walks on six short stubby insect legs.
A rounded pebble-shaped shell sitting low to the ground, warm amber light glowing
out from the seams between its plates. NO head and NO arms, just the domed shell
carried on its little scuttling legs, with a bundle of short frayed cables
trailing behind like a tail. [...] Mid-scuttle, legs splayed, restless and eager.
```

---

### 2026-07-25 (2차) — 주인공 2인 귀여운(치비) 방향 재생성

- **목적**: 1차 결과가 너무 무겁고 사실적이라는 피드백 → 치비/마스코트 방향으로 톤 전환.
- **방향 전환 근거**: 치비 비율(큰 머리 + 작은 몸)은 저해상도 스프라이트에서 실루엣 가독성이 가장 좋음. 1차에서 "온기 머리 과대 = 가분수 위험"으로 지적했던 항목이 이 방향에서는 **의도된 장점으로 반전**됨.
- **모델**: `z_image` / **비용**: 0.15 × 2장 = 0.3 크레딧 (`count: 4` 요청, 2장 반환)
- **유지**: 팔레트(1차와 동일), 얼개=냉색·차분 / 온기=난색·들썩 대비
- **변경**: 2등신 SD 비율, 둥근 모서리, 장난감 같은 친근함

- **결과**: **E안 채택 권장 (미확정 — 사용자 선택 대기)**

- **검토 메모**:
  - **D** (`71613cea`): 마스코트 페어로서 귀여움 최고. **치명적 문제 — 두 캐릭터가 색만 다르고 실루엣이 거의 동일.** 스왑 메카닉상 전투 중 즉시 구분이 필요한데 16x16에서 팔레트 스왑으로 보일 위험. 색각 이상 플레이어에게는 구분 불가.
  - **E** (`0251ca14`): **실루엣 구분이 3중으로 확보됨** — ① 키 차이(얼개가 더 큼) ② 두상(얼개=길쭉한 각진 박스 / 온기=납작한 원형) ③ 눈(얼개=외눈 시안 / 온기=두 눈 + 더듬이 2개). 저해상도에서도 형태만으로 판별 가능. 구도가 왼쪽으로 쏠려 여백이 큼 → 크롭 필요.

  **기술적 한계는 1차와 동일** — 소프트 셰이딩·글로우·AA·굵은 아웃라인. 여전히 스프라이트 아님, 도트 작업용 레퍼런스.

- **미해결 이슈**:
  - 두 안 모두 온기의 "앞으로 기운 채 들썩이는 자세"가 반영 안 됨 (둘 다 정자세)
  - "가슴 이음새로 새어나오는 따뜻한 빛"이 두 안 모두 **배 전체를 덮는 평면 패널**로 해석됨 — 의도와 다름
  - E안 구도 쏠림 (크롭으로 해결 가능)

- **파일 경로**: 미저장 (CDN 원본만 존재)
  - D: `hf_20260725_123220_71613cea-689e-44ff-ab9d-22184fb31d0d.png`
  - E: `hf_20260725_123220_0251ca14-fa4f-4639-966c-d127d45507af.png`

- **생성 프롬프트**: 1차 프롬프트에서 아래 블록을 교체 (나머지 팔레트/스타일 지시문 동일)
```
Cute chibi pixel art character reference sheet [...] Two small adorable robot
mascot characters [...]

Super-deformed chibi proportions: about 2 heads tall, oversized round head, tiny
stubby body, short chubby arms and little rounded feet. Soft rounded corners
everywhere, no sharp or menacing shapes. Friendly toy-like appeal.

LEFT ROBOT (the Observer): a calm tidy little robot, slightly taller and neater.
Big rounded boxy head with one huge glowing cyan eye lens taking up most of the
face, a small bent antenna with a tiny ball on top. [...] Standing politely
upright, curious and thoughtful expression.

RIGHT ROBOT (the Doer): a chubby bouncy little robot, rounder and squatter. Big
dome head with a wide happy amber visor slit curved like a smile, two short
springy cable antennae flopping to one side. [...] Leaning forward on tiptoes
mid-bounce, energetic and eager.

Both are cute little scavenged robots with a soft warm glow in the chest.
Charming, cozy, wholesome mood.
```

---

### 2026-07-25 (1차) — 주인공 2인 (얼개 / 온기) 컨셉 레퍼런스

- **목적**: 주인공 로봇 2체의 실루엣 대비 + 팔레트 방향 확정. 스왑 메카닉상 두 로봇이 한눈에 구분돼야 하므로 한 장에 나란히 배치해 대비를 함께 검증.
- **생성 경로**: Higgsfield MCP (`generate_image`) — 수동 ChatGPT 실행 아님
- **모델**: `z_image` (Tongyi-MAI)
  - `recraft_v4_1`을 1순위로 시도 → **403 `job_minimum_basic_plan_required`** (유료 Basic 플랜 전용). free 플랜에서 사용 불가.
  - `recraft_v4_1`은 `colors` 파라미터로 팔레트를 모델 레벨에서 강제할 수 있어 이 프로젝트에 이상적 — **유료 전환 시 1순위 재시도 대상**.
- **비용**: 0.15 크레딧/장 × 3장 = 0.45 크레딧 (`recraft_v4_1`은 1.25 크레딧/장)
- **파라미터**: `aspect_ratio: 4:3`, `count: 4` (3장 반환), 출력 2048x1536

**제안 팔레트 (미확정 — STYLE_GUIDE.md는 여전히 TBD)**

캐릭터 설정에서 도출: 얼개=기계적 명료함(냉색), 온기=생물체적 온기(난색).

```
#12131A  다크 아웃라인
#2A2E3D  그림자
#4A5164  중간 메탈
#7C8598  밝은 메탈
#C3CBD8  하이라이트
#2E6F8E  얼개 스틸블루
#5FC9E8  얼개 시안 발광
#9B4A22  온기 러스트
#F2903C  온기 앰버
#F6D26A  난색 하이라이트
배경:    #1B1E28
```

- **결과**: **컨셉 레퍼런스로는 승인 / 스프라이트로는 사용 불가**

- **검토 메모** (STYLE_GUIDE 체크리스트):
  | 항목 | 결과 |
  |------|------|
  | 1px 다크 아웃라인 | ✗ 두껍고 굵기가 일정하지 않음 |
  | 팔레트 범위 준수 | ✗ 지정 10색을 크게 벗어남 (소프트 셰이딩·블룸) |
  | 안티앨리어싱 없음 | ✗ AA·글로우 블룸 다수 |
  | 스프라이트 크기 사양(16x16) | ✗ 2048x1536 일러스트 |
  | 게임 분위기 부합 | ✓ 폐기물·풍화된 기계, 쓸쓸한 톤 잘 맞음 |
  | 애니메이션 프레임 | N/A (단일 idle 레퍼런스) |

  **결론**: 3장 모두 "픽셀 아트 *풍* 일러스트"이지 진짜 픽셀 아트가 아님. 텍스트→이미지 모델의 구조적 한계 — 게임에 바로 넣을 16x16 스프라이트는 이 경로로 안 나옴. **디자인 레퍼런스로 확정하고, 실제 스프라이트는 이걸 보고 도트를 찍는 용도**로 사용.

  개별 평:
  - **A** (`2af468a3`): 두 캐릭터 대비가 가장 명확. 온기 머리가 둥근 돔(용접 마스크형). 온기의 "앞으로 기운 자세"는 거의 반영 안 됨.
  - **B** (`dd688e84`): 온기 머리가 각진 기계형, 몸통이 가장 묵직. **다운스케일 후에도 실루엣이 살아남을 가능성이 가장 높음.**
  - **C** (`46a97de8`): 케이블 다발·풍화 표현 등 캐릭터 설정 반영도가 가장 높고 온기가 앞으로 웅크린 자세. 다만 **머리가 몸에 비해 과대 → 16x16에서 가분수로 읽힐 위험**.

- **미해결 이슈**:
  - 얼개 쪽은 3장 모두 거의 동일 — 관찰자의 "분석적/철학적" 성격을 실루엣으로 더 밀어낼 여지 있음
  - 온기 머리 크기가 저해상도에서 실루엣 가독성을 해칠 수 있음 → 스프라이트화 시 두상 비율 축소 필요
  - 배경 투명 PNG 아님 (플랫 `#1B1E28`). 컷아웃 필요 시 `remove_background` 툴로 후처리 가능

- **파일 경로**: 미저장 (아래 CDN 원본만 존재, 만료 가능성 있음 — 채택본은 리포로 내려받을 것)
  - A: `hf_20260725_122716_2af468a3-fc0f-4acc-9ed6-ffba168e699e.png`
  - B: `hf_20260725_122716_dd688e84-7dd1-424c-9e49-6ea51b232736.png`
  - C: `hf_20260725_122716_46a97de8-8274-46d6-a28a-842aa2061d54.png`

- **생성 프롬프트**:
```
Pixel art character reference sheet, retro 16-bit game sprite style. Two humanoid
robot characters standing side by side, full body, front-facing idle pose, clearly
separated with empty space between them, on a flat solid dark background (#1B1E28).

LEFT ROBOT (the Observer): tall and thin, angular geometric frame, sharp rectangular
plating, exposed segmented joints, a single large round glowing cyan lens as its
head-eye, one narrow antenna. Cold steel-blue and cyan color scheme (#2E6F8E,
#5FC9E8). Posture perfectly upright, still, watchful and analytical.

RIGHT ROBOT (the Doer): shorter and stockier, rounded dented chassis, thick sturdy
limbs, a horizontal visor slit face glowing warm amber, frayed cable tufts at the
shoulders. Rust-orange and amber color scheme (#9B4A22, #F2903C). Posture leaning
forward with weight on the front foot, restless and eager to move.

Both are scavenged weathered post-apocalyptic machines, with a faint warm glow
leaking through the seams of the chest plate. Melancholic lonely tone.

Strict limited palette, only these colors: #12131A dark outline, #2A2E3D shadow,
#4A5164 mid metal, #7C8598 light metal, #C3CBD8 highlight, #2E6F8E steel blue,
#5FC9E8 cyan glow, #9B4A22 rust, #F2903C amber, #F6D26A warm highlight.

Style: crisp pixel art, hard-edged blocky pixels, 1px dark outline around every
silhouette, flat color fills only, no anti-aliasing, no gradients, no blur, no soft
shading, no text, no labels, no watermark. Clean readable silhouettes.
```

---

### 2026-08-04 — 리부트 세계관 기반 맵/캐릭터 컨셉 6종

- **목적**: `2026-07-30-mind-economy-worldbuilding-design.md`와 `2026-08-03-story-ignition-design.md` 기준으로 예상 맵 이미지 3장, 캐릭터 이미지 3장을 픽셀 아트 레퍼런스로 생성. 최신 설정은 A=스위치가 달린 깃든 로봇, B=스위치를 누른 생명체, 적=균형자 현상으로 해석.
- **모델/경로**: Codex built-in `image_gen` 직접 생성 후 프로젝트 폴더로 복사.
- **생성 프롬프트**:

```text
1. map_01_awakening_cave
Pixel art map concept, Metroidvania area screenshot reference. The awakening cave where a living creature finds and switches on an inhabited robot object. Quiet underground cave chamber, side-view Metroidvania layout with platforms, lower path, high ledge, broken stone steps, hanging roots, old machinery half-buried in rock, and a small switch pedestal near a dormant compact robot. Include two tiny readable figures only as scale cues: one small curious organic creature and one just-awakened switch-bearing robot with a dim cyan eye. Crisp retro pixel art, 16x16 tile language, 1px dark outline, limited low-saturation palette, no text, no labels, no UI, no minimap, no blur, no watermark.

2. map_02_collapse_archive
Pixel art map concept, Metroidvania area screenshot reference. A collapsed knowledge settlement where explorers who chased the legend have begun to break down from knowing too much. Abandoned hillside archive-village with cracked library walls grown into roots, broken ladders, suspended book-stone tablets, shrine alcoves, and branching platform routes. Include a few tiny collapsed wanderer silhouettes as environmental storytelling, plus distant A/B scale silhouettes. Crisp retro pixel art, 16x16 tile language, 1px dark outline, muted ink blue, old stone, moss, parchment, pale gold, faint cyan. No text, no readable symbols, no UI, no gore, no blur, no watermark.

3. map_03_balancer_route
Pixel art map concept, Metroidvania area screenshot reference. A silent route where the Balancer manifests as a world phenomenon trying to switch A off. Deep subterranean mechanism-temple with massive broken counting wheels, vertical balance scales built into architecture, sealed pale-light channels, black stone platforms, and a central corridor that feels watched without eyes. Show tiny A/B silhouettes near lower left facing a distant abstract pale geometric presence embedded in architecture. Crisp retro pixel art, 16x9 side-scrolling room, no humanoid boss, no face, no text, no UI, no blur, no watermark.

4. character_01_A_switch_robot
Pixel art character concept reference. A, the inhabited object: a small non-humanoid robot object with a clear physical toggle switch on its top-back, compact squat body, one large round cyan lens in front, old stone-and-metal casing, tiny roller feet or low crawler base, simple side brackets but no hands. Newly awake, blank, curious, a little lost. Centered on flat dark background, large readable silhouette, no humanoid head-torso-arms-legs design, no weapon, no text, no scenery, no watermark.

5. character_02_B_living_creature
Pixel art character concept reference. B, the living creature who accidentally switches on A and begins the dangerous path of knowing too much. Small non-human organic lifeform, curious young wanderer, soft compact silhouette, leaf-like ear fins, short tail, small hands, wide attentive dark eyes, simple travel wrap and root-fiber satchel. Subtle pale-gold crackle or ring motif near the head to hint at an overfull mental vessel. Centered on flat dark background, no weapon, no text, no scenery, no watermark.

6. character_03_balancer
Pixel art character/boss concept reference. The Balancer, a silent world phenomenon that comes to switch A off, not a villain and not a person. Abstract non-humanoid boss-like manifestation: floating pale geometric core nested inside broken brass balance-scale arms and dark stone plates, with a small switch-like prong or key-shaped actuator beneath it. Inevitable, precise, impersonal. No face, no eyes, no mouth, no limbs, no robe, no skull, no text, no scenery, no watermark.
```

- **결과**: 사용자 선별 후 채택 후보로 보존. 실제 16x16 게임 스프라이트가 아니라 도트 작업·구역 방향성 검토용 고해상도 픽셀 아트 레퍼런스로 사용.
- **사용자 반응**: "처음에 뽑아준 6장 좀 맘에드는데" 이후 마음에 들지 않는 이미지를 직접 삭제. 2026-08-04 재확인 기준 현재 5장 보존.
- **검토 메모**:
  - 보존된 맵 2종은 시작 동굴과 균형자 접근 구역의 기능과 분위기가 분리되어 읽힘.
  - `map_02_collapse_archive.png`는 사용자 삭제로 비채택 처리.
  - A는 스위치, 단안 렌즈, 저상 몸통이 명확해 최신 설정과 잘 맞음.
  - B는 호기심 많은 생명체로 잘 읽히나, 최종 스프라이트화 시 더 작은 실루엣과 적은 디테일로 재도트 필요.
  - 균형자는 비인간형·비악당형 현상으로 읽히며, 보스 또는 맵 장치로 확장 가능.
- **현재 보존 파일 경로**:
  - `docs/art/assets/generated-concepts/maps/awakening-cave/map_01_awakening_cave.png`
  - `docs/art/assets/generated-concepts/maps/balancer-route/map_03_balancer_route.png`
  - `docs/art/assets/generated-concepts/characters/A/character_01_A_switch_robot.png`
  - `docs/art/assets/generated-concepts/characters/B/character_02_B_living_creature.png`
  - `docs/art/assets/generated-concepts/characters/balancer/character_03_balancer.png`
- **사용자 삭제 / 비채택**:
  - `map_02_collapse_archive.png` — 사용자 삭제, 파일 미보존

---

### 2026-08-04 — 리부트 세계관 보너스 컨셉 6종

- **목적**: 사용자 요청 "6개 더, 원하는 걸로"에 따라 최신 리부트 세계관의 빈 축을 확장. 다른 깃든 사물 2종, 붕괴자 2종, 초반 놀이 구역/전설의 흉터 구역 2종을 생성.
- **모델/경로**: Codex built-in `image_gen` 직접 생성 후 프로젝트 폴더로 복사.
- **생성 프롬프트**:

```text
1. bonus_01_rain_listening_bell
Another inhabited object: the Rain-Listening Bell, one of the few rare objects where excess mind settled after the legend. A small non-humanoid bell-like relic character made of cracked dark bronze and pale ceramic plates, hovering slightly, with a tiny old pull-switch lever and a soft internal cyan-gold slit of awareness. Ancient, patient, lonely. Flat dark background, 1px dark outline, limited low-saturation palette, no humanoid body, no text, no scenery.

2. bonus_02_ash_cradle_cart
Another inhabited object: the Ash-Cradle Cart, a rare object carrying settled excess mind. A tiny wheeled cradle-cart made from charred wood, old brass hinges, cracked white stone, and a protected ember-like core inside. Two simple wheel pods, one fold-out switch tab, fragile and practical. Flat dark background, 1px dark outline, no humanoid body, no animal traits, no text, no scenery.

3. bonus_03_collapsed_wanderer
A collapsed wanderer: a living being whose vessel broke from knowing too much. Small non-human organic wanderer wrapped in old travel cloth, hunched but not monstrous, with fragmented pale-gold memory shards around the head and one hand clutching a blank charm. Sad, confused, dangerous only because broken. Flat dark background, no gore, no text, no weapon.

4. bonus_04_pathless_collapsed_seeker
A pathless collapsed seeker: a mid-game enemy born from overfull knowledge. Low crawling non-human seeker made of organic bark-like limbs and torn travel gear, carrying blank map scraps tied to its back. Bowed head under fractured pale-gold memory pieces, four uneven limbs close to the ground, simple readable silhouette. Flat dark background, no gore, no readable map symbols, no weapon.

5. bonus_05_first_play_area
The first play area where A and B simply play before the Balancer arrives. Side-view Metroidvania room in a soft underground overgrown ruin: low platforms, hollow pipes, a small seesaw-like stone beam, shallow safe water, hanging roots, harmless broken mechanisms, and multiple tiny routes for jumping and experimenting. Include tiny A/B silhouettes separated across a toy-like obstacle. 16x16 tile language, no UI, no text.

6. bonus_06_scar_of_legend
The Scar of the Legend: a place where excess mind first settled into objects after a forgotten mass death. Vast side-view Metroidvania chamber with ancient broken memorial machinery, silent stone basins, half-buried relic objects glowing faintly, shattered balance-scale architecture, and platforms around a deep central void. Suggest catastrophe without bodies or gore. 16x16 tile language, no UI, no text.
```

- **결과**: 사용자 선별 후 채택 후보로 보존. 실제 게임용 16x16 스프라이트/타일셋이 아니라 고해상도 픽셀 아트 레퍼런스로 사용.
- **사용자 반응**: "사진도 좀 맘에든다" 이후 마음에 들지 않는 이미지를 직접 삭제. 2026-08-04 재확인 기준 현재 5장 보존.
- **검토 메모**:
  - `bonus_01`은 깃든 사물의 "사물이 인격이 된 느낌"이 가장 강함. 단, 종 형태가 고정 장식으로도 읽힐 수 있어 이동 연출이 필요.
  - `bonus_02`는 사용자 삭제로 비채택 처리.
  - `bonus_03`은 붕괴자의 슬픔이 잘 드러나지만 인체형에 가까워질 위험이 있음.
  - `bonus_04`는 적 실루엣이 강함. 실제 저해상도화 시 지도 조각/끈 디테일은 줄여야 함.
  - `bonus_05`는 초반 "목적 없이 논다" 구간에 적합. 맵 기믹 후보가 자연스럽게 보임.
  - `bonus_06`은 후반 로어 구역 후보로 강함. 장식 밀도가 높아 실제 타일셋화 시 구역 대표 장면으로 쓰는 편이 좋음.
- **현재 보존 파일 경로**:
  - `docs/art/assets/generated-concepts/characters/inhabited-objects/bonus_01_rain_listening_bell.png`
  - `docs/art/assets/generated-concepts/characters/collapsed/bonus_03_collapsed_wanderer.png`
  - `docs/art/assets/generated-concepts/characters/collapsed/bonus_04_pathless_collapsed_seeker.png`
  - `docs/art/assets/generated-concepts/maps/first-play-area/bonus_05_first_play_area.png`
  - `docs/art/assets/generated-concepts/maps/scar-of-legend/bonus_06_scar_of_legend.png`
- **사용자 삭제 / 비채택**:
  - `bonus_02_ash_cradle_cart.png` — 사용자 삭제, 파일 미보존

---

### 2026-08-07 — 세계 기술 수준 비교 3종 (통제 비교)

- **목적**: 세계의 기술 수준(㉮ 소박한 기계 일상 / ㉯ 몰락 문명 유물 / ㉰ 첨단 SF)을 결정하기 위한 시각 비교.
- **설계**: **같은 장면(여행길가 여관)·구도·톤·A/B 실루엣·픽셀 스타일을 전부 고정하고 "기계가 어떻게 읽히는가"만 변주**한 통제 비교. A/B 실루엣은 2026-08-04 컨셉과 연속성 유지(A=시안 렌즈+스위치+저상 크롤러, B=잎귀+새챌 어린 방랑자).
- **경로**: Codex built-in `image_gen` 직접 생성 후 프로젝트 폴더로 복사. 프롬프트 전문은 `docs/art/WORLD_TECH_LEVEL_COMPARISON.md`.
- **판단 포인트**: A(작은 기계)가 ㉮ 시시한 물건 / ㉯ 신비한 유물 / ㉰ 흔한 기술로 읽히는지. 설정상 A가 시시해야 `무의미 반전 금지`·루브릭 `고대문명 유적 🔴포화` 회피가 성립.
- **결과**: 생성 완료. 사용자 선별 후 채택 결정 → 세계관 스펙·DECISIONS.md 반영 예정.
- **검토 메모**:
  - ㉮는 A가 여관 앞 생활 기계들과 섞여 "시시한 물건"으로 가장 잘 읽힘. 현재 설정의 권고안에 가장 가깝다.
  - ㉯는 기계가 로프 친 유물/성물로 강하게 읽혀 분위기는 좋지만 `무의미 반전 금지`와 고대문명 클리셰 위험이 큼.
  - ㉰는 A가 흔한 로봇 사회에 묻히고 도보 여행/여관 톤이 약해져 세계관 핵심과 충돌함.
- **현재 보존 파일 경로**:
  - `docs/art/assets/generated-concepts/world/tech-level/tech_level_01_simple_machines.png`
  - `docs/art/assets/generated-concepts/world/tech-level/tech_level_02_ruin_relics.png`
  - `docs/art/assets/generated-concepts/world/tech-level/tech_level_03_high_tech.png`

---

### 2026-08-07 — 비인간 건축 서명 비교 3종 (라운드 2)

- **목적**: 1라운드에서 ㉮(소박한 기계 일상)가 유력해진 뒤, "사람이 만든 집처럼 보인다"는 문제를 해결하기 위한 비인간 건축 언어 비교.
- **설계**: 기술 수준은 ㉮로 고정하고, 변수는 건축 서명 하나만 둔다. 2-A 껍질·굴 / 2-B 균사·버섯 / 2-C 엮은 둥지.
- **경로**: Codex built-in `image_gen` 직접 생성 후 프로젝트 폴더로 복사. 프롬프트 전문은 `docs/art/WORLD_TECH_LEVEL_COMPARISON.md` 라운드 2 절.
- **결과**: 생성 완료. 사용자 선별 후 세계의 건축 언어로 채택 여부 결정.
- **검토 메모**:
  - 2-A는 사람 집 느낌을 가장 잘 벗고, 껍질·굴 문명으로 읽힌다. 할로우나이트식 낯섦과 소박함의 균형이 좋다.
  - 2-B는 가장 몽환적이고 아름답지만, 버섯 마을 판타지로 강하게 읽혀 톤 선택이 필요하다.
  - 2-C는 연약하고 따뜻한 생활감이 좋지만, 엮은 섬유/둥지 디테일이 많아 실제 타일셋화 때 단순화가 필요하다.
- **현재 보존 파일 경로**:
  - `docs/art/assets/generated-concepts/world/architecture-signatures/architecture_2a_shell_burrow.png`
  - `docs/art/assets/generated-concepts/world/architecture-signatures/architecture_2b_fungal_grown.png`
  - `docs/art/assets/generated-concepts/world/architecture-signatures/architecture_2c_woven_nest.png`

---

### 2026-08-07 — ✅ 세계 아트 방향 확정 (라운드 1~2 결론)

위 라운드 1(기술 수준)·라운드 2(비인간 건축)의 최종 결정.

- **기술 수준 = ㉮** (소박한 기계 + 느린 여행 공존). ㉯ 유물·㉰ SF 탈락
- **건축 = 비인간·유기적** (인간 건축 배제)
- **3종 서명 전부 채택** — 사용자 "셋 다 맘에든다" → **구역별 건축 언어**로 사용(구역-서명 배정은 지리 설계 때). `architecture_2a/2b/2c` 3장 보존
- **Codex 픽셀 질감 = 아트 파이프라인 기준** (사용자 승인)
- **팔레트** — 구역별로 채택 이미지에서 샘플링 예정 (`STYLE_GUIDE.md` 구역별 표 TBD)
- **반영**: `DECISIONS.md` 2026-08-07 행, `STYLE_GUIDE.md` 세계 아트 디렉션·구역별 서명 표, `WORLD_TECH_LEVEL_COMPARISON.md` 결론 절

---

### 2026-08-20 — ★ A(플레이어) 인게임 스프라이트 + 애니메이션 4종 (첫 실전 에셋)

- **목적**: 그레이박스 1x1 사각형이던 Player_A를 실제 픽셀 스프라이트로 교체. 컨셉 → 게임 에셋 전환의 첫 사례.
- **경로**: **PixelLab MCP** (Codex/DALL-E 수동 실행 아님 — 생성부터 Unity 적용까지 자동 파이프라인). 트라이얼 계정, 총 4생성 사용(잔여 3).
- **기준 컨셉**: `character_01_A_switch_robot.png` (2026-08-04 채택) — 상단 토글 스위치 / 단안 시안 렌즈 / 이끼 낀 석재-금속 몸통 / 하단 롤러가 32px에서 전부 생존.
- **생성 설계 (저비용 파이프라인)**:
  1. `create_image_pixen` 32x32 측면(동향) 기본 스프라이트 1장 — 1생성
  2. `animate_image`로 기본 프레임에서 Idle(4f)·Run(8f)·Jump/Fall(6f) 파생 — 각 1생성
- **프레임 선별**:
  - Idle = 생성 0~3 (렌즈 명멸 + 1px 들썩임). **4번 탈락 — 렌즈가 꺼진 실루엣.** A의 「꺼짐」은 서사 사건이므로 아이들에 쓰면 안 됨
  - Run = 생성 1~8 (전진 기울기 + 롤러 회전 + 스위치 흔들림)
  - Jump = 점프 생성분 1~3 (웅크림→발사), Fall = 4·6 (기수 하강)
- **검토 메모** (스타일 가이드 체크리스트):
  - 1px 다크 아웃라인 ✓ / AA 없음 ✓ / 저채도 팔레트·쓸쓸한 톤 ✓ / 프레임 수 기준(권장치) ✓
  - ⚠️ **크기 32x32 — 가이드 기본 16x16에서 이탈.** 스위치+렌즈+롤러 3요소가 16px에서 뭉개져 32px 채택(PPU 32 → 월드 1유닛으로 크기 동일). **가이드의 「기본 스프라이트 크기」 항목은 재검토 필요** — 타일 16px과의 혼용 시 mixel 문제는 타일셋 제작 때 판단
  - 팔레트는 여전히 미확정 — 이 스프라이트에서 샘플링해 확정하는 것도 가능
- **Unity 적용** (2026-08-20, 테스트 27/27 유지):
  - `Assets/Art/Player/` — 개별 프레임 PNG 17장 (A_idle_0~3 / A_run_0~7 / A_jump_0~2 / A_fall_0~1), PPU 32·Point·무압축
  - 클립 4종(`A_Idle/Run/Jump/Fall.anim`) + `A_Animator.controller` (Speed/IsGrounded/VerticalVelocity, 전환 duration 0)
  - `PlayerAnimator.cs` 신설 (Gameplay/Player) — Motor 상태 → 파라미터, Facing → flipX. 뷰 전용(꺼도 게임 동작)
  - `Greybox_Movement.unity` Player_A에 Animator + PlayerAnimator 연결, 기본 스프라이트 A_idle_0
- **파일 경로**: `src/Miji/Assets/Art/Player/*.png` (원본 프레임·검수 스트립은 세션 스크래치패드 — 리포 보존본은 Assets가 원본)
- **결과**: 승인 대기 — 플레이 모드에서 Idle/Run/공중 프레임 전환 확인 완료. 재생성 원하면 잔여 3생성 내에서 부분 교체 가능

---

### 2026-08-20 (2차) — 동굴 지형 타일 2종: 생성 실패 → A 팔레트 기반 수제작

- **목적**: 그레이박스 지형(민무늬 사각형)에 첫 타일 텍스처. 대상 구역은 각성 동굴 톤.
- **PixelLab 시도 (2생성 소모, 잔여 1)**: pixen 16x16 「심리스 타일 텍스처」 2종 요청 → **둘 다 실패.** 텍스처가 아니라 **블록 오브젝트 낱개**를 그려서 반복 배치 시 사이가 뜬다. ★ **교훈: pixen은 "seamless tileable texture" 지시를 무시하고 사물을 그린다** — 타일 텍스처는 이 경로로 안 나온다 (`create_sidescroller_tileset`은 20~40생성이라 트라이얼 불가)
- **대체 경로 — 수제작**: `A_idle_0`에서 팔레트 실측 샘플링(슬레이트 #262939/#191B27/#3B3F50/#2E3243 + 이끼 #8A9C39/#4A5824/#2A3314) 후 16x16 타일 2종을 직접 도트:
  - `Tile_CaveTop.png` — 상단 3px 이끼 캡(하이라이트/본체/그림자 지그재그) + 하단 12행은 채움 타일과 동일 → **위-아래로 쌓아도 무이음**
  - `Tile_CaveFill.png` — 슬레이트 암반 + 산점 크랙/스페클, 사방 무이음(구조적 보장)
- **Unity 적용**: `Assets/Art/Tiles/`, PPU 32(16px=0.5유닛)·Point·Repeat·FullRect. 씬 지형 5종을 Tiled draw mode로 전환(스케일→renderer.size 이관, 콜라이더 월드 크기 불변) — 발판 3종=이끼 상단 / 벽=채움(이끼줄 반복 방지) / 지반=채움+표면 스트립 자식. 카메라 배경 #0E1017. 상세는 IMPL_REGISTRY 4차
- **검토 메모**: 16px 타일과 32px 캐릭터의 픽셀 밀도 동일(둘 다 PPU 32) — 스타일 가이드 「타일 16x16」과 정합. 수제 타일이라 팔레트 이탈 0. 플레이 실측으로 무이음·물리 불변 확인
- **결과**: 승인 대기 (미커밋 — 사용자 확인 후 커밋)

---

### 2026-08-20 (3차) — B(무리비) 테스트용 스프라이트 1장 ⚠️ 트라이얼 소진

- **목적**: 테스트용 동행자. A 뒤를 따라다니는 최소 구현에 쓸 단일 프레임.
- **경로**: PixelLab pixen 32x32 측면(동향) 1장 — **마지막 1생성 사용, 트라이얼 잔여 0.** 이후 생성은 구독 전환 또는 수제 도트 필요
- **기준 컨셉**: `character_02_B_living_creature.png` — 잎귀 핀·큰 눈·갈색 여행 랩·잎꼬리 재현. ★ **이마 금빛 균열은 의도적으로 제외** — 균열은 중반 이후 상태이고 시작 시점의 B에게는 없다(캐릭터 스펙 「금빛 균열」 절). 컨셉 이미지가 중반 상태라는 점을 프롬프트에서 명시적으로 차단
- **검토 메모**: 잎귀·큰 눈·랩·꼬리 32px 생존 ✓ / 1px 아웃라인 ✓ / 저채도 이끼+녹빛 팔레트로 A와 톤 일치 ✓ / 새챌은 랩에 뭉개짐(허용 — 재생성 예산 없음)
- **애니메이션**: 없음(예산 0). 걸음 들썩임은 `CompanionFollower`가 코드로 처리(사인 바운스). 정식 Idle/Walk는 구독 후
- **파일 경로**: `src/Miji/Assets/Art/Player/B_idle_0.png`
- **결과**: 승인 대기 (미커밋)

---

### 2026-08-20 (4차) — B 스프라이트 재구성: A 렌더링 문법으로 후처리 (생성 0회)

- **발단**: 사용자 지적 「B가 너무 2D 같음, A처럼」. 원인 분석 — B 원본은 ①채도 높고 밝은 녹색(#78A54D·#B2DB78)이라 A의 풍화된 저채도 세계에서 만화처럼 뜨고 ②생성 노이즈로 색이 56개로 흩어져 명암 단이 뭉개져 있었다. 아웃라인은 검정이라 문제없음
- **처리 (트라이얼 소진 상태 — 전량 후처리, 생성 0회)**:
  1. **팔레트 통합**: 56색 → 계열별 램프로 양자화. 녹색은 **A의 이끼 올리브 계열**(#26301A→#A8B85F 6단), 랩은 러스트 4단, 눈 글린트는 웜 뉴트럴
  2. **엣지 릴라이트**: 위가 뚫린 픽셀 +1단(빛)·아래가 뚫린 픽셀 -1단(그림자) — A의 판때기 명암 문법을 기계적으로 재현
  3. **눈 수술**: 릴라이트로 뭉개진 눈(3x4px) 직접 복원 — 검정 아몬드 유지, 홍채 #A65B32, 글린트 #E8E4D8
- **도구**: PowerShell System.Drawing 픽셀 처리(`relight_b.ps1`). ★ **재사용 가능** — 향후 생성물이 세계 톤에서 뜰 때 같은 램프로 통과시키면 된다
- **검토 메모**: A와 나란히 두면 같은 팔레트 가족으로 읽힘 ✓ / 상단 하이라이트·턱 밑 음영으로 부피감 ✓ / 인게임 확인 완료
- **파일 경로**: `B_idle_0.png` 제자리 교체 (원본은 스크래치패드 `B_base_32.png`)
- **결과**: 승인 대기 (미커밋)

---

### 2026-08-20 (5차) — 픽셀 밀도 상향: AK-xolotl: Together 수준을 밀도 레퍼런스로 채택

- **목적**: 사용자 요청 "AK-xolotl: Together 해당 게임정도로 픽셀 농도?를 높이고 싶음" 반영. 기존 16x16 캐릭터 기본값을 폐기하고, A/B 실전 에셋에서 이미 검증된 32x32 캐릭터 밀도를 공식 기준으로 승격.
- **레퍼런스 해석**: AK-xolotl은 밝은 탑다운 액션/슈터 톤이 아니라, 화면 안 오브젝트·캐릭터·지형의 조밀한 픽셀 정보량만 참고한다. Miji는 사이드뷰 메트로배니아, 쓸쓸함, lived-in, 소박한 민속 기계 톤을 유지.
- **생성 프롬프트**:

```text
Pixel art sprite sheet, 32x32 pixels per frame, [N] frames.
Subject: [Miji character/object description]
Animation: [idle/run/jump/fall/attack/etc.]
Style: dense indie pixel art, 1px dark outline, limited low-saturation palette, crisp hard-edged pixels.
Pixel density: comparable detail density to AK-xolotl: Together screenshots, used only as a density reference; do not copy its characters, UI, weapons, gore, top-down composition, or bright comedic tone.
Miji art direction: melancholic side-view metroidvania, lived-in folk machinery, organic non-human architecture, old slate stone, moss olive, weathered brass, faint cyan awareness light.
Sprite rules: readable silhouette first, 2-3 identity details maximum, no tiny repeated decorations, no humanoid body plan unless explicitly requested.
Rendering rules: no anti-aliasing, no soft gradients, no bloom, no blur, no text, transparent background PNG.
```

- **가이드 반영**: `STYLE_GUIDE.md` 기본 스프라이트 크기 = 32x32 캐릭터 / 16x16 타일, PPU 32, 보스 64~96, 밀도 체크리스트 추가.
- **검토 메모**: A/B의 32px 실측과 정합 ✓ / 16px 타일 모듈과 PPU 32로 mixel 위험 낮음 ✓ / AK-xolotl의 IP·톤 복제 방지 문구 포함 ✓
- **파일 경로**: 문서 변경만 해당 (`docs/art/style-guide/STYLE_GUIDE.md`, `docs/art/ART_LOG.md`, `docs/DECISIONS.md`)
- **결과**: 기준 확정. 이후 플레이 레이어 에셋은 이 밀도 기준으로 생성/후처리.

---

### 2026-08-20 (6차) — A/B/균형자 캐릭터 라인업 아트워크

- **목적**: 사용자 요청 "A,B,균형자 캐릭터 아트워크 좀 그려주셈 도트 풍으로" 반영. 기존 채택 컨셉 3장(`character_01_A_switch_robot.png`, `character_02_B_living_creature.png`, `character_03_balancer.png`)을 기준 이미지로 사용해 한 화면의 캐스트 라인업을 생성.
- **경로**: Codex built-in `image_gen` 직접 생성 후 프로젝트 폴더로 복사.
- **생성 프롬프트**:

```text
Use case: stylized-concept
Asset type: Miji game character lineup concept artwork, pixel-art style reference
Primary request: Create one polished pixel-art-style character artwork showing the three main characters A, B, and the Balancer together as a side-view metroidvania cast lineup.
Input images: Image 1 is A reference; Image 2 is B reference; Image 3 is Balancer reference. Preserve their core identities and visual continuity, but redraw them into one coherent artwork.
Scene/backdrop: very dark simple cave-stage backdrop with a small slate stone platform under the characters, subtle moss, no readable text, no UI.
Subjects:
- A: small squat non-humanoid switch robot, weathered slate stone and old metal casing, clear physical toggle switch on top, one large cyan lens, compact roller/crawler base, humble everyday folk machine, not a sacred relic.
- B: small non-human organic wanderer, soft leaf-like ear fins, large dark attentive eyes, muted moss-olive skin, rusty brown travel wrap and root-fiber satchel, curious but worried. Early-game B: no golden forehead cracks.
- The Balancer: much larger, looming but not evil; impersonal accounting presence made from a heavy vessel body, stolen observation lens/helmet feeling, broken brass balance-scale arms, dark stone plates, pale geometric core, small switch/key-like actuator beneath. No face, no mouth, no human limbs, no robe, no skull.
Style/medium: dense indie pixel art concept illustration, crisp hard-edged pixels, 1px dark outlines, limited low-saturation palette, high pixel density comparable to AK-xolotl: Together only as a density reference; do not copy its characters, UI, weapons, gore, top-down composition, or bright comedic tone.
Composition/framing: horizontal lineup, A and B small in the foreground left/center, Balancer towering behind/right as a large silhouette; side-view metroidvania readability, character sizes clearly different. Keep negative space around silhouettes. No labels.
Lighting/mood: melancholic, quiet, lived-in, faint cyan awareness light from A, muted pale gold and cyan from the Balancer, soft but pixelated rim lighting.
Color palette: old slate blue-gray, moss olive, weathered brass, dark charcoal, muted rust cloth, faint cyan, pale gold. Avoid saturated greens and bright cartoon colors.
Materials/textures: weathered stone plates, worn brass joints, moss stains, root-fiber cloth, cracked ceramic/stone, hand-made folk machinery.
Constraints: no anti-aliasing, no soft blur, no bloom, no smooth vector look, no text, no watermark, no weapons, no blood, no gore, no modern sci-fi chrome, no human architecture, no humanoid A or Balancer. The result should feel like production concept art for Miji rather than a game screenshot.
```

- **검토 메모**:
  - A: 상단 스위치 / 단안 시안 렌즈 / 저상 롤러 실루엣 유지 ✓
  - B: 잎귀 / 여행 랩 / 작은 방랑자 인상 유지 ✓. 단, 이마 쪽에 금빛 균열처럼 보이는 장식이 남아 **초기 B 설정과는 약간 충돌** — 최종 채택 시 수정 후보.
  - 균형자: 기존 `character_03_balancer`의 저울 구조와 pale geometric core가 강하게 유지됨 ✓. 다만 여전히 "장치" 성격이 강하므로, 이후 3장 적대 캐릭터화 시 관측기/기동부/큰 그릇 실루엣을 더 추가 탐색할 것.
  - 전체: 픽셀 밀도 상향 기준과 어두운 lived-in 톤이 잘 맞음 ✓ / 텍스트·UI·무기·고어 없음 ✓
- **파일 경로**: `main_cast_lineup_pixel_art.png` — 파일 미보존
- **결과**: 생성 완료. 캐스트 대표 컨셉 후보로 보존.

---

### 2026-08-20 (7차) — A/B 단독 아트워크 + 균형자 5방향 탐색

- **목적**: 사용자 요청 "각각 따로 해주고, 균형자는 내가 레퍼런스로 잡은 캐릭터를 기반으로 예시로 5개 각기 다르게" 반영. A/B는 단독 캐릭터 아트워크로 분리하고, 균형자는 2026-08-19 비주얼 레퍼런스(구조=무거운 봉인 그릇/관측기, 질감=불쾌한 유물·개조된 몸)를 직접 복제 없이 5방향으로 변주.
- **경로**: Codex built-in `image_gen` 직접 생성 후 프로젝트 폴더로 복사.
- **공통 생성 지시**:

```text
Dense indie pixel art concept illustration for Miji, crisp hard-edged pixels, 1px dark outline, limited low-saturation palette, dark charcoal background, side-view metroidvania production art. Preserve Miji's melancholic, lived-in, folk-machine tone. No text, no UI, no watermark, no gore, no modern sci-fi chrome, no smooth vector look, no anti-aliasing, no blur, no bloom.
```

- **개별 생성 지시 / 결과**:

| 파일 | 방향 | 검토 메모 |
|---|---|---|
| `character_A_standalone_pixel_art.png` | A 단독. 상단 토글 스위치, 단안 시안 렌즈, 저상 롤러, 낡은 석재-금속 생활 기계 | 기존 A 정체성 유지 ✓ / 시시한 물건 느낌 유지 ✓ / 실제 스프라이트 기준으로 단순화 가능 |
| `character_B_standalone_pixel_art.png` | B 단독. 잎귀, 큰 눈, 러스트 여행 랩, 뿌리섬유 가방. 초기 B라 금빛 균열 금지 | 초기 B에 더 적합 ✓ / 이전 라인업보다 이마 균열 문제 적음 ✓ |
| `balancer_variant_01_pressure_vessel.png` | 무거운 압력 그릇형. 둥근 봉인 몸통 + 관측 포트 + 사후 부착 기동부 | 추격 보스 실루엣 최강. 단 전투 로봇처럼 읽힐 위험 있음 |
| `balancer_variant_02_reliquary_vessel.png` | 수직 성물/받이형. 긴 봉인 용기 + 작은 관측 렌즈 + 접힌 계량 팔 | 기존 추상 균형자와 연결성 최고. 장치/성물 느낌이 강해 캐릭터성 보강 필요 |
| `balancer_variant_03_crawler_collector.png` | 저상 회수기형. 납작한 석판 껍질 + 기어가는 브레이스 다리 + 하부 관측 렌즈 | 5개 중 실루엣 차별 최강. 못 이기는 추격 위협으로 유력 |
| `balancer_variant_04_observation_helmet.png` | 관측 헬멧형. 큰 렌즈/포트가 얼굴을 대체하고, 몸은 봉인 그릇 | "A를 보지 않는 관측기" 시각화 최고. 레퍼런스 구조감과 가장 잘 맞음 |
| `balancer_variant_05_asym_usurper.png` | 비대칭 찬탈자형. 빌린 부품, 한쪽 집게/한쪽 저울, 오프셋 렌즈 | "남이 준 몸을 전용"한 느낌 최고. 최종 보스/대립 인격 후보로 강함 |

- **파일 경로**:
  - `docs/art/assets/generated-concepts/characters/A/character_A_standalone_pixel_art.png`
  - `docs/art/assets/generated-concepts/characters/B/character_B_standalone_pixel_art.png`
  - `docs/art/assets/generated-concepts/characters/balancer/variants/selected/balancer_variant_01_pressure_vessel.png`
  - `balancer_variant_02_reliquary_vessel.png` — 비채택, 파일 미보존
  - `balancer_variant_03_crawler_collector.png` — 비채택, 파일 미보존
  - `docs/art/assets/generated-concepts/characters/balancer/variants/rejected/balancer_variant_04_observation_helmet.png`
  - `docs/art/assets/generated-concepts/characters/balancer/variants/selected/balancer_variant_05_asym_usurper.png`
- **사용자 선별**: ② 수직 받이형 / ③ 저상 회수기형은 "너무 구림"으로 탈락. ① 압력 그릇형 / ⑤ 비대칭 찬탈자형이 "그나마 좀 맘에듦"으로 유력 후보. ④ 관측 헬멧형은 Codex 자체평과 달리 사용자 유력 후보에 들지 않음.
- **결과**: 생성 완료. 다음 라운드는 ①의 육중한 덩어리감·압력 그릇 실루엣 + ⑤의 비대칭 찬탈자성·빌린 부품 느낌을 섞는 방향이 우선.

---

### 2026-08-20 (8차) — 균형자 1+5 혼합 5방향: 색감·웨더링 강화

- **목적**: 사용자 요청 "1번과 5번 기준으로 5개 더, 색감 다양하게, 웨더링, Titanfall 로봇 레퍼런스 추가" 반영. 7차 유력 후보인 ① 압력 그릇형과 ⑤ 비대칭 찬탈자형을 합성하되, 팔레트와 표면 손상을 5방향으로 분리.
- **레퍼런스 운용**: Titanfall 로봇은 직접 복제하지 않고 **산업용 거대 메카의 중량감, 관절 가독성, 피스톤/유압 브레이스, 실용 기계 질감**만 참고. 무기, 콕핏, 데칼, 특정 실루엣은 배제.
- **공통 생성 지시**:

```text
Combine the massive round pressure-vessel body from balancer_variant_01 with the asymmetrical borrowed parts, off-center observation lens, actuator clamp, and usurper feeling from balancer_variant_05. Incorporate only broad, non-identifiable Titanfall-style qualities: utilitarian industrial mech weight, readable mechanical joints, exposed pistons, hydraulic braces, believable heavy machine articulation. Do not copy any specific Titanfall robot, cockpit, weapon, markings, silhouette, or decals. Dense indie pixel art, 1px dark outline, low-saturation palette, no face, no mouth, no weapon, no text, no gore, no modern sci-fi chrome.
```

- **개별 생성 지시 / 결과**:

| 파일 | 색감/방향 | 웨더링 | 검토 메모 |
|---|---|---|---|
| `balancer_variant_06_verdigris_pressure_usurper.png` | 녹청 황동 + 암청 슬레이트 + 시안 렌즈 | 녹청, 이끼 낀 관절, 긁힌 렌즈링, 금 간 세라믹 | 1+5 혼합 정확도 좋음. 낡은 압력 그릇감 강함 |
| `balancer_variant_07_red_rust_iron.png` | 적갈 녹철 + 검은 철 + 둔한 황동 | 붉은 녹, 그을음, 벗겨진 도장, 오일 얼룩 | 색감 차별 큼. 산업 메카 관절감이 가장 강함 |
| `balancer_variant_08_cold_blue_ceramic.png` | 냉청 세라믹 + 남청 슬레이트 + 은회 금속 | 청색 유약 칩, 거미줄 균열, 차가운 마모 | 차가운 회계 기계 느낌 좋음. Miji 톤보다는 조금 차갑다 |
| `balancer_variant_09_moss_brass_folk_machine.png` | 이끼 올리브 + 황동 + 따뜻한 백자 | 이끼, 흙먼지, 닳은 손잡이, 오래된 오일 | 세계관의 소박한 민속 기계 톤과 가장 잘 붙음 |
| `balancer_variant_10_blackstone_violet_ceramic.png` | 흑석 + 백자 + 낮은 자홍 균열 | 흑석 깨짐, 자홍 얼룩 균열, 어두운 황동 마모 | 이질감·찬탈자성 강함. 자홍이 과해지면 톤 이탈 주의 |

- **파일 경로**:
  - `docs/art/assets/generated-concepts/characters/balancer/variants/selected/balancer_variant_06_verdigris_pressure_usurper.png`
  - `docs/art/assets/generated-concepts/characters/balancer/variants/rejected/balancer_variant_07_red_rust_iron.png`
  - `docs/art/assets/generated-concepts/characters/balancer/variants/selected/balancer_variant_08_cold_blue_ceramic.png`
  - `docs/art/assets/generated-concepts/characters/balancer/variants/selected/balancer_variant_09_moss_brass_folk_machine.png`
  - `docs/art/assets/generated-concepts/characters/balancer/variants/rejected/balancer_variant_10_blackstone_violet_ceramic.png`
- **사용자 선별**: ⑥ 녹청 압력 그릇 / ⑧ 냉청 세라믹 / ⑨ 이끼 황동 민속 기계가 마음에 듦. ⑦ 적갈 녹철 / ⑩ 흑석·자홍 세라믹은 폐기.
- **결과**: 생성 완료. 다음 라운드는 ⑥/⑧/⑨의 색감·웨더링을 유지하고, 실루엣만 다양화한다. ⑦/⑩은 파일 보존하되 비채택 처리.

---

### 2026-08-20 (9차) — 균형자 6/8/9 색감 기반 실루엣 탐색 5종

- **목적**: 사용자 요청 "6,8,9 맘에들고 나머지 두 안은 폐기. 해당 색감 바탕으로 여러가지 모양, 레퍼런스는 유지" 반영. ⑥/⑧/⑨의 색감·웨더링은 유지하고, 균형자의 몸통/관절/계량 장치 실루엣을 5방향으로 재탐색.
- **레퍼런스 운용**: 기존 균형자 축(봉인 그릇, 관측 렌즈, 비대칭 빌린 부품, 저울/계량 장치) + Titanfall식 산업 메카 질감(피스톤, 유압 브레이스, 중량감) 유지. 직접 복제 요소(무기, 콕핏, 데칼, 특정 로봇 실루엣)는 금지.
- **개별 생성 지시 / 결과**:

| 파일 | 실루엣 방향 | 검토 메모 |
|---|---|---|
| `balancer_variant_11_hunched_carrier_frame.png` | 구부정 운반 프레임. 둥근 받이를 등짐처럼 매단 구조 | 6의 색감과 산업 프레임이 잘 붙음. 추격 시 느리고 부담스러운 인상 |
| `balancer_variant_12_tripod_measuring_vessel.png` | 삼각대 계량기. 받이를 세 다리 위에 올린 측량 장치형 | 8 색감과 가장 잘 맞음. 다리 실루엣이 신선하나 장치성 강함 |
| `balancer_variant_13_shield_backed_vessel.png` | 방패등 압력 그릇. 커다란 등판/금고형 실루엣 | 압박감 좋음. 9의 이끼·황동 톤이 세계관과 잘 맞음 |
| `balancer_variant_14_recovery_crane_vessel.png` | 한팔 회수 크레인. 긴 집게/체인/계량추 중심 | 형태 변화 최강. 단 크레인 팔이 너무 장비처럼 읽히면 캐릭터성 보강 필요 |
| `balancer_variant_15_walking_amphora_vessel.png` | 보행 항아리 받이. 세로 용기 + 산업 브레이스 | "받이" 설정 시각화 좋음. 2차 탈락안과 달리 보행성·관절감 보강됨 |

- **파일 경로**:
  - `docs/art/assets/generated-concepts/characters/balancer/variants/rejected/balancer_variant_11_hunched_carrier_frame.png`
  - `docs/art/assets/generated-concepts/characters/balancer/variants/rejected/balancer_variant_12_tripod_measuring_vessel.png`
  - `docs/art/assets/generated-concepts/characters/balancer/variants/rejected/balancer_variant_13_shield_backed_vessel.png`
  - `docs/art/assets/generated-concepts/characters/balancer/variants/rejected/balancer_variant_14_recovery_crane_vessel.png`
  - `docs/art/assets/generated-concepts/characters/balancer/variants/rejected/balancer_variant_15_walking_amphora_vessel.png`
- **사용자 피드백**: "일단 다 별로." 원인: ⑴ `균형자`라는 이름을 너무 문자 그대로 저울·균형추·계량 장치로 시각화해 이름 설명처럼 보임 ⑵ 집게/클램프가 반복적으로 부적절한 실루엣으로 읽힘.
- **폐기/금지 규칙**:
  - 균형자 디자인에서 **저울 접시, 균형추, 매달린 계량추, 긴 체인 장식, 삼각대 계량기, scale/balance literal motif** 금지.
  - "균형자"는 시각 모티프가 아니라 역할명이다. 비주얼은 **장부/회수/받이/관측기/남이 붙인 몸**으로 풀 것.
  - 집게는 벌어진 양갈래 클로 금지. 필요하면 **둔탁한 패드형 조작 팔, 넓은 압착판, 짧은 유압 브레이스, 손목 없는 공구 암**으로 대체.
  - 다음 프롬프트에는 `no scales, no hanging weights, no dangling chains, no pincer claws, no crab claws, no split clamp silhouette, no phallic silhouette`를 명시.
- **결과**: 9차 5종은 비채택. 이후 라운드는 ⑥/⑧/⑨의 색감·웨더링만 유지하고, 저울/추/집게 모티프를 제거한 산업용 봉인 그릇·관측기·유압 보행체 방향으로 재탐색.

---

### 2026-08-20 (6차) — ★ Tier 2 가동: A·B 프로 재생성 + 첫 배경 「엮은 둥지 × 유물 색감」

- **구독**: Tier 2 「Pixel Artisan」 활성 (월 5,000생성). 이번 라운드 소모 125생성, 잔여 4,875.
- **A 재생성** (`create_image_pro` 32px, 후보 64장 → 1번 채택): 기존 A를 레퍼런스로 identity 고정. 후보 다수가 스위치 누락(16~63)·렌즈 누락(5~15) — **레퍼런스를 걸어도 정체성 요소는 후보 선별로 지켜야 함**. 애니메이션 4벌 재파생(idle/run/jump), jump는 렌즈에 글리프가 생겨 1회 재롤(「plain glowing oval, no patterns inside the lens」 명시로 해결)
- **B 재생성** (후보 64장 → 16번 채택): 정자세·새챌·랩·글린트·잎꼬리 전부 가시. 이마 무균열 유지
- **배경** (`create_image_pro` 688x384, 레퍼런스 2장 라벨링): `architecture_2c_woven_nest`에서 **구조**(둥지 포드·매듭 로프·가지 기둥), `tech_level_02_ruin_relics`에서 **색감만**(이끼 녹·슬레이트·랜턴 온광·안개 — ㉯은 기술수준으로는 탈락이지만 팔레트 차용은 설정 무충돌). 2안 생성 → **v1 채택**(v2는 원본의 보라 황혼이 남아 색감 지시 미달). 원경의 시안 빛이 시각 언어(시안=인식)와 호응
- **Unity 적용**: `Assets/Art/Backgrounds/BG_WovenNest.png` (PPU 32, 1.5배 스케일, sortingOrder -100) + `ParallaxLayer.cs` 신설(카메라 추적 대비, 현재 카메라 고정이라 대기). A 프레임 17장 제자리 교체 → 클립 자동 반영. 비채택 v2는 `docs/art/assets/generated-concepts/maps/woven-nest/bg_woven_nest_alt_dusk.png` 보존
- **검증**: 플레이 실측 — 새 A·B 주행/점프/추종, 배경 톤 정합 확인. 테스트 무영향(물리 불변)
- **결과**: 승인 대기 (미커밋)

---

### 2026-08-20 (7차) — 사용자 피드백 반영: B 3/4 뷰 전환 + A 렌즈 상시 밝음

- **피드백**: ① B가 순측면이라 평면적 → A처럼 대각(3/4)으로 입체감 ② A 아이들의 렌즈 명멸 제거, 상시 밝게.
- **B 3/4 재생성** (pro 32px, 후보 64장 → 9번 채택): 6차 B(16번)를 identity 레퍼런스로 걸고 뷰만 3/4로 지시. 먼쪽 잎귀·어깨가 뒤로 보이고 양눈 가시 — 큰 눈이 캐논 「wide attentive dark eyes」에 더 근접. 소품 든 후보 다수(우산·책·무기·새) — **pro는 32px에서도 프롬프트에 없는 소품을 자주 붙인다. 정자세 지정 필수**
- **A 아이들 재생성** (animate_image 1생성): 「렌즈 상시 최대 밝기, 명멸 금지」 명시. 5프레임 중 2번만 어둡게 나와 **0·1·3·4 채택**으로 해결(재롤 불필요). 명멸 컨셉은 폐기 — 렌즈 밝기 변화는 이제 서사 이벤트(꺼짐)에만 예약
- **소모**: 21생성 (누적 146 / 5,000, 잔여 4,854)
- **파일**: `B_idle_0.png`·`A_idle_0~3.png` 제자리 교체. 인게임 확인 완료
- **결과**: 승인 대기 (미커밋)

---

### 2026-08-20 (8차) — B 명암 보강 + Idle/Walk 애니메이션 (사용자 피드백)

- **피드백**: ① B 애니메이션 부재(정지 스프라이트 + 코드 들썩임뿐) ② 명암이 얕아 장면에서 따로 논다.
- **순서가 핵심**: 명암을 베이스에서 먼저 확정(릴라이트) → 그 베이스로 애니메이션 파생 → 전 프레임이 명암을 상속.
- **명암 보강** (`relight_b34.ps1`, 생성 0회): 올리브 램프 양자화 + 엣지 릴라이트 + **몸 하단 38% 내부 한 단 어둡게**(부피 음영, 신규 기법) + 글린트·아웃라인 보호. A의 명암 단과 정합
- **애니메이션** (animate_image 각 1생성): Idle 4f(숨쉬기+깜빡임 — 얼굴이 사라진 프레임 1개 제외하고 0·1·3·4 채택) / Walk 8f(스텝·귀 바운스·새챌 흔들림, 1~8 채택)
- **Unity**: `B_Idle/B_Walk.anim` + `B_Animator.controller`(Speed 0.25 경계) 신설, Companion_B에 Animator 연결. `CompanionFollower` 확장 — Animator가 있으면 Speed 파라미터를 구동하고 **코드 들썩임은 자동 비활성**(이중 바운스 방지, 없으면 기존 동작 유지)
- **소모**: 2생성 (누적 148 / 5,000). 컴파일 0에러, 플레이 실측 — 추종 중 Walk 재생·정지 시 Idle 복귀 확인
- **결과**: 승인 대기 (미커밋)

---

### 2026-08-20 (9차) — B 품질 재생성: 후처리 폐기, 생성 단계 해결 (사용자 피드백)

- **피드백**: ① 빛 처리가 붉음 ② A·B 퀄리티 격차 ③ 얼굴이 넓은 단색 면 ④ 걸을 때 눈 감음.
- **원인 자인**: 전부 8차 후처리(릴라이트 양자화)의 부작용 — 데운 러스트 램프가 빛을 붉히고, 양자화가 얼굴을 단색으로 뭉갬. **교훈: Tier 2 예산에서는 후처리로 때우지 말고 생성 단계에서 품질을 뽑는다.** 릴라이트 파이프라인은 「톤 보정」용으로 강등(품질 보강용 아님)
- **재생성** (pro 32px 후보 64장 → **29번 채택**): identity 레퍼런스 = 기존 B34, **style_image = A 스프라이트(shading·detail만 복사, 팔레트 제외)** + 「차가운 조명, 붉은 하이라이트 금지, 얼굴 다톤(볼·눈썹 음영)」 명시. 29번은 정수리 상단광 밴드가 A의 판때기 명암 문법과 일치
- **애니메이션 재파생** (각 1생성): Idle 4f / Walk 8f — 「모든 프레임 눈 뜸·글린트 유지」 강제. 전 프레임 결손 없음, 선별 그대로 사용(idle 0~3 / walk 1~8)
- **소모**: 22생성 (누적 170 / 5,000)
- **결과**: 인게임 확인 완료, 승인 대기 (미커밋)

---

### 2026-08-20 (10차) — 균형자 09 리파인 테스트: sprite-gen 시도 + image_gen fallback

- **목적**: 사용자 요청 "맘에들었던 사진중 하나를 sprite-gen으로 다듬어 전/후 차이 보기" 반영. 사용자 선호안 ⑥/⑧/⑨ 중 미지 톤과 가장 잘 붙는 ⑨ 이끼 황동 민속 기계를 기준 이미지로 선택.
- **sprite-gen 시도**:
  - 기준 이미지: `docs/art/assets/generated-concepts/characters/balancer/variants/selected/balancer_variant_09_moss_brass_folk_machine.png`
  - 실행 경로: `tools/sprite-gen/.venv/Scripts/python.exe -m sprite_gen.cli gen --provider codex ...`
  - 결과: 현재 `codex` CLI가 `Not logged in` 상태라 provider 세션이 정상 완료되지 못함. `grok` CLI도 로컬에서 발견되지 않음.
  - 판정: 이번 결과물은 엄밀한 sprite-gen 산출물이 아니라, 동일 레퍼런스와 동일 리파인 지시를 Codex built-in `image_gen`으로 실행한 fallback 샘플이다.
- **리파인 지시 핵심**:
  - 유지: 이끼 올리브, 웨더링 황동, 암청 슬레이트, 세라믹 압력 그릇, 오프센터 시안 관측 렌즈, 낡은 민속 기계 톤.
  - 제거: 저울 접시, 균형추, 매달린 체인, 계량봉, 삼각 계량 장식, 양갈래 집게/클로, 부적절한 실루엣.
  - 대체: 둔탁한 패드형 조작 팔, 짧은 유압 브레이스, 봉인 그릇, 관측기, 유압 보행체, 긁힘/녹청/금 간 세라믹/오일 얼룩.
- **검토 메모**:
  - 장점: 원본의 "이름 직역" 요소였던 체인·추·저울 구조가 거의 사라지고, 더 단순한 봉인 그릇 + 유압 다리 실루엣으로 정리됨. 집게 실루엣도 제거되어 부적절한 오독 위험이 크게 줄었다.
  - 단점: 원본보다 비대칭 찬탈자성은 약해지고, 덩어리가 너무 정돈되어 개체성/불쾌감이 줄었다. 다음 라운드는 이 구조를 유지하되 관측 렌즈 위치와 붙인 부품의 어색함을 다시 올리는 쪽이 좋다.
- **파일 경로**:
  - 전: `docs/art/assets/generated-concepts/characters/balancer/variants/selected/balancer_variant_09_moss_brass_folk_machine.png`
  - 후: `docs/art/assets/generated-concepts/characters/balancer/variants/selected/balancer_variant_09_refined_imagegen_fallback.png`
- **결과**: 비교용 fallback 샘플 생성 완료. sprite-gen 정식 재실행 조건 = `codex login` 완료 또는 사용 가능한 `grok` provider 설치/로그인.

---

### 2026-08-20 (10차) — 픽셀 수술 2건 + B 수면 애니 + A·B 정면 뷰(3D 턴) + 변형 4종 보존

- **수정 ①** A 아이들 롤러 찌그러짐: 프레임 1~3에서 롤러가 뭉개짐 → **롤러 밴드(y26~31)를 프레임 0으로 고정**(섀시 바닥 윤곽 y25는 프레임별 유지). 생성 0회
- **수정 ②** B 아이들 글린트 벌어짐: 2번 프레임에서 양눈 글린트가 세로 2px → 아래쪽 중복 픽셀 2개((26,11)·(19,12))만 눈 암색으로 복원. ⚠️ 처음에 눈 박스를 통째로 고정했다가 **머리가 프레임마다 1px씩 움직여 유령 눈 발생 → 롤백.** 교훈: 움직이는 부위에 프레임 고정 금지, 결함 픽셀만 수술
- **B 수면 애니** (1생성): 사용자 지정 `B34_13`(누운 포즈) 베이스 → 6프레임 호흡·귀 트임. `B_Sleep.anim`(4fps) + 컨트롤러 Sleep 상태(IsAsleep). `CompanionFollower`가 **7초 정지 시 잠들고 움직이면 즉시 깸**
- **3D 턴** (pro 2회 = 40생성): 좌우 반전 대신 **측면→정면→반전측면** 3박자. A 정면 = AF_16(이끼 새시), B 정면 = BF_9(새싹 튜프트). 애니메이터 상태 증설 없이 **LateUpdate에서 0.09초 정면 스프라이트 덮어쓰기**(Animator가 먼저 쓰고 우리가 덮음). PlayerAnimator·CompanionFollower 공통 패턴
- **변형 보존**: `docs/art/assets/b-variants/` — B34_13(눕기)·27(지도)·30(잎우산)·42(눈감고 웃음, 재회 신 후보)
- **보류**: 점프 애니메이션 개선은 사용자 지시대로 마지막에 제작 후 컨펌
- **소모**: 41생성 (누적 211 / 5,000). PlayMode 8/8 유지
- ⚠️ **운영 노트**: 플레이 모드 중 uloop dynamic-code가 간헐적으로 행 — 씬 필드 조작은 에디트 모드에서 하고 플레이는 입력·스크린샷만

---

### 2026-08-20 (11차) — 턴 3프레임화 + 정면 아트 정합 수정 (사용자 피드백)

- **피드백**: ① 턴이 1프레임이니 180도를 3프레임으로 ② 정면 샷이 기존 캐릭터 아트와 다름.
- **정면 이탈 원인**: 10차 정면 생성은 identity 레퍼런스만 걸고 **팔레트를 스타일 복사하지 않았다** → A는 몸통이 밝은 청회색·렌즈 과대·이끼 소실, B는 튜프트가 커져 4귀로 읽힘. 이번엔 `style_image_url`에 **측면 스프라이트를 걸고 팔레트까지 복사**
- **생성** (pro 4회 = 80생성): A 정면/45°, B 정면/45°. ⚠️ **A 정면 세트(AF2)는 실패** — 팔레트 복사 + 「대칭」 제약이 겹쳐 몸통이 검게 뭉개지고 부속 소실. **대신 45° 세트(AQ) 안에 거의 정면인 후보가 있어 그걸로 대체**(AQ_44). 재생성 없이 해결
- **채택**: A 45°=AQ_12 / A 정면=AQ_44 / B 45°=BQ2_63 / B 정면=BF2_14
- ★ **실루엣 치수 정합을 선별 기준에 추가**: 처음 고른 B 45°(BQ2_24)는 21x26으로 측면(24x29)보다 3px 작아 **회전 중 크기가 출렁였다.** 전 후보 bbox를 실측해 폭·발높이가 맞는 것으로 교체(BQ2_63 22x29), 45°는 발 위치가 1px 높아 **아래로 1px 시프트**. 최종 A 26/26/26 · B 24/22/24, 발 위치 전부 31
- **코드**: `Gameplay/View/TurnView.cs` 신설 — 회전 시간을 3등분해 **45°(출발) → 정면 → 45°(도착, flip)**. A·B 공용 정적 헬퍼이며 Animator 상태를 늘리지 않는다(LateUpdate 덮어쓰기). turnDuration 0.14s
- **소모**: 80생성 (누적 291 / 5,000). 컴파일 0에러, **EditMode 19/19 + PlayMode 8/8**
- **보류**: 점프 애니메이션(사용자 지시 — 마지막에 제작 후 컨펌)
