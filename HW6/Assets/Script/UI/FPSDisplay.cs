using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class FPSDisplay : MonoBehaviour
{
    private float m_fps;
    private TextMeshProUGUI fpsText;

    private void Start()
    {
        fpsText = GetComponent<TextMeshProUGUI>();
        InvokeRepeating("GetFPS", 1, 1);
    }

    private void GetFPS()
    {
        m_fps = 1 / Time.unscaledDeltaTime;
        fpsText.text = m_fps.ToString("F0")+ " FPS";
    }

    private void OnDestroy()
    {
        CancelInvoke();
    }
}