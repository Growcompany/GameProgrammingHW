using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int hp = 100;
    private TextMeshPro textMesh;

    void Start()
    {
        textMesh = GetComponentInChildren<TextMeshPro>();
        UpdateHPText();
    }

    private void Update()
    {
        print("적hp: " + hp);
    }
    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp < 0) hp = 0;
        UpdateHPText();
        if (hp == 0)
        {
            // 오브젝트를 파괴하거나 다른 처리를 수행합니다.
        }
    }

    void UpdateHPText()
    {
        if (textMesh != null)
        {
            textMesh.text = "HP: " + hp.ToString();
        }
    }
}
