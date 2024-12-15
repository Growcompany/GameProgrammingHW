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
        // To Do - 팝업 UI 열기        
    }

    public void ClosePopupUI()
    {
        // To Do - 팝업 UI 닫기
    }

    public void ShowLinkedUI()
    {
        // To Do - 연속 UI 띄워주기, 마지막 UI Active(false)
    }

    public void UndoLinkedUI()
    {
        // To Do - 연속 UI 뒤로가기
    }
}