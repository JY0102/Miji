// B(무리비) Idle 애니메이션 빌더 — 2026-08-21 확정 (시안 F)
//
// AI 생성이 아니다. poses/B01_idle.png 의 픽셀을 레이어로 갈라 좌표만 옮긴다.
// 근거: docs/art/assets/b-current/README.md 3절(눈 규격)·5절, docs/art/ART_LOG.md 5차.
//
//   node tools/b-anim/build-idle.mjs
//
// 의존성 0 — Node 내장 zlib 만 쓴다. PNG 디코드/인코드와 GIF89a 인코더는 imglib.mjs 에 있다.

import fs from 'fs';
import path from 'path';
import { fileURLToPath } from 'url';
import { decodePNG, encodePNG, encodeGIF, clone, blank, getPx, setPx, scaleNN } from './imglib.mjs';

const ROOT = path.resolve(path.dirname(fileURLToPath(import.meta.url)), '../..');
const SRC       = path.join(ROOT, 'docs/art/assets/b-current/poses/B01_idle.png');
const OUT_DOCS  = path.join(ROOT, 'docs/art/assets/b-current/anim/idle');
const OUT_UNITY = path.join(ROOT, 'src/Miji/Assets/Art/Characters/B/Sprites');

// ── 레이어 경계 (B01_idle.png 실측) ─────────────────────────────────────────
// 이 스프라이트는 y축으로 깨끗하게 갈린다. 그래서 이 공정이 성립한다.
const EAR_BOTTOM   = 11;  // y 0~11  : 귀 끝만 (머리 정수리는 y=12부터)
const HEAD_BOTTOM  = 33;  // y 0~33  : 귀 + 머리 전부 — 목도리·몸통이 한 픽셀도 안 섞인다
const TORSO_BOTTOM = 54;  // y 34~54 : 목도리 + 몸통 + 꼬리 + 팔
const TAIL_BOTTOM  = 44;  // 꼬리 끝 = y<=44 ∩ x<=20
const TAIL_X       = 20;

// ★ 다리(y 55~60)는 「덮지 말고 압축한다」.
// 덮으면 y=55~56 — 두 다리 사이가 갈라진 유일한 줄 — 이 지워져 다리가 통짜가 된다.
// 이 구간은 55=56, 57=58, 59=60 으로 같은 줄이 두 번씩이라 중복 줄만 빼면 실루엣이 안 깨진다.
// 발(59~60)과 갈라짐(55)은 어떤 압축량에서도 남는다.
const LEG_ROWS = { 0: [55,56,57,58,59,60], 1: [55,57,58,59,60], 2: [55,57,59,60] };

// ── 확정 시안 F — 프레임별 이동량(px) ──────────────────────────────────────
// ★ 모든 값은 0 이상이어야 한다. 레이어를 위로 올리면 머리 밑변(33)과 몸통 윗변(34)
//   사이에 1px 구멍이 뚫린다. 같은 이유로 head[i] >= torso[i] 도 불변식이다
//   (= 머리가 늦게 따라오는 lag 체인은 만들 수 없다. 머리를 더 크게 움직여 리드시킨다).
const F = {
  head:  [0, 1, 2, 1],   // 머리 + 귀      (y 0~33)
  ear:   [0, 0, -1, -1], // 귀 끝 x축      (y 0~11)
  torso: [0, 1, 1, 0],   // 몸통 + 꼬리    (y 34~54), 다리는 이만큼 압축된다
  tail:  [0, 0, 1, 1],   // 꼬리 끝 추가   (y<=44 ∩ x<=20)
};

// 클립 구성: 호흡 3사이클, 3번째 사이클의 f1·f2 를 깜빡임으로 교체 (루프 1.54초)
const CYCLES = 3, BLINK_CYCLE = 2, BLINK_FRAMES = [1, 2];
const DELAY_CS = 14, BLINK_DELAY_CS = 7;

const base = decodePNG(SRC);
const W = base.width, H = base.height;
const opaque = (img, x, y) => getPx(img, x, y)[3] >= 128;

// ── 깜빡임 ────────────────────────────────────────────────────────────────
function darkComponent(img, sx, sy) {
  const isDark = (x, y) => opaque(img, x, y) && Math.max(...getPx(img, x, y).slice(0, 3)) < 45;
  const seen = new Set([sy * W + sx]), out = [], st = [[sx, sy]];
  while (st.length) {
    const [cx, cy] = st.pop(); out.push([cx, cy]);
    for (const [dx, dy] of [[1,0],[-1,0],[0,1],[0,-1]]) {
      const nx = cx + dx, ny = cy + dy;
      if (nx < 0 || ny < 0 || nx >= W || ny >= H || seen.has(ny * W + nx) || !isDark(nx, ny)) continue;
      seen.add(ny * W + nx); st.push([nx, ny]);
    }
  }
  return out;
}
// 눈 검은 덩어리를 바로 위 머리 초록으로 메우고 눈꺼풀 선을 얹는다.
// ⚠️ 선은 평평해야 한다 — 위로 휜 아치는 B06_laugh 의 웃는 눈이라 깜빡일 때마다 B가 웃는다.
function closeEye(img, seedX, seedY) {
  const px = darkComponent(img, seedX, seedY);
  const xs = px.map(p => p[0]), ys = px.map(p => p[1]);
  const x0 = Math.min(...xs), x1 = Math.max(...xs), y0 = Math.min(...ys), y1 = Math.max(...ys);
  for (let x = x0; x <= x1; x++) {
    let fill = null;
    for (let y = y0 - 1; y >= y0 - 4 && y >= 0; y--) {
      const p = getPx(img, x, y);
      if (p[3] >= 128 && Math.max(p[0], p[1], p[2]) >= 60) { fill = p; break; }
    }
    if (!fill) fill = [105, 122, 71, 255];
    for (let y = y0; y <= y1; y++) if (opaque(img, x, y)) setPx(img, x, y, fill);
  }
  const ly = y0 + Math.round((y1 - y0) * 0.55);
  const LID = [21, 29, 6, 255], LID_END = [38, 53, 23, 255];
  for (let x = x0; x <= x1; x++) {
    const edge = (x === x0 || x === x1);
    setPx(img, x, ly, edge ? LID_END : LID);
    if (!edge) setPx(img, x, ly + 1, LID);
  }
}
function blinkBase() { const b = clone(base); closeEye(b, 35, 25); closeEye(b, 50, 25); return b; }

// ── 프레임 조립 ───────────────────────────────────────────────────────────
function buildFrame(src, headDy, earDx, torsoDy, tailDy) {
  if (headDy < torsoDy) throw new Error(`목에 구멍이 뚫린다: head ${headDy} < torso ${torsoDy}`);
  const out = blank(W, H);
  // 1) 다리 — 정강이 중복 행을 빼서 torsoDy 만큼 압축. 발은 바닥에 고정
  LEG_ROWS[torsoDy].forEach((sy, i) => {
    const ty = 55 + torsoDy + i;
    for (let x = 0; x < W; x++) if (opaque(src, x, sy)) setPx(out, x, ty, getPx(src, x, sy));
  });
  // 2) 몸통 + 꼬리 — 아래 행부터 그려 꼬리 끝이 위에 얹히게 한다
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

// ── 출력 ──────────────────────────────────────────────────────────────────
const bl = blinkBase();
const normal = F.head.map((_, i) => buildFrame(base, F.head[i], F.ear[i], F.torso[i], F.tail[i]));
const blink  = F.head.map((_, i) => buildFrame(bl,   F.head[i], F.ear[i], F.torso[i], F.tail[i]));

fs.mkdirSync(OUT_DOCS, { recursive: true });
const written = [];
// 픽셀이 이미 같으면 안 쓴다 — 인코더가 달라 바이트만 바뀌면 Unity 가 괜히 재임포트하고
// git 에도 의미 없는 diff 가 남는다 (B_idle_0 은 원본 그대로라 매번 여기 걸린다).
function samePixels(p, img) {
  if (!fs.existsSync(p)) return false;
  let cur;
  try { cur = decodePNG(p); } catch { return false; }
  if (cur.width !== img.width || cur.height !== img.height) return false;
  for (let y = 0; y < img.height; y++) for (let x = 0; x < img.width; x++) {
    const a = getPx(cur, x, y), b = getPx(img, x, y);
    const at = a[3] < 128, bt = b[3] < 128;
    if (at !== bt || (!at && (a[0] !== b[0] || a[1] !== b[1] || a[2] !== b[2]))) return false;
  }
  return true;
}
function put(name, img) {
  for (const dir of [OUT_DOCS, OUT_UNITY]) {
    if (!fs.existsSync(dir)) continue;
    const p = path.join(dir, name);
    if (samePixels(p, img)) continue;
    encodePNG(p, img);
  }
  written.push(name);
}
normal.forEach((f, i) => put(`B_idle_${i}.png`, f));
BLINK_FRAMES.forEach(i => put(`B_idle_blink_${i}.png`, blink[i]));

const seq = [], delays = [];
for (let c = 0; c < CYCLES; c++) for (let i = 0; i < normal.length; i++) {
  const isBlink = (c === BLINK_CYCLE && BLINK_FRAMES.includes(i));
  seq.push(isBlink ? blink[i] : normal[i]);
  delays.push(isBlink ? BLINK_DELAY_CS : DELAY_CS);
}
encodeGIF(path.join(OUT_DOCS, 'B_idle_preview.gif'), seq.map(f => scaleNN(f, 4)), delays);

// 불변식 검사 — f0 는 원본과 픽셀이 같아야 한다
let diff = 0;
for (let y = 0; y < H; y++) for (let x = 0; x < W; x++) {
  const a = getPx(normal[0], x, y), b = getPx(base, x, y);
  const at = a[3] < 128, bt = b[3] < 128;
  if (at !== bt || (!at && (a[0] !== b[0] || a[1] !== b[1] || a[2] !== b[2]))) diff++;
}
if (diff) throw new Error(`f0 가 원본과 ${diff}px 다르다`);

console.log(`${written.join(', ')} + B_idle_preview.gif (${seq.length}프레임, ${delays.reduce((a,b)=>a+b,0)/100}초 루프)`);
console.log('f0 == B01_idle.png ✓');
