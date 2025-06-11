using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    public string wallID; // unique per wall, like "wall_jungle_01"

    public GameObject breakEffect; // Optional: particle or debris effect
    public float destroyDelay = 0.1f;
    public float wallHealth = 1;

    void Start()
    {
        if (SaveManager.Instance.GetFlag(wallID))
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int amount)
    {
        wallHealth -= amount;
        Debug.Log(name + " took " + amount + " damage.");

        if (wallHealth <= 0)
        {
            Break();
        }
    }

    public void Break()
    {
        if (breakEffect != null)
        {
            Instantiate(breakEffect, transform.position, Quaternion.identity);
        }

        SaveManager.Instance.SetFlag(wallID, true);
        Destroy(gameObject, destroyDelay);
    }
}