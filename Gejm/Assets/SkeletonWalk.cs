using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonWalk : StateMachineBehaviour
{
    public float speed = 0.1f;
    private Transform player;
    private Rigidbody2D rb;
    public LayerMask groundLayer; // Warstwa ziemi
    public float groundCheckDistance = 0.1f; // Odleg³oœæ do sprawdzenia ziemi

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
    }

    // OnStateUpdate is called on each update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // SprawdŸ, czy przeciwnik dotyka ziemi
        if (!IsGrounded())
        {
            // Zatrzymaj animacjê ruchu
            animator.SetBool("isFalling", true);
            return;
        }
        else
        {
            animator.SetBool("isFalling", false);
        }

        Vector2 target = new Vector2(player.position.x, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);

        rb.MovePosition(newPos);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Mo¿esz tutaj dodaæ kod, który bêdzie wykonywany, gdy przeciwnik przestanie siê poruszaæ
    }

    // Sprawdza, czy przeciwnik dotyka ziemi
    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(rb.position, Vector2.down, groundCheckDistance, groundLayer);
        return hit.collider != null;
    }
}