using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public Rigidbody rb;
    [SerializeField] private GroundCheck groundCheck;
    [SerializeField] private Camera mainCamera;

    [Header("// SPEED -----------------------------------------------------------------------------------------")]
    [SerializeField] float speed;
    
    [HideInInspector] public bool IsMoving;
    [HideInInspector] public bool IsWalking;
    [HideInInspector] public float inputMagnitude;
    [SerializeField] float runSpeed;
    [HideInInspector] public bool IsRunning;
    float curretSpeed;
    [Space(5)]
    [SerializeField] float rotationSpeed;
    [HideInInspector] public float moveRotation;
    private float moveInputAngle;

    [Header("// JUMP ------------------------------------------------------------------------------------------")]
    [SerializeField] float jumpForce;
    [SerializeField] float coyoteTime;
    [SerializeField] float jumpBoostLimit;
    [HideInInspector] public bool IsJumping;
    float marginTimer;

    private float horizontal;
    private float vertical;
    private Vector3 direction;
    private Vector2 inputAngle;


    // GAME //-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-
    private void Awake()
    {
        if (!rb) rb = GetComponent<Rigidbody>();
        if (!groundCheck) groundCheck = GetComponentInChildren<GroundCheck>();

        mainCamera = Camera.main;
    }
    
    void Update()
    {
        // Take direction
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        IsMoving = horizontal != 0 || vertical != 0;


        // For Animator
        if (IsMoving && groundCheck.IsGrounded) IsWalking = true;
        else IsWalking = false;

        inputAngle = new Vector2(horizontal, vertical);
        inputMagnitude = inputAngle.magnitude;

        moveInputAngle = Mathf.Atan2(inputAngle.x, inputAngle.y) * Mathf.Rad2Deg + mainCamera.transform.eulerAngles.y;
        moveRotation = Mathf.DeltaAngle(transform.rotation.eulerAngles.y, moveInputAngle);


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