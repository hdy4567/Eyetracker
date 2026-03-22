- # EyeTracker 유니티 씬 구성 가이드 (Scene Setup Guide)
- 본 문서는 구현된 모든 핵심 라이브러리 스크립트와 셰이더를 하나의 유니티 씬(Scene)으로 통합하는 절차를 명세합니다.

- 대주제: AR 카메라 및 인프라 설정 (Camera Setup)
    - 소주제: AR 세션 구성
        - 세부사항: AR Session 및 AR Session Origin 오브젝트를 씬에 생성
        - 세부사항: AR Camera에 ARFaceManager 컴포넌트 추가 및 0.02 단계 추출기 연결
    - 소주제: 렌더링 설정 (URP)
        - 세부사항: Project Settings에서 Universal Render Pipeline 활성화 확인
        - 세부사항: AR Camera의 Environment 레이어에 적절한 Skybox (평야 배경) 설정

- 대주제: 핵심 라이브러리 컴포넌트 배치 (Component Wiring)
    - 소주제: 하이어라키(Hierarchy) 구조 설계
        - 세부사항: [GazeRoot]: EyeGazeProvider.cs 부착 (AR Camera 참조 가능)
        - 세부사항: [SessionManager]: GazeSessionManager.cs 부착 및 시선 데이터 안정화 필터 초기화 연동
        - 세부사항: [TrainingCanvas]: TrainingUIManager.cs 부착 및 텍스트/슬라이더 UI 요소 물리적 연결
    - 소주제: 디옵터 기반 타겟(열기구) 설정
        - 세부사항: [TargetObject]: GaborPatch.shader가 적용된 Material을 가진 3D 구체 또는 평면 생성
        - 세부사항: [TargetObject]: AccommodationTarget.cs 및 AdaptiveContrastEngine.cs 부착

- 대주제: 렌더링 및 사시 교정 (Stereo Integration)
    - 소주제: Dichoptic 환경 구축
        - 세부사항: Scene 내에 DichopticCameraManager.cs를 배치하고 프로젝트 전역 셰이더 변수 활성화
        - 세부사항: 적 기체(타겟)의 레이어를 WeakEyeOnly (Layer 11)로 설정하여 억제 제거 훈련 강제
    - 소주제: 세이프가드 및 로깅 연동
        - 세부사항: MedicalSafetyGuard.cs를 배치하여 세션 타이머 및 시청 거리 실시간 감시
        - 세부사항: DataLoggingSystem.cs를 통해 훈련 종료 시 JSON 로그 자동 저장 브릿지 연결

- 대주제: 훈련 시나리오 실행 절차 (Workflow)
    - 소주제: 초기화 단계
        - 세부사항: 앱 실행 시 사용자의 우세안 정보를 바탕으로 Dichoptic 대비 밸런스 자동 보정
    - 소주제: 훈련 루틴 실행
        - 세부사항: 15분 루틴 가동 -> 실시간 대비 임계값 트래킹 -> 데이터 기록 -> 세이프가드 휴식 알림 순으로 진행
