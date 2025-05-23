using Unity.Netcode;
using UnityEngine;

public class PlayerNetwork : NetworkBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;


    private float moveX;
    private float moveZ;
    private Vector3 lastMoveDir = Vector3.forward;
    private Vector3 startPos;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        if (!IsOwner) return;
        
        HandleInput();
        HandleMovement();
        HandleRotation();

        if (Input.GetKeyDown(KeyCode.Space)) 
        { 
            moveSpeed *= 1.25f;
        }

        if(transform.position.y < -5f)
        {
            transform.position = startPos;
            moveSpeed = 3f;
            rb.linearVelocity = Vector3.zero;
        }

        Debug.DrawRay(transform.position, lastMoveDir * 2f, Color.red);
    }
    private void HandleInput()
    {
        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");
    }

    private void HandleMovement()
    {
        Vector3 moveDir = transform.forward * moveZ + transform.right * moveX;

        if (moveDir != Vector3.zero)
        {
            lastMoveDir = moveDir.normalized;
        }

        rb.MovePosition(rb.position + moveDir * moveSpeed * Time.deltaTime);
    }



    private void HandleRotation()
    {
        if (lastMoveDir != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(lastMoveDir);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

}

