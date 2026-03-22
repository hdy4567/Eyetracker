using UnityEngine;

namespace EyeTracker.Core.Logic
{
    // 0.10 단계: 적응형 대비 임계값 트래킹 시스템
    // 3-down/1-up 계단식 알고리즘을 사용하여 사용자의 시각 민감도 임계값을 추적합니다.
    public class AdaptiveContrastEngine : MonoBehaviour
    {
        private float currentContrast = 1.0f;
        private int consecutiveHits = 0;
        private const int HITS_TO_DECREASE = 3;
        private const float CONTRAST_STEP = 0.9f; // 10% 단위 조정

        private Material targetMaterial;
        private static readonly int ContrastProp = Shader.PropertyToID("_Contrast");

        void Awake()
        {
            var renderer = GetComponent<Renderer>();
            if (renderer != null) targetMaterial = renderer.material;
        }

        public void RegisterHit()
        {
            consecutiveHits++;
            if (consecutiveHits >= HITS_TO_DECREASE)
            {
                currentContrast *= CONTRAST_STEP;
                consecutiveHits = 0;
                UpdateShader();
            }
        }

        public void RegisterMiss()
        {
            currentContrast = Mathf.Min(1.0f, currentContrast / CONTRAST_STEP);
            consecutiveHits = 0;
            UpdateShader();
        }

        private void UpdateShader()
        {
            if (targetMaterial != null)
                targetMaterial.SetFloat(ContrastProp, currentContrast);
        }

        public float GetContrast() => currentContrast;
    }
}
