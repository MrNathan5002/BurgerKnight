using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneInitializer : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(DelayedRespawn());
    }

    private IEnumerator DelayedRespawn()
    {
        // Wait just a moment to ensure everything in the scene (like spawn points) is initialized
        yield return new WaitForSeconds(0.1f);

        // Now call the RespawnManager to move the player to the correct spawn point
        RespawnManager.Instance.MovePlayerToSpawn();
    }
}
