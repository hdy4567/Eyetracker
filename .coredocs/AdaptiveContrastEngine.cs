using UnityEngine;
using System.Collections.Generic;

public class AdaptiveContrastEngine : MonoBehaviour
{
    private float currentContrast = 1.0f; // 1.0 (100%) ~ 0.01 (1%)
    private int consecutiveHits = 0;
    private const int HITS_TO_DECREASE = 3; // 3-down
    private const float CONTRAST_STEP_FACTOR = 0.9f; // 10% 감소 (로그 스케일 유사)
    
    private Renderer targetRenderer;
    private Material targetMaterial;
    private string contrastProperty = "_Contrast"; // 셰이더 프로퍼티 이름

    void Start()
    {
        // 초기화: 타겟 렌더러와 머티리얼 설정
        targetRenderer = GetComponent<Renderer>();
        if (targetRenderer != null)
        {
            targetMaterial = targetRenderer.material;
            UpdateMaterialContrast();
        }
    }

    // 사용자가 타겟 응시에 성공했을 때 호출 (Gaze Provider에서 트리거)
    public void RegisterHit()
    {
        consecutiveHits++;
        if (consecutiveHits >= HITS_TO_DECREASE)
        {
            // 난이도 상승: 대비 감소
            currentContrast *= CONTRAST_STEP_FACTOR;
            consecutiveHits = 0;
            UpdateMaterialContrast();
            Debug.Log($"Difficulty UP: Current Contrast {currentContrast * 100}%");
        }
    }

    // 사용자가 응시에 실패하거나 오답을 냈을 때 호출
    public void RegisterMiss()
    {
        // 난이도 하락: 대비 즉시 증가 (1-up)
        currentContrast = Mathf.Min(1.0f, currentContrast / CONTRAST_STEP_FACTOR);
        consecutiveHits = 0;
        UpdateMaterialContrast();
        Debug.Log($"Difficulty DOWN: Current Contrast {currentContrast * 100}%");
    }

    private void UpdateMaterialContrast()
    {
        if (targetMaterial != null)
        {
            // 셰이더의 대비 파라미터를 실시간 업데이트
            targetMaterial.SetFloat(contrastProperty, currentContrast);
            
            // 시각적 피드백: 알파 채널이나 컬러 오프셋을 조절하여 실제 대비 효과 시뮬레이션
            Color color = targetMaterial.color;
            color.a = currentContrast; 
            targetMaterial.color = color;
        }
    }

    public float GetCurrentContrast() => currentContrast;
}
