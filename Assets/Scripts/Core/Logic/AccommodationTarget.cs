using UnityEngine;

namespace EyeTracker.Core.Logic
{
    // 0.05 단계: 디옵터 기반 원근 조절 엔진
    // 타겟을 Z축으로 정현파 운동시켜 수정체 근육의 이완과 수축을 유도합니다.
    public class AccommodationTarget : MonoBehaviour
    {
        public float minZ = 0.25f; // 4 디옵터
        public float maxZ = 2.0f;  // 0.5 디옵터
        public float speed = 0.2f; // 주파수 (Hz)

        private float timer = 0f;

        void Update()
        {
            timer += Time.deltaTime * speed * 2 * Mathf.PI;
            float wave = (Mathf.Sin(timer) + 1.0f) * 0.5f; // 0 ~ 1 범위
            
            float zPos = Mathf.Lerp(minZ, maxZ, wave);
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, zPos);
        }
    }
}
