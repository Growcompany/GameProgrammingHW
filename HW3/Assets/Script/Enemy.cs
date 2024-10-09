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
        print("��hp: " + hp);
    }
    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp < 0) hp = 0;
        UpdateHPText();
        if (hp == 0)
        {
            // ������Ʈ�� �ı��ϰų� �ٸ� ó���� �����մϴ�.
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
