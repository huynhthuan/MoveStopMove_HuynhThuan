using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : Singleton<CameraFollow>
{
    [SerializeField]
    internal Transform target;

    [SerializeField]
    internal Vector3 offset;
    internal Transform TF;

    private void Start()
    {
        TF = transform;
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            TF.position = target.position + offset;
        }
    }
}
