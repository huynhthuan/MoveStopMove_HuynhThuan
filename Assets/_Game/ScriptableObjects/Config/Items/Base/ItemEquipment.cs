using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
public class ItemEquipment : Item
{
    public Weapon prefab;
    public List<materialAvaibleItem> materialAvaibleItem;

    public override void Use(Character owner)
    {
        base.Use(owner);
        owner.characterEquipment.EquipItem(itemId, equipmentSlot);
    }
}



[System.Serializable]
public class materialAvaibleItem
{
    public MaterialId materialId;
    public Sprite icon;
}