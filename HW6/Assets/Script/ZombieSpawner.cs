using UnityEngine;
using System.Collections;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab; // 생성할 좀비 프리팹
    public float spawnInterval = 3f; // 생성 간격
    public int maxZombies = 20; // 최대 생성할 좀비 수
    private int currentZombieCount = 0; // 현재 생성된 좀비 수
    public AudioClip zombie_created;
    private AudioSource audioSource;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(SpawnZombies());
    }

    IEnumerator SpawnZombies()
    {
        while (currentZombieCount < maxZombies)
        {
            SpawnZombie();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnZombie()
    {
        // 현재 스폰 위치를 기준으로 X, Z축 방향으로 약간씩 위치를 변경
        Vector3 spawnPosition = transform.position;
        spawnPosition.x += Random.Range(-2f, 2f);
        spawnPosition.z += Random.Range(-2f, 2f);
        spawnPosition.y = transform.position.y;

        Instantiate(zombiePrefab, spawnPosition, transform.rotation);
        currentZombieCount++;

        audioSource.clip = zombie_created;
        audioSource.Play();
    }
}