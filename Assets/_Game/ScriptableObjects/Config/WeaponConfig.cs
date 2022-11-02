using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/Item/Weapon", order = 1)]
public class WeaponConfig : ItemConfig
{
    public GameObject prefabWeapon;
    public Weapon prefabWeaponBullet;
}
