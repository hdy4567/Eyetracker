using UnityEngine;
using EyeTracker.Core.Logic;

namespace EyeTracker.Games
{
    // 0.05/0.06 단계: Butterfly Pursuit (부드러운 추적 훈련)
    // 가보 패치 타겟이 복잡한 3D 곡선을 그리며 이동하며 사용자의 추적 능력을 강화합니다.
    public class ButterflyTrackingGame : MonoBehaviour
    {
        [Header("Movement Settings")]
        public float amplitude = 2.0f; // 이동 범위
        public float frequency = 0.5f; // 이동 속도
        public float depthBase = 1.0f; // 기본 깊이 (Z)

        private float timer = 0f;
        private Vector3 initialPosition;

        void Start()
        {
            initialPosition = transform.localPosition;
        }

        void Update()
        {
            timer += Time.deltaTime * frequency;

            // 리사쥬 곡선(Lissajous Curve)을 활용한 복잡한 추적 경로 생성
            float x = Mathf.Sin(timer) * amplitude;
            float y = Mathf.Sin(timer * 1.2f) * amplitude;
            float z = depthBase + Mathf.Cos(timer * 0.8f) * 0.5f;

            transform.localPosition = initialPosition + new Vector3(x, y, z);
            
            // 시각적 자극을 위한 가보 패치 회전 (Orientation 정합성)
            transform.Rotate(Vector3.forward, Time.deltaTime * 45f);
        }

        // GazeSessionManager에서 호출하여 정밀 추적 여부 판정
        public void OnGazeStay()
        {
            // 정밀 추적 성공 시 보상 로직 (점수 등)
            Debug.Log("[Game] Butterfly Tracking Success.");
        }
    }
}
