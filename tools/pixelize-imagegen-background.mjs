import { createRequire } from "node:module";
import { writeFile } from "node:fs/promises";
import path from "node:path";

const require = createRequire(import.meta.url);
const moduleRoot = process.env.MIJI_NODE_MODULES;

if (!moduleRoot) {
  throw new Error("MIJI_NODE_MODULES is required.");
}

const sharp = require(path.join(moduleRoot, "sharp"));

const [inputPath, nativePath, previewPath, paletteReferencePath] = process.argv.slice(2);
if (!inputPath || !nativePath || !previewPath) {
  throw new Error("Usage: node pixelize-imagegen-background.mjs <input> <native> <preview>");
}

const nativeWidth = 416;
const nativeHeight = 234;
const previewScale = 4;
const maxColours = 32;
const cropWidth = nativeWidth * previewScale;
const cropHeight = nativeHeight * previewScale;

const metadata = await sharp(inputPath).metadata();
if ((metadata.width ?? 0) < cropWidth || (metadata.height ?? 0) < cropHeight) {
  throw new Error(`Input is too small: ${metadata.width}x${metadata.height}`);
}

const left = Math.floor(((metadata.width ?? cropWidth) - cropWidth) / 2);
const top = Math.floor(((metadata.height ?? cropHeight) - cropHeight) / 2);

const logicalSource = await sharp(inputPath)
  .extract({ left, top, width: cropWidth, height: cropHeight })
  .resize(nativeWidth, nativeHeight, { kernel: "nearest" })
  .png()
  .toBuffer();

async function quantize(quality) {
  const buffer = await sharp(logicalSource)
    .png({ palette: true, colours: maxColours, quality, dither: 0 })
    .toBuffer();
  const { data } = await sharp(buffer)
    .ensureAlpha()
    .raw()
    .toBuffer({ resolveWithObject: true });
  const colours = new Set();
  for (let offset = 0; offset < data.length; offset += 4) {
    colours.add(data.readUInt32BE(offset));
  }
  return { buffer, colourCount: colours.size };
}

let selected = null;
let selectedQuality = null;

if (paletteReferencePath) {
  const { data: paletteData, info: paletteInfo } = await sharp(paletteReferencePath)
    .removeAlpha()
    .raw()
    .toBuffer({ resolveWithObject: true });
  const palette = [];
  const paletteKeys = new Set();
  for (let offset = 0; offset < paletteData.length; offset += paletteInfo.channels) {
    const r = paletteData[offset];
    const g = paletteData[offset + 1];
    const b = paletteData[offset + 2];
    const key = (r << 16) | (g << 8) | b;
    if (!paletteKeys.has(key)) {
      paletteKeys.add(key);
      palette.push([r, g, b]);
    }
  }
  if (palette.length > maxColours) {
    throw new Error(`Palette reference has ${palette.length} colours; maximum is ${maxColours}.`);
  }

  const { data: sourceData, info: sourceInfo } = await sharp(logicalSource)
    .removeAlpha()
    .raw()
    .toBuffer({ resolveWithObject: true });
  const mapped = Buffer.alloc(nativeWidth * nativeHeight * 3);
  const nearestCache = new Map();
  for (let sourceOffset = 0, targetOffset = 0; sourceOffset < sourceData.length; sourceOffset += sourceInfo.channels, targetOffset += 3) {
    const r = sourceData[sourceOffset];
    const g = sourceData[sourceOffset + 1];
    const b = sourceData[sourceOffset + 2];
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
  }
  const buffer = await sharp(mapped, {
    raw: { width: nativeWidth, height: nativeHeight, channels: 3 }
  }).png().toBuffer();
  selected = { buffer, colourCount: palette.length };
  selectedQuality = "reference-palette";
} else {
  let low = 1;
  let high = 100;
  while (low <= high) {
    const quality = Math.floor((low + high) / 2);
    const candidate = await quantize(quality);
    if (candidate.colourCount <= maxColours) {
      selected = candidate;
      selectedQuality = quality;
      low = quality + 1;
    } else {
      high = quality - 1;
    }
  }
}

if (!selected) {
  throw new Error(`Could not quantize to ${maxColours} colours.`);
}

await writeFile(nativePath, selected.buffer);

await sharp(nativePath)
  .resize(cropWidth, cropHeight, { kernel: "nearest" })
  .png()
  .toFile(previewPath);

console.log(`${path.basename(nativePath)} ${nativeWidth}x${nativeHeight} colours=${selected.colourCount} quality=${selectedQuality}`);
console.log(`${path.basename(previewPath)} ${cropWidth}x${cropHeight}`);
