using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedPlanter : MonoBehaviour
{
    public GameObject sesamePlantPrefab;
    public Transform plantPoint;
    private PlayerMovement playerMovement;

    public float maxCooldown = 10f;
    private float currentCooldown = 0f;

    public bool CanPlant => currentCooldown <= 0f;

    void Start()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    void Update()
    {
        if (currentCooldown > 0f)
        {
            currentCooldown -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.E) && CanPlant && playerMovement.IsGrounded())
        {
            Instantiate(sesamePlantPrefab, plantPoint.position, Quaternion.identity);
            currentCooldown = maxCooldown;
        }
    }

    public void AddCooldownProgress(float amount)
    {
        currentCooldown -= amount;
        currentCooldown = Mathf.Clamp(currentCooldown, 0f, maxCooldown);
    }
}