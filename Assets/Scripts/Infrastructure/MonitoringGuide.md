- # EyeTracker 실시간 모니터링 가이드 (ADB Logcat)
- 본 문서는 터미널을 활용하여 모바일 기기에서 발생하는 시료 데이터를 실시간으로 감시하는 최적의 조합을 명세합니다.

- 대주제: 최고의 모니터링 조합 (Professional Stack)
    - 소주제: 구성 요소 (The Trinity)
        - 세부사항: Terminal (PowerShell): ADB (Android Debug Bridge)를 통한 실시간 로그 스트리밍
        - 세부사항: Logcat Filter: EyeTracker 관련 로그만을 추출하는 정규식 필터링
        - 세부사항: Visual Studio Debugger: 코드 수준의 브레이크포인트 검증 (필요시)

- 대주제: 실시간 로그 스트리밍 명령어 (Terminal Commands)
    - 소주제: ADB Logcat 실행
        - 세부사항: 명령어: `adb logcat -s Unity:V | grep -E "Gaze|Clinical|CRITICAL"`
        - 세부사항: 기능: 유니티 엔진의 일반 로그 중 [Gaze], [Clinical], [CRITICAL] 태그가 붙은 핵심 치료 데이터만 필터링
    - 소주제: 장치 연결 확인
        - 세부사항: 명령어: `adb devices`
        - 세부사항: 조치: 'Unauthorized' 상태일 경우 기기에서 디버깅 권한 승인 필수

- 대주제: 모니터링 데이터 분석 및 해결 절차
    - 소주제: 이슈 감지 (Detection)
        - 세부사항: [Gaze] 로그가 중단될 경우: 카메라 권한 또는 얼굴 인식 Subsystem 충돌 의심
        - 세부사항: [Clinical] 대비 수치가 고착될 경우: Staircase 알고리즘의 보상/패널티 루프 임계값 재조정 필요
    - 소주제: 즉각적 해결 (Action)
        - 세부사항: 터미널 로그를 통해 발견된 런타임 오류는 `BuildScript.cs`를 통해 즉시 수정 및 재배포 수행
