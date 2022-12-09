using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TPSCharacterController : MonoBehaviour
{
    [SerializeField]
    private Transform characterBody;
    [SerializeField]
    private Transform cameraArm;
    private float cameraDistance;

    Animator animator;
    Rigidbody rigid;

    bool isJumping; // 위로 점프 중

    void Awake()
    {
        animator = characterBody.GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        isJumping = true;
    }

    void Update()
    {
        if (GameManager.Instance.CurrentMode == GameManager.PlayMode.Write)
            return;

        // scroll to zoom
        // cameraDistance = Mathf.Clamp(cameraDistance + Input.mouseScrollDelta.y, 0, 10);
        cameraDistance = 1f;

        Move();
        InteractObj();
    }

    private void FixedUpdate()
    {
        Jump();
    }

    private void LateUpdate()
    {
        if (GameManager.Instance.CurrentMode == GameManager.PlayMode.Write)
            return;

        LookAround();
    }

    private void LookAround()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector3 camAngle = cameraArm.rotation.eulerAngles;

        // 카메라 각도 제한 (마우스를 움직이다보면 화면이 뒤집어지는 문제 발생)
        float x = camAngle.x - mouseDelta.y;
        if (x < 180f)
        {
            x = Mathf.Clamp(x, -1f, 70f);
        }
        else
        {
            x = Mathf.Clamp(x, 335f, 361f);
        }

        cameraArm.rotation = Quaternion.Euler(x, camAngle.y + mouseDelta.x, camAngle.z);
    }

    private void Move()
    {
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        bool isMoving = moveInput.magnitude != 0;
        animator.SetBool("isMoving", isMoving);
        if (isMoving)
        {
            Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
            Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
            Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;

            characterBody.forward = moveDir;
            transform.position += moveDir * Time.deltaTime * 5f;
        }
    }

    private void InteractObj()
    {
        if (Input.GetMouseButton(0))
            animator.SetBool("isInteracting", true);
        else
            animator.SetBool("isInteracting", false);
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && isJumping)
        {
            rigid.AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
            isJumping = false;
            Debug.Log("Jumping");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer != LayerMask.NameToLayer("Player"))
        {
            isJumping = true;
        }
    }

    public float CameraDistance
    {
        get { return cameraDistance; }
        set { cameraDistance = value; }
    }
}

