using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pumpkin : MonoBehaviour
{
    private Rigidbody monsterRb;
    public float jumpForce = 500f; // 점프 힘의 크기

    void Start()
    {
        monsterRb = GetComponent<Rigidbody>();

        if (monsterRb == null)
        {
            Debug.LogError("Rigidbody가 몬스터에 없습니다!");
        }
    }

    // 트리거에 부딪혔을 때 호출되는 함수
    private void OnTriggerEnter(Collider other)
    {
        // 부딪힌 트리거가 특정 태그(예: "JumpTrigger")를 가진 경우
        if (other.CompareTag("Player"))
        {
            // 위쪽으로 점프하는 힘을 가함
            Jump();
        }
    }

    // 점프 기능을 수행하는 함수
    private void Jump()
    {
        if (monsterRb != null)
        {
            // Rigidbody에 위쪽 방향으로 힘을 가함
            monsterRb.AddForce(Vector3.up * jumpForce);
        }
    }

}
