using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Items/Item Type/Weapon Equipment")]

public class WeaponEquipment : ItemEquipment
{
    public string itemName;
    [TextArea]
    public string description;
    public List<materialAvaibleItem> materialAvaibleItem;
    public Weapon weaponBullet;
    public List<ItemSkin> itemSkins = new List<ItemSkin>();

    public override void Use(Character owner)
    {
        base.Use(owner);
    }
}