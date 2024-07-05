using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonWalk : StateMachineBehaviour
{
    public float speed = 0.1f;
    public float attackRange = 2f;
    public float detectionRange = 5f;  // Range within which the skeleton starts moving towards the player

    private Transform player;
    private Rigidbody2D rb;
    private FlipEnemy flipenemy;

    [HideInInspector] public Transform groundCheck;
    [HideInInspector] public LayerMask groundLayer;
    private float checkRadius = 0.1f;  // Radius for the ground check

    private bool isGrounded;
    private bool playerInRange;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        flipenemy = animator.GetComponent<FlipEnemy>();
    }

    // OnStateUpdate is called on each update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Check if the player is within the detection range
        playerInRange = Vector2.Distance(player.position, rb.position) <= detectionRange;

        if (playerInRange)
        {
            flipenemy.LookAtPlayer();

            // Check if the groundCheck is touching the ground layer
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);

            if (isGrounded)
            {
                Vector2 target = new Vector2(player.position.x, rb.position.y);
                Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);

                rb.MovePosition(newPos);

                // Set isMoving to true if the skeleton is moving
                animator.SetBool("isMoving", true);
            }
            else
            {
                // Set isMoving to false if the skeleton is not grounded
                animator.SetBool("isMoving", false);
            }
        }
        else
        {
            // Set isMoving to false if the player is out of range
            animator.SetBool("isMoving", false);
        }

        if (Vector2.Distance(player.position, rb.position) <= attackRange)
        {
            animator.SetTrigger("Attack");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }
}
