using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject Bullet; // 총알 프리팹
    public GameObject Red_Bullet; // 총알 프리팹
    public GameObject shootPoint; // 발사 위치
    public float bulletSpeed = 20f; // 총알 속도
    public float bulletLifeTime = 2f; // 총알이 사라질 시간

    // Update is called once per frame
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) // 왼쪽 마우스 클릭 발사
        {
            // 총알 생성
            GameObject clone = Instantiate(Bullet, shootPoint.transform.position, shootPoint.transform.rotation);

            Rigidbody rb = clone.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // 총알을 forward 방향(즉, 슈팅 포인트가 바라보는 방향)으로 발사
                rb.velocity = shootPoint.transform.forward * bulletSpeed;
            }

            // 일정 시간 후 총알 파괴
            Destroy(clone, bulletLifeTime);
        }

        if (Input.GetKeyDown(KeyCode.Mouse1)) // 오른쪽 마우스 클릭 발사
        {
            // 총알 생성
            GameObject clone = Instantiate(Red_Bullet, shootPoint.transform.position, shootPoint.transform.rotation);

            Rigidbody rb = clone.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // 총알을 forward 방향(즉, 슈팅 포인트가 바라보는 방향)으로 발사
                rb.velocity = shootPoint.transform.forward * bulletSpeed;
            }

            // 일정 시간 후 총알 파괴
            Destroy(clone, bulletLifeTime);
        }
    }
}
