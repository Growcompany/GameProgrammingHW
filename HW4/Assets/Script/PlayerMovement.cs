using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    public float speed;
    public float rotationSpeed;
    public float jumpForce = 1000f; // ���� ���� ������ ������ ����
    private bool isGrounded = true; // �÷��̾ ���鿡 �ִ��� Ȯ��

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody�� �����ϴ�. Rigidbody�� �߰��ϼ���.");
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
        }
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

        // Rigidbody�� �ӵ��� ���� �����Ͽ� ������
        rb.velocity = new Vector3(moveDirection.x * speed, rb.velocity.y, moveDirection.z * speed);
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
