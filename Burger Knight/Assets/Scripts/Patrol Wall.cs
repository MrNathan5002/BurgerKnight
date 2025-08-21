using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolWall : MonoBehaviour
{
    private PatrolFloorCheck patrol;

    private void Start()
    {
        patrol = GetComponentInParent<PatrolFloorCheck>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            patrol.Flip();
        }
    }
}
