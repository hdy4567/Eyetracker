using UnityEngine;
using EyeTracker.UI;
using EyeTracker.Games;
using EyeTracker.Core.Logic;
using System.Collections.Generic;

namespace EyeTracker.Infrastructure
{
    // 0.19 단계: Scene Auto-Initializer (지능형 자동 연결 엔진)
    // 유니티 씬에 있는 모든 스크립트들을 마우스 조작 없이 실행 즉시 자동으로 연결합니다.
    public class SceneAutoInitializer : MonoBehaviour
    {
        [Header("Auto-Search Root")]
        public GameObject uiRoot;

        void Awake()
        {
            InitializeProject();
        }

        private void InitializeProject()
        {
            Debug.Log("[System] Auto-Initialize sequence started.");

            // 1. 핵심 UI 메뉴(GameSelectionMenu) 찾기 및 연동
            GameSelectionMenu menu = FindFirstObjectByType<GameSelectionMenu>();
            if (menu != null)
            {
                // 인스펙터 연동을 생략할 수 있게 리소스 폴더에서 데이터를 자동 로드
                menu.gameLibrary = new List<GameScenarioData>(Resources.LoadAll<GameScenarioData>("Games/"));
                
                // 각 게임 컨트롤러를 씬에서 자동으로 찾아 메뉴에 할당 (Dependency Injection)
                menu.shooterController = FindFirstObjectByType<TrainingGameController>();
                menu.butterflyGame = FindFirstObjectByType<ButterflyTrackingGame>();
                menu.saccadeGame = FindFirstObjectByType<SaccadeMatrixGame>();
                menu.flickerGame = FindFirstObjectByType<NeuroFlickerStimulus>();
                menu.crowdingGame = FindFirstObjectByType<CrowdingEscapeGame>();
                menu.darknessPriming = FindFirstObjectByType<DarknessPrimingMode>();
                menu.pressureRelief = FindFirstObjectByType<PressureReliefFlow>();
                
                // 헤더 대시보드 자동 할당
                menu.headerDashboard = FindFirstObjectByType<PerformanceDashboard>();
                
                Debug.Log("[System] All Game Modules linked successfully.");
            }
            else
            {
                Debug.LogError("[System] GameSelectionMenu not found in scene!");
            }

            // 2. 기본 임상 엔진(Gaze, Contrast) 초기 상태 확인
            GazeSessionManager session = FindFirstObjectByType<GazeSessionManager>();
            if (session != null)
            {
                Debug.Log("[System] Clinical Gaze Engine detected and ready.");
            }
        }
    }
}
