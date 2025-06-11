using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SaveTools
{
    [MenuItem("Tools/Clear Save Data %#r")] // Ctrl/Cmd + Shift + R
    public static void ClearSave()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("Save data has been cleared.");
    }
}
