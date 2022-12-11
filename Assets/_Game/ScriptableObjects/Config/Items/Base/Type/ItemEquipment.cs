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
        Debug.Log(message: $"Use {(ItemId)itemId}");
    }

    public override void UnUse(Character owner)
    {
        owner.characterEquipment.UnEquipItem(equipmentSlot);
        Debug.Log(message: $"Unuse {(ItemId)itemId}");
    }
}

[System.Serializable]
public class materialAvaibleItem
{
    public MaterialId materialId;
    public Sprite icon;
}