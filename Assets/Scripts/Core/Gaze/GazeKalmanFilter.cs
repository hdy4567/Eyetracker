using UnityEngine;

namespace EyeTracker.Core.Gaze
{
    // 0.03 단계: 시선 데이터 정밀 안정화 알고리즘
    // 3D 칼만 필터를 적용하여 센서 노이즈 및 지터를 제거합니다.
    public class GazeKalmanFilter
    {
        private Vector3 estimate; // 현재 상태 추정치
        private Vector3 errorCovariance; // 추정 오차 공분산
        private Vector3 processNoise; // 시스템 불확실성 (Q)
        private Vector3 measurementNoise; // 측정 오차 (R)

        private bool initialized = false;

        public GazeKalmanFilter(float q = 0.015f, float r = 0.5f)
        {
            processNoise = new Vector3(q, q, q);
            measurementNoise = new Vector3(r, r, r);
            errorCovariance = Vector3.one;
        }

        public Vector3 Update(Vector3 measurement)
        {
            if (!initialized)
            {
                estimate = measurement;
                initialized = true;
                return estimate;
            }

            // 예측 단계
            errorCovariance += processNoise;

            // 업데이트 단계 (칼만 이득 계산 및 상태 보정)
            Vector3 gain;
            gain.x = errorCovariance.x / (errorCovariance.x + measurementNoise.x);
            gain.y = errorCovariance.y / (errorCovariance.y + measurementNoise.y);
            gain.z = errorCovariance.z / (errorCovariance.z + measurementNoise.z);

            estimate.x += gain.x * (measurement.x - estimate.x);
            estimate.y += gain.y * (measurement.y - estimate.y);
            estimate.z += gain.z * (measurement.z - estimate.z);

            errorCovariance.x = (1.0f - gain.x) * errorCovariance.x;
            errorCovariance.y = (1.0f - gain.y) * errorCovariance.y;
            errorCovariance.z = (1.0f - gain.z) * errorCovariance.z;

            return estimate;
        }

        public void Reset() => initialized = false;
    }
}
