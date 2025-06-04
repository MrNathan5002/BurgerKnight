using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 5;
    public float invincibilityDuration = 1f;

    private int currentHealth;
    private bool isInvincible;
    public bool isDead = false;
    private float invincibilityTimer;

    public float knockbackForceX = 5f;
    public float knockbackForceY = 3f;
    private Rigidbody2D rb;

    private HealthUI healthUI;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        healthUI = FindObjectOfType<HealthUI>();
        healthUI.UpdateHealth(currentHealth); // Set initial display
    }

    private void Update()
    {
        if (isInvincible)
        {
            invincibilityTimer -= Time.deltaTime;
            if (invincibilityTimer <= 0)
            {
                isInvincible = false;
            }
        }
    }

    public void TakeDamage(int amount, Vector2 sourcePosition)
    {
        if (isInvincible) return;

        currentHealth -= amount;
        isInvincible = true;
        invincibilityTimer = invincibilityDuration;

        Debug.Log("Player took " + amount + " damage. Remaining HP: " + currentHealth);

        // Knockback
        Vector2 knockbackDir = ((Vector2)transform.position - sourcePosition).normalized;
        Vector2 force = new Vector2(knockbackDir.x * knockbackForceX, knockbackForceY);
        rb.velocity = Vector2.zero; // Clear current momentum
        rb.AddForce(force, ForceMode2D.Impulse);

        if (currentHealth <= 0)
        {
            Die();
        }

        if (healthUI != null)
        {
            healthUI.UpdateHealth(currentHealth);
        }
    }

    private void Die()
    {
        isDead = true;
        Debug.Log("Player Died");

        // Call the Respawn Manager to handle it
        RespawnManager.Instance.RespawnPlayer();
    }

    public void RestoreFullHealth()
    {
        currentHealth = maxHealth;
        isDead = false;
        healthUI.UpdateHealth(currentHealth);
    }

    public void Heal(int amount)
    {
        if (isDead) return;

        currentHealth += amount;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        Debug.Log("Player healed " + amount + " HP. Current HP: " + currentHealth);

        if (healthUI != null)
        {
            healthUI.UpdateHealth(currentHealth);
        }
    }
}