using UnityEngine;
using System.Collections;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab; // ������ ���� ������
    public float spawnInterval = 3f; // ���� ����
    public int maxZombies = 20; // �ִ� ������ ���� ��
    private int currentZombieCount = 0; // ���� ������ ���� ��
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
        // ���� ���� ��ġ�� �������� X, Z�� �������� �ణ�� ��ġ�� ����
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