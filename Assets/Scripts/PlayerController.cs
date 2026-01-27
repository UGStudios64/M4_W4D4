using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GroundCheck groundCheck;
    [SerializeField] private Camera mainCamera;
 
    [Header("// SPEED ------------------------------------------------------------------------------------------")]
    [SerializeField] float speed;
    [SerializeField] public bool IsMoving;
    [SerializeField] float runSpeed;
    [SerializeField] public bool IsRunning;
    float curretSpeed;

    [SerializeField] float rotationSpeed;

    [Header("// JUMP ------------------------------------------------------------------------------------------")]
    [SerializeField] float jumpForce;
    [SerializeField] float jumpMargin;
    [SerializeField] public bool IsJumping;
    float marginTimer;

    private float horizontal;
    private float vertical;
    private Vector3 direction;


    // GAME //-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-
    private void Awake()
    {
        if (rb == null) rb = GetComponent<Rigidbody>();
        if (groundCheck == null) groundCheck = GetComponentInChildren<GroundCheck>();
        mainCamera = Camera.main;
    }
    
    void Update()
    {
        // Take direction
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        IsMoving = horizontal != 0 || vertical != 0;


        // Switch Velocity
        if (Input.GetButton("Run") && IsMoving) IsRunning = true;
        else IsRunning = false;

        if (IsRunning) { curretSpeed = runSpeed; }
        else { curretSpeed = speed; }


        // Jumping
        if (groundCheck.IsGrounded) marginTimer = jumpMargin;
        else marginTimer -= Time.deltaTime;

        if (Input.GetButtonDown("Jump") && marginTimer > 0f) IsJumping = true;
    }

    private void FixedUpdate()
    {
        Move();
        Jump();
    }


    // FUNCTIONS //-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-
    private void Move()
    {
        // Move based on the Camera
        direction = mainCamera.transform.forward * vertical + mainCamera.transform.right * horizontal;
        direction.y = 0f;

        // Vector normalized
        if (direction.magnitude > 1f) direction.Normalize();

        Vector3 velocity = direction * curretSpeed;
        rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);



        // ROTATION //----------------------------------------------------
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            Quaternion smoothRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            rb.MoveRotation(smoothRotation);
        }
    }

    private void Jump()
    {
        if (IsJumping)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            IsJumping = false;
        }
    }
}