- # EyeTracker 모바일 배포 및 인프라 체크리스트
- 본 문서는 유니티(Unity 2024) 기반의 모바일 AR 어플리케이션 배포를 위해 반드시 갖추어야 할 환경을 명세합니다.

- 대주제: 개발 환경 인프라 (Software Stack)
    - 소주제: Unity Editor 설정
        - 세부사항: Unity Version: 2024.x LTS (Universal Render Pipeline 필수)
        - 세부사항: Build Support: Android Build Support (Open JDK/NDK 포함) 또는 iOS Build Support (Xcode 필요)
    - 소주제: 필수 패키지 (Unity Package Manager)
        - 세부사항: AR Foundation (버전 6.0 이상 권장)
        - 세부사항: Apple ARKit XR Plugin (iOS 배포용)
        - 세부사항: Google ARCore XR Plugin (Android 배포용)
        - 세부사항: Universal RP (Gabor 및 Dichoptic 셰이더 작동용)

- 대주제: 하드웨어 및 권한 설정 (Hardware & Permissions)
    - 소주제: 디바이스 사양
        - 세부사항: iOS: A11 Bionic 칩 이상 (FaceID 센서 탑재 모델 권장)
        - 세부사항: Android: ARCore 지원 기기 (ToF 센서 탑재 시 정밀도 상승)
    - 소주제: 어플리케이션 권한 (Project Settings)
        - 세부사항: Camera: 실시간 시선 및 얼굴 트래킹용 카메라 접근 권한 필수
        - 세부사항: Internet: Firebase 연동 및 RAG 지식 업데이트용 네트워크 권한

- 대주제: 렌더링 및 빌드 설정 (Build Settings)
    - 소주제: 배포 사양
        - 세부사항: XR Plug-in Management: ARKit/ARCore 활성화 확인
        - 세부사항: Scripting Backend: IL2CPP (고성능 시선 연산용)
        - 세부사항: Target API Level: Android SDK 33 이상 / iOS 14.0 이상
