using UnityEngine;
using UnityEngine.SceneManagement; // 씬 관리에 필요한 네임스페이스

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
