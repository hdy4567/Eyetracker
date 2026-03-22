using UnityEngine;
using UnityEngine.UI;
using EyeTracker.Core.Logic;

namespace EyeTracker.Games
{
    // 0.13 단계: Neuro-Flicker 신경 세포 활성화 엔진
    // 핸드폰 주사율에 동기화된 고주파 대비 자극을 생성합니다.
    [RequireComponent(typeof(MeshRenderer))]
    public class NeuroFlickerStimulus : MonoBehaviour
    {
        [Header("Flicker Settings")]
        [Range(10f, 40f)]
        public float flickerFrequency = 40f; // 40Hz (Gamma) 권장
        public float maxContrast = 1.0f;
        public float minContrast = 0.1f;

        private Material targetMaterial;
        private float flickerTimer = 0f;
        private bool isFlickering = false;

        void Start()
        {
            targetMaterial = GetComponent<MeshRenderer>().material;
        }

        void Update()
        {
            if (!isFlickering) return;

            flickerTimer += Time.deltaTime;
            
            // 정현파(Sin) 기반의 매끄러운 대비 깜빡임 제어
            // 뇌파 동기화(Entrainment)를 위해 정확한 주기를 유지함
            float phase = Mathf.Sin(flickerTimer * flickerFrequency * 2f * Mathf.PI);
            float currentContrast = Mathf.Lerp(minContrast, maxContrast, (phase + 1f) / 2f);

            // GaborPatch.shader 또는 유사 셰이더의 _Contrast 프로퍼티 제어
            targetMaterial.SetFloat("_Contrast", currentContrast);
        }

        public void StartFlicker()
        {
            isFlickering = true;
            flickerTimer = 0f;
            Debug.Log($"[Neuro] Flicker Started at {flickerFrequency}Hz");
        }

        public void StopFlicker()
        {
            isFlickering = false;
            targetMaterial.SetFloat("_Contrast", 1.0f); // 기본값 복구
        }
    }
}
