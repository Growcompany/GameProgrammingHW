using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenu; // PauseMenu Panel

    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // ESC Ű�ε� Pause ����
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
            Time.timeScale = 0f; // ���� �Ͻ� ����
        }
        else
        {
            Time.timeScale = 1f; // ���� �簳
        }
    }
}
