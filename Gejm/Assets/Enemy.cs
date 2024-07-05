using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;

    public int maxHealth = 100;
    int currentHealth;

    AudioMan audioMan;

    private void Awake()
    {
        audioMan = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioMan>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        audioMan.PlaySFX(audioMan.enemyhit);

        currentHealth -= damage;

        animator.SetTrigger("Hurt");


        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy died!");

        animator.SetBool("isDead", true);

        GetComponent<Collider2D>().enabled = false;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Static;
        }

        this.enabled = false;

        audioMan.PlaySFX(audioMan.enemydeath);
    }

}