using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject Bullet; // �Ѿ� ������
    public GameObject Red_Bullet; // �Ѿ� ������
    public GameObject shootPoint; // �߻� ��ġ
    public float bulletSpeed = 20f; // �Ѿ� �ӵ�
    public float bulletLifeTime = 2f; // �Ѿ��� ����� �ð�

    // Update is called once per frame
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) // ���� ���콺 Ŭ�� �߻�
        {
            // �Ѿ� ����
            GameObject clone = Instantiate(Bullet, shootPoint.transform.position, shootPoint.transform.rotation);

            Rigidbody rb = clone.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // �Ѿ��� forward ����(��, ���� ����Ʈ�� �ٶ󺸴� ����)���� �߻�
                rb.velocity = shootPoint.transform.forward * bulletSpeed;
            }

            // ���� �ð� �� �Ѿ� �ı�
            Destroy(clone, bulletLifeTime);
        }

        if (Input.GetKeyDown(KeyCode.Mouse1)) // ������ ���콺 Ŭ�� �߻�
        {
            // �Ѿ� ����
            GameObject clone = Instantiate(Red_Bullet, shootPoint.transform.position, shootPoint.transform.rotation);

            Rigidbody rb = clone.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // �Ѿ��� forward ����(��, ���� ����Ʈ�� �ٶ󺸴� ����)���� �߻�
                rb.velocity = shootPoint.transform.forward * bulletSpeed;
            }

            // ���� �ð� �� �Ѿ� �ı�
            Destroy(clone, bulletLifeTime);
        }
    }
}
