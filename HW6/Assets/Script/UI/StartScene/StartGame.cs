using UnityEngine;
using UnityEngine.SceneManagement; // �� ������ �ʿ��� ���ӽ����̽�

public class StartGame : MonoBehaviour
{
    public void GoMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void GoStartScene()
    {
        SceneManager.LoadScene("StartScene");
    }
}
