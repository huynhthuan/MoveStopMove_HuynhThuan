using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
public class ItemEquipment : Item
{
    public GameObject prefab;

    public override void Use(Character owner)
    {
        base.Use(owner);
        owner.characterEquipment.EquipItem(itemId, equipmentSlot);
    }

    public override void UnUse(Character owner)
    {
        owner.characterEquipment.UnEquipItem(equipmentSlot);
    }
}

[System.Serializable]
public class materialAvaibleItem
{
    public MaterialId materialId;
    public Sprite icon;
}