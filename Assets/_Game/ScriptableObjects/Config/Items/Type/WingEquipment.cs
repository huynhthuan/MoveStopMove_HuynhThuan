using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

[CreateAssetMenu(fileName = "New Wing", menuName = "Items/Item Type/Wing Equipment")]

public class WingEquipment : ItemEquipment
{
    public override void Use(Character owner)
    {
        base.Use(owner);
    }
}