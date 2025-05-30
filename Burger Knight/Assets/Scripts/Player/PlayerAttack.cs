using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int damage = 1;
    public float attackCooldown = 0.3f;
    public Vector2 knockbackForce = new Vector2(5f, 2f);
    public Vector2 recoilForce = new Vector2(3f, 1.5f);

    private float lastAttackTime;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= lastAttackTime + attackCooldown)
        {
            Attack();
        }
    }

    void Attack()
    {
        lastAttackTime = Time.time;

        // Detect enemies
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            // Deal damage
            enemy.GetComponent<EnemyHealth>()?.TakeDamage(damage);

            // Apply knockback to enemy
            Rigidbody2D enemyRb = enemy.GetComponent<Rigidbody2D>();
            if (enemyRb != null)
            {
                Vector2 direction = (enemy.transform.position - transform.position).normalized;
                enemyRb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
            }

            // Recoil knockback to player
            Vector2 recoilDir = (transform.position - enemy.transform.position).normalized;
            rb.AddForce(recoilDir * recoilForce, ForceMode2D.Impulse);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
}