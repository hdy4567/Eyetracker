using UnityEngine;
using System.Collections.Generic;
using EyeTracker.Core.Logic;

namespace EyeTracker.Games
{
    // 0.14 단계: Crowding Escape (혼잡 효과 인지 훈련)
    // 중심 타켓과 주변 방해 요소 간의 거리를 좁혀가며 뇌의 공간 해상도를 극한으로 끌어올립니다.
    public class CrowdingEscapeGame : MonoBehaviour
    {
        [Header("Prefab References")]
        public GameObject targetPrefab;
        public GameObject flankerPrefab;
        
        [Header("Clinical Settings")]
        public float initialSpacing = 2.0f;
        public float minSpacing = 0.3f;
        public int flankerCount = 4;

        private float currentSpacing;
        private GameObject centralTarget;
        private List<GameObject> activeFlankers = new List<GameObject>();
        private bool isRunning = false;

        public void StartCrowdingTraining()
        {
            currentSpacing = initialSpacing;
            isRunning = true;
            SpawnRound();
        }

        private void SpawnRound()
        {
            ClearPrevious();

            // 1. 중심 타켓 생성
            centralTarget = Instantiate(targetPrefab, transform.position + Vector3.forward * 5f, Quaternion.identity);
            // 무작위 방향 설정 (사용자가 맞춰야 할 정답)
            float randomAngle = Random.Range(0, 4) * 45f;
            centralTarget.transform.rotation = Quaternion.Euler(0, 0, randomAngle);

            // 2. 주변 방해 요소(Flankers) 생성
            for (int i = 0; i < flankerCount; i++)
            {
                float angle = i * (360f / flankerCount) * Mathf.Deg2Rad;
                Vector3 offset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * currentSpacing;
                
                GameObject flanker = Instantiate(flankerPrefab, centralTarget.transform.position + offset, Quaternion.identity);
                // 방해 요소는 무작위 방향으로 교란
                flanker.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360f));
                activeFlankers.Add(flanker);
            }
        }

        public void OnUserSuccess()
        {
            // 성공 시 간격을 좁혀 난이도 상승 (혼잡 효과 유발)
            currentSpacing = Mathf.Max(minSpacing, currentSpacing * 0.9f);
            SpawnRound();
            Debug.Log($"[Game] Success. Current Spacing: {currentSpacing:F2}");
        }

        private void ClearPrevious()
        {
            if (centralTarget != null) Destroy(centralTarget);
            foreach (var f in activeFlankers) if (f != null) Destroy(f);
            activeFlankers.Clear();
        }
    }
}
