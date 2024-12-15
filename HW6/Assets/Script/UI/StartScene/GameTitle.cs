using UnityEngine;
using UnityEngine.UI;

public class GameTitle : MonoBehaviour
{
    public RectTransform titleImage; // Ÿ��Ʋ �̹��� RectTransform
    public float moveSpeed = 2f; // ���Ʒ� �����̴� �ӵ�
    public float scaleSpeed = 1.5f; // ũ�� ��ȭ �ӵ�
    public float scaleMagnitude = 0.2f; // ũ�� ��ȭ ũ��
    public float moveMagnitude = 50f; // ���Ʒ� ������ ũ��

    private Vector2 initialPosition;
    private Vector3 initialScale;
    private float time;

    void Start()
    {
        // �ʱ� ��ġ�� ũ�� ����
        if (titleImage == null)
        {
            Debug.LogError("TitleImage�� �����ϼ���!");
            return;
        }

        initialPosition = titleImage.anchoredPosition;
        initialScale = titleImage.localScale;
    }

    void Update()
    {
        if (titleImage == null) return;

        // �ð� �� ������Ʈ
        time += Time.deltaTime;

        // ���Ʒ� ������
        float yOffset = Mathf.Sin(time * moveSpeed) * moveMagnitude;
        titleImage.anchoredPosition = initialPosition + new Vector2(0, yOffset);

        // ũ�� ��ȭ
        float scaleOffset = Mathf.Sin(time * scaleSpeed) * scaleMagnitude;
        titleImage.localScale = initialScale + new Vector3(scaleOffset, scaleOffset, 0);
    }
}
