using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack Points")]
    public Transform attackPointRight;
    public Transform attackPointLeft;
    public Transform attackPointUp;
    public Transform attackPointDown;

    [Header("Attack Settings")]
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int damage = 1;
    public float attackCooldown = 0.3f;

    private float lastAttackTime;
    private PlayerMovement playerMovement;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
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

        Transform attackPoint = GetAttackPointFromInput();
        if (attackPoint == null) return;

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            // Deal damage
            enemy.GetComponent<EnemyHealth>()?.TakeDamage(damage);

            // Pogo if attacking downward
            if (IsAttackingDownward())
            {
                playerMovement.Jump();
            }
        }
    }

    Transform GetAttackPointFromInput()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if (y > 0.5f && attackPointUp != null) return attackPointUp;
        if (y < -0.5f && attackPointDown != null) return attackPointDown;
        if (x < 0 && attackPointLeft != null) return attackPointLeft;
        if (x > 0 && attackPointRight != null) return attackPointRight;

        // Default to right if no direction input
        return attackPointRight;
    }

    bool IsAttackingDownward()
    {
        return Input.GetAxisRaw("Vertical") < -0.5f;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPointRight) Gizmos.DrawWireSphere(attackPointRight.position, attackRange);
        if (attackPointLeft) Gizmos.DrawWireSphere(attackPointLeft.position, attackRange);
        if (attackPointUp) Gizmos.DrawWireSphere(attackPointUp.position, attackRange);
        if (attackPointDown) Gizmos.DrawWireSphere(attackPointDown.position, attackRange);
    }
}