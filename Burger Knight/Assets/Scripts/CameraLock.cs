using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class CameraLock : MonoBehaviour
{
    public bool lockCamera = false;

    public Bounds GetBounds()
    {
        return GetComponent<BoxCollider2D>().bounds;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = lockCamera ? Color.red : Color.green;
        Gizmos.DrawWireCube(GetComponent<BoxCollider2D>().bounds.center, GetComponent<BoxCollider2D>().bounds.size);
    }
}
