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
    PANTS_ID,
}

public class DataManager : Singleton<DataManager>
{
    [SerializeField]
    internal DataWeaponConfig dataWeapon;

    [SerializeField]
    internal DataPantsConfig dataPants;

    [SerializeField]
    public PlayerData playerData;

    private string[] DataKeyString = { "GOLD", "USER_NAME", "CURRENT_STAGE", "WEAPON_ID", "WEAPON_MATERIAL_1", "WEAPON_MATERIAL_2", "PANTS_ID" };

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
        playerData.pantsId = PlayerPrefs.GetString(GetKey(DataKey.PANTS_ID), ConstString.DEFAULT_PANTS_ID);
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
        PlayerPrefs.SetString(GetKey(DataKey.PANTS_ID), playerData.pantsId);
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

    public PantsConfig GetPantsConfig(string pantsId)
    {
        Debug.Log("[~] Get pants config. Pants: " + pantsId);
        return dataPants.FindById(pantsId);
    }

}