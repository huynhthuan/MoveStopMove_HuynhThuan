using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    KNIFE,
    HAMMER,
    BOOMERANG,
}

public class Weapon : GameUnit
{
    [SerializeField]
    private WeaponType weaponType;
    [SerializeField]
    private Rigidbody rb;

    internal bool isHasFire;
    private Transform target;
    private Vector3 dirToTarget;


    private Character character;


    public void SetDir(Vector3 dir)
    {
        dirToTarget = dir;
    }

    public override void OnInit()
    {

        rb.velocity = dirToTarget * 5f;
    }


    public override void OnDespawn()
    {
        SimplePool.Despawn(this);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag(ConstString.TAG_BOT))
        {
            IHit colliderHit = ColliderCacheIHit.GetHit(other);
            colliderHit.OnHit();
            OnDespawn();
        }
    }
}
