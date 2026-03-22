using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using EyeTracker.Games;

namespace EyeTracker.UI
{
    // 상단에 성과 지표가 항상 노출되는 '헤더형 대시보드' 통합 메뉴
    public class GameSelectionMenu : MonoBehaviour
    {
        [Header("Header Dashboard (Always on top)")]
        public PerformanceDashboard headerDashboard; 

        [Header("Scrollable Game Cards")]
        public GameObject cardPrefab;
        public Transform contentRoot;

        [Header("Game Data Library")]
        public List<GameScenarioData> gameLibrary;

        [Header("System References")]
        public TrainingGameController shooterController;
        public ButterflyTrackingGame butterflyGame;
        public SaccadeMatrixGame saccadeGame;
        public NeuroFlickerStimulus flickerGame;
        public CrowdingEscapeGame crowdingGame;
        public DarknessPrimingMode darknessPriming;
        public PressureReliefFlow pressureRelief;
        public PerformanceDashboard dashboard;

        void Start()
        {
            InitializeMenu();
        }

        private void InitializeMenu()
        {
            // 리스트에 등록된 모든 데이터를 순회하며 동적으로 카드 생성
            foreach (var gameData in gameLibrary)
            {
                AddGameCardFromData(gameData);
            }
        }

        private void AddGameCardFromData(GameScenarioData data)
        {
            GameObject card = Instantiate(cardPrefab, contentRoot);
            
            card.transform.Find("Title").GetComponent<Text>().text = data.gameTitle;
            card.transform.Find("Description").GetComponent<Text>().text = data.gameDescription;
            if (data.gameIcon != null)
                card.transform.Find("Icon").GetComponent<Image>().sprite = data.gameIcon;
            
            card.GetComponent<Button>().onClick.AddListener(() => {
                this.gameObject.SetActive(false);
                HandleGameStart(data.type);
            });
        }

        private void HandleGameStart(GameScenarioData.GameType type)
        {
            // 데이터 타입에 따른 실행 분기 (팩토리 패턴 확장 가능)
            switch (type)
            {
                case GameScenarioData.GameType.Shooter: shooterController.StartGame(); break;
                case GameScenarioData.GameType.Pursuit: butterflyGame.enabled = true; break;
                case GameScenarioData.GameType.Saccade: saccadeGame.StartSaccadeTraining(); break;
                case GameScenarioData.GameType.Flicker: flickerGame.StartFlicker(); break;
                case GameScenarioData.GameType.Crowding: crowdingGame.StartCrowdingTraining(); break;
                case GameScenarioData.GameType.Priming: darknessPriming.StartPriming(); break;
                case GameScenarioData.GameType.PressureRelief: pressureRelief.StartPressureRelief(); break;
            }
        }

        public void OnDashboardTabClicked()
        {
            dashboard.ShowDashboard();
        }
    }
}
