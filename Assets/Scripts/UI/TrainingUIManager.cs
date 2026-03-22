using UnityEngine;
using UnityEngine.UI;
using EyeTracker.Core.Logic;
using EyeTracker.Core.Gaze;

namespace EyeTracker.UI
{
    // 훈련 세션의 상태와 성과를 실시간으로 시각화하는 UI 매니저
    public class TrainingUIManager : MonoBehaviour
    {
        [Header("References")]
        public GazeSessionManager sessionManager;
        public AdaptiveContrastEngine contrastEngine;
        public EyeGazeProvider gazeProvider;
        
        [Header("UI Elements")]
        public Text statusText;
        public Text contrastValueText;
        public Text hitsText;
        public Slider progressSlider;
        public GameObject warningPanel;

        void Update()
        {
            UpdateStatusDisplay();
            UpdatePerformanceMetrics();
        }

        private void UpdateStatusDisplay()
        {
            if (gazeProvider.IsTracking())
            {
                statusText.text = "Status: Tracking Active";
                statusText.color = Color.green;
            }
            else
            {
                statusText.text = "Status: No Face Detected";
                statusText.color = Color.red;
            }
        }

        private void UpdatePerformanceMetrics()
        {
            // 현재 적응형 대비값 표시 (0.01 ~ 1.00)
            float contrast = contrastEngine.GetContrast();
            contrastValueText.text = $"Contrast: {contrast:P0}"; // 퍼센트 형식

            // 세션 성과 업데이트
            // 0.08 단계의 로깅 데이터와 연동 예정
        }

        public void OnStartButtonPressed()
        {
            // 세션 시작 로직
            Debug.Log("Vision Training Session Started");
        }

        public void OnResetButtonPressed()
        {
            // 세션 초기화 및 세이프가드 리셋
            Debug.Log("Session Reset");
        }
    }
}
