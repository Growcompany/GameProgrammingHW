using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseManager : MonoBehaviour
{
    // 충돌을 통해 승리 또는 패배를 처리하는 함수
    private void OnTriggerEnter(Collider other)
    {
        // 플레이어가 기지에 닿으면 승리 장면으로 전환
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("WinScene");  // 승리 장면으로 전환
        }
        // 몬스터가 기지에 닿으면 패배 장면으로 전환
        else if (other.CompareTag("Monster"))
        {
            SceneManager.LoadScene("LoseScene");  // 패배 장면으로 전환
        }
    }
}
