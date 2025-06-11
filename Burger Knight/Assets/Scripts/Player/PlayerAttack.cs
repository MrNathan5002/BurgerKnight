using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack Points")]
    public Transform attackPointRight;
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
    private Animator animator;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= lastAttackTime + attackCooldown)
        {
            Attack();
            animator.SetTrigger("Attack");
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

            // Pogo if attacking downward and NOT grounded
            if (IsAttackingDownward() && !IsGrounded())
            {
                if (pogoCoroutine != null) StopCoroutine(pogoCoroutine);
                pogoCoroutine = StartCoroutine(DoPogo());
            }
            else
            {
                // Horizontal recoil
                Vector2 recoilDirection = (transform.position - attackPoint.position).normalized;
                rb.AddForce(new Vector2(recoilDirection.x * playerRecoilForce.x, playerRecoilForce.y), ForceMode2D.Impulse);
            }
        }
    }

    Transform GetAttackPointFromInput()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        // Upward attack
        if (y > 0.5f && attackPointUp != null)
            return attackPointUp;

        // Downward attack only if airborne
        if (y < -0.5f && !IsGrounded() && attackPointDown != null)
            return attackPointDown;

        // Horizontal attack — always use right point since player is flipped
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

    private bool IsGrounded()
    {
        return playerMovement != null && playerMovement.IsGrounded();
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPointRight) Gizmos.DrawWireSphere(attackPointRight.position, attackRange);
        if (attackPointUp) Gizmos.DrawWireSphere(attackPointUp.position, attackRange);
        if (attackPointDown) Gizmos.DrawWireSphere(attackPointDown.position, attackRange);
    }
}