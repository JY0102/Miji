# sprite-gen 세팅 스크립트 (2026-08-20)
#
# sprite-gen = Codex/Claude용 스프라이트 아틀라스 파이프라인 (aldegad/sprite-gen, Apache-2.0).
# 우리 분업 구도: PixelLab = 플레이 레이어 실기 에셋 / sprite-gen(Codex 프로바이더) = 컨셉·배경 레이어.
#
# 외부 리포이므로 저장소에 벤더링하지 않는다 — tools/sprite-gen/ 은 .gitignore 대상이고
# 이 스크립트가 재현의 원본이다. 새 환경에서는 이 스크립트만 실행하면 된다.
#
# 요구사항: Python 3.10+ / git / (생성 기능은) codex CLI 로그인
# ⚠️ 실행 시 반드시 PYTHONUTF8=1 — 도움말·로그에 유니코드가 있어 cp949 콘솔에서 죽는다.

$ErrorActionPreference = 'Stop'
$dir = "$PSScriptRoot\sprite-gen"

if (-not (Test-Path $dir)) {
    git clone https://github.com/aldegad/sprite-gen.git $dir
}

Set-Location $dir
if (-not (Test-Path ".venv")) {
    python -m venv .venv
}
& .\.venv\Scripts\python.exe -m pip install -q -e .

# 검증
$env:PYTHONUTF8 = '1'
& .\.venv\Scripts\python.exe -c "import sprite_gen, PIL, numpy; print('sprite_gen OK')"
& .\.venv\Scripts\python.exe scripts\prepare_sprite_run.py --help | Select-Object -First 1

Write-Host @"

세팅 완료. 사용 예:
  `$env:PYTHONUTF8='1'
  .\.venv\Scripts\python.exe scripts\prepare_sprite_run.py --out-dir <run> --character-id <id> --base-image <png> ...
  .\.venv\Scripts\python.exe scripts\serve_curation.py ...   # 선별 웹뷰

Claude Code 슬래시 스킬로 등록하려면 (선택, 외부 코드 자동 로드이므로 수동 결정):
  이 폴더를 .claude\skills\sprite-gen 으로 복사/이동
"@
