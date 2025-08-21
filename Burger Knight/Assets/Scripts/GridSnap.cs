using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GridSnap : MonoBehaviour
{
    public float cellSize = 1f; // match your grid PPU scale
    void Update()
    {
        if (!Application.isPlaying)
        {
            Vector3 pos = transform.position;
            pos.x = Mathf.Round(pos.x / cellSize) * cellSize;
            pos.y = Mathf.Round(pos.y / cellSize) * cellSize;
            transform.position = pos;
        }
    }
}
