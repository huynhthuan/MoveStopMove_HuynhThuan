using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class Item : ScriptableObject
{
    public EquipmentSlot equipmentSlot;
    public ItemId itemId;
    public bool isShowOnStore = true;
    public Sprite thumbnail;

    public virtual void Use(Character owner)
    {

    }
}

public enum ItemId
{
    HEAD_1,
    HEAD_2,
    HEAD_3,
    HEAD_4,
    KINIFE,
    HAMMER,
    ARROW,
    AXE_1,
    AXE_2,
    CANDY_1,
    CANDY_2,
    CANDY_3,
    GUN_UZI,
    THUNDER_Z,
    BOOMERANG,
    SHIELD_1,
    SHIELD_2,
    SHIELD_3,
    SHIELD_4,
    WING_1,
    WING_2,
    WING_3,
    WING_4,
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
    HEAD_5,
    HEAD_6,
    HEAD_7,
    HEAD_8,
    HEAD_9,
    HEAD_10,
    HEAD_11,
    HEAD_12,
    HEAD_13,
}