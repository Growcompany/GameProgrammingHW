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

    public Transform baseTarget; // ������ ��ġ
    public float attackDistance = 2f; // ���� ���� �Ÿ�
    public float playerAttackDistance = 1.5f; // �÷��̾� ���� �Ÿ�
    public float detectionRadius = 10f; // �÷��̾� ���� ����
    public float attackInterval = 1f; // ���� ����
    public int damage = 10; // ���� ������

    public int maxHealth = 10;
    private int currentHealth;
    private bool isDead = false;

    private Animator animator;
    private CharacterController controller;
    private float attackTimer = 0f;
    private Transform playerTarget;
    public float moveSpeed = 3f; // �̵� �ӵ�
    public float rotationSpeed = 5f; // ȸ�� �ӵ�

    public ParticleSystem bloodEffect; // �ǰ� ���� ��ƼŬ ȿ��
    public AudioClip zombie_attackted; // ���� ȿ����
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
        if (isDead) return; // ��� �� ������Ʈ ����

        // Idle ���� ����
        if (isIdle)
        {
            idleTimeRemaining -= Time.deltaTime;
            if (idleTimeRemaining <= 0)
            {
                isIdle = false;
                currentState = ZombieState.MoveToBase; // Idle ���� ���� �� MoveToBase�� ���ư�
            }
            return; // Idle ������ ���� ������ ���� ���� �� ��
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

        // ���� ȸ��
        Quaternion toRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

        // ������ �̵�
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
        // Ž�� ���� ���� ���� (������ ������)
        Gizmos.color = new Color(1, 0, 0, 0.3f);
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        // ���� ���� ���� ���� ���� (������ �Ķ���)
        Gizmos.color = new Color(0, 0, 1, 0.3f);
        Gizmos.DrawWireSphere(transform.position, attackDistance);

        // �÷��̾� ���� ���� ���� ���� (������ �ʷϻ�)
        Gizmos.color = new Color(0, 1, 0, 0.3f);
        Gizmos.DrawWireSphere(transform.position, playerAttackDistance);
    }

    void MoveToBase()
    {
        if (baseTarget == null) return;

        Vector3 direction = (baseTarget.position - transform.position).normalized;

        // ���� ȸ��
        Quaternion toRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

        // ������ �̵�
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

            // �� ��ƼŬ ȿ�� ���
            bloodEffect.Play();

            // ������ �������� ������ ����
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

            // �� ��ƼŬ ȿ�� ���
            bloodEffect.Play();

            // �÷��̾ �������� ������ ����
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
        currentState = ZombieState.Idle; // Idle ���·� ��ȯ
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;

        // �� ��ƼŬ ȿ�� ���
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

        // ��� �� �� ��ƼŬ ȿ�� ���
        bloodEffect.Play();

        // ���� �ð� �� ������Ʈ ����
        Destroy(gameObject, 2f); // 2�� �Ŀ� ����
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            if (bullet != null)
            {
                TakeDamage(5); // ������ ����
                bloodEffect.Play(); // �� ��ƼŬ ����
            }

            Destroy(collision.gameObject); // �浹�� �Ѿ� ����
        }
    }

}
