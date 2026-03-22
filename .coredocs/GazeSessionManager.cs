using UnityEngine;

public class GazeSessionManager : MonoBehaviour
{
    [Header("Core Modules")]
    public EyeGazeProvider gazeProvider;
    public AdaptiveContrastEngine contrastEngine;
    public AccommodationTarget accommodationTarget;

    private GazeKalmanFilter kalmanFilter;
    private Vector3 stabilizedGazePoint;

    [Header("Session Settings")]
    public float hitThreshold = 0.5f; // 타겟 근처 응시 인정 범위
    public float sessionDuration = 900f; // 15분 (초 단위)

    void Start()
    {
        kalmanFilter = new GazeKalmanFilter(0.01f, 0.4f);
        Debug.Log("Gaze Session Started: Metaverse Integration Active.");
    }

    void Update()
    {
        // 1. 시선 데이터 획득 및 안정화 (0.02 + 0.03)
        Vector3 rawGaze = gazeProvider.GetCurrentGazeDirection();
        stabilizedGazePoint = kalmanFilter.Update(rawGaze);

        // 2. 응시 충돌 테스트 (Raycasting)
        CheckTargetGaze();
    }

    private void CheckTargetGaze()
    {
        Ray ray = new Ray(Camera.main.transform.position, stabilizedGazePoint);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == accommodationTarget.gameObject)
            {
                // 성공적인 응시 (Hit)
                contrastEngine.RegisterHit();
                // 시각적 피드백 로직 추가 가능 (예: 이펙트 발생)
            }
            else
            {
                // 오답 또는 타겟 외 응시 (Miss)
                contrastEngine.RegisterMiss();
            }
        }
    }

    public Vector3 GetStabilizedPoint() => stabilizedGazePoint;
}
