using System.Collections;
using UnityEngine;

public class DelayedActivation : MonoBehaviour
{
    public GameObject objectToDisable;
    public GameObject objectToActivate; // 활성화할 오브젝트
    public float delay = 10f; // 딜레이 시간 (초)

    private void Start()
    {
        StartCoroutine(ActivateAfterDelay());
        objectToDisable.SetActive(false);
    }

    private IEnumerator ActivateAfterDelay()
    {
        // 딜레이 시간 기다리기
        yield return new WaitForSeconds(delay);

        // 특정 오브젝트 활성화
        if (objectToActivate != null)
            objectToActivate.SetActive(true);

        // 자신 삭제
        Destroy(gameObject);
    }
}
