import { createRequire } from "node:module";
import path from "node:path";

const require = createRequire(import.meta.url);
const moduleRoot = process.env.MIJI_NODE_MODULES;

if (!moduleRoot) {
  throw new Error("MIJI_NODE_MODULES is required.");
}

const sharp = require(path.join(moduleRoot, "sharp"));
const [inputPath, palettePath] = process.argv.slice(2);

if (!inputPath) {
  throw new Error("Usage: node inspect-png-asset.mjs <input> [palette-reference]");
}

const source = await sharp(inputPath)
  .ensureAlpha()
  .raw()
  .toBuffer({ resolveWithObject: true });

const alpha = { transparent: 0, semi: 0, opaque: 0 };
const colours = new Map();
for (let offset = 0; offset < source.data.length; offset += 4) {
  const a = source.data[offset + 3];
  if (a === 0) {
    alpha.transparent += 1;
    continue;
  }
  if (a === 255) alpha.opaque += 1;
  else alpha.semi += 1;
  const key = (source.data[offset] << 16) | (source.data[offset + 1] << 8) | source.data[offset + 2];
  colours.set(key, (colours.get(key) ?? 0) + 1);
}

let outsidePalette = null;
if (palettePath) {
  const paletteRaw = await sharp(palettePath).ensureAlpha().raw().toBuffer();
  const palette = new Set();
  for (let offset = 0; offset < paletteRaw.length; offset += 4) {
    if (paletteRaw[offset + 3] === 0) continue;
    palette.add((paletteRaw[offset] << 16) | (paletteRaw[offset + 1] << 8) | paletteRaw[offset + 2]);
  }
  outsidePalette = [...colours.keys()].filter((key) => !palette.has(key)).length;
}

const topColours = [...colours.entries()]
  .sort((left, right) => right[1] - left[1])
  .slice(0, 12)
  .map(([key, count]) => ({
    hex: `#${key.toString(16).padStart(6, "0")}`,
    count,
  }));

console.log(JSON.stringify({
  file: path.basename(inputPath),
  width: source.info.width,
  height: source.info.height,
  opaqueColours: colours.size,
  alpha,
  outsidePalette,
  topColours,
}, null, 2));
