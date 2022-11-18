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
    [SerializeField]
    private Transform anim;
    internal bool isHasFire;
    private Transform target;
    private Vector3 dirToTarget;
    internal Character owner;
    internal Transform animTF;
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

    public override void OnDespawn()
    {
        SimplePool.Despawn(this);
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

            Debug.Log("Run hit");
            colliderHit.OnHit(owner.TF);
            OnDespawn();
        }
    }

    public virtual void Move()
    {
        rb.velocity = dirToTarget * 8.5f;
    }

    public override void OnInit()
    {

    }

    private void FixedUpdate()
    {
        if (isHasFire && anim != null)
        {
            Move();
        }
    }
}
