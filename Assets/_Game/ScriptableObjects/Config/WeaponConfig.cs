using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/Item/Weapon", order = 1)]
public class WeaponConfig : ItemConfig
{
    public GameObject prefabWeapon;
    public Weapon prefabWeaponBullet;

    public List<MaterialItem> materialItems;
}

// [System.Serializable]
// public class MaterialItem
// {
//     public int materialIndex;
//     public List<materialAvaibleItem> materialAvaibleItems;
// }

// [System.Serializable]
// public class materialAvaibleItem
// {
//     public Material material;
//     public Sprite icon;
//     public int price;
// }