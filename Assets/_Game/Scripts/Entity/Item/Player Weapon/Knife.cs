using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : Weapon
{
    private float speedRotation = 440f;
    public override void Move()
    {
        base.Move();

        // Hammer

        // Quaternion animRotLocal = animTF.localRotation;
        // animTF.Rotate(0f, 0f, speedRotation * Time.fixedDeltaTime);
    }

    // Boomerang

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.CompareTag(ConstString.TAG_BOT) || other.CompareTag(ConstString.TAG_PLAYER))
    //     {

    //         IHit colliderHit = ColliderCache.GetHit(other);
    //         Character colliderCharacter = ColliderCache.GetCharacter(other);

    //         if (colliderCharacter == owner)
    //         {
    //             return;
    //         }

    //         Debug.Log("Run hit");
    //         colliderHit.OnHit(owner.TF);

    //         Vector3 dirToOwner = (owner.TF.position - TF.position).normalized;
    //         SetDir(dirToOwner);
    //     }
    // }
}
