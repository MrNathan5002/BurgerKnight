using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 3;

    public void TakeDamage(int amount)
    {
        health -= amount;
        Debug.Log(name + " took " + amount + " damage.");

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Replace with animation or effects later
        Debug.Log(name + " died.");
        GetComponent<EnemyCrumbDropper>()?.DropCrumbs();
        Destroy(gameObject);
    }
}
