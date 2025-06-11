using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    public GameObject breakEffect; // Optional: particle or debris effect
    public float destroyDelay = 0.1f;

    public void Break()
    {
        if (breakEffect != null)
            Instantiate(breakEffect, transform.position, Quaternion.identity);

        Destroy(gameObject, destroyDelay);
    }
}