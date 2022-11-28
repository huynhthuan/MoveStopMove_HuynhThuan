using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New item skin", menuName = "Items/Item Type/Item Skin")]
public class ItemSkin : ScriptableObject
{
    public SkinId skinId;
    public List<MaterialId> skinMaterialIds = new List<MaterialId>();
}

public enum SkinId
{
    SKIN_WEAPON_CUSTOM,
    SKIN_KNIFE_1,
    SKIN_KNIFE_2,
    SKIN_KNIFE_3,
    SKIN_KNIFE_4,
    SKIN_HAMMER_1,
    SKIN_HAMMER_2,
    SKIN_HAMMER_3,
    SKIN_HAMMER_4,
    SKIN_BOOMERANG_1,
    SKIN_BOOMERANG_2,
    SKIN_BOOMERANG_3,
    SKIN_BOOMERANG_4,
}