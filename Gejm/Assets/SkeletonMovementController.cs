using UnityEngine;

public class SkeletonMovementController : MonoBehaviour
{
    public Animator animator;
    public float detectionRange = 5f;  // Range within which the skeleton starts moving towards the player

    private Transform player;
    private Rigidbody2D rb;
    private FlipEnemy flipEnemy;

    private bool isPlayerInRange = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        flipEnemy = GetComponent<FlipEnemy>();
    }

    private void Update()
    {
        // Check if player is in detection range
        if (Vector2.Distance(player.position, transform.position) <= detectionRange)
        {
            if (!isPlayerInRange)
            {
                isPlayerInRange = true;
                animator.SetBool("isMoving", true);  // Set isMoving to true when player enters range
            }
        }
        else
        {
            if (isPlayerInRange)
            {
                isPlayerInRange = false;
                animator.SetBool("isMoving", false);  // Set isMoving to false when player exits range
            }
        }
    }
}
