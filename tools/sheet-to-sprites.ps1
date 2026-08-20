<#
.SYNOPSIS
  생성형 모델이 뽑은 「격자 시트」 한 장을 낱장 픽셀 스프라이트로 자른다.

.DESCRIPTION
  higgsfield / DALL-E 같은 모델은 픽셀아트를 요청해도 고해상도 이미지로 그린다.
  그 결과를 게임에 쓸 수 있는 크기로 내리는 것이 이 스크립트의 일이다.

  핵심은 **최빈색(mode) 샘플링**이다. 평균(bilinear) 다운스케일은 픽셀아트의 평면 색을
  섞어 1px 아웃라인을 회색으로 뭉갠다. 각 목표 픽셀에 대응하는 원본 블록에서 가장 많이
  나온 색을 그대로 집으면 평면 색과 아웃라인이 살아남는다.

  배경은 「완성된 셀에서 가장 흔한 색」으로 판정해 투명화한다. 캐릭터가 배경색과
  비슷한 톤이면 -BgTolerance 를 낮춰라.

.PARAMETER Source      자를 시트 이미지 경로
.PARAMETER OutDir      낱장을 쓸 폴더 (없으면 만든다)
.PARAMETER Cols/Rows   시트의 칸 수
.PARAMETER Size        낱장 한 변의 픽셀 수 (기본 64)
.PARAMETER Inset       칸 경계에서 안쪽으로 버릴 픽셀 (모델이 그린 칸 테두리선 제거용)
.PARAMETER Names       낱장 이름 배열. 개수가 모자라면 cell_01... 로 채운다
.PARAMETER BgTolerance 배경으로 볼 색 거리(R+G+B 절대차 합). 기본 24

.EXAMPLE
  ./tools/sheet-to-sprites.ps1 -Source sheet.png -OutDir out -Cols 3 -Rows 3 `
      -Names @("sit","peer","point","beckon","startled","happy","sleep","glum","profile")

.NOTES
  2026-08-21 B 포즈 시트 작업에서 뽑아낸 것. ART_LOG.md 2026-08-21 항목 참조.
  ⚠ 캐릭터가 칸 경계에 닿아 있으면 잘린다 — 그건 이 스크립트가 아니라 생성 단계에서 막아야 한다.
#>
param(
  [Parameter(Mandatory = $true)][string]$Source,
  [Parameter(Mandatory = $true)][string]$OutDir,
  [int]$Cols = 3,
  [int]$Rows = 3,
  [int]$Size = 64,
  [int]$Inset = 8,
  [string[]]$Names = @(),
  [int]$BgTolerance = 24
)

Add-Type -AssemblyName System.Drawing

if (-not (Test-Path $Source)) { throw "시트를 찾지 못했다: $Source" }
if (-not (Test-Path $OutDir)) { New-Item -ItemType Directory -Force $OutDir | Out-Null }

$img = New-Object System.Drawing.Bitmap($Source)
$W = $img.Width; $H = $img.Height
$rect = New-Object System.Drawing.Rectangle(0, 0, $W, $H)
$bd = $img.LockBits($rect, [System.Drawing.Imaging.ImageLockMode]::ReadOnly, [System.Drawing.Imaging.PixelFormat]::Format32bppArgb)
$stride = $bd.Stride
$buf = New-Object byte[] ($stride * $H)
[System.Runtime.InteropServices.Marshal]::Copy($bd.Scan0, $buf, 0, $buf.Length)
$img.UnlockBits($bd)
$img.Dispose()

$cellW = $W / [double]$Cols
$cellH = $H / [double]$Rows

for ($cy = 0; $cy -lt $Rows; $cy++) {
  for ($cx = 0; $cx -lt $Cols; $cx++) {
    $idx = $cy * $Cols + $cx
    $name = if ($idx -lt $Names.Count) { $Names[$idx] } else { "cell_{0:D2}" -f ($idx + 1) }

    $x0 = [int]([Math]::Round($cx * $cellW)) + $Inset
    $y0 = [int]([Math]::Round($cy * $cellH)) + $Inset
    $x1 = [int]([Math]::Round(($cx + 1) * $cellW)) - $Inset
    $y1 = [int]([Math]::Round(($cy + 1) * $cellH)) - $Inset
    $cw = $x1 - $x0; $ch = $y1 - $y0

    $out = New-Object System.Drawing.Bitmap($Size, $Size, [System.Drawing.Imaging.PixelFormat]::Format32bppArgb)
    $orect = New-Object System.Drawing.Rectangle(0, 0, $Size, $Size)
    $obd = $out.LockBits($orect, [System.Drawing.Imaging.ImageLockMode]::WriteOnly, [System.Drawing.Imaging.PixelFormat]::Format32bppArgb)
    $ostride = $obd.Stride
    $obuf = New-Object byte[] ($ostride * $Size)
    $counts = @{}

    for ($ty = 0; $ty -lt $Size; $ty++) {
      $sy0 = $y0 + [int]([Math]::Floor($ty * $ch / $Size))
      $sy1 = $y0 + [int]([Math]::Floor(($ty + 1) * $ch / $Size))
      if ($sy1 -le $sy0) { $sy1 = $sy0 + 1 }
      for ($tx = 0; $tx -lt $Size; $tx++) {
        $sx0 = $x0 + [int]([Math]::Floor($tx * $cw / $Size))
        $sx1 = $x0 + [int]([Math]::Floor(($tx + 1) * $cw / $Size))
        if ($sx1 -le $sx0) { $sx1 = $sx0 + 1 }

        # 대응 블록의 최빈색을 그대로 집는다 (평균내지 않는다)
        $hist = @{}
        for ($sy = $sy0; $sy -lt $sy1; $sy++) {
          $rowOff = $sy * $stride
          for ($sx = $sx0; $sx -lt $sx1; $sx++) {
            $o = $rowOff + $sx * 4
            # 5비트로 양자화해 거의 같은 명암은 한 통에 모은다
            $r = [int]($buf[$o + 2] -band 0xF8); $g = [int]($buf[$o + 1] -band 0xF8); $b = [int]($buf[$o] -band 0xF8)
            $key = ($r -shl 16) -bor ($g -shl 8) -bor $b
            if ($hist.ContainsKey($key)) { $hist[$key] = $hist[$key] + 1 } else { $hist[$key] = 1 }
          }
        }
        $best = -1; $bestN = -1
        foreach ($k in $hist.Keys) { if ($hist[$k] -gt $bestN) { $bestN = $hist[$k]; $best = $k } }

        $oo = $ty * $ostride + $tx * 4
        $obuf[$oo + 2] = [byte](($best -shr 16) -band 0xFF)
        $obuf[$oo + 1] = [byte](($best -shr 8) -band 0xFF)
        $obuf[$oo] = [byte]($best -band 0xFF)
        $obuf[$oo + 3] = 255
        if ($counts.ContainsKey($best)) { $counts[$best] = $counts[$best] + 1 } else { $counts[$best] = 1 }
      }
    }

    # 가장 흔한 색 = 배경
    $bg = -1; $bgN = -1
    foreach ($k in $counts.Keys) { if ($counts[$k] -gt $bgN) { $bgN = $counts[$k]; $bg = $k } }
    $bgR = ($bg -shr 16) -band 0xFF; $bgG = ($bg -shr 8) -band 0xFF; $bgB = $bg -band 0xFF
    for ($ty = 0; $ty -lt $Size; $ty++) {
      for ($tx = 0; $tx -lt $Size; $tx++) {
        $oo = $ty * $ostride + $tx * 4
        $d = [Math]::Abs([int]$obuf[$oo + 2] - $bgR) + [Math]::Abs([int]$obuf[$oo + 1] - $bgG) + [Math]::Abs([int]$obuf[$oo] - $bgB)
        if ($d -le $BgTolerance) { $obuf[$oo + 3] = 0 }
      }
    }

    [System.Runtime.InteropServices.Marshal]::Copy($obuf, 0, $obd.Scan0, $obuf.Length)
    $out.UnlockBits($obd)
    $path = Join-Path $OutDir ("$name.png")
    $out.Save($path, [System.Drawing.Imaging.ImageFormat]::Png)
    $out.Dispose()
    Write-Output ("{0}  (bg #{1:X2}{2:X2}{3:X2})" -f $path, $bgR, $bgG, $bgB)
  }
}
