using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform orientation;
    
    private Rigidbody rb;

    private Vector3 moveDir;
    private float moveX;
    private float moveZ;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        HandleInput();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleInput()
    {
        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");
    }

    private void HandleMovement()
    {
        moveDir = orientation.forward * moveZ + orientation.right * moveX;

        rb.MovePosition(rb.position + moveDir.normalized * moveSpeed * Time.deltaTime);
    }
}
