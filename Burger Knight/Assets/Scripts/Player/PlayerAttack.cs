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

    [Header("Pogo Settings")]
    public float pogoForce = 15f;
    public float pogoGravityScale = 0.5f;
    public float pogoFloatTime = 0.2f;

    private Coroutine pogoCoroutine;

    [Header("Knockback Settings")]
    public Vector2 enemyKnockbackForce = new Vector2(5f, 2f);
    public Vector2 playerRecoilForce = new Vector2(3f, 1.5f);

    private float lastAttackTime;
    private PlayerMovement playerMovement;
    private Rigidbody2D rb;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
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

        Transform attackPoint = GetAttackPointFromInput();
        if (attackPoint == null) return;

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            // Deal damage
            enemy.GetComponent<EnemyHealth>()?.TakeDamage(damage);

            // Knockback enemy
            Rigidbody2D enemyRb = enemy.GetComponent<Rigidbody2D>();
            if (enemyRb != null)
            {
                Vector2 knockDir = (enemy.transform.position - transform.position).normalized;
                enemyRb.AddForce(knockDir * enemyKnockbackForce, ForceMode2D.Impulse);
            }

            // Pogo if attacking downward
            if (IsAttackingDownward())
            {
                if (pogoCoroutine != null) StopCoroutine(pogoCoroutine);
                pogoCoroutine = StartCoroutine(DoPogo());
            }
            // Recoil if attacking horizontally
            else if (!IsAttackingDownward())
            {
                Vector2 recoilDirection = (transform.position - attackPoint.position).normalized;
                rb.AddForce(new Vector2(recoilDirection.x * playerRecoilForce.x, playerRecoilForce.y), ForceMode2D.Impulse);
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

    IEnumerator DoPogo()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        PlayerMovement movement = GetComponent<PlayerMovement>();

        // Cancel downward velocity
        if (rb.velocity.y < 0) rb.velocity = new Vector2(rb.velocity.x, 0);

        // Suppress jump-cut immediately
        movement.suppressJumpCut = true;

        // Wait one frame to ensure Update() in PlayerMovement sees suppressJumpCut
        yield return null;

        // Apply upward pogo force
        rb.velocity = new Vector2(rb.velocity.x, pogoForce);

        // Make gravity lighter temporarily
        float originalGravity = rb.gravityScale;
        rb.gravityScale = pogoGravityScale;

        yield return new WaitForSeconds(pogoFloatTime);

        // Restore gravity and jump-cut
        rb.gravityScale = originalGravity;
        movement.suppressJumpCut = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPointRight) Gizmos.DrawWireSphere(attackPointRight.position, attackRange);
        if (attackPointLeft) Gizmos.DrawWireSphere(attackPointLeft.position, attackRange);
        if (attackPointUp) Gizmos.DrawWireSphere(attackPointUp.position, attackRange);
        if (attackPointDown) Gizmos.DrawWireSphere(attackPointDown.position, attackRange);
    }
}