using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 3f; // 적 이동 속도
    public float lifeTime = 5f; // 적이 자동으로 파괴될 시간
    private Vector3 randomDirection; // 적의 랜덤 이동 방향

    // 이벤트로 적이 파괴될 때 호출
    public event System.Action OnDestroyed;

    void Start()
    {
        // 랜덤한 방향으로 이동 설정
        randomDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;

        // 일정 시간 후 적 파괴
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // 적 이동
        transform.Translate(randomDirection * moveSpeed * Time.deltaTime);
    }

    void OnDestroy()
    {
        // 적이 파괴될 때 이벤트 호출
        if (OnDestroyed != null)
        {
            OnDestroyed();
        }
    }
}
