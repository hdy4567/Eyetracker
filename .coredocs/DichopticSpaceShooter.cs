using UnityEngine;
using System.Collections.Generic;

public class DichopticSpaceShooter : MonoBehaviour
{
    [Header("Eye Layer Settings")]
    public int dominantEyeLayer = 10; // 우세안용 레이어 (배경 등)
    public int weakEyeLayer = 11;     // 약안용 레이어 (적 기체 등)

    [Header("Enemy Spawning")]
    public GameObject enemyPrefab;
    public float spawnInterval = 3f;
    public float enemySpeed = 1.5f;
    
    private List<GameObject> activeEnemies = new List<GameObject>();
    private float timer = 0f;

    [Header("Integration")]
    public GazeSessionManager sessionManager;
    public DichopticCameraManager cameraManager;

    void Start()
    {
        // 약안(Weak Eye)의 대비를 높여 억제를 제거하는 초기 설정 시뮬레이션
        cameraManager.SetEyeBalance(1.0f, 0.3f, false); // 오른쪽 눈이 우세안이라고 가정
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }

        MoveEnemies();
    }

    private void SpawnEnemy()
    {
        // 평야 메타버스 환경 내부의 무작위 위치에 적 소환
        Vector3 spawnPos = new Vector3(Random.Range(-5f, 5f), Random.Range(-3f, 3f), 10f);
        GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        
        // 중요: 적 기체는 '약안용 레이어'에만 할당하여 억제 제거 훈련 강제
        enemy.layer = weakEyeLayer;
        
        activeEnemies.Add(enemy);
    }

    private void MoveEnemies()
    {
        for (int i = activeEnemies.Count - 1; i >= 0; i--)
        {
            activeEnemies[i].transform.Translate(Vector3.back * enemySpeed * Time.deltaTime);
            
            // 카메라 뒤로 넘어가면 자동 제거
            if (activeEnemies[i].transform.position.z < 0)
            {
                Destroy(activeEnemies[i]);
                activeEnemies.RemoveAt(i);
            }
        }
    }

    // GazeSessionManager에서 호출하는 충돌 성공 처리
    public void HandleEnemyHit(GameObject hitEnemy)
    {
        if (activeEnemies.Contains(hitEnemy))
        {
            // 폭발 이펙트 및 점수 로직 (생략)
            Destroy(hitEnemy);
            activeEnemies.Remove(hitEnemy);
            Debug.Log("Enemy Destroyed by Fused Vision!");
        }
    }
}
