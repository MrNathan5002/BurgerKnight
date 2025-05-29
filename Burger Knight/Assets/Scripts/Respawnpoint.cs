using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawnpoint : MonoBehaviour
{
    public string spawnID; // Must be unique!

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            RespawnManager.Instance.SetRespawnPoint(spawnID);
            Debug.Log("Spawn point set to: " + spawnID);
        }
    }
}
