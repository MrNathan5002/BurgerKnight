using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedPlanter : MonoBehaviour
{
    public GameObject sesamePlantPrefab;
    public Transform plantPoint;

    private PlayerMovement playerMovement;

    void Start()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerMovement.IsGrounded())
        {
            Instantiate(sesamePlantPrefab, plantPoint.position, Quaternion.identity);
        }
    }
}
