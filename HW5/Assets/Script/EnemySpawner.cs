using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform player; // 플레이어 위치
    public float spawnRadius = 10f; // 플레이어 주변에서 적이 스폰되는 반경
    public float spawnInterval = 3f; // 적이 생성 간격
    public int maxEnemies = 10; // 동시에 존재할 수 있는 적의 최대 수
    private int currentEnemyCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        // 적을 주기적으로 생성
        InvokeRepeating("SpawnEnemy", spawnInterval, spawnInterval);
    }

    void SpawnEnemy()
    {
        if (currentEnemyCount >= maxEnemies)
            return;

        // 플레이어 주변 랜덤 위치 계산
        Vector3 spawnPosition = player.position + Random.insideUnitSphere * spawnRadius;
        spawnPosition.y = 104; // y 좌표를 104로 고정

        // 적 생성
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        currentEnemyCount++;

        // 적이 자동 파괴될 때 카운트 감소
        newEnemy.GetComponent<Enemy>().OnDestroyed += () => { currentEnemyCount--; };
    }
}
