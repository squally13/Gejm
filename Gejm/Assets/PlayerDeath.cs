using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public Animator animator;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("KillPlayer"))
        {
            Debug.Log("Player Dead!");
            animator.SetBool("isDead", true);

            StartCoroutine(WaitForAnimation());
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
}
