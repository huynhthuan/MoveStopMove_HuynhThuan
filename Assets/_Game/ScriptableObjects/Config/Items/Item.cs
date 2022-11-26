using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class Item : ScriptableObject
{
    public ItemId itemId;
    public string itemName;
    [TextArea]
    public string description;
    public int price;
    public bool isShowOnStore = true;

    public virtual void Use()
    {
        Debug.Log("Equipment " + itemName);
    }
}

public enum EquipmentSlot { HEAD, WEAPON, SHIELD, WING, TAIL, PANT }
public enum ItemId
{
    HEAD_1,
    KINIFE_1,
    HAMMER_1,
    SHIELD_1,
    WING_1,
    TAIL_1,
    PANT_1,
    PANT_2,
    PANT_3,
    PANT_4,
    PANT_5,
    PANT_6,
    PANT_7,
    PANT_8,
    PANT_9,
}