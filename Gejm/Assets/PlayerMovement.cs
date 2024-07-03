using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    public float runSpeed = 40f;

    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;

    public bool isAttacking = false;  // New variable to check if attacking

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!isAttacking)  // Check if the player is attacking
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        }
        else
        {
            horizontalMove = 0;  // Stop movement if attacking
        }

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump") && !isAttacking)  // Allow jumping only if not attacking
        {
            jump = true;
            animator.SetBool("isJumping", true);
        }

        if (Input.GetButtonDown("Crouch") && !isAttacking)  // Allow crouching only if not attacking
        {
            crouch = true;
            runSpeed = 0f;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
            runSpeed = 40f;
        }
    }

    public void OnLanding()
    {
        animator.SetBool("isJumping", false);
    }

    public void OnCrouching(bool isCrouching)
    {
        animator.SetBool("isCrouching", isCrouching);
    }

    public void OnCrouchingTransitionDown(bool isCrouchingTransD)
    {
        animator.SetBool("isCrouching", isCrouchingTransD);
    }

    public void OnCrouchingTransitionUp(bool isCrouchingTransU)
    {
        animator.SetBool("isCrouching", isCrouchingTransU);
    }

    public void StopMovement()
    {
        // Stop the player's horizontal movement immediately
        horizontalMove = 0f;
        controller.Move(0, crouch, jump);  // Apply zero movement to stop sliding
    }

    void FixedUpdate()
    {
        // Character movement
        if (!isAttacking)  // Move the character only if not attacking
        {
            controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        }
        else
        {
            // Stop the player if they are attacking
            controller.Move(0, crouch, jump);
        }
        jump = false;
    }
}
