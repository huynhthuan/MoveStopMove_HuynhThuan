using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataWeapon", menuName = "ScriptableObjects/Database/DatabaseWeapon", order = 1)]
public class DataWeaponConfig : ScriptableObject
{
    public List<WeaponConfig> weaponItems;

    public WeaponConfig GetWeapon(string id)
    {
        return weaponItems.Find(item => item.id == id);
    }
}
