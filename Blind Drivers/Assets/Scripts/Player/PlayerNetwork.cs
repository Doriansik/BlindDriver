using Unity.Netcode;
using UnityEngine;

public class PlayerNetwork : NetworkBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;


    private float moveX;
    private float moveZ;
    private Vector3 moveDir;
    private Vector3 lastMoveDir = Vector3.forward;
    private Vector3 startPos;

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
        Vector3 localMove = transform.forward * moveZ + transform.right * moveX;

        if (localMove != Vector3.zero)
        {
            lastMoveDir = localMove.normalized;
        }

        transform.position += localMove.normalized * moveSpeed * Time.deltaTime;
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

