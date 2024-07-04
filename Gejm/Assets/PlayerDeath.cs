using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public Animator animator;
    private PlayerMovement playerMovement;  // Reference to PlayerMovement

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();  // Get the PlayerMovement component
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("KillPlayer"))
        {
            Debug.Log("Player Dead!");
            animator.SetBool("isDead", true);
            playerMovement.isDead = true;  // Set isDead to true

            StartCoroutine(WaitForAnimation());
        }
    }

    IEnumerator WaitForAnimation()
    {
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName("death"))
        {
            yield return null;
        }

        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yield return null;
        }

        animator.speed = 0;
    }
}
