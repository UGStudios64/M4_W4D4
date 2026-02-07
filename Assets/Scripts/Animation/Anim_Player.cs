using UnityEngine;

public class AnimPlayer : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private PlayerController player;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GroundCheck groundCheck;
    [Space(5)]
    [SerializeField] private Material body;
    [SerializeField] private UI_Lifebar lifebar;

    [Header("// PULSE IN DANGER -----------------------------------------------------------------------------------------")]
    [SerializeField] private float minIntensity;
    [SerializeField] private float maxIntensity;
    [SerializeField] private float pulseSpeed;

    // DIRECTIONS --------------------------------------------------------------------------------------------------------
    private Vector2 inputAngle;
    private float inputMagnitude;
    private float moveInputAngle;
    private float moveRotation;
    

    // GAME //-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-
    private void Awake()
    {
        if (!anim) anim = GetComponentInChildren<Animator>();
        if (!player) player = GetComponent<PlayerController>();
        if (!groundCheck) groundCheck = GetComponentInChildren<GroundCheck>();
        if (!rb) rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Speed -------------------- to regulate the animation speed
        inputAngle = new Vector2(player.GetHorizontal(), player.GetVertical());
        inputMagnitude = inputAngle.magnitude;
        anim.SetFloat("Speed", Mathf.Clamp(inputMagnitude, 0.3f, 1f));


        // Rotation -------------------- Left/Right for inclinating movement
        moveInputAngle = Mathf.Atan2(inputAngle.x, inputAngle.y) * Mathf.Rad2Deg + player.GetMainCamera().transform.eulerAngles.y;
        moveRotation = Mathf.DeltaAngle(player.transform.rotation.eulerAngles.y, moveInputAngle);
        anim.SetFloat("Rotation", moveRotation, 0.1f, Time.deltaTime);


        // Walk --------------------
        anim.SetBool("Walk", player.GetIsMoving() && groundCheck.GetIsGrounded());


        // Run --------------------
        anim.SetBool("Run", player.GetIsRunning() && groundCheck.GetIsGrounded());


        // Jump --------------------
        if (player.IsJumping) anim.SetTrigger("Jump");
        anim.SetFloat("yVelocity", player.GetRb().velocity.y);
        anim.SetBool("Grounded", groundCheck.GetIsGrounded());


        // In Danger --------------------
        if (lifebar.GetInDanger())
        {
            float emission = Mathf.Lerp(minIntensity, maxIntensity, (Mathf.Sin(Time.time * pulseSpeed) + 1f) / 2f);
            body.SetColor("_EmissionColor", Color.white * emission);
        }
        else body.SetColor("_EmissionColor", Color.white * minIntensity);
    }


    // Dead --------------------
    public void OnDeath()
    {
        anim.SetTrigger("Dead");
        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
    }
}