using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform player; // �÷��̾� ��ġ
    public float spawnRadius = 10f; // �÷��̾� �ֺ����� ���� �����Ǵ� �ݰ�
    public float spawnInterval = 3f; // ���� ���� ����
    public int maxEnemies = 10; // ���ÿ� ������ �� �ִ� ���� �ִ� ��
    private int currentEnemyCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        // ���� �ֱ������� ����
        InvokeRepeating("SpawnEnemy", spawnInterval, spawnInterval);
    }

    void SpawnEnemy()
    {
        if (currentEnemyCount >= maxEnemies)
            return;

        // �÷��̾� �ֺ� ���� ��ġ ���
        Vector3 spawnPosition = player.position + Random.insideUnitSphere * spawnRadius;
        spawnPosition.y = 104; // y ��ǥ�� 104�� ����

        // �� ����
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        currentEnemyCount++;

        // ���� �ڵ� �ı��� �� ī��Ʈ ����
        newEnemy.GetComponent<Enemy>().OnDestroyed += () => { currentEnemyCount--; };
    }
}
