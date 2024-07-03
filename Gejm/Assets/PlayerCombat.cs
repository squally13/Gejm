using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    public Transform AttackPoint;
    public float AttackRange = 0.5f;
    public LayerMask enemyLayers;
    public int attackDamage = 40;

    private PlayerMovement playerMovement;  // Reference to PlayerMovement

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();  // Get the PlayerMovement component
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    void Attack()
    {
        animator.SetTrigger("Attack");
        playerMovement.isAttacking = true;  // Set isAttacking to true

        // Stop the player from moving immediately
        playerMovement.StopMovement();

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }

        StartCoroutine(EndAttack());  // Start coroutine to reset isAttacking
    }

    IEnumerator EndAttack()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        playerMovement.isAttacking = false;  // Reset isAttacking to false
    }

    void OnDrawGizmosSelected()
    {
        if (AttackPoint == null)
            return;

        Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
    }
}
