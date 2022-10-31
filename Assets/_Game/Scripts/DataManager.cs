using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DataKey
{
    GOLD,
    LEVEl_PROGRESS,
    EQUIPMENT
}

public class DataManager : Singleton<DataManager>
{
    [SerializeField]
    private DataWeaponConfig dataWeapon;

    public void OnInit() { }

    public void LoadData() { }

    public void SaveData() { }

    public WeaponConfig GetDataWeapon(string weaponId)
    {
        return dataWeapon.GetWeapon(weaponId);
    }
}
