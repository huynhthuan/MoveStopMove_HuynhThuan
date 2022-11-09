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
    public PlayerData currentPlayerData;

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
        Debug.Log("Load data...");
        currentPlayerData.currentStage = PlayerPrefs.GetInt(GetKeyDataString(DataKey.CURRENT_STAGE), 0);
        currentPlayerData.gold = PlayerPrefs.GetInt(GetKeyDataString(DataKey.GOLD), 100);
        currentPlayerData.userName = PlayerPrefs.GetString(GetKeyDataString(DataKey.CURRENT_STAGE), "Unknow name");
        currentPlayerData.weaponId = PlayerPrefs.GetString(GetKeyDataString(DataKey.WEAPON_ID), "Weapon_1");
        currentPlayerData.weaponMaterial1 = PlayerPrefs.GetInt(GetKeyDataString(DataKey.CURRENT_STAGE), 0);
        currentPlayerData.weaponMaterial2 = PlayerPrefs.GetInt(GetKeyDataString(DataKey.CURRENT_STAGE), 0);
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt(GetKeyDataString(DataKey.CURRENT_STAGE), currentPlayerData.currentStage);
        PlayerPrefs.SetInt(GetKeyDataString(DataKey.GOLD), currentPlayerData.gold);
        PlayerPrefs.SetString(GetKeyDataString(DataKey.USER_NAME), currentPlayerData.userName);
        PlayerPrefs.SetString(GetKeyDataString(DataKey.WEAPON_ID), currentPlayerData.weaponId);
        PlayerPrefs.SetInt(GetKeyDataString(DataKey.WEAPON_MATERIAL_1), currentPlayerData.weaponMaterial1);
        PlayerPrefs.SetInt(GetKeyDataString(DataKey.WEAPON_MATERIAL_2), currentPlayerData.weaponMaterial2);
    }

    public string GetKeyDataString(DataKey key)
    {
        return DataKeyString[(int)key];
    }

    public WeaponConfig GetDataWeapon(string weaponId)
    {
        return dataWeapon.weaponItems.Find(item => item.id == weaponId);
    }

}

#if UNITY_EDITOR
[CustomEditor(typeof(DataManager))]
public class GetCurrentDataButton : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("Get current data"))
        {
            ((DataManager)target).LoadData();
            Debug.Log("Load data success.");
        }

        if (GUILayout.Button("Save current data"))
        {
            ((DataManager)target).SaveData();
            Debug.Log("Save data success.");
        }
    }
}
#endif
