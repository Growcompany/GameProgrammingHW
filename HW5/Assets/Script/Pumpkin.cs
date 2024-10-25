using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pumpkin : MonoBehaviour
{
    private Rigidbody monsterRb;
    public float jumpForce = 500f; // ���� ���� ũ��

    void Start()
    {
        monsterRb = GetComponent<Rigidbody>();

        if (monsterRb == null)
        {
            Debug.LogError("Rigidbody�� ���Ϳ� �����ϴ�!");
        }
    }

    // Ʈ���ſ� �ε����� �� ȣ��Ǵ� �Լ�
    private void OnTriggerEnter(Collider other)
    {
        // �ε��� Ʈ���Ű� Ư�� �±�(��: "JumpTrigger")�� ���� ���
        if (other.CompareTag("Player"))
        {
            // �������� �����ϴ� ���� ����
            Jump();
        }
    }

    // ���� ����� �����ϴ� �Լ�
    private void Jump()
    {
        if (monsterRb != null)
        {
            // Rigidbody�� ���� �������� ���� ����
            monsterRb.AddForce(Vector3.up * jumpForce);
        }
    }

}
