# B(무리비) 현행 컨셉 자산

**최종 정리:** 2026-08-21 — 모든 포즈를 `poses/` 한 폴더, `B01`~`B15` 번호로 통일

```
b-current/
├── README.md
├── B_artwork_all.png     ← 15포즈 합본 아트워크
└── poses/                ← 64x64 PNG, 배경 투명
```

---

## 1. 포즈 15종

### 확정 4종

| 파일 | 용도 | 출처 |
|---|---|---|
| `B01_idle.png` | **Idle** | 구 split 판본 (**눈 수작업 확대·수정본** — 3절) |
| `B02_sit.png` | **앉기** | split-ears-unified |
| `B03_sit_sad.png` | **앉기 + 슬픔** | split-ears-unified |
| `B04_question.png` | **물음표(의문)** | split-ears-unified — 머리 위 `?` 를 그대로 쓴다 |

### 보류 11종 — 용도 미확정

| 파일 | 임시 용도 | 출처 |
|---|---|---|
| `B05_run.png` | 뛰기 | split-ears-unified |
| `B06_laugh.png` | 웃기 | split-ears-unified |
| `B07_handover.png` | 건네주기 | split-ears-unified |
| `B08_eat.png` | 밥 | split-ears-unified |
| `B09_greet.png` | 인사 | split-ears-unified |
| `B10_sit_front.png` | 정면 앉기 | higgsfield 생성본 |
| `B11_peer.png` | 들여다보기 | higgsfield 생성본 — ⚠ 없던 회색 선반이 함께 그려져 있다 |
| `B12_explain.png` | 설명하기 | higgsfield 생성본 — 입모양 수작업 수정본 |
| `B13_startled.png` | 놀람 | higgsfield 생성본 — ⚠ 눈 규격 위반(흰자위) |
| `B14_asleep.png` | 잠 | higgsfield 생성본 |
| `B15_glum.png` | 지침 | higgsfield 생성본 — ⚠ 귀가 잎귀가 아니라 강아지 귀로 그려졌다 |

> **출처가 두 갈래다.** `B01`~`B09` 는 손으로 그린 컨셉이고, `B10`~`B15` 는 higgsfield 생성 후 64x64로 내린 것이다.
> 화풍이 미세하게 다르므로 인게임에 넣기 전에 한 번 더 통일해야 한다.
> **`fall` 에 배정된 그림은 아직 없다** (B04가 question으로 배정되면서 비었다).

---

## 2. 귀 판본 주의 ★

`B01_idle` 만 **구 split 판본**이라 귀가 갈라져 있고, 나머지는 잎귀 통일본이다.
Idle이 가장 많이 보이는 프레임이므로, 통일할지 이대로 갈지는 결정이 필요하다.

---

## 3. 눈 규격 ★

**검은 구체 + 작은 크림 하이라이트 + 아래쪽 갈색 크레센트.** 기준 파일은 `B02_sit.png`.
크기까지 규격이다 — 눈 하나가 대략 **9x10 px**.

- `B01_idle` 은 원래 눈이 **탁한 갈색 덩어리 7x7** 이었다. 2026-08-21에 `B02_sit` 의 눈 블록을 그대로 이식해 색·구조·크기를 맞췄다.
- **흰자위 + 검은 동공 분리는 위반이다.** 생성 모델은 「놀람·크게 뜬 눈」 지시를 받으면 반드시 이렇게 그린다(`B13_startled` 이 그 상태로 남아 있다) → **표정 극단값은 생성 대신 수작업 수정**한다.

---

## 4. 이후 생성 프롬프트 규칙

`docs/art/style-guide/STYLE_GUIDE.md` 「생성 프롬프트 금지 사항」 참조. 요약:

- **평면적으로 보이는 각도를 요청하지 않는다** — 3/4 앵글이 기본
- 기본 스프라이트에 **금빛 균열을 넣지 않는다** (`CHARACTER_B.md` 4절)
