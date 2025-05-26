using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Sprite fullBun;
    public Sprite bittenBun;

    public Image[] bunImages;

    public void UpdateHealth(int currentHealth)
    {
        for (int i = 0; i < bunImages.Length; i++)
        {
            if (i < currentHealth)
            {
                bunImages[i].sprite = fullBun;
            }
            else
            {
                bunImages[i].sprite = bittenBun;
            }
        }
    }
}