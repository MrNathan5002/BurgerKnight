using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class RespawnManager : MonoBehaviour
{
    public static RespawnManager Instance;

    [System.Serializable]
    public class SpawnScenePair
    {
        public string spawnID;
        public string sceneName;
    }

    public List<SpawnScenePair> spawnMappings;
    private Dictionary<string, string> spawnIDToScene = new Dictionary<string, string>();

    public string currentSpawnID;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            foreach (var pair in spawnMappings)
            {
                spawnIDToScene[pair.spawnID] = pair.sceneName;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetRespawnPoint(string spawnID)
    {
        if (spawnIDToScene.ContainsKey(spawnID))
        {
            currentSpawnID = spawnID;
        }
        else
        {
            Debug.LogWarning("Tried to set unknown spawnID: " + spawnID);
        }
    }

    public void RespawnPlayer()
    {
        if (!string.IsNullOrEmpty(currentSpawnID) && spawnIDToScene.ContainsKey(currentSpawnID))
        {
            string sceneToLoad = spawnIDToScene[currentSpawnID];
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogError("No valid spawn ID set for respawn.");
        }
    }

    // Called after scene load
    public void MovePlayerToSpawn()
    {
        Respawnpoint spawnPoint = GameObject.FindObjectsOfType<Respawnpoint>()
            .FirstOrDefault(sp => sp.spawnID == currentSpawnID);

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && spawnPoint != null)
        {
            player.transform.position = spawnPoint.transform.position;

            // Restore health after respawn
            PlayerHealth health = player.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.RestoreFullHealth();
            }
        }
        else
        {
            Debug.LogError("Player or spawn point not found.");
        }
    }
}