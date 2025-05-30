using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public static string targetSpawnPoint;
    private static SceneTransitionManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (string.IsNullOrEmpty(targetSpawnPoint)) return;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject spawn = GameObject.Find(targetSpawnPoint);
        if (player != null && spawn != null)
        {
            player.transform.position = spawn.transform.position;
        }

        targetSpawnPoint = null;
    }
}