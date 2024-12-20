using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject Bullet; // 총알 프리팹
    public GameObject Red_Bullet; // 총알 프리팹
    public GameObject shootPoint; // 발사 위치
    public GameObject BombPrefab; // 폭탄 프리팹
    public float bulletSpeed = 20f; // 총알 속도
    public float bulletLifeTime = 2f; // 총알이 사라질 시간
    public float bombSpeed = 10f; // 폭탄 속도
    public AudioClip GunSound; // 공격 효과음
    private AudioSource audioSource;
    private Animator animator;
    private bool isShooting = false; // 발사 중인지 확인

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) // 왼쪽 마우스 클릭 발사
        {
            animator.SetTrigger("IsShoot1");
            // 총알 생성
            GameObject clone = Instantiate(Bullet, shootPoint.transform.position, shootPoint.transform.rotation);

            audioSource.PlayOneShot(GunSound);


            Rigidbody rb = clone.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // 총알을 forward 방향(즉, 슈팅 포인트가 바라보는 방향)으로 발사
                rb.velocity = shootPoint.transform.forward * bulletSpeed;
            }

            // 일정 시간 후 총알 파괴
            Destroy(clone, bulletLifeTime);
        }


        if (Input.GetKeyDown(KeyCode.Mouse1) && !isShooting) // 오른쪽 마우스 클릭, 발사 중이 아닐 때
        {
            animator.SetTrigger("IsShoot2");
            audioSource.PlayOneShot(GunSound); // PlayOneShot으로 겹쳐 재생
            StartCoroutine(ShootWithDelay());
        }

        if (Input.GetKeyDown(KeyCode.Z)) // Z 키로 폭탄 생성
        {
            GameObject bomb = Instantiate(BombPrefab, transform.position, Quaternion.identity);
            Rigidbody rb = bomb.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = transform.forward * bombSpeed; // 폭탄이 발사되도록 설정
            }
        }
    }

    private IEnumerator ShootWithDelay()
    {
        isShooting = true;

        // 애니메이션 시작
        animator.SetTrigger("IsShoot2");

        // 0.5초 대기 (애니메이션이 끝날 때까지 대기)
        yield return new WaitForSeconds(0.75f);

        // 총알 생성
        GameObject clone = Instantiate(Red_Bullet, shootPoint.transform.position, shootPoint.transform.rotation);
        audioSource.PlayOneShot(GunSound);

        Rigidbody rb = clone.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = shootPoint.transform.forward * bulletSpeed;
        }

        // 일정 시간 후 총알 파괴
        Destroy(clone, bulletLifeTime);

        // 발사 완료 후 다시 발사 가능
        isShooting = false;
    }
}