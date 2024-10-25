using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseManager : MonoBehaviour
{
    // �浹�� ���� �¸� �Ǵ� �й踦 ó���ϴ� �Լ�
    private void OnTriggerEnter(Collider other)
    {
        // �÷��̾ ������ ������ �¸� ������� ��ȯ
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("WinScene");  // �¸� ������� ��ȯ
        }
        // ���Ͱ� ������ ������ �й� ������� ��ȯ
        else if (other.CompareTag("Monster"))
        {
            SceneManager.LoadScene("LoseScene");  // �й� ������� ��ȯ
        }
    }
}
