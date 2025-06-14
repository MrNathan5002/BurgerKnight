using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crumb : MonoBehaviour
{
    public int value = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CurrencyManager.Instance.AddCrumbs(value);
            Destroy(gameObject);
        }
    }
}
