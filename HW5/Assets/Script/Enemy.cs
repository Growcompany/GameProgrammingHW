using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 3f; // �� �̵� �ӵ�
    public float lifeTime = 5f; // ���� �ڵ����� �ı��� �ð�
    private Vector3 randomDirection; // ���� ���� �̵� ����

    // �̺�Ʈ�� ���� �ı��� �� ȣ��
    public event System.Action OnDestroyed;

    void Start()
    {
        // ������ �������� �̵� ����
        randomDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;

        // ���� �ð� �� �� �ı�
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // �� �̵�
        transform.Translate(randomDirection * moveSpeed * Time.deltaTime);
    }

    void OnDestroy()
    {
        // ���� �ı��� �� �̺�Ʈ ȣ��
        if (OnDestroyed != null)
        {
            OnDestroyed();
        }
    }
}
