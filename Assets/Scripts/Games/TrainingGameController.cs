using UnityEngine;
using EyeTracker.Core.Logic;
using EyeTracker.UI;

namespace EyeTracker.Games
{
    // 훈련 게임 전체의 흐름과 점수를 관리하는 컨트롤러
    public class TrainingGameController : MonoBehaviour
    {
        [Header("Systems")]
        public DichopticEnemySpawner spawner;
        public GazeSessionManager sessionManager;
        public DataLoggingSystem logger;
        public TrainingUIManager uiManager;

        private float gameStartTime;
        private int hitCount = 0;
        private bool isGameRunning = false;

        public void StartGame()
        {
            gameStartTime = Time.time;
            hitCount = 0;
            isGameRunning = true;
            spawner.enabled = true;
            Debug.Log("Training Game Started.");
        }

        public void StopGame()
        {
            isGameRunning = false;
            spawner.enabled = false;
            spawner.ClearEnemies();
            
            float duration = Time.time - gameStartTime;
            float finalContrast = sessionManager.contrastEngine.GetContrast();
            
            // 0.08 단계: 데이터 로깅 실행
            logger.LogSessionSummary(duration, finalContrast, hitCount);
            
            Debug.Log($"Training Finished. Hits: {hitCount}, Contrast: {finalContrast:P}");
        }

        // GazeSessionManager로부터 적중 이벤트를 수신함
        public void RegisterHit()
        {
            if (!isGameRunning) return;
            hitCount++;
            // 추가적인 피드백(사운드, 이펙트) 여기에서 처리
        }
    }
}
