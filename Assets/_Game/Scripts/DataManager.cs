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
    WEAPON_MATERIAL_3,
    PANTS_ID,
    INVENTORY,
}

public class DataManager : Singleton<DataManager>
{
    [SerializeField]
    internal ListEquipment listEquipment;
    [SerializeField]
    internal ListMaterial listMaterial;
    [SerializeField]
    internal PlayerData playerData;

    private string[] DataKeyString = { "GOLD", "USER_NAME", "CURRENT_STAGE", "WEAPON_ID", "WEAPON_MATERIAL_1", "WEAPON_MATERIAL_2", "WEAPON_MATERIAL_3", "PANTS_ID", "INVENTORY" };

    public void OnInit()
    {
        // Load data player
        Debug.Log("Oninit data manager...");
        // PlayerPrefs.SetInt(GetKey(DataKey.GOLD), 9999);
        // PlayerPrefs.DeleteAll();
        LoadData();
    }

    public void LoadData()
    {
        PlayerInventory playerInventoryDefault = new PlayerInventory();
        playerInventoryDefault.Add(new InventorySlot(ItemId.KINIFE));

        PlayerData initData = new PlayerData(
            100,
            ConstString.DEFAULT_USER_NAME,
            0, (int)ItemId.KINIFE,
            (int)MaterialId.MATERIAL_WEAPON_COLOR_1,
            (int)MaterialId.MATERIAL_WEAPON_COLOR_2,
            (int)MaterialId.MATERIAL_WEAPON_COLOR_3,
            (int)ItemId.PANT_1,
            playerInventoryDefault
        );

        Debug.Log($"Player data default {JsonUtility.ToJson(initData)}");

        playerData.currentStage = PlayerPrefs.GetInt(GetKey(DataKey.CURRENT_STAGE), initData.currentStage);
        playerData.gold = PlayerPrefs.GetInt(GetKey(DataKey.GOLD), 100);
        playerData.userName = PlayerPrefs.GetString(GetKey(DataKey.CURRENT_STAGE), initData.userName);
        playerData.weaponId = PlayerPrefs.GetInt(GetKey(DataKey.WEAPON_ID), initData.weaponId);
        playerData.weaponMaterial1 = PlayerPrefs.GetInt(GetKey(DataKey.WEAPON_MATERIAL_1), initData.weaponMaterial1);
        playerData.weaponMaterial2 = PlayerPrefs.GetInt(GetKey(DataKey.WEAPON_MATERIAL_2), initData.weaponMaterial2);
        playerData.weaponMaterial3 = PlayerPrefs.GetInt(GetKey(DataKey.WEAPON_MATERIAL_3), initData.weaponMaterial3);
        playerData.pantsId = PlayerPrefs.GetInt(GetKey(DataKey.PANTS_ID), initData.pantsId);
        playerData.playerInventory = JsonUtility.FromJson<PlayerInventory>(PlayerPrefs.GetString(GetKey(DataKey.INVENTORY), JsonUtility.ToJson(initData.playerInventory)));

        Debug.Log($"Player data {JsonUtility.ToJson(playerData)}");
    }

    public void SaveData()
    {
        Debug.Log("[+] Save data player.");
        PlayerPrefs.SetInt(GetKey(DataKey.CURRENT_STAGE), playerData.currentStage);
        PlayerPrefs.SetInt(GetKey(DataKey.GOLD), playerData.gold);
        PlayerPrefs.SetString(GetKey(DataKey.USER_NAME), playerData.userName);
        PlayerPrefs.SetInt(GetKey(DataKey.WEAPON_ID), playerData.weaponId);
        PlayerPrefs.SetInt(GetKey(DataKey.WEAPON_MATERIAL_1), playerData.weaponMaterial1);
        PlayerPrefs.SetInt(GetKey(DataKey.WEAPON_MATERIAL_2), playerData.weaponMaterial2);
        PlayerPrefs.SetInt(GetKey(DataKey.PANTS_ID), playerData.pantsId);
        PlayerPrefs.SetString(GetKey(DataKey.INVENTORY), JsonUtility.ToJson(playerData.playerInventory));
    }

    public void SaveDataByKey(DataKey dataKey)
    {

    }

    public string GetKey(DataKey key)
    {
        return DataKeyString[(int)key];
    }
}