using UnityEngine;
using UnityEngine.UI;

public class GameTitle : MonoBehaviour
{
    public RectTransform titleImage; // 타이틀 이미지 RectTransform
    public float moveSpeed = 2f; // 위아래 움직이는 속도
    public float scaleSpeed = 1.5f; // 크기 변화 속도
    public float scaleMagnitude = 0.2f; // 크기 변화 크기
    public float moveMagnitude = 50f; // 위아래 움직임 크기

    private Vector2 initialPosition;
    private Vector3 initialScale;
    private float time;

    void Start()
    {
        // 초기 위치와 크기 저장
        if (titleImage == null)
        {
            Debug.LogError("TitleImage를 연결하세요!");
            return;
        }

        initialPosition = titleImage.anchoredPosition;
        initialScale = titleImage.localScale;
    }

    void Update()
    {
        if (titleImage == null) return;

        // 시간 값 업데이트
        time += Time.deltaTime;

        // 위아래 움직임
        float yOffset = Mathf.Sin(time * moveSpeed) * moveMagnitude;
        titleImage.anchoredPosition = initialPosition + new Vector2(0, yOffset);

        // 크기 변화
        float scaleOffset = Mathf.Sin(time * scaleSpeed) * scaleMagnitude;
        titleImage.localScale = initialScale + new Vector3(scaleOffset, scaleOffset, 0);
    }
}
