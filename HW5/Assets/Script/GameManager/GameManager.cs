using UnityEngine;
using TMPro; 
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;     // Ÿ�̸� TextMeshPro
    public float countdownTime = 30f;  // ���� �ð� (30��)

    private bool gameStarted = false; 

    private void Start()
    {
        timerText.text = countdownTime.ToString("F2");  // Ÿ�̸� �ʱ�ȭ (�Ҽ��� 2�ڸ� ǥ��)
        gameStarted = true;
    }

    private void Update()
    {
        if (gameStarted)
        {
            // ī��Ʈ�ٿ� ����
            countdownTime -= Time.deltaTime;

            // Ÿ�̸� UI ������Ʈ
            timerText.text = countdownTime.ToString("F2");  // �Ҽ��� 2�ڸ����� ǥ��

            // �ð��� �� ���� �� �й� ó��
            if (countdownTime <= 0)
            {
                countdownTime = 0;
                timerText.text = "00.00";  // �ð��� 0�� �Ǹ� 00.00���� ǥ��
                gameStarted = false;       // ī��Ʈ�ٿ� ����

                // LoseScene���� ��ȯ
                SceneManager.LoadScene("WinScene");
            }
        }
    }
}
