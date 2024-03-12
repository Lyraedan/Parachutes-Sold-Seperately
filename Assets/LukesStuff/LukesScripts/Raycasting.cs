using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycasting
{
    private Transform transform;

    public Raycasting(Transform transform)
    {
        this.transform = transform;
    }

    public RaycastHit raycast(Vector3 direction)
    {
        int layerMask = 1 << 8;

        layerMask = ~layerMask;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(direction), out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(direction) * hit.distance, Color.yellow);
        } else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(direction) * 1000, Color.white);
        }

        return hit;
    }
}
