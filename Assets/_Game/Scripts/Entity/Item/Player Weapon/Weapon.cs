using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    KNIFE,
    HAMMER,
    BOOMERANG,
}

public class Weapon : ItemEquip
{
    [SerializeField]
    internal Rigidbody rb;
    [SerializeField]
    internal Transform anim;
    internal Transform animTF;

    [SerializeField]
    private WeaponType weaponType;

    internal bool isHasFire;
    private Transform target;
    private Vector3 dirToTarget;
    internal Character owner;

    private void Start()
    {
        if (anim != null)
        {
            animTF = anim.transform;
        }

    }

    public void SetDir(Vector3 dir)
    {
        dirToTarget = dir;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ConstString.TAG_BOT) || other.CompareTag(ConstString.TAG_PLAYER))
        {

            IHit colliderHit = ColliderCache.GetHit(other);
            Character colliderCharacter = ColliderCache.GetCharacter(other);

            if (colliderCharacter == owner)
            {
                return;
            }

            // Debug.Log("Run hit");
            colliderHit.OnHit(owner.TF);
            OnDespawn();
        }
    }

    public virtual void Move()
    {
        rb.velocity = dirToTarget * 8.5f;
    }

    private void FixedUpdate()
    {
        if (isHasFire && anim != null)
        {
            Move();
        }
    }

    public override void OnDespawn()
    {
        base.OnDespawn();
        SimplePool.Despawn(this);
    }
}
