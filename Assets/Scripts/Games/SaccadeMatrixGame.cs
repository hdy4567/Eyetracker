using UnityEngine;
using System.Collections;
using EyeTracker.Core.Logic;

namespace EyeTracker.Games
{
    // Saccade Matrix Jump (신속 시선 이동 훈련)
    // 화면의 임의 지점에 순차적으로 나타나는 타겟을 신속하게 응시하도록 유도합니다.
    public class SaccadeMatrixGame : MonoBehaviour
    {
        public GameObject targetPrefab;
        public float jumpInterval = 1.5f;
        public float matrixArea = 3.0f;

        private GameObject currentTarget;
        private bool isRunning = false;

        public void StartSaccadeTraining()
        {
            isRunning = true;
            StartCoroutine(SaccadeRoutine());
        }

        private IEnumerator SaccadeRoutine()
        {
            while (isRunning)
            {
                if (currentTarget != null) Destroy(currentTarget);

                // 무작위 메트릭스 위치에 타겟 생성
                Vector3 randomPos = new Vector3(
                    Random.Range(-matrixArea, matrixArea),
                    Random.Range(-matrixArea, matrixArea),
                    5.0f // 고정 거리
                );

                currentTarget = Instantiate(targetPrefab, randomPos, Quaternion.identity);
                
                yield return new WaitForSeconds(jumpInterval);
            }
        }

        public void StopTraining()
        {
            isRunning = false;
            if (currentTarget != null) Destroy(currentTarget);
        }
    }
}
