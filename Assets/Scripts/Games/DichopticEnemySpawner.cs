using UnityEngine;
using System.Collections.Generic;
using EyeTracker.Core.Logic;

namespace EyeTracker.Games
{
    // 사시 교정용 Dichoptic 적 기체 생성기
    // 약안(Weak Eye)용 레이어에만 적을 생성하여 융합 훈련을 강제합니다.
    public class DichopticEnemySpawner : MonoBehaviour
    {
        [Header("Prefab & Layers")]
        public GameObject enemyPrefab;
        public int weakEyeLayer = 11; // .rags/구현계획서.md 사양 준수
        
        [Header("Spawn Settings")]
        public float spawnRadius = 5.0f;
        public float spawnInterval = 2.0f;
        
        private float timer = 0f;
        private List<GameObject> activeEnemies = new List<GameObject>();

        void Update()
        {
            timer += Time.deltaTime;
            if (timer >= spawnInterval)
            {
                SpawnEnemy();
                timer = 0f;
            }
        }

        private void SpawnEnemy()
        {
            // 카메라 전방 임의의 위치에 적 생성
            Vector3 spawnPos = Camera.main.transform.position + Camera.main.transform.forward * 10f;
            spawnPos += Random.insideUnitSphere * spawnRadius;
            
            GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            
            // 핵심 로직: 적 기체는 약안용 레이어에만 할당 (억제 제거 훈련)
            enemy.layer = weakEyeLayer;
            
            activeEnemies.Add(enemy);
        }

        public void ClearEnemies()
        {
            foreach (var enemy in activeEnemies)
            {
                if (enemy != null) Destroy(enemy);
            }
            activeEnemies.Clear();
        }
    }
}
