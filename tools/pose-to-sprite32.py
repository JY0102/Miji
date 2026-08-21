"""컨셉 포즈(64x64)를 인게임 스프라이트(32x32)로 내린다.

왜 따로 만들었나
----------------
평균(bilinear/lanczos) 축소는 픽셀아트의 평면 색을 섞어 **1px 검은 아웃라인을 회색으로
뭉갠다.** 실측해 보면 아웃라인이 통째로 사라져 실루엣이 물러진다.
그래서 두 단계를 쓴다.

1. **최빈색(mode) 블록 샘플링** — 목표 픽셀 하나에 대응하는 원본 2x2 블록에서 가장 많이
   나온 색을 그대로 집는다. 동률이면 블록 평균에 가장 가까운 색(= 몸 색)을 고른다.
   동률 때 「가장 어두운 색」을 고르면 아웃라인이 안쪽으로 번져 캐릭터가 지저분해진다.
   불투명 픽셀이 4칸 중 1칸 이하면 투명 — 실루엣이 부풀지 않게.
2. **아웃라인 복원** — 축소하면 아웃라인이 군데군데 끊긴다. 실루엣 경계 픽셀을 전부
   팔레트의 가장 어두운 색으로 되돌린다. A와 같은 「전둘레 1px 검은선」 규격이 된다.

마지막으로 **발 위치와 가로 중심을 규격에 맞춘다**(A 26px 폭 / 발 y=31 기준).
프레임마다 실루엣 치수가 어긋나면 애니메이션에서 캐릭터가 출렁인다.

사용
----
    tools/sprite-gen/.venv/Scripts/python.exe tools/pose-to-sprite32.py \
        docs/art/assets/b-current/poses/B01_idle.png out/B_idle_0.png

    옵션: --size 32 --feet 31 --no-outline --compare out/compare.png

참고: 2026-08-21 B 인게임 32px 반입 작업에서 뽑아냈다. `ART_LOG.md` 2026-08-21 항목.
⚠ 원본 한 변은 목표의 정수배여야 한다(64 -> 32 = 2배).
"""

import argparse
from collections import Counter
from pathlib import Path

import numpy as np
from PIL import Image


def block_downscale(rgba: np.ndarray, factor: int) -> np.ndarray:
    """최빈색 블록 샘플링. 동률은 블록 평균에 가장 가까운 색으로 깬다."""
    h, w = rgba.shape[:2]
    out = np.zeros((h // factor, w // factor, 4), np.uint8)
    need = max(2, (factor * factor) // 4 + 1)  # 이보다 불투명이 적으면 투명으로 둔다
    for y in range(out.shape[0]):
        for x in range(out.shape[1]):
            block = rgba[y * factor:(y + 1) * factor, x * factor:(x + 1) * factor].reshape(-1, 4)
            opaque = [tuple(int(v) for v in p) for p in block if p[3] > 128]
            if len(opaque) < need:
                continue
            ranked = Counter(opaque).most_common()
            top = ranked[0][1]
            tied = [c for c, n in ranked if n == top]
            if len(tied) == 1:
                out[y, x] = tied[0]
            else:
                mean = np.mean([[c[0], c[1], c[2]] for c in opaque], axis=0)
                out[y, x] = min(tied, key=lambda c: sum((c[i] - mean[i]) ** 2 for i in range(3)))
    return out


def restore_outline(rgba: np.ndarray, color) -> np.ndarray:
    """실루엣 경계 픽셀을 아웃라인 색으로 되돌린다(4방향 기준)."""
    solid = rgba[:, :, 3] > 128
    pad = np.pad(solid, 1, constant_values=False)
    edge = solid & ~(pad[:-2, 1:-1] & pad[2:, 1:-1] & pad[1:-1, :-2] & pad[1:-1, 2:])
    out = rgba.copy()
    out[edge] = (color[0], color[1], color[2], 255)
    return out


def align(rgba: np.ndarray, size: int, feet: int) -> Image.Image:
    """발끝을 feet 행에 붙이고 가로 중심을 맞춘 size x size 캔버스로."""
    image = Image.fromarray(rgba, "RGBA")
    box = Image.fromarray(rgba[:, :, 3]).getbbox()
    if box is None:
        raise SystemExit("불투명 픽셀이 하나도 없다 — 원본을 확인할 것")
    crop = image.crop(box)
    canvas = Image.new("RGBA", (size, size), (0, 0, 0, 0))
    canvas.paste(crop, ((size - crop.width) // 2, feet + 1 - crop.height), crop)
    return canvas


def main() -> None:
    ap = argparse.ArgumentParser(description=__doc__, formatter_class=argparse.RawDescriptionHelpFormatter)
    ap.add_argument("source")
    ap.add_argument("output")
    ap.add_argument("--size", type=int, default=32, help="목표 캔버스 한 변 (기본 32)")
    ap.add_argument("--feet", type=int, default=31, help="발끝이 놓일 행 (기본 31)")
    ap.add_argument("--no-outline", action="store_true", help="아웃라인 복원을 끈다")
    ap.add_argument("--compare", help="원본/결과 확대 비교 시트를 이 경로에 쓴다")
    args = ap.parse_args()

    src = Image.open(args.source).convert("RGBA")
    if src.width % args.size or src.height % args.size:
        raise SystemExit(f"원본 {src.size} 이 목표 {args.size} 의 정수배가 아니다")
    factor = src.width // args.size

    rgba = np.array(src)
    small = block_downscale(rgba, factor)
    if not args.no_outline:
        opaque = [c for _, c in src.getcolors(1 << 20) if c[3] > 0]
        small = restore_outline(small, min(opaque, key=lambda c: c[0] + c[1] + c[2]))

    result = align(small, args.size, args.feet)
    Path(args.output).parent.mkdir(parents=True, exist_ok=True)
    result.save(args.output)

    box = Image.fromarray(np.array(result)[:, :, 3]).getbbox()
    print(f"{args.source} {src.size} -> {args.output} {result.size} "
          f"실루엣 {box[2] - box[0]}x{box[3] - box[1]} 색 {len(result.getcolors(1 << 20))}종")

    if args.compare:
        zoom = 12
        cells = [src.resize((src.width * zoom // factor, src.height * zoom // factor), Image.NEAREST),
                 result.resize((args.size * zoom, args.size * zoom), Image.NEAREST)]
        pad = 16
        sheet = Image.new("RGBA", (sum(c.width for c in cells) + pad * 3,
                                   max(c.height for c in cells) + pad * 2), (28, 30, 34, 255))
        x = pad
        for cell in cells:
            sheet.paste(cell, (x, pad), cell)
            x += cell.width + pad
        Path(args.compare).parent.mkdir(parents=True, exist_ok=True)
        sheet.save(args.compare)
        print("비교 시트 ->", args.compare)


if __name__ == "__main__":
    main()
