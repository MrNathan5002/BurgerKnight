using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolFloorCheck : MonoBehaviour
{
    [SerializeField] private float speed;
    private bool isFacingRight = true;

    [SerializeField] private Transform groundChecker;
    [SerializeField] private float checkDistance;
    [SerializeField] private LayerMask layerMask;

    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        RaycastHit2D ray = Physics2D.Raycast(groundChecker.position, Vector2.down, checkDistance, layerMask);
        if (!ray.collider)
        {
            Flip();
        }

        if (isFacingRight)
        {
            transform.eulerAngles = Vector3.zero;
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    public void Flip()
    {
        isFacingRight = !isFacingRight;
    }
}