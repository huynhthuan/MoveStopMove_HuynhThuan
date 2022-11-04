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
        transform.position = target.position + offset;
    }
}
