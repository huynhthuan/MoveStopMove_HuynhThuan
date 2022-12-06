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
    public int price;

    public virtual void Use(Character owner)
    {
        Debug.Log($"---- Use item {itemId}");
    }

    public virtual void UnUse(Character owner)
    {
        Debug.Log($"---- Un use item {itemId}");
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
    SKIN_CHARACTER_DEVIL,
    EMPTY,
    PANT_0,
    BODY_0,
    BODY_1,
    BODY_2,
    BODY_3,
    BODY_4,
    BODY_5,
    BODY_6,
    BODY_7,
    BODY_8,
    BODY_9,
    BODY_10,
    BODY_ANGLE,
    BODY_DEADPOOL,
    BODY_THOR,
    BODY_DEVIL,
    SKIN_CHARACTER_THOR,
    SKIN_CHARACTER_DEADPOOL,
    SKIN_CHARACTER_ANGLE,
}