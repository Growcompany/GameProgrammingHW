using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    public float speed;
    public float rotationSpeed;
    public float jumpForce = 1000f; // 점프 힘을 적당한 값으로 설정
    private bool isGrounded = true; // 플레이어가 지면에 있는지 확인
    public VisualEffect jumpEffect;  // Visual Effect Graph로 만든 점프 효과

    private Rigidbody rb;
    private Animator animator; // Animator 컴포넌트

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody가 없습니다.");
        }

        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator가 없습니다.");
        }
    }

    void Update()
    {
        // 플레이어의 방향 회전 처리
        float mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(0, mouseX * rotationSpeed * Time.deltaTime, 0);

        // 점프 처리
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false; // 점프 후 지면에서 떨어짐
            // 캐릭터의 발 위치에서 효과 실행
            jumpEffect.transform.position = transform.position;
            jumpEffect.Play();
            StartCoroutine(StopJumpEffectAfterDelay(0.5f));
            animator.SetTrigger("IsJump");
        }


    }

    IEnumerator StopJumpEffectAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // 0.5초 대기
        jumpEffect.Stop(); // 파티클 중지
    }

    void FixedUpdate()
    {
        // 이동 처리 - transform.Translate 대신 Rigidbody 사용
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

        // 달리기 속도 증가 처리
        bool isRunning = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        float currentSpeed = isRunning ? speed * 2f : speed; // Shift 누르면 속도 증가

        // Rigidbody에 속도를 직접 적용하여 움직임
        rb.velocity = new Vector3(moveDirection.x * currentSpeed, rb.velocity.y, moveDirection.z * currentSpeed);


        // Animator 파라미터 업데이트
        UpdateAnimation(moveDirection, isRunning);
    }

    void UpdateAnimation(Vector3 moveDirection, bool isRunning)
    {
        // 로컬 좌표계로 이동 벡터 변환
        Vector3 localMove = transform.InverseTransformDirection(moveDirection).normalized;

        // PosX와 PosY 값 설정
        float posX = localMove.x;
        float posY = localMove.z;

        animator.SetFloat("PosX", posX);
        animator.SetFloat("PosY", posY);

        // IsMoving 값 설정: 이동 중인지 확인
        bool isMoving = Mathf.Abs(posX) > 0.1f || Mathf.Abs(posY) > 0.1f;
        animator.SetBool("IsMoving", isMoving);

        // IsRun 값 설정: Shift 키로 달리기 여부 확인
        animator.SetBool("IsRun", isRunning);
    }


    // 지면 충돌 감지
    void OnCollisionEnter(Collision collision)
    {
        // 충돌한 오브젝트가 지면이면 다시 점프 가능하게 설정
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
