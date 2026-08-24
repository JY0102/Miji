// B(무리비) Walk 애니메이션 빌더 — 수제 시안 (2026-08-21 기각: PixelLab 초안 채택)
// 채택본은 docs/art/assets/b-current/anim/walk/B_walk_0~7.png (animate_image, 1생성).
// 이 스크립트는 수제 공정의 기록/대안으로 남긴다. 출력이 채택본을 덮지 않도록
// 파일명에 handmade 를 붙인다.
//
// idle 과 같은 공정: AI 생성이 아니라 poses/B01_idle.png 의 픽셀을 레이어로 갈라
// 좌표만 옮긴다 (크레딧 0). 근거: build-idle.mjs, docs/art/assets/b-current/README.md 5절.
//
//   node tools/b-anim/build-walk.mjs
//
// idle 과 다른 점: 다리를 앞/뒤 두 레이어로 더 가른다.
// B01 실측 — 두 다리는 x축으로 완전히 분리된 연결요소다 (사이 x34~40 투명):
//   뒷다리 x21~33 (발 y59~60), 앞다리 x41~48 (발 y57~58, 3/4 뷰라 접지선이 2px 높다)
//
// 보행 4프레임 (접지-스윙-접지-스윙):
//   f0: 앞다리 +2 앞으로 딛고 / 뒷다리 -2 뒤로 뻗음 — 보폭 최대, 몸 낮음
//   f1: 두 다리 중립, 뒷다리 들려서(1px) 앞으로 스윙 중 — 몸 원위치
//   f2: f0 의 좌우 반대
//   f3: f1 의 좌우 반대
// 몸통 바운스는 idle 과 같은 제약을 따른다: 레이어는 아래로만, head >= torso.

import fs from 'fs';
import path from 'path';
import { fileURLToPath } from 'url';
import { decodePNG, encodePNG, encodeGIF, clone, blank, getPx, setPx, scaleNN } from './imglib.mjs';

const ROOT = path.resolve(path.dirname(fileURLToPath(import.meta.url)), '../..');
const SRC      = path.join(ROOT, 'docs/art/assets/b-current/poses/B01_idle.png');
const OUT_DOCS = path.join(ROOT, 'docs/art/assets/b-current/anim/walk');
// 컨펌용 미리보기는 컨펌 대기함으로 간다 (docs/art/confirm/README.md, 2026-08-21 규칙).
const OUT_CONFIRM = path.join(ROOT, 'docs/art/confirm');
// ⚠️ Unity 반입은 컨펌 후 — build-idle 과 달리 OUT_UNITY 에 아직 안 쓴다.

// ── 레이어 경계 (build-idle.mjs 와 동일 실측) ───────────────────────────────
const EAR_BOTTOM   = 11;
const HEAD_BOTTOM  = 33;
const TORSO_BOTTOM = 54;
const TAIL_BOTTOM  = 44;
const TAIL_X       = 20;
const LEG_TOP      = 55;
const LEG_SPLIT_X  = 38;  // x < 38 뒷다리, x >= 38 앞다리 (실제 갭은 x34~40)

// 다리 압축 규칙은 idle 과 동일 — 55=56, 57=58, 59=60 중복 행만 뺀다.
const LEG_ROWS = { 0: [55, 56, 57, 58, 59, 60], 1: [55, 57, 58, 59, 60] };

// ── 시안 — 프레임별 이동량(px) ─────────────────────────────────────────────
// leg: [dx, lift] — lift 1 = 발이 땅에서 1px 뜬 채 통째로 이동(압축 없음),
//                   lift 0 = 접지. 발은 바닥선 고정, torso 만큼 압축.
const F = {
  head:  [1, 0, 1, 0],           // 걸음마다 머리가 까딱인다 (head >= torso 불변식)
  ear:   [0, -1, 0, -1],         // 몸이 뜰 때 귀가 1px 뒤로 처진다
  torso: [1, 0, 1, 0],           // 접지 프레임에서 몸이 낮다
  tail:  [0, 1, 0, 1],           // 몸이 뜰 때 꼬리가 처진다 (팔로스루)
  front: [[2, 0], [0, 0], [-2, 0], [0, 1]],   // 앞다리 (오른쪽, 진행 방향)
  hind:  [[-2, 0], [0, 1], [2, 0], [0, 0]],   // 뒷다리
};
const DELAY_CS = 10;  // 100ms/프레임 = 0.4초 보행 사이클

const base = decodePNG(SRC);
const W = base.width, H = base.height;
const opaque = (img, x, y) => getPx(img, x, y)[3] >= 128;

function buildFrame(src, i) {
  const headDy = F.head[i], earDx = F.ear[i], torsoDy = F.torso[i], tailDy = F.tail[i];
  if (headDy < torsoDy) throw new Error(`목에 구멍이 뚫린다: head ${headDy} < torso ${torsoDy}`);
  const out = blank(W, H);
  // 1) 다리 — 앞/뒤 독립. 들린 다리 윗행(y54)은 나중에 그리는 몸통이 덮는다
  for (const leg of ['hind', 'front']) {
    const [dx, lift] = F[leg][i];
    const inLeg = x => (leg === 'hind') === (x < LEG_SPLIT_X);
    if (lift) {
      for (let y = LEG_TOP; y < H; y++)
        for (let x = 0; x < W; x++)
          if (opaque(src, x, y) && inLeg(x)) setPx(out, x + dx, y - 1, getPx(src, x, y));
    } else {
      LEG_ROWS[torsoDy].forEach((sy, k) => {
        const ty = LEG_TOP + torsoDy + k;
        for (let x = 0; x < W; x++)
          if (opaque(src, x, sy) && inLeg(x)) setPx(out, x + dx, ty, getPx(src, x, sy));
      });
    }
  }
  // 2) 몸통 + 꼬리
  for (let y = TORSO_BOTTOM; y > HEAD_BOTTOM; y--)
    for (let x = 0; x < W; x++) {
      if (!opaque(src, x, y)) continue;
      const dy = torsoDy + ((y <= TAIL_BOTTOM && x <= TAIL_X) ? tailDy : 0);
      setPx(out, x, y + dy, getPx(src, x, y));
    }
  // 3) 머리 + 귀
  for (let y = HEAD_BOTTOM; y >= 0; y--)
    for (let x = 0; x < W; x++) {
      if (!opaque(src, x, y)) continue;
      setPx(out, x + (y <= EAR_BOTTOM ? earDx : 0), y + headDy, getPx(src, x, y));
    }
  return out;
}

const frames = [0, 1, 2, 3].map(i => buildFrame(base, i));

fs.mkdirSync(OUT_DOCS, { recursive: true });
frames.forEach((f, i) => encodePNG(path.join(OUT_DOCS, `B_walk_handmade_${i}.png`), f));

const CYCLES = 3;
const seq = [], delays = [];
for (let c = 0; c < CYCLES; c++) for (const f of frames) { seq.push(f); delays.push(DELAY_CS); }
fs.mkdirSync(OUT_CONFIRM, { recursive: true });
encodeGIF(path.join(OUT_CONFIRM, 'B_walk_handmade_preview.gif'), seq.map(f => scaleNN(f, 4)), delays);

console.log(`B_walk_0~3.png + confirm/B_walk_handmade_preview.gif (${seq.length}프레임, ${DELAY_CS * 4 / 100}초/사이클)`);
