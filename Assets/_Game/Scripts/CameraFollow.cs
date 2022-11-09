using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : Singleton<CameraFollow>
{
    [SerializeField]
    internal Transform target;

    [SerializeField]
    private Vector3 offset;

    private void FixedUpdate()
    {
        if (target != null)
        {
            transform.position = target.position + offset;
        }
    }
}
