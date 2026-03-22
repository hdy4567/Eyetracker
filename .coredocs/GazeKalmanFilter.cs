using UnityEngine;

public class GazeKalmanFilter
{
    private Vector3 x; // 현재 상태 추정치 (Position)
    private Vector3 p; // 추정 오차 공분산
    private Vector3 q; // 프로세스 노이즈 공분산 (시스템 자체의 불확실성)
    private Vector3 r; // 측정 노이즈 공분산 (센서 오차)
    private Vector3 k; // 칼만 이득

    private bool isInitialized = false;

    public GazeKalmanFilter(float qValue = 0.015f, float rValue = 0.5f)
    {
        q = new Vector3(qValue, qValue, qValue);
        r = new Vector3(rValue, rValue, rValue);
        p = Vector3.one;
    }

    public Vector3 Update(Vector3 measurement)
    {
        if (!isInitialized)
        {
            x = measurement;
            isInitialized = true;
            return x;
        }

        // 1. 예측 (Prediction)
        // x_pred = x_prev (정적인 모델 가정)
        // p_pred = p_prev + q
        p = p + q;

        // 2. 업데이트 (Update)
        // k = p_pred / (p_pred + r)
        k.x = p.x / (p.x + r.x);
        k.y = p.y / (p.y + r.y);
        k.z = p.z / (p.z + r.z);

        // x = x_pred + k * (measurement - x_pred)
        x.x = x.x + k.x * (measurement.x - x.x);
        x.y = x.y + k.y * (measurement.y - x.y);
        x.z = x.z + k.z * (measurement.z - x.z);

        // p = (1 - k) * p_pred
        p.x = (1.0f - k.x) * p.x;
        p.y = (1.0f - k.y) * p.y;
        p.z = (1.0f - k.z) * p.z;

        return x;
    }

    public void Reset() => isInitialized = false;
}
