using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor;

public enum DataKey
{
    GOLD,
    USER_NAME,
    CURRENT_STAGE,
    HEAD_ID,
    WEAPON_ID,
    WEAPON_MATERIAL_1,
    WEAPON_MATERIAL_2,
    WEAPON_MATERIAL_3,
    SHIED_ID,
    WING_ID,
    TAIL_ID,
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

    private string[] DataKeyString = { "GOLD", "USER_NAME", "CURRENT_STAGE", "HEAD_ID", "WEAPON_ID", "WEAPON_MATERIAL_1", "WEAPON_MATERIAL_2", "WEAPON_MATERIAL_3", "SHIED_ID", "WING_ID", "TAIL_ID", "PANTS_ID", "INVENTORY" };

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
            0,
            0,
            (int)ItemId.KINIFE,
            (int)MaterialId.MATERIAL_WEAPON_COLOR_1,
            (int)MaterialId.MATERIAL_WEAPON_COLOR_2,
            (int)MaterialId.MATERIAL_WEAPON_COLOR_3,
            0,
            0,
            0,
            (int)ItemId.PANT_1,
            playerInventoryDefault
        );

        Debug.Log($"Player data default {JsonUtility.ToJson(initData)}");

        playerData.currentStage = PlayerPrefs.GetInt(GetKey(DataKey.CURRENT_STAGE), initData.currentStage);
        playerData.gold = PlayerPrefs.GetInt(GetKey(DataKey.GOLD), 100);
        playerData.userName = PlayerPrefs.GetString(GetKey(DataKey.CURRENT_STAGE), initData.userName);
        playerData.headId = PlayerPrefs.GetInt(GetKey(DataKey.HEAD_ID), initData.headId);
        playerData.weaponId = PlayerPrefs.GetInt(GetKey(DataKey.WEAPON_ID), initData.weaponId);
        playerData.weaponMaterial1 = PlayerPrefs.GetInt(GetKey(DataKey.WEAPON_MATERIAL_1), initData.weaponMaterial1);
        playerData.weaponMaterial2 = PlayerPrefs.GetInt(GetKey(DataKey.WEAPON_MATERIAL_2), initData.weaponMaterial2);
        playerData.weaponMaterial3 = PlayerPrefs.GetInt(GetKey(DataKey.WEAPON_MATERIAL_3), initData.weaponMaterial3);
        playerData.shieldId = PlayerPrefs.GetInt(GetKey(DataKey.SHIED_ID), initData.shieldId);
        playerData.wingId = PlayerPrefs.GetInt(GetKey(DataKey.WING_ID), initData.wingId);
        playerData.taildId = PlayerPrefs.GetInt(GetKey(DataKey.TAIL_ID), initData.taildId);
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
        PlayerPrefs.SetInt(GetKey(DataKey.HEAD_ID), playerData.headId);
        PlayerPrefs.SetInt(GetKey(DataKey.WEAPON_ID), playerData.weaponId);
        PlayerPrefs.SetInt(GetKey(DataKey.WEAPON_MATERIAL_1), playerData.weaponMaterial1);
        PlayerPrefs.SetInt(GetKey(DataKey.WEAPON_MATERIAL_2), playerData.weaponMaterial2);
        PlayerPrefs.SetInt(GetKey(DataKey.WEAPON_MATERIAL_3), playerData.weaponMaterial3);
        PlayerPrefs.SetInt(GetKey(DataKey.SHIED_ID), playerData.shieldId);
        PlayerPrefs.SetInt(GetKey(DataKey.WING_ID), playerData.wingId);
        PlayerPrefs.SetInt(GetKey(DataKey.TAIL_ID), playerData.taildId);
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


#if UNITY_EDITOR
[CustomEditor(typeof(DataManager))]
public class DataButton : Editor
{
    public override void OnInspectorGUI()
    {
        DataManager self = ((DataManager)target);

        DrawDefaultInspector();

        if (GUILayout.Button("Set 99999 gold"))
        {
            PlayerPrefs.SetInt(self.GetKey(DataKey.GOLD), 99999);
            Debug.Log($"Gold after set {PlayerPrefs.GetInt(self.GetKey(DataKey.GOLD))}");
        }

        if (GUILayout.Button("Reset weapon"))
        {
            PlayerInventory inventoryDefault = new PlayerInventory();
            inventoryDefault.Add(new InventorySlot(ItemId.KINIFE));
            PlayerPrefs.SetString(self.GetKey(DataKey.INVENTORY), JsonUtility.ToJson(inventoryDefault));

            PlayerPrefs.SetInt(self.GetKey(DataKey.WEAPON_ID), (int)ItemId.KINIFE);

            PlayerPrefs.SetInt(self.GetKey(DataKey.WEAPON_MATERIAL_1), (int)MaterialId.MATERIAL_WEAPON_COLOR_1);
            PlayerPrefs.SetInt(self.GetKey(DataKey.WEAPON_MATERIAL_2), (int)MaterialId.MATERIAL_WEAPON_COLOR_2);
            PlayerPrefs.SetInt(self.GetKey(DataKey.WEAPON_MATERIAL_3), (int)MaterialId.MATERIAL_WEAPON_COLOR_3);

            Debug.Log($"Inventory after reset {PlayerPrefs.GetString(self.GetKey(key: DataKey.INVENTORY))}");
            Debug.Log($"Weapon after reset {PlayerPrefs.GetInt(self.GetKey(key: DataKey.WEAPON_ID))}");
            Debug.Log($"Weapon material 1 after reset {PlayerPrefs.GetInt(self.GetKey(key: DataKey.WEAPON_MATERIAL_1))}");
            Debug.Log($"Weapon material 2 after reset {PlayerPrefs.GetInt(self.GetKey(key: DataKey.WEAPON_MATERIAL_2))}");
            Debug.Log($"Weapon material 3 after reset {PlayerPrefs.GetInt(self.GetKey(key: DataKey.WEAPON_MATERIAL_3))}");
        }
    }
}
#endif