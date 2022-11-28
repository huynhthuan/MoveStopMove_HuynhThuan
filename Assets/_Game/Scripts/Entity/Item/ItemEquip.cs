using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemEquip : GameUnit
{
    [SerializeField]
    internal Rigidbody rb;
    [SerializeField]
    internal Transform anim;
    internal Transform animTF;
    internal Item itemData;

    private void Start()
    {
        if (anim != null)
        {
            animTF = anim.transform;
        }

    }

    public override void OnDespawn()
    {

    }

    public override void OnInit()
    {

    }
}