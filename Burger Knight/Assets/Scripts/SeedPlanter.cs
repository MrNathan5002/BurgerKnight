using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedPlanter : MonoBehaviour
{
    public GameObject sesamePlantPrefab;
    public Transform plantPoint;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Instantiate(sesamePlantPrefab, plantPoint.position, Quaternion.identity);
        }
    }
}
