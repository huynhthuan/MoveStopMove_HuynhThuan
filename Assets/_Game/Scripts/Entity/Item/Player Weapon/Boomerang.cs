using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : Weapon
{
    public override void Move()
    {
        base.Move();
    }

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
