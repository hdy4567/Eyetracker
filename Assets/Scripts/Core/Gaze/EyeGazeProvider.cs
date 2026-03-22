using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace EyeTracker.Core.Gaze
{
    // 0.02 단계: 실시간 시선 벡터 추출 모듈
    // ARFoundation의 ARFaceManager를 사용하여 양안의 시선 데이터를 추출합니다.
    public class EyeGazeProvider : MonoBehaviour
    {
        private ARFaceManager faceManager;
        private Vector3 currentGazeDirection;
        private bool isTracking = false;

        void Awake()
        {
            faceManager = GetComponent<ARFaceManager>();
        }

        void OnEnable()
        {
            if (faceManager != null)
                faceManager.facesChanged += OnFacesChanged;
        }

        void OnDisable()
        {
            if (faceManager != null)
                faceManager.facesChanged -= OnFacesChanged;
        }

        private void OnFacesChanged(ARFacesChangedEventArgs args)
        {
            foreach (var face in args.updated)
            {
                if (face.trackingState == TrackingState.Tracking)
                {
                    isTracking = true;
                    // 양안의 forward 벡터를 평균하여 중앙 시선 방향 도출
                    currentGazeDirection = (face.leftEye.forward + face.rightEye.forward).normalized;
                }
                else
                {
                    isTracking = false;
                }
            }
        }

        public Vector3 GetCurrentGazeDirection() => currentGazeDirection;
        public bool IsTracking() => isTracking;
    }
}
