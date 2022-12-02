using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

[CreateAssetMenu(fileName = "New Skin", menuName = "Items/Item Type/Skin Equipment")]

public class SkinEquipment : Item
{
    public List<Item> itemsOfSkin = new List<Item>();
    public override void Use(Character owner)
    {
        base.Use(owner);
        owner.characterEquipment.ApplySkin(itemsOfSkin, owner);
    }

    public override void UnUse(Character owner)
    {
        base.UnUse(owner);
        owner.characterEquipment.RemoveSkin(itemsOfSkin, owner);
    }
}