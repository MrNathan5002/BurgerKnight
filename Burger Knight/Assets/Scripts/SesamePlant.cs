using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SesamePlant : MonoBehaviour
{
    public int hitsToGrow = 5;
    public float growRadius = 5f;
    public GameObject bunPickupPrefab;
    public Transform spawnPoint;

    private int currentHits = 0;
    private bool hasGrown = false;

    public void RegisterHit(Vector2 hitPosition)
    {
        if (hasGrown) return;

        if (Vector2.Distance(hitPosition, transform.position) <= growRadius)
        {
            currentHits++;
            Debug.Log("SesamePlant: Hit registered! (" + currentHits + "/" + hitsToGrow + ")");

            if (currentHits >= hitsToGrow)
            {
                Grow();
            }
        }
    }

    void Grow()
    {
        hasGrown = true;
        Debug.Log("SesamePlant: Fully grown!");
        Instantiate(bunPickupPrefab, spawnPoint.position, Quaternion.identity);
        Destroy(gameObject); 
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, growRadius);
    }
}
