using UnityEngine;
using EyeTracker.Core.Gaze;

namespace EyeTracker.Core.Logic
{
    // 0.04 단계: 메타버스 통합 제어 및 세션 매니저
    // 시선 데이터, 안정화 필터, 적응형 엔진을 통합하여 전체 훈련 세션을 관리합니다.
    public class GazeSessionManager : MonoBehaviour
    {
        public EyeGazeProvider gazeProvider;
        public AdaptiveContrastEngine contrastEngine;
        public AccommodationTarget targetObject;

        private GazeKalmanFilter kalmanFilter;
        private Vector3 stabilizedGaze;

        void Start()
        {
            kalmanFilter = new GazeKalmanFilter();
        }

        void Update()
        {
            if (gazeProvider.IsTracking())
            {
                // 1. 시선 데이터 안정화 (0.03)
                stabilizedGaze = kalmanFilter.Update(gazeProvider.GetCurrentGazeDirection());

                // 2. 응시 충돌 테스트 (Raycast)
                CheckGazeHit();
            }
        }

        private void CheckGazeHit()
        {
            Ray ray = new Ray(Camera.main.transform.position, stabilizedGaze);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.gameObject == targetObject.gameObject)
                {
                    contrastEngine.RegisterHit();
                }
                else
                {
                    contrastEngine.RegisterMiss();
                }
            }
        }

        public Vector3 GetGaze() => stabilizedGaze;
    }
}
