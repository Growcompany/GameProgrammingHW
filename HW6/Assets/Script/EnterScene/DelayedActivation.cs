using System.Collections;
using UnityEngine;

public class DelayedActivation : MonoBehaviour
{
    public GameObject objectToDisable;
    public GameObject objectToActivate; // Ȱ��ȭ�� ������Ʈ
    public float delay = 10f; // ������ �ð� (��)

    private void Start()
    {
        StartCoroutine(ActivateAfterDelay());
        objectToDisable.SetActive(false);
    }

    private IEnumerator ActivateAfterDelay()
    {
        // ������ �ð� ��ٸ���
        yield return new WaitForSeconds(delay);

        // Ư�� ������Ʈ Ȱ��ȭ
        if (objectToActivate != null)
            objectToActivate.SetActive(true);

        // �ڽ� ����
        Destroy(gameObject);
    }
}
