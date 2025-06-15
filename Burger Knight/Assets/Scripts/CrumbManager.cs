using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance;

    public int crumbCount = 0;
    public TMP_Text crumbText;

    private const string CrumbKey = "CrumbCount";

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        LoadCrumbs();
        UpdateCrumbUI();
    }

    public void AddCrumbs(int amount)
    {
        crumbCount += amount;
        UpdateCrumbUI();
        SaveCrumbs();
    }

    public void SpendCrumbs(int amount)
    {
        crumbCount = Mathf.Max(crumbCount - amount, 0);
        UpdateCrumbUI();
        SaveCrumbs();
    }

    private void UpdateCrumbUI()
    {
        if (crumbText != null)
            crumbText.text = crumbCount.ToString();
    }

    private void SaveCrumbs()
    {
        PlayerPrefs.SetInt(CrumbKey, crumbCount);
        PlayerPrefs.Save();
    }

    private void LoadCrumbs()
    {
        crumbCount = PlayerPrefs.GetInt(CrumbKey, 0);
    }
}
