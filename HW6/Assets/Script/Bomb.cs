using System.Collections;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float speed = 5f; // 폭탄 이동 속도
    public float explosionRadius = 5f; // 폭발 반경
    public float explosionDelay = 1f; // 목표 도달 후 폭발 지연 시간
    public float idleDuration = 5f; // 몬스터가 Idle 상태가 되는 시간
    public AudioClip explosionSound; // 폭발 효과음
    private bool hasExploded = false; // 폭발 여부 플래그

    private Transform target; // 목표 몬스터
    private AudioSource audioSource;
    private Animator animator; // 애니메이터

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        animator.SetBool("walk", true);
    }

    void Update()
    {
        FindClosestMonster();
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
            

            float distance = Vector3.Distance(transform.position, target.position);
            Debug.Log("Distance to target: " + distance);

            if (distance < 30f && !hasExploded)
            {
                StartCoroutine(Explode());
                hasExploded = true; // Explode가 한 번만 실행
            }
        }
    }

    void FindClosestMonster()
    {
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
        float closestDistance = Mathf.Infinity;

        foreach (GameObject monster in monsters)
        {
            float distance = Vector3.Distance(transform.position, monster.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                target = monster.transform;
            }
        }
    }


    IEnumerator Explode()
    {
        animator.SetBool("walk", false);
        animator.SetTrigger("attack01");

        yield return new WaitForSeconds(explosionDelay);

        if (audioSource != null && explosionSound != null)
        {
            audioSource.PlayOneShot(explosionSound); // PlayOneShot으로 중복 방지
        }

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Monster"))
            {
                ZombieAI zombieAI = hitCollider.GetComponent<ZombieAI>();
                if (zombieAI != null)
                {
                    zombieAI.EnterIdleMode(idleDuration);
                }
            }
        }

        Destroy(gameObject, 1.5f);
    }

    private void OnDrawGizmosSelected()
    {
        // 폭발 반경을 나타내는 기즈모 (빨간색 반투명)
        Gizmos.color = new Color(1, 0, 0, 0.3f);
        Gizmos.DrawSphere(transform.position, explosionRadius);

        // 목표 방향으로의 이동 경로를 나타내는 기즈모 (녹색 선)
        if (target != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, target.position);
            Gizmos.DrawWireSphere(target.position, 0.3f); // 목표 지점에 작은 원으로 표시
        }
    }
}
