using UnityEngine;
using UnityEngine.UI;

namespace EyeTracker.Core.Logic
{
    // 0.07 단계: 의학적 세이프가드 시스템
    // 과도한 안구 피로를 방지하고 훈련 안전성을 모니터링합니다.
    public class MedicalSafetyGuard : MonoBehaviour
    {
        [Header("Safety Parameters")]
        public float maxSessionTime = 900f; // 15분
        public float minViewDistance = 0.20f; // 20cm (과도한 근거리 방지)
        
        [Header("Alert UI")]
        public GameObject alertOverlay; // 화면 블러 또는 안내 UI
        
        private float sessionTimer = 0f;
        private bool isWarningActive = false;

        void Update()
        {
            MonitorSessionTime();
            MonitorDistance();
        }

        private void MonitorSessionTime()
        {
            sessionTimer += Time.deltaTime;
            if (sessionTimer >= maxSessionTime && !isWarningActive)
            {
                TriggerRestAlert("Session limit reached. Please rest your eyes for 5 minutes.");
            }
        }

        private void MonitorDistance()
        {
            // 카메라와 타겟(또는 얼굴) 사이의 거리 체크
            float distance = Vector3.Distance(Camera.main.transform.position, transform.position);
            if (distance < minViewDistance)
            {
                TriggerRestAlert("Too close! Please keep a safe distance.");
            }
            else if (isWarningActive && sessionTimer < maxSessionTime)
            {
                DismissAlert();
            }
        }

        private void TriggerRestAlert(string message)
        {
            isWarningActive = true;
            if (alertOverlay != null) alertOverlay.SetActive(true);
            Debug.LogWarning($"Medical Safety Alert: {message}");
        }

        private void DismissAlert()
        {
            isWarningActive = false;
            if (alertOverlay != null) alertOverlay.SetActive(false);
        }

        public void ResetSession() => sessionTimer = 0f;
    }
}
