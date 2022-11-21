using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Items/Weapon Equipment")]
public class WeaponEquipment : Item
{
    public WeaponId itemId;
    public Weapon weaponPrefab;
    public Weapon weaponBullet;
    public List<MaterialItem> materialItems;
}

[System.Serializable]
public class MaterialItem
{
    public int materialIndex;
    public List<materialAvaibleItem> materialAvaibleItems;
}

[System.Serializable]
public class materialAvaibleItem
{
    public Material material;
    public Sprite icon;
    public int price;
}