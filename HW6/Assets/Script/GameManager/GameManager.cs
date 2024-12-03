using UnityEngine;
using TMPro; 
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;     // 타이머 TextMeshPro
    public float countdownTime = 30f;  // 제한 시간 (30초)

    private bool gameStarted = false; 

    private void Start()
    {
        timerText.text = countdownTime.ToString("F2");  // 타이머 초기화 (소수점 2자리 표시)
        gameStarted = true;
    }

    private void Update()
    {
        if (gameStarted)
        {
            // 카운트다운 진행
            countdownTime -= Time.deltaTime;

            // 타이머 UI 업데이트
            timerText.text = countdownTime.ToString("F2");  // 소수점 2자리까지 표시

            // 시간이 다 됐을 때 패배 처리
            if (countdownTime <= 0)
            {
                countdownTime = 0;
                timerText.text = "00.00";  // 시간이 0이 되면 00.00으로 표시
                gameStarted = false;       // 카운트다운 종료

                // LoseScene으로 전환
                SceneManager.LoadScene("WinScene");
            }
        }
    }
}
