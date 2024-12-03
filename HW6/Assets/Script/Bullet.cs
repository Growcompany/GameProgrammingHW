using UnityEngine;

public class Bullet : MonoBehaviour
{
    public ParticleSystem explosionEffect;  // 폭발 파티클 시스템

    private void OnCollisionEnter(Collision collision)
    {
        // 적과 충돌했을 때
        if (collision.gameObject.tag == "Monster")
        {
            // 폭발 파티클 시스템 생성
            Instantiate(explosionEffect, transform.position, Quaternion.identity);

            // 총알 오브젝트 제거
            Destroy(gameObject);
        }
    }
}
