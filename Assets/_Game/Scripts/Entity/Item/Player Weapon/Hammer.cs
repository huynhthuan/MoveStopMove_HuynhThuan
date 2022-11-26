using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : Weapon
{
    private float speedRotation = 540f;
    public override void Move()
    {
        base.Move();

        Quaternion animRotLocal = animTF.localRotation;
        animTF.Rotate(0f, 0f, speedRotation * Time.fixedDeltaTime);
    }
}
