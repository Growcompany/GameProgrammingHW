using UnityEngine;

public class Bullet : MonoBehaviour
{
    public ParticleSystem explosionEffect;  // ���� ��ƼŬ �ý���

    private void OnCollisionEnter(Collision collision)
    {
        // ���� �浹���� ��
        if (collision.gameObject.tag == "Monster")
        {
            // ���� ��ƼŬ �ý��� ����
            Instantiate(explosionEffect, transform.position, Quaternion.identity);

            // �Ѿ� ������Ʈ ����
            Destroy(gameObject);
        }
    }
}
