# 컨펌 대기함

**컨펌이 필요한 아트는 전부 이 폴더에 넣는다** (2026-08-21 사용자 지시). 여기만 열어 보면 된다.

- 파일명: `[대상]_[방식]_preview.*` — 예: `B_walk_pixellab_preview.gif`
- 원본 프레임은 원래 위치(`assets/…`)에 그대로 두고, **판단용 미리보기만** 여기 온다
- 컨펌되면 여기서 지우고, 결과는 원래 위치에서 확정 + `ART_LOG.md`에 기록
- 기각되면 여기서 지우고 원본 프레임도 정리

## 현재 대기 중

*(없음 — A 64px 애니 4종 모두 결정 완료. 마스터는 `assets/a-64px/anim-*/`)*

## 처리 이력

- 2026-08-21 — B 걷기: **PixelLab 초안 채택** (수제 시안 기각) → `assets/b-current/anim/walk/B_walk_0~7.png` 확정, Unity 반입
- 2026-08-22 — A 64px 업스케일 + idle: **v8(바퀴 개선) + seedA idle 채택** → `A/Sprites/A_idle_0~3.png` 64px/PPU64 반입, `A_Idle.anim` GUID 유지. 기각: 1차 v1~v3(이끼 소실)·절차판(바퀴 딸려올라감)·AI f4(렌즈 슬릿)
- 2026-08-24 — A 64px 애니 4종 **결정 확정** (Unity 반입은 보류 — 별도 세션). 마스터를 `assets/a-64px/anim-*/`로 정렬:
  - **run** = seedA(9091) 6프레임 → `anim-run/A_run_0~5.png`
  - **jump** = PixelLab seed8484, idx1·2·3 → `anim-jump/A_jump_0~2.png`
  - **turn** = 몸 전체 회전(PixelLab), idx2=turn45 / idx6=front → `anim-turn/A_turn_frames.png`(+preview)
  - **fall** = PixelLab seed1440 tilt(직립→앞으로 다이빙) idx0~5 → `anim-fall/A_fall_0~5.png`. **하강 거리에 비례해 최대 기울기까지** 가는 거리 구동 방식으로 반입 예정. idx6~8은 텀블링이라 미채택(`anim-fall/overshoot/` 보관). 수제 NN 회전본(픽셀 짤림)은 폐기
