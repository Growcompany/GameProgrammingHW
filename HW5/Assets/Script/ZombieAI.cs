using UnityEngine;

public class ZombieAI : MonoBehaviour
{
    public enum ZombieState
    {
        Idle,
        MoveToBase,
        AttackBase,
        ChasePlayer,
        AttackPlayer,
        Dead
    }

    public ZombieState currentState = ZombieState.Idle;

    public Transform baseTarget; // 기지의 위치
    public float attackDistance = 2f; // 기지 공격 거리
    public float playerAttackDistance = 1.5f; // 플레이어 공격 거리
    public float detectionRadius = 10f; // 플레이어 감지 범위
    public float attackInterval = 1f; // 공격 간격
    public int damage = 10; // 공격 데미지

    public int maxHealth = 10;
    private int currentHealth;
    private bool isDead = false;

    private Animator animator;
    private CharacterController controller;
    private float attackTimer = 0f;
    private Transform playerTarget;
    public float moveSpeed = 3f; // 이동 속도
    public float rotationSpeed = 5f; // 회전 속도

    public ParticleSystem bloodEffect; // 피가 나는 파티클 효과
    public AudioClip zombie_attackted; // 공격 효과음
    private AudioSource audioSource;

    private bool isIdle = false;
    private float idleTimeRemaining = 0f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();

        if (baseTarget == null)
        {
            GameObject baseObj = GameObject.FindGameObjectWithTag("PlayerBase");
            if (baseObj != null)
            {
                baseTarget = baseObj.transform;
            }
        }

        currentHealth = maxHealth;
    }

    void Update()
    {
        if (isDead) return; // 사망 시 업데이트 중지

        // Idle 상태 관리
        if (isIdle)
        {
            idleTimeRemaining -= Time.deltaTime;
            if (idleTimeRemaining <= 0)
            {
                isIdle = false;
                currentState = ZombieState.MoveToBase; // Idle 상태 종료 후 MoveToBase로 돌아감
            }
            return; // Idle 상태일 때는 나머지 로직 실행 안 함
        }

        switch (currentState)
        {
            case ZombieState.Idle:
                currentState = ZombieState.MoveToBase;
                break;

            case ZombieState.MoveToBase:
                MoveToBase();
                DetectPlayer();
                break;

            case ZombieState.AttackBase:
                AttackBase();
                DetectPlayer();
                break;

            case ZombieState.ChasePlayer:
                ChasePlayer();
                break;

            case ZombieState.AttackPlayer:
                AttackPlayer();
                break;

            case ZombieState.Dead:
                break;
        }
    }

    void DetectPlayer()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                playerTarget = hitCollider.transform;
                currentState = ZombieState.ChasePlayer;
                break;
            }
        }
    }

    void ChasePlayer()
    {
        if (playerTarget == null)
        {
            currentState = ZombieState.MoveToBase;
            return;
        }

        Vector3 direction = (playerTarget.position - transform.position).normalized;

        // 좀비 회전
        Quaternion toRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

        // 앞으로 이동
        Vector3 move = direction * moveSpeed * Time.deltaTime;
        controller.Move(move);

        float distance = Vector3.Distance(transform.position, playerTarget.position);
        if (distance <= playerAttackDistance)
        {
            currentState = ZombieState.AttackPlayer;
        }
        else if (distance > detectionRadius)
        {
            playerTarget = null;
            currentState = ZombieState.MoveToBase;
        }
    }


    private void OnDrawGizmosSelected()
    {
        // 탐색 범위 색상 설정 (반투명 빨간색)
        Gizmos.color = new Color(1, 0, 0, 0.3f);
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        // 기지 공격 범위 색상 설정 (반투명 파란색)
        Gizmos.color = new Color(0, 0, 1, 0.3f);
        Gizmos.DrawWireSphere(transform.position, attackDistance);

        // 플레이어 공격 범위 색상 설정 (반투명 초록색)
        Gizmos.color = new Color(0, 1, 0, 0.3f);
        Gizmos.DrawWireSphere(transform.position, playerAttackDistance);
    }

    void MoveToBase()
    {
        if (baseTarget == null) return;

        Vector3 direction = (baseTarget.position - transform.position).normalized;

        // 좀비 회전
        Quaternion toRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

        // 앞으로 이동
        Vector3 move = direction * moveSpeed * Time.deltaTime;
        controller.Move(move);

        float distance = Vector3.Distance(transform.position, baseTarget.position);
        if (distance <= attackDistance)
        {
            currentState = ZombieState.AttackBase;
        }
    }

    void AttackBase()
    {
        if (baseTarget == null) return;

        transform.LookAt(baseTarget);

        attackTimer += Time.deltaTime;
        if (attackTimer >= attackInterval)
        {
            attackTimer = 0f;
            animator.SetTrigger("Attack");

            // 피 파티클 효과 재생
            bloodEffect.Play();

            // 기지에 데미지를 입히는 로직
            // BaseHealth baseHealth = baseTarget.GetComponent<BaseHealth>();
            // if (baseHealth != null)
            // {
            //     baseHealth.TakeDamage(damage);
            // }
        }

        float distance = Vector3.Distance(transform.position, baseTarget.position);
        if (distance > attackDistance)
        {
            currentState = ZombieState.MoveToBase;
        }
    }

    void AttackPlayer()
    {
        if (playerTarget == null)
        {
            currentState = ZombieState.MoveToBase;
            return;
        }

        transform.LookAt(playerTarget);

        attackTimer += Time.deltaTime;
        if (attackTimer >= attackInterval)
        {
            attackTimer = 0f;
            animator.SetTrigger("Attack");

            audioSource.clip = zombie_attackted;
            audioSource.Play();

            // 피 파티클 효과 재생
            bloodEffect.Play();

            // 플레이어에 데미지를 입히는 로직
            // PlayerHealth playerHealth = playerTarget.GetComponent<PlayerHealth>();
            // if (playerHealth != null)
            // {
            //     playerHealth.TakeDamage(damage);
            // }
        }

        float distance = Vector3.Distance(transform.position, playerTarget.position);
        if (distance > playerAttackDistance)
        {
            currentState = ZombieState.ChasePlayer;
        }
        else if (distance > detectionRadius)
        {
            playerTarget = null;
            currentState = ZombieState.MoveToBase;
        }
    }

    public void EnterIdleMode(float duration)
    {
        isIdle = true;
        idleTimeRemaining = duration;
        currentState = ZombieState.Idle; // Idle 상태로 전환
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;

        // 피 파티클 효과 재생
        bloodEffect.Play();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        currentState = ZombieState.Dead;
        animator.SetTrigger("Dead");

        // 사망 시 피 파티클 효과 재생
        bloodEffect.Play();

        // 일정 시간 후 오브젝트 제거
        Destroy(gameObject, 2f); // 2초 후에 제거
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            if (bullet != null)
            {
                TakeDamage(5); // 데미지 적용
                bloodEffect.Play(); // 피 파티클 실행
            }

            Destroy(collision.gameObject); // 충돌한 총알 제거
        }
    }

}
