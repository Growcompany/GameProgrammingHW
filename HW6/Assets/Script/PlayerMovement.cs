using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    public float speed;
    public float rotationSpeed;
    public float jumpForce = 1000f; // ���� ���� ������ ������ ����
    private bool isGrounded = true; // �÷��̾ ���鿡 �ִ��� Ȯ��
    public VisualEffect jumpEffect;  // Visual Effect Graph�� ���� ���� ȿ��

    private Rigidbody rb;
    private Animator animator; // Animator ������Ʈ

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody�� �����ϴ�.");
        }

        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator�� �����ϴ�.");
        }
    }

    void Update()
    {
        // �÷��̾��� ���� ȸ�� ó��
        float mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(0, mouseX * rotationSpeed * Time.deltaTime, 0);

        // ���� ó��
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false; // ���� �� ���鿡�� ������
            // ĳ������ �� ��ġ���� ȿ�� ����
            jumpEffect.transform.position = transform.position;
            jumpEffect.Play();
            StartCoroutine(StopJumpEffectAfterDelay(0.5f));
            animator.SetTrigger("IsJump");
        }


    }

    IEnumerator StopJumpEffectAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // 0.5�� ���
        jumpEffect.Stop(); // ��ƼŬ ����
    }

    void FixedUpdate()
    {
        // �̵� ó�� - transform.Translate ��� Rigidbody ���
        Vector3 moveDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            moveDirection += transform.forward;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            moveDirection -= transform.forward;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            moveDirection -= transform.right;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            moveDirection += transform.right;
        }

        // �޸��� �ӵ� ���� ó��
        bool isRunning = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        float currentSpeed = isRunning ? speed * 2f : speed; // Shift ������ �ӵ� ����

        // Rigidbody�� �ӵ��� ���� �����Ͽ� ������
        rb.velocity = new Vector3(moveDirection.x * currentSpeed, rb.velocity.y, moveDirection.z * currentSpeed);


        // Animator �Ķ���� ������Ʈ
        UpdateAnimation(moveDirection, isRunning);
    }

    void UpdateAnimation(Vector3 moveDirection, bool isRunning)
    {
        // ���� ��ǥ��� �̵� ���� ��ȯ
        Vector3 localMove = transform.InverseTransformDirection(moveDirection).normalized;

        // PosX�� PosY �� ����
        float posX = localMove.x;
        float posY = localMove.z;

        animator.SetFloat("PosX", posX);
        animator.SetFloat("PosY", posY);

        // IsMoving �� ����: �̵� ������ Ȯ��
        bool isMoving = Mathf.Abs(posX) > 0.1f || Mathf.Abs(posY) > 0.1f;
        animator.SetBool("IsMoving", isMoving);

        // IsRun �� ����: Shift Ű�� �޸��� ���� Ȯ��
        animator.SetBool("IsRun", isRunning);
    }


    // ���� �浹 ����
    void OnCollisionEnter(Collision collision)
    {
        // �浹�� ������Ʈ�� �����̸� �ٽ� ���� �����ϰ� ����
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
