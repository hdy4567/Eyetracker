# EyeTracker Unity 프로젝트 초기화 스크립트 (CLI 용)
# 필수 폴더 구조 및 최소 메타데이터 생성

$projectPath = "h:\내 드라이브\CStest\Eyetracker"

# 1. 필수 디렉토리 생성
New-Item -ItemType Directory -Force -Path "$projectPath\ProjectSettings"
New-Item -ItemType Directory -Force -Path "$projectPath\Packages"
New-Item -ItemType Directory -Force -Path "$projectPath\Assets\Scenes"

# 2. Package Manifest 생성 (기본 종속성)
$manifest = @'
{
  "dependencies": {
    "com.unity.modules.ui": "1.0.0",
    "com.unity.render-pipelines.universal": "17.0.0",
    "com.unity.xr.arfoundation": "6.0.0",
    "com.unity.xr.arkit": "6.0.0",
    "com.unity.xr.arcore": "6.0.0"
  }
}
'@
Set-Content -Path "$projectPath\Packages\manifest.json" -Value $manifest

# 3. ProjectSettings (최소 버전 설정)
$projectVersion = "m_EditorVersion: 6000.3.11f1"
Set-Content -Path "$projectPath\ProjectSettings\ProjectVersion.txt" -Value $projectVersion

Write-Host "Unity Project Boilerplate Initialized Successfully."
