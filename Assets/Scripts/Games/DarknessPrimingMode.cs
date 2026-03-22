using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace EyeTracker.Games
{
    // 0.15 단계: Darkness Priming (암흑 프라이밍)
    // 훈련 전 뇌의 신경 가소성을 최적화하기 위해 시각 자극을 차단하는 전처리 모드
    public class DarknessPrimingMode : MonoBehaviour
    {
        [Header("Settings")]
        public float primingDuration = 300f; // 5분 권장 (연구 사례 준수)
        public Image blackOverlay;
        public Text countdownText;

        private bool isPriming = false;
        private float remainingTime;

        public void StartPriming()
        {
            this.gameObject.SetActive(true);
            remainingTime = primingDuration;
            isPriming = true;
            
            // 시각적 방해 최소화를 위해 UI를 매우 어둡게 설정
            blackOverlay.color = Color.black;
            countdownText.color = new Color(0.2f, 0.2f, 0.2f, 0.1f); // 아주 희미한 회색
            
            StartCoroutine(PrimingRoutine());
            Debug.Log("[Darkness] Priming Started. Cortex resetting...");
        }

        private IEnumerator PrimingRoutine()
        {
            while (remainingTime > 0)
            {
                remainingTime -= Time.deltaTime;
                int minutes = Mathf.FloorToInt(remainingTime / 60F);
                int seconds = Mathf.FloorToInt(remainingTime - minutes * 60);
                
                countdownText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
                yield return null;
            }

            OnPrimingComplete();
        }

        private void OnPrimingComplete()
        {
            isPriming = false;
            Debug.Log("[Darkness] Priming Complete. Ready for high-intensity training.");
            
            // 완료 후 자동으로 메인 메뉴로 복귀하거나 다음 추천 게임 제안
            this.gameObject.SetActive(false);
        }
    }
}
