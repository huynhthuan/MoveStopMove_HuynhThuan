using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/Item Equipment")]

public class WeaponEquipment : ItemEquipment
{
    public Weapon weaponBullet;
}