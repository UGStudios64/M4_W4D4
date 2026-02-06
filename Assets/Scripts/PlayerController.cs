using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public Rigidbody rb;
    [SerializeField] private GroundCheck groundCheck;
    [SerializeField] public Camera mainCamera;
    private Collider col;

    [Header("// SPEED -----------------------------------------------------------------------------------------")]
    [SerializeField] float speed;
    [SerializeField] float runSpeed;
    private float curretSpeed;

    [HideInInspector] public bool IsMoving;
    [HideInInspector] public bool IsRunning;

    [Space(5)]
    [SerializeField] float rotationSpeed;

    [Header("// JUMP ------------------------------------------------------------------------------------------")]
    [SerializeField] float jumpForce;
    [SerializeField] float jumpBoostLimit;
    [SerializeField] float coyoteTime;
    private float marginTimer;

    [HideInInspector] public bool IsJumping;

    // DIRECTIONS -------------------------------------------------------------------------------------------------
    [HideInInspector] public float horizontal;
    [HideInInspector] public float vertical;
    private Vector3 direction;


    // GAME //-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-
    private void Awake()
    {
        if (!rb) rb = GetComponent<Rigidbody>();
        if (!groundCheck) groundCheck = GetComponentInChildren<GroundCheck>();
        if (!col) col = GetComponentInChildren<Collider>();
        if (!mainCamera) mainCamera = Camera.main;
    }
    
    void Update()
    {
        // Take direction
        if (col.CompareTag("Player"))
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");

            IsMoving = horizontal != 0 || vertical != 0;
        }
        

        // Switch Velocity
        if (Input.GetButton("Run") && IsMoving) IsRunning = true;
        else IsRunning = false;

        if (IsRunning) curretSpeed = runSpeed;
        else curretSpeed = speed;


        // Jumping
        if (groundCheck.IsGrounded) marginTimer = coyoteTime; 
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

        // Apply Move and Speed
        Vector3 velocity = direction * curretSpeed;
        rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);


        // ROTATION //------- Roteate the player in the move direction
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            Quaternion smoothRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            rb.MoveRotation(smoothRotation);
        }


        // JUMP BOOST LIMIT //------- This will limit an accidental jump boost when the player jump on some edges
        if (!IsJumping && groundCheck.IsGrounded && rb.velocity.y > jumpBoostLimit)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpBoostLimit, rb.velocity.z);
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