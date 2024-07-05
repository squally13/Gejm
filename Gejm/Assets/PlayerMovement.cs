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

    public GameObject background;

    public bool isAttacking = false;  // Variable to check if attacking
    public bool isDead = false;  // Variable to check if dead

    void Start()
    {
        // Initialize any necessary components or variables here
    }

    void Update()
    {
        if (isDead)
        {
            horizontalMove = 0;  // Stop movement if dead
            return;
        }

        if (!isAttacking)
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        }
        else
        {
            horizontalMove = 0;  // Stop movement if attacking
        }

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump") && !isAttacking)
        {
            jump = true;
            animator.SetBool("isJumping", true);
        }

        if (Input.GetButtonDown("Crouch") && !isAttacking)
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "WIN")
        {
            background.gameObject.SetActive(true);
            Time.timeScale = 0;
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
        horizontalMove = 0f;
        controller.Move(0, crouch, jump);  // Apply zero movement to stop sliding
    }

    void FixedUpdate()
    {
        if (isDead)
        {
            // Stop the player if dead
            controller.Move(0, crouch, jump);
            return;
        }

        if (!isAttacking)
        {
            controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        }
        else
        {
            controller.Move(0, crouch, jump);
        }
        jump = false;
    }
}