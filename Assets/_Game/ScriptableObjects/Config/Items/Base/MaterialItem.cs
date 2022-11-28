using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New material item", menuName = "Items/Item Type/Material")]
public class MaterialItem : ScriptableObject
{
    public MaterialId materialId;
    public Material material;

}

[System.Serializable]
public enum MaterialId
{
    MATERIAL_WEAPON_COLOR_1,
    MATERIAL_WEAPON_COLOR_2,
    MATERIAL_WEAPON_COLOR_3,
    MATERIAL_WEAPON_COLOR_4,
    MATERIAL_WEAPON_COLOR_5,
    MATERIAL_WEAPON_COLOR_6,
    MATERIAL_WEAPON_COLOR_7,
    MATERIAL_WEAPON_COLOR_8,
    MATERIAL_WEAPON_COLOR_9,
    MATERIAL_WEAPON_COLOR_10,
    MATERIAL_WEAPON_COLOR_11,
    MATERIAL_WEAPON_COLOR_12,
    MATERIAL_WEAPON_COLOR_13,
    MATERIAL_WEAPON_COLOR_14,
    MATERIAL_WEAPON_COLOR_15,
    MATERIAL_WEAPON_COLOR_16,
    MATERIAL_WEAPON_COLOR_17,
    MATERIAL_WEAPON_COLOR_18,
    MATERIAL_PANT_1,
    MATERIAL_PANT_2,
    MATERIAL_PANT_3,
    MATERIAL_PANT_4,
    MATERIAL_PANT_5,
    MATERIAL_PANT_6,
    MATERIAL_PANT_7,
    MATERIAL_PANT_8,
    MATERIAL_PANT_9,
}