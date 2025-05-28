using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject playerPrefab;
    public string spawnPointName = "SpawnPoint_Entrance";

    void Start()
    {
        // Don't spawn a player if one already exists
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            return;
        }

        GameObject spawnPoint = GameObject.Find(spawnPointName);
        if (spawnPoint != null)
        {
            GameObject player = Instantiate(playerPrefab, spawnPoint.transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning($"Spawn point '{spawnPointName}' not found.");
        }
    }
}