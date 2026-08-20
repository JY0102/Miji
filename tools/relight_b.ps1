Add-Type -AssemblyName System.Drawing

$sp = "C:\Users\DUMARU~1\AppData\Local\Temp\claude\C--Work-Project-Game-Miji\0ce38b99-c2a3-4505-9cc1-6d7d8c2b9941\scratchpad"
$src = New-Object System.Drawing.Bitmap("$sp\B_base_32.png")

# A의 이끼 올리브 계열로 통일한 램프 (어두운 쪽 -> 밝은 쪽)
$greenRamp = @('#26301A', '#3C4A1D', '#4A5824', '#6E8038', '#8A9C39', '#A8B85F')
$rustRamp  = @('#1E100A', '#46231A', '#7A3D22', '#A65B32')
$neutRamp  = @('#3A3440', '#A89878', '#D8C89A', '#E8E4D8')

function ToColor($hex) { [System.Drawing.ColorTranslator]::FromHtml($hex) }
$ramps = @{ g = ($greenRamp | ForEach-Object { ToColor $_ }); r = ($rustRamp | ForEach-Object { ToColor $_ }); n = ($neutRamp | ForEach-Object { ToColor $_ }) }

# 1) 각 픽셀을 (계열, 램프 인덱스)로 양자화
$family = New-Object 'string[,]' 32, 32
$index  = New-Object 'int[,]' 32, 32

for ($y = 0; $y -lt 32; $y++) {
    for ($x = 0; $x -lt 32; $x++) {
        $p = $src.GetPixel($x, $y)
        if ($p.A -le 8) { $family[$x, $y] = ''; continue }

        $lum = 0.299 * $p.R + 0.587 * $p.G + 0.114 * $p.B
        if ($lum -lt 25) { $family[$x, $y] = 'o'; continue }   # 아웃라인 유지

        if ($p.G -ge $p.R -and $p.G -gt $p.B) {
            $family[$x, $y] = 'g'
            if ($lum -ge 190) { $index[$x, $y] = 5 } elseif ($lum -ge 150) { $index[$x, $y] = 4 } elseif ($lum -ge 110) { $index[$x, $y] = 3 } elseif ($lum -ge 75) { $index[$x, $y] = 2 } elseif ($lum -ge 45) { $index[$x, $y] = 1 } else { $index[$x, $y] = 0 }
        } elseif ($p.R -gt $p.G) {
            $family[$x, $y] = 'r'
            if ($lum -ge 120) { $index[$x, $y] = 3 } elseif ($lum -ge 70) { $index[$x, $y] = 2 } elseif ($lum -ge 40) { $index[$x, $y] = 1 } else { $index[$x, $y] = 0 }
        } else {
            $family[$x, $y] = 'n'
            if ($lum -ge 200) { $index[$x, $y] = 3 } elseif ($lum -ge 140) { $index[$x, $y] = 2 } elseif ($lum -ge 80) { $index[$x, $y] = 1 } else { $index[$x, $y] = 0 }
        }
    }
}

# 2) 엣지 릴라이트: 위가 비면 +1(빛), 아래가 비면 -1(그림자). A의 판때기 명암 문법.
function IsOpenOrOutline($fx, $fy) {
    if ($fx -lt 0 -or $fx -ge 32 -or $fy -lt 0 -or $fy -ge 32) { return $true }
    $f = $family[$fx, $fy]
    return ($f -eq '' -or $f -eq 'o')
}

$out = New-Object System.Drawing.Bitmap(32, 32, [System.Drawing.Imaging.PixelFormat]::Format32bppArgb)
for ($y = 0; $y -lt 32; $y++) {
    for ($x = 0; $x -lt 32; $x++) {
        $f = $family[$x, $y]
        if ($f -eq '') { continue }
        if ($f -eq 'o') { $out.SetPixel($x, $y, (ToColor '#000000')); continue }

        $i = $index[$x, $y]
        if (IsOpenOrOutline $x ($y - 1)) { $i++ }
        elseif (IsOpenOrOutline $x ($y + 1)) { $i-- }

        $ramp = $ramps[$f]
        $i = [Math]::Max(0, [Math]::Min($ramp.Count - 1, $i))
        $out.SetPixel($x, $y, $ramp[$i])
    }
}
$src.Dispose()
$out.Save("$sp\B_relit_32.png")
$out.Dispose()

# 3) 검수용: A와 B(전/후)를 나란히 8배 확대
$scale = 8
$sheet = New-Object System.Drawing.Bitmap((32 * 3 * $scale + 32), (32 * $scale))
$g = [System.Drawing.Graphics]::FromImage($sheet)
$g.InterpolationMode = 'NearestNeighbor'; $g.PixelOffsetMode = 'Half'
$imgs = @("C:\Work\Project\Game\Miji\src\Miji\Assets\Art\Player\A_idle_0.png", "$sp\B_base_32.png", "$sp\B_relit_32.png")
for ($k = 0; $k -lt 3; $k++) {
    $im = [System.Drawing.Image]::FromFile($imgs[$k])
    $g.DrawImage($im, ($k * (32 * $scale + 16)), 0, (32 * $scale), (32 * $scale))
    $im.Dispose()
}
$g.Dispose()
$sheet.Save("$sp\B_compare.png")
$sheet.Dispose()
Write-Output "done"
