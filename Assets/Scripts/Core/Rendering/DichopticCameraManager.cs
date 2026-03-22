using UnityEngine;

namespace EyeTracker.Core.Rendering
{
    // 0.11 단계: 양안 억제 제어용 Dichoptic 훈련 로직
    // 각 눈에 독립적인 대비 파라미터를 실시간으로 주입하여 융합 훈련을 돕습니다.
    public class DichopticCameraManager : MonoBehaviour
    {
        [Range(0, 1)] public float leftEyeContrast = 1.0f;
        [Range(0, 1)] public float rightEyeContrast = 1.0f;

        void Update()
        {
            Shader.SetGlobalFloat("_LeftContrast", leftEyeContrast);
            Shader.SetGlobalFloat("_RightContrast", rightEyeContrast);
        }

        // 약시/사시 훈련을 위한 우세안 억제 밸런싱 세팅
        public void SetBalance(float nonDominantC, float dominantC, bool leftIsDominant)
        {
            if (leftIsDominant)
            {
                leftEyeContrast = dominantC;
                rightEyeContrast = nonDominantC;
            }
            else
            {
                leftEyeContrast = nonDominantC;
                rightEyeContrast = dominantC;
            }
        }
    }
}
