using System.Collections;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float speed = 5f; // ��ź �̵� �ӵ�
    public float explosionRadius = 5f; // ���� �ݰ�
    public float explosionDelay = 1f; // ��ǥ ���� �� ���� ���� �ð�
    public float idleDuration = 5f; // ���Ͱ� Idle ���°� �Ǵ� �ð�
    public AudioClip explosionSound; // ���� ȿ����
    private bool hasExploded = false; // ���� ���� �÷���

    private Transform target; // ��ǥ ����
    private AudioSource audioSource;
    private Animator animator; // �ִϸ�����

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
                hasExploded = true; // Explode�� �� ���� ����
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
            audioSource.PlayOneShot(explosionSound); // PlayOneShot���� �ߺ� ����
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
        // ���� �ݰ��� ��Ÿ���� ����� (������ ������)
        Gizmos.color = new Color(1, 0, 0, 0.3f);
        Gizmos.DrawSphere(transform.position, explosionRadius);

        // ��ǥ ���������� �̵� ��θ� ��Ÿ���� ����� (��� ��)
        if (target != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, target.position);
            Gizmos.DrawWireSphere(target.position, 0.3f); // ��ǥ ������ ���� ������ ǥ��
        }
    }
}
