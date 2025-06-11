using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    private Dictionary<string, bool> boolFlags = new Dictionary<string, bool>();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetFlag(string key, bool value)
    {
        boolFlags[key] = value;
        PlayerPrefs.SetInt(key, value ? 1 : 0);
        PlayerPrefs.Save();
    }

    public bool GetFlag(string key)
    {
        if (boolFlags.TryGetValue(key, out bool value))
            return value;

        // If not in memory, try loading from PlayerPrefs
        value = PlayerPrefs.GetInt(key, 0) == 1;
        boolFlags[key] = value;
        return value;
    }
}
