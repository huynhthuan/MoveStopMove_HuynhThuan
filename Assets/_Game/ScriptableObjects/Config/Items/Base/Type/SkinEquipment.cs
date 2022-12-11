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
        Debug.Log(message: $"Use Skin {(ItemId)itemId}");
        owner.characterEquipment.ApplySkin(itemsOfSkin, owner);

    }

    public override void UnUse(Character owner)
    {
        base.UnUse(owner);
        Debug.Log(message: $"Unuse Skin {(ItemId)itemId}");

        for (int i = 0; i < itemsOfSkin.Count; i++)
        {
            itemsOfSkin[i].UnUse(owner);
        }

    }
}