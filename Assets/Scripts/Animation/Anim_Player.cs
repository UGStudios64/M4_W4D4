using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimPlayer : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private PlayerController player;
    [SerializeField] private GroundCheck groundCheck;


    // GAME //-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-/-
    private void Awake()
    {
        if (!anim) anim = GetComponentInChildren<Animator>();
        if (!player) player = GetComponent<PlayerController>();
        if (!groundCheck) groundCheck = GetComponentInChildren<GroundCheck>();
    }

    private void Update()
    {
        // Walk
        anim.SetBool("Walk", player.IsWalking);
        anim.SetFloat("WalkSpeed", Mathf.Clamp(player.inputMagnitude, 0.3f, 1f));

        // Run
        anim.SetBool("Run", player.IsRunning && groundCheck.IsGrounded);

        // Jump
        anim.SetBool("Grounded", groundCheck.IsGrounded);
    }
}