using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCrumbDropper : MonoBehaviour
{
    public GameObject crumbPrefab;
    public int crumbAmount = 5;
    public float spreadForce = 2f;

    public void DropCrumbs()
    {
        for (int i = 0; i < crumbAmount; i++)
        {
            GameObject crumb = Instantiate(crumbPrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = crumb.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 randomDir = Random.insideUnitCircle.normalized;
                rb.AddForce(randomDir * spreadForce, ForceMode2D.Impulse);
            }
        }
    }
}
