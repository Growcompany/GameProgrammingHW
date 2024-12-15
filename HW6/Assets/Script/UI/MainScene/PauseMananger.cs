using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenu; // PauseMenu Panel

    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // ESC 키로도 Pause 가능
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        pauseMenu.SetActive(isPaused);

        if (isPaused)
        {
            Time.timeScale = 0f; // 게임 일시 정지
        }
        else
        {
            Time.timeScale = 1f; // 게임 재개
        }
    }
}
