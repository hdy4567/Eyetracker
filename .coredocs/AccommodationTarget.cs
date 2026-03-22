using UnityEngine;

public class AccommodationTarget : MonoBehaviour
{
    private float minZ = 0.2f; // 근거리 (약 5D)
    private float maxZ = 2.0f; // 원거리 (약 0.5D)
    private float frequency = 0.2f; // 왕복 주기 (Hz)
    private float phase = 0f;

    private Vector3 initialPos;
    private AdaptiveContrastEngine contrastEngine;

    void Start()
    {
        initialPos = transform.localPosition;
        contrastEngine = GetComponent<AdaptiveContrastEngine>();
    }

    void Update()
    {
        // 1. Z축 정현파 운동 (Accommodation Training)
        // 타겟이 부드럽게 앞뒤로 움직이며 수정체 조절력을 유도함
        float offsetZ = (maxZ - minZ) * 0.5f;
        float midZ = minZ + offsetZ;
        
        float currentZ = midZ + Mathf.Sin(2 * Mathf.PI * frequency * Time.time + phase) * offsetZ;
        
        transform.localPosition = new Vector3(initialPos.x, initialPos.y, currentZ);
    }

    // 의학적 근거에 따른 주기 및 범위 설정 (RAG 연동 포인트)
    public void SetParameters(float min, float max, float freq)
    {
        minZ = min;
        maxZ = max;
        frequency = freq;
    }
}
