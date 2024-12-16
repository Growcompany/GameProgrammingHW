using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject Bullet; // �Ѿ� ������
    public GameObject Red_Bullet; // �Ѿ� ������
    public GameObject shootPoint; // �߻� ��ġ
    public GameObject BombPrefab; // ��ź ������
    public float bulletSpeed = 20f; // �Ѿ� �ӵ�
    public float bulletLifeTime = 2f; // �Ѿ��� ����� �ð�
    public float bombSpeed = 10f; // ��ź �ӵ�
    public AudioClip GunSound; // ���� ȿ����
    private AudioSource audioSource;
    private Animator animator;
    private bool isShooting = false; // �߻� ������ Ȯ��

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) // ���� ���콺 Ŭ�� �߻�
        {
            animator.SetTrigger("IsShoot1");
            // �Ѿ� ����
            GameObject clone = Instantiate(Bullet, shootPoint.transform.position, shootPoint.transform.rotation);

            audioSource.PlayOneShot(GunSound);


            Rigidbody rb = clone.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // �Ѿ��� forward ����(��, ���� ����Ʈ�� �ٶ󺸴� ����)���� �߻�
                rb.velocity = shootPoint.transform.forward * bulletSpeed;
            }

            // ���� �ð� �� �Ѿ� �ı�
            Destroy(clone, bulletLifeTime);
        }


        if (Input.GetKeyDown(KeyCode.Mouse1) && !isShooting) // ������ ���콺 Ŭ��, �߻� ���� �ƴ� ��
        {
            animator.SetTrigger("IsShoot2");
            audioSource.PlayOneShot(GunSound); // PlayOneShot���� ���� ���
            StartCoroutine(ShootWithDelay());
        }

        if (Input.GetKeyDown(KeyCode.Z)) // Z Ű�� ��ź ����
        {
            GameObject bomb = Instantiate(BombPrefab, transform.position, Quaternion.identity);
            Rigidbody rb = bomb.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = transform.forward * bombSpeed; // ��ź�� �߻�ǵ��� ����
            }
        }
    }

    private IEnumerator ShootWithDelay()
    {
        isShooting = true;

        // �ִϸ��̼� ����
        animator.SetTrigger("IsShoot2");

        // 0.5�� ��� (�ִϸ��̼��� ���� ������ ���)
        yield return new WaitForSeconds(0.75f);

        // �Ѿ� ����
        GameObject clone = Instantiate(Red_Bullet, shootPoint.transform.position, shootPoint.transform.rotation);
        audioSource.PlayOneShot(GunSound);

        Rigidbody rb = clone.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = shootPoint.transform.forward * bulletSpeed;
        }

        // ���� �ð� �� �Ѿ� �ı�
        Destroy(clone, bulletLifeTime);

        // �߻� �Ϸ� �� �ٽ� �߻� ����
        isShooting = false;
    }
}