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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("isJumping", true);
        }

        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
            runSpeed = 0f;
        } else if (Input.GetButtonUp("Crouch")) 
        {
            crouch = false;
            runSpeed = 40f;
        }
    }

    public void OnLanding ()
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

    void FixedUpdate()
    {
        // Character movement
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;

    }
}
