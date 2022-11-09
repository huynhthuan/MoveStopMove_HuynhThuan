using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public enum DataKey
{
    GOLD,
    USER_NAME,
    CURRENT_STAGE,
    WEAPON_ID,
    WEAPON_MATERIAL_1,
    WEAPON_MATERIAL_2,
}

public class DataManager : Singleton<DataManager>
{
    [SerializeField]
    internal DataWeaponConfig dataWeapon;

    [SerializeField]
    public PlayerData playerData;

    private string[] DataKeyString = { "GOLD", "USER_NAME", "CURRENT_STAGE", "WEAPON_ID", "WEAPON_MATERIAL_1", "WEAPON_MATERIAL_2" };

    public void OnInit()
    {
        // Load data player
        Debug.Log("Oninit data manager...");
        LoadData();
    }

    private void Start()
    {

    }

    public void LoadData()
    {
        Debug.Log("[~] Load data player.");
        playerData.currentStage = PlayerPrefs.GetInt(GetKey(DataKey.CURRENT_STAGE), 0);
        playerData.gold = PlayerPrefs.GetInt(GetKey(DataKey.GOLD), 100);
        playerData.userName = PlayerPrefs.GetString(GetKey(DataKey.CURRENT_STAGE), ConstString.DEFAULT_USER_NAME);
        playerData.weaponId = PlayerPrefs.GetString(GetKey(DataKey.WEAPON_ID), ConstString.DEFAULT_WEAPON_ID);
        playerData.weaponMaterial1 = PlayerPrefs.GetInt(GetKey(DataKey.CURRENT_STAGE), 0);
        playerData.weaponMaterial2 = PlayerPrefs.GetInt(GetKey(DataKey.CURRENT_STAGE), 0);
    }

    public void SaveData()
    {
        Debug.Log("[+] Save data player.");
        PlayerPrefs.SetInt(GetKey(DataKey.CURRENT_STAGE), playerData.currentStage);
        PlayerPrefs.SetInt(GetKey(DataKey.GOLD), playerData.gold);
        PlayerPrefs.SetString(GetKey(DataKey.USER_NAME), playerData.userName);
        PlayerPrefs.SetString(GetKey(DataKey.WEAPON_ID), playerData.weaponId);
        PlayerPrefs.SetInt(GetKey(DataKey.WEAPON_MATERIAL_1), playerData.weaponMaterial1);
        PlayerPrefs.SetInt(GetKey(DataKey.WEAPON_MATERIAL_2), playerData.weaponMaterial2);
    }

    public string GetKey(DataKey key)
    {
        return DataKeyString[(int)key];
    }

    public WeaponConfig GetWeaponConfig(string weaponId)
    {
        Debug.Log("[~] Get weapon config. Weapon: " + weaponId);
        return dataWeapon.FindById(weaponId);
    }

}