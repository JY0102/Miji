import { createRequire } from "node:module";
import { writeFile } from "node:fs/promises";
import path from "node:path";

const require = createRequire(import.meta.url);
const moduleRoot = process.env.MIJI_NODE_MODULES;

if (!moduleRoot) {
  throw new Error("MIJI_NODE_MODULES is required.");
}

const sharp = require(path.join(moduleRoot, "sharp"));

const [
  inputPath,
  nativePath,
  previewPath,
  widthArg,
  heightArg,
  coloursArg,
  scaleArg,
  paletteReferencePath,
  backgroundMode,
] = process.argv.slice(2);

if (!inputPath || !nativePath || !previewPath || !widthArg || !heightArg) {
  throw new Error(
    "Usage: node pixelize-imagegen-asset-hd.mjs <input> <native> <preview> <width> <height> [colours=64] [previewScale=2] [paletteReference]",
  );
}

const nativeWidth = Number(widthArg);
const nativeHeight = Number(heightArg);
const maxColours = Number(coloursArg ?? 64);
const previewScale = Number(scaleArg ?? 2);

for (const [name, value] of [
  ["width", nativeWidth],
  ["height", nativeHeight],
  ["colours", maxColours],
  ["previewScale", previewScale],
]) {
  if (!Number.isInteger(value) || value <= 0) {
    throw new Error(`${name} must be a positive integer.`);
  }
}

const metadata = await sharp(inputPath).metadata();
const sourceWidth = metadata.width ?? 0;
const sourceHeight = metadata.height ?? 0;
if (!sourceWidth || !sourceHeight) {
  throw new Error("Input dimensions are unavailable.");
}

const targetRatio = nativeWidth / nativeHeight;
const sourceRatio = sourceWidth / sourceHeight;
let extract;
if (sourceRatio > targetRatio) {
  const width = Math.max(1, Math.floor(sourceHeight * targetRatio));
  extract = {
    left: Math.floor((sourceWidth - width) / 2),
    top: 0,
    width,
    height: sourceHeight,
  };
} else {
  const height = Math.max(1, Math.floor(sourceWidth / targetRatio));
  extract = {
    left: 0,
    top: Math.floor((sourceHeight - height) / 2),
    width: sourceWidth,
    height,
  };
}

const logicalSource = await sharp(inputPath)
  .extract(extract)
  .resize(nativeWidth, nativeHeight, { kernel: "nearest" })
  .ensureAlpha()
  .raw()
  .toBuffer({ resolveWithObject: true });

if (backgroundMode === "strip-neutral") {
  for (let offset = 0; offset < logicalSource.data.length; offset += 4) {
    const r = logicalSource.data[offset];
    const g = logicalSource.data[offset + 1];
    const b = logicalSource.data[offset + 2];
    const minimum = Math.min(r, g, b);
    const maximum = Math.max(r, g, b);
    // ImageGen sometimes bakes a white/gray checkerboard plus a faint neutral
    // contact shadow into a nominally transparent sheet. Remove both while
    // preserving the saturated green and amber pixels used by the assets.
    if (minimum >= 160 && maximum - minimum <= 48) {
      logicalSource.data[offset] = 0;
      logicalSource.data[offset + 1] = 0;
      logicalSource.data[offset + 2] = 0;
      logicalSource.data[offset + 3] = 0;
    }
  }
}

let selected;
let selectedMode;

if (paletteReferencePath) {
  const paletteRaw = await sharp(paletteReferencePath)
    .ensureAlpha()
    .raw()
    .toBuffer({ resolveWithObject: true });
  const palette = [];
  const paletteKeys = new Set();
  for (let offset = 0; offset < paletteRaw.data.length; offset += 4) {
    if (paletteRaw.data[offset + 3] === 0) continue;
    const r = paletteRaw.data[offset];
    const g = paletteRaw.data[offset + 1];
    const b = paletteRaw.data[offset + 2];
    const key = (r << 16) | (g << 8) | b;
    if (!paletteKeys.has(key)) {
      paletteKeys.add(key);
      palette.push([r, g, b]);
    }
  }
  if (!palette.length || palette.length > maxColours) {
    throw new Error(`Palette reference has ${palette.length} opaque colours; maximum is ${maxColours}.`);
  }

  const mapped = Buffer.alloc(nativeWidth * nativeHeight * 4);
  const nearestCache = new Map();
  for (let sourceOffset = 0, targetOffset = 0; sourceOffset < logicalSource.data.length; sourceOffset += 4, targetOffset += 4) {
    const alpha = logicalSource.data[sourceOffset + 3] >= 128 ? 255 : 0;
    if (alpha === 0) {
      mapped[targetOffset + 3] = 0;
      continue;
    }
    const r = logicalSource.data[sourceOffset];
    const g = logicalSource.data[sourceOffset + 1];
    const b = logicalSource.data[sourceOffset + 2];
    const key = (r << 16) | (g << 8) | b;
    let nearest = nearestCache.get(key);
    if (!nearest) {
      let bestDistance = Number.POSITIVE_INFINITY;
      for (const candidate of palette) {
        const dr = r - candidate[0];
        const dg = g - candidate[1];
        const db = b - candidate[2];
        const distance = (dr * dr * 30) + (dg * dg * 59) + (db * db * 11);
        if (distance < bestDistance) {
          bestDistance = distance;
          nearest = candidate;
        }
      }
      nearestCache.set(key, nearest);
    }
    mapped[targetOffset] = nearest[0];
    mapped[targetOffset + 1] = nearest[1];
    mapped[targetOffset + 2] = nearest[2];
    mapped[targetOffset + 3] = 255;
  }
  selected = await sharp(mapped, {
    raw: { width: nativeWidth, height: nativeHeight, channels: 4 },
  }).png().toBuffer();
  selectedMode = `reference-palette-${palette.length}`;
} else {
  async function quantize(quality) {
    const buffer = await sharp(logicalSource.data, { raw: logicalSource.info })
      .png({ palette: true, colours: maxColours, quality, dither: 0 })
      .toBuffer();
    const raw = await sharp(buffer)
      .ensureAlpha()
      .raw()
      .toBuffer({ resolveWithObject: true });
    const colours = new Set();
    for (let offset = 0; offset < raw.data.length; offset += 4) {
      if (raw.data[offset + 3] === 0) continue;
      colours.add(
        (raw.data[offset] << 16) |
        (raw.data[offset + 1] << 8) |
        raw.data[offset + 2],
      );
    }
    return { buffer, colourCount: colours.size };
  }

  let low = 1;
  let high = 100;
  let candidate;
  while (low <= high) {
    const quality = Math.floor((low + high) / 2);
    const attempt = await quantize(quality);
    if (attempt.colourCount <= maxColours) {
      candidate = attempt;
      low = quality + 1;
    } else {
      high = quality - 1;
    }
  }
  if (!candidate) {
    throw new Error(`Could not quantize to ${maxColours} opaque colours.`);
  }
  selected = candidate.buffer;
  selectedMode = `quantized-${candidate.colourCount}`;
}

await writeFile(nativePath, selected);

await sharp(nativePath)
  .resize(nativeWidth * previewScale, nativeHeight * previewScale, { kernel: "nearest" })
  .png()
  .toFile(previewPath);

console.log(`${path.basename(nativePath)} ${nativeWidth}x${nativeHeight} mode=${selectedMode}`);
console.log(`${path.basename(previewPath)} ${nativeWidth * previewScale}x${nativeHeight * previewScale}`);
