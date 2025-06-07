using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBoundary : MonoBehaviour
{
    public Vector2 minBounds;
    public Vector2 maxBounds;

    void Start()
    {
        CameraController cam = Camera.main.GetComponent<CameraController>();
        if (cam != null)
        {
            //cam.SetBounds(minBounds, maxBounds);
        }
    }
}
