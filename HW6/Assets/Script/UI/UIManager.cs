using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    public void ShowPopupUI()
    {
        // To Do - �˾� UI ����        
    }

    public void ClosePopupUI()
    {
        // To Do - �˾� UI �ݱ�
    }

    public void ShowLinkedUI()
    {
        // To Do - ���� UI ����ֱ�, ������ UI Active(false)
    }

    public void UndoLinkedUI()
    {
        // To Do - ���� UI �ڷΰ���
    }
}