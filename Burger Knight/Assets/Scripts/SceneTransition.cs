using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string targetSceneName;
    public string spawnPointName; // Name of the spawn point in the target scene

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SceneTransitionManager.targetSpawnPoint = spawnPointName;
            SceneManager.LoadScene(targetSceneName);
        }
    }
}
