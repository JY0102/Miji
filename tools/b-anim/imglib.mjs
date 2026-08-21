import fs from 'fs';
import zlib from 'zlib';

// ---------- PNG decode (8-bit, non-interlaced) ----------
export function decodePNG(path) {
  const buf = fs.readFileSync(path);
  let off = 8;
  let w = 0, h = 0, bd = 0, ct = 0, inter = 0;
  const idat = [];
  let plte = null, trns = null;
  while (off < buf.length) {
    const len = buf.readUInt32BE(off);
    const type = buf.toString('ascii', off + 4, off + 8);
    const data = buf.subarray(off + 8, off + 8 + len);
    if (type === 'IHDR') {
      w = data.readUInt32BE(0); h = data.readUInt32BE(4);
      bd = data[8]; ct = data[9]; inter = data[12];
    } else if (type === 'PLTE') plte = Buffer.from(data);
    else if (type === 'tRNS') trns = Buffer.from(data);
    else if (type === 'IDAT') idat.push(Buffer.from(data));
    else if (type === 'IEND') break;
    off += 12 + len;
  }
  if (bd !== 8) throw new Error('unsupported bit depth ' + bd);
  if (inter) throw new Error('interlaced unsupported');
  const raw = zlib.inflateSync(Buffer.concat(idat));
  const ch = ct === 6 ? 4 : ct === 2 ? 3 : ct === 3 ? 1 : ct === 4 ? 2 : 1;
  const stride = w * ch;
  const out = Buffer.alloc(h * stride);
  let p = 0;
  for (let y = 0; y < h; y++) {
    const f = raw[p++];
    const line = raw.subarray(p, p + stride); p += stride;
    const cur = out.subarray(y * stride, (y + 1) * stride);
    const prev = y > 0 ? out.subarray((y - 1) * stride, y * stride) : null;
    for (let i = 0; i < stride; i++) {
      const a = i >= ch ? cur[i - ch] : 0;
      const b = prev ? prev[i] : 0;
      const c = (prev && i >= ch) ? prev[i - ch] : 0;
      let v = line[i];
      if (f === 1) v = (v + a) & 255;
      else if (f === 2) v = (v + b) & 255;
      else if (f === 3) v = (v + ((a + b) >> 1)) & 255;
      else if (f === 4) {
        const pp = a + b - c, pa = Math.abs(pp - a), pb = Math.abs(pp - b), pc = Math.abs(pp - c);
        const pr = (pa <= pb && pa <= pc) ? a : (pb <= pc ? b : c);
        v = (v + pr) & 255;
      } else if (f !== 0) throw new Error('bad filter ' + f);
      cur[i] = v;
    }
  }
  const rgba = Buffer.alloc(w * h * 4);
  for (let i = 0; i < w * h; i++) {
    let r, g, b, a = 255;
    if (ct === 6) { r = out[i * 4]; g = out[i * 4 + 1]; b = out[i * 4 + 2]; a = out[i * 4 + 3]; }
    else if (ct === 2) { r = out[i * 3]; g = out[i * 3 + 1]; b = out[i * 3 + 2]; }
    else if (ct === 0) { r = g = b = out[i]; }
    else if (ct === 4) { r = g = b = out[i * 2]; a = out[i * 2 + 1]; }
    else { const ix = out[i]; r = plte[ix * 3]; g = plte[ix * 3 + 1]; b = plte[ix * 3 + 2]; if (trns && ix < trns.length) a = trns[ix]; }
    rgba[i * 4] = r; rgba[i * 4 + 1] = g; rgba[i * 4 + 2] = b; rgba[i * 4 + 3] = a;
  }
  return { width: w, height: h, data: rgba };
}

// ---------- PNG encode (RGBA) ----------
function crc32(buf) {
  let t = crc32.t;
  if (!t) {
    t = crc32.t = [];
    for (let n = 0; n < 256; n++) { let c = n; for (let k = 0; k < 8; k++) c = c & 1 ? 0xEDB88320 ^ (c >>> 1) : c >>> 1; t[n] = c >>> 0; }
  }
  let crc = 0xFFFFFFFF;
  for (let i = 0; i < buf.length; i++) crc = t[(crc ^ buf[i]) & 0xFF] ^ (crc >>> 8);
  return (crc ^ 0xFFFFFFFF) >>> 0;
}
function chunk(type, data) {
  const len = Buffer.alloc(4); len.writeUInt32BE(data.length);
  const td = Buffer.concat([Buffer.from(type, 'ascii'), data]);
  const crc = Buffer.alloc(4); crc.writeUInt32BE(crc32(td));
  return Buffer.concat([len, td, crc]);
}
export function encodePNG(path, img) {
  const { width: w, height: h, data } = img;
  const raw = Buffer.alloc(h * (w * 4 + 1));
  for (let y = 0; y < h; y++) { raw[y * (w * 4 + 1)] = 0; data.copy(raw, y * (w * 4 + 1) + 1, y * w * 4, (y + 1) * w * 4); }
  const ihdr = Buffer.alloc(13);
  ihdr.writeUInt32BE(w, 0); ihdr.writeUInt32BE(h, 4); ihdr[8] = 8; ihdr[9] = 6;
  fs.writeFileSync(path, Buffer.concat([
    Buffer.from([0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A]),
    chunk('IHDR', ihdr),
    chunk('IDAT', zlib.deflateSync(raw, { level: 9 })),
    chunk('IEND', Buffer.alloc(0)),
  ]));
}

// ---------- helpers ----------
export function blank(w, h) { return { width: w, height: h, data: Buffer.alloc(w * h * 4) }; }
export function clone(img) { return { width: img.width, height: img.height, data: Buffer.from(img.data) }; }
export function getPx(img, x, y) {
  if (x < 0 || y < 0 || x >= img.width || y >= img.height) return [0, 0, 0, 0];
  const i = (y * img.width + x) * 4; return [img.data[i], img.data[i + 1], img.data[i + 2], img.data[i + 3]];
}
export function setPx(img, x, y, p) {
  if (x < 0 || y < 0 || x >= img.width || y >= img.height) return;
  const i = (y * img.width + x) * 4; img.data[i] = p[0]; img.data[i + 1] = p[1]; img.data[i + 2] = p[2]; img.data[i + 3] = p[3];
}
export function scaleNN(img, f) {
  const o = blank(img.width * f, img.height * f);
  for (let y = 0; y < o.height; y++) for (let x = 0; x < o.width; x++) setPx(o, x, y, getPx(img, (x / f) | 0, (y / f) | 0));
  return o;
}

// ---------- GIF89a encode ----------
function lzwEncode(indices, minCodeSize) {
  const clear = 1 << minCodeSize, eoi = clear + 1;
  let codeSize = minCodeSize + 1, next = eoi + 1;
  let dict = new Map();
  const out = []; let cur = 0, curBits = 0;
  const emit = (code) => {
    cur |= code << curBits; curBits += codeSize;
    while (curBits >= 8) { out.push(cur & 255); cur >>>= 8; curBits -= 8; }
  };
  emit(clear);
  let prefix = indices[0];
  for (let i = 1; i < indices.length; i++) {
    const k = indices[i];
    const key = prefix * 4096 + k;
    if (dict.has(key)) { prefix = dict.get(key); continue; }
    emit(prefix);
    dict.set(key, next++);
    if (next > (1 << codeSize)) {
      if (codeSize < 12) codeSize++;
      else { emit(clear); dict = new Map(); codeSize = minCodeSize + 1; next = eoi + 1; }
    }
    prefix = k;
  }
  emit(prefix); emit(eoi);
  if (curBits > 0) out.push(cur & 255);
  return Buffer.from(out);
}
export function encodeGIF(path, frames, delayCs, alphaThreshold = 128) {
  const w = frames[0].width, h = frames[0].height;
  const map = new Map(); const pal = [[0, 0, 0]];
  const idxFrames = frames.map(f => {
    const idx = new Uint8Array(w * h);
    for (let i = 0; i < w * h; i++) {
      if (f.data[i * 4 + 3] < alphaThreshold) { idx[i] = 0; continue; }
      const r = f.data[i * 4], g = f.data[i * 4 + 1], b = f.data[i * 4 + 2];
      const key = (r << 16) | (g << 8) | b;
      let v = map.get(key);
      if (v === undefined) { v = pal.length; if (v > 255) throw new Error('>255 colors'); pal.push([r, g, b]); map.set(key, v); }
      idx[i] = v;
    }
    return idx;
  });
  let bits = 1; while ((1 << bits) < pal.length) bits++;
  if (bits < 2) bits = 2;
  const palSize = 1 << bits;
  const gct = Buffer.alloc(palSize * 3);
  pal.forEach((c, i) => { gct[i * 3] = c[0]; gct[i * 3 + 1] = c[1]; gct[i * 3 + 2] = c[2]; });

  const parts = [];
  parts.push(Buffer.from('GIF89a', 'ascii'));
  const lsd = Buffer.alloc(7);
  lsd.writeUInt16LE(w, 0); lsd.writeUInt16LE(h, 2);
  lsd[4] = 0x80 | ((bits - 1) << 4) | (bits - 1);
  parts.push(lsd, gct);
  parts.push(Buffer.from([0x21, 0xFF, 0x0B]), Buffer.from('NETSCAPE2.0', 'ascii'), Buffer.from([0x03, 0x01, 0x00, 0x00, 0x00]));
  idxFrames.forEach((idx, fi) => {
    const d = Array.isArray(delayCs) ? delayCs[fi] : delayCs;
    const gce = Buffer.alloc(8);
    gce[0] = 0x21; gce[1] = 0xF9; gce[2] = 0x04;
    gce[3] = (2 << 2) | 0x01;
    gce.writeUInt16LE(d, 4); gce[6] = 0; gce[7] = 0;
    parts.push(gce);
    const ib = Buffer.alloc(10);
    ib[0] = 0x2C; ib.writeUInt16LE(0, 1); ib.writeUInt16LE(0, 3);
    ib.writeUInt16LE(w, 5); ib.writeUInt16LE(h, 7); ib[9] = 0;
    parts.push(ib);
    const mcs = Math.max(2, bits);
    parts.push(Buffer.from([mcs]));
    const lzw = lzwEncode(idx, mcs);
    for (let i = 0; i < lzw.length; i += 255) {
      const sub = lzw.subarray(i, Math.min(i + 255, lzw.length));
      parts.push(Buffer.from([sub.length]), sub);
    }
    parts.push(Buffer.from([0x00]));
  });
  parts.push(Buffer.from([0x3B]));
  fs.writeFileSync(path, Buffer.concat(parts));
}
