using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class AnimPlayer : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private PlayerController player;
    [SerializeField] private GroundCheck groundCheck;
    [Space(5)]
    [SerializeField] private Material body;
    [SerializeField] private UI_Lifebar lifebar;

    [Header("// PULSE IN DANGER -----------------------------------------------------------------------------------------")]
    [SerializeField] private float minIntensity;
    [SerializeField] private float maxIntensity;
    [SerializeField] private float pulseSpeed;


    // GAME //-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-
    private void Awake()
    {
        if (!anim) anim = GetComponentInChildren<Animator>();
        if (!player) player = GetComponent<PlayerController>();
        if (!groundCheck) groundCheck = GetComponentInChildren<GroundCheck>();
    }

    private void Update()
    {
        // Move
        anim.SetFloat("Speed", Mathf.Clamp(player.inputMagnitude, 0.3f, 1f));
        anim.SetFloat("Rotation", player.moveRotation, 0.1f, Time.deltaTime);

        // Walk
        anim.SetBool("Walk", player.IsWalking && groundCheck.IsGrounded);

        // Run
        anim.SetBool("Run", player.IsRunning && groundCheck.IsGrounded);

        // Jump
        if (player.IsJumping) anim.SetTrigger("Jump");
        anim.SetFloat("yVelocity", player.rb.velocity.y);
        anim.SetBool("Grounded", groundCheck.IsGrounded);

        // In Danger
        if (lifebar.InDanger)
        {
            float emission = Mathf.Lerp(minIntensity, maxIntensity, (Mathf.Sin(Time.time * pulseSpeed) + 1f) / 2f);
            body.SetColor("_EmissionColor", Color.white * emission);
        }
        else body.SetColor("_EmissionColor", Color.white * minIntensity);
    }
}