using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance;

    public int crumbCount = 0;
    public TMP_Text crumbText;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        UpdateCrumbUI();
    }

    public void AddCrumbs(int amount)
    {
        crumbCount += amount;
        UpdateCrumbUI();
    }

    private void UpdateCrumbUI()
    {
        crumbText.text = crumbCount.ToString();
    }
}
