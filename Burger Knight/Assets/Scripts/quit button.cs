using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quitbutton : MonoBehaviour
{
    public void ExitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
        Debug.Log("Game is exiting");
    }
}