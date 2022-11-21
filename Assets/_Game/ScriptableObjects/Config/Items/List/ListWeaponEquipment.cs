using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

[CreateAssetMenu(fileName = "New List Weapon Equipment", menuName = "Items/List/List Weapon Equipment")]
public class ListWeaponEquipment : ListEquipment
{
    public List<WeaponEquipment> weapons;
}

public enum WeaponId { WEAPON_1, WEAPON_2, WEAPON_3, WEAPON_4, WEAPON_5, WEAPON_6 }