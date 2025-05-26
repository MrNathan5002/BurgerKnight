using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class load_game_button_script : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadScene(1);//main game scene
    }
}
