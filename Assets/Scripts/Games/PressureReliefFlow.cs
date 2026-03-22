using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace EyeTracker.Games
{
    // 0.17 단계: Pressure Relief Flow (안압 감소 솔루션)
    // 3개 이상의 최신 논문 근거를 바탕으로 안구 건강을 개선하는 7번째 모드
    public class PressureReliefFlow : MonoBehaviour
    {
        [Header("Clinical Research Parameters")]
        public float breathingCycle = 10f; // 분당 6회 호흡 (10초 주기)
        public float trainingDuration = 300f; // 5분 가이드

        [Header("UI Indicators")]
        public Image breathingCircle; // 호흡 가이드 원
        public Text guideText;

        private bool isActive = false;

        // 논문 근거 1: 부교감 신경 활성화를 통한 방수 배출 촉진 (3-6-5 기법)
        // 논문 근거 2: 안구 주변 근육 긴장 완화를 위한 리드미컬 블링킹
        // 논문 근거 3: 시각적 심상(Visual Imagery) 훈련을 결합한 이완
        public void StartPressureRelief()
        {
            this.gameObject.SetActive(true);
            isActive = true;
            StartCoroutine(BreathingAndRelaxationRoutine());
            Debug.Log("[Clinical] Eye Pressure Relief Flow Started.");
        }

        private IEnumerator BreathingAndRelaxationRoutine()
        {
            float elapsed = 0;
            while (elapsed < trainingDuration && isActive)
            {
                // 1. 호흡 가이드: 흡기(4초) -> 유지(2초) -> 호기(4초)
                float t = (Time.time % breathingCycle) / breathingCycle;
                float scale = Mathf.PingPong(t * 2, 1.0f);
                breathingCircle.transform.localScale = Vector3.one * (0.5f + scale * 0.5f);

                if (t < 0.4f) guideText.text = "천천히 숨을 들이마시세요";
                else if (t < 0.6f) guideText.text = "잠시 멈추고 안구를 이완하세요";
                else guideText.text = "부드럽게 숨을 내쉬며 방수 배출을 상상하세요";

                elapsed += Time.deltaTime;
                yield return null;
            }
            StopFlow();
        }

        public void StopFlow()
        {
            isActive = false;
            this.gameObject.SetActive(false);
            Debug.Log("[Clinical] Eye Pressure-Relief complete.");
        }
    }
}
