using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100;
    public GameObject deathEffect;
    public Animator animator;
    private PlayerMovement playerMovement;

    private bool isDead;
    
    public GameManager gameManager;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        StartCoroutine(DamageAnimation());

        if (health <= 0 && !isDead)
        {
            isDead = true;
            Die();
        }
    }

    void Die()
    {
        animator.SetBool("isDead", true);
        playerMovement.isDead = true;
        StartCoroutine(HandleDeath());
    }

    IEnumerator HandleDeath()
    {
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName("death"))
        {
            yield return null;
        }

        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yield return null;
        }

        gameManager.gameOver();
        Debug.Log("Dead and GameOver");
    }

    IEnumerator DamageAnimation()
    {
        SpriteRenderer[] srs = GetComponentsInChildren<SpriteRenderer>();

        for (int i = 0; i < 3; i++)
        {
            foreach (SpriteRenderer sr in srs)
            {
                Color c = sr.color;
                c.a = 0;
                sr.color = c;
            }

            yield return new WaitForSeconds(.1f);

            foreach (SpriteRenderer sr in srs)
            {
                Color c = sr.color;
                c.a = 1;
                sr.color = c;
            }

            yield return new WaitForSeconds(.1f);
        }
    }
}
