using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Numerics;
using System.Globalization;
using Newtonsoft.Json;

[CreateAssetMenu(fileName = "UserData", menuName = "ScriptableObjects/UserData", order = 1)]
public class UserData : ScriptableObject
{
#if UNITY_EDITOR
    [Header(" ----Test Data----")]

    public bool IsTestCheckData = false;
#endif

    [Header("----Data----")]

    public int gold = 0;
    public string userName = "Unknowname";
    public int currentStage = 0;
    public List<PlayerItem> currentItems;
    public PlayerInventory playerInventory;

    #region List

    /// <summary>
    ///  0 = lock , 1 = unlock , 2 = selected
    ///  luu mot danh sach gia tri, key la ten list, id la so thu tu, state la trang thai
    /// </summary>
    /// <param name="key"></param>
    /// <param name="ID"></param>
    /// <param name="state"></param>
    public void SetDataState(string key, int ID, int state)
    {
        PlayerPrefs.SetInt(key + ID, state);
    }

    /// <summary>
    ///  0 = lock , 1 = unlock , 2 = selected
    /// </summary>
    /// <param name="key"></param>
    /// <param name="ID"></param>
    /// <param name="state"></param>
    public int GetDataState(string key, int ID, int defaultID = 0)
    {
        return PlayerPrefs.GetInt(key + ID, defaultID);
    }

    /// <summary>
    ///  0 = lock , 1 = unlock , 2 = selected
    /// </summary>
    /// <param name="key"></param>
    /// <param name="ID"></param>
    /// <param name="state"></param>
    public void SetDataState(string key, int ID, ref List<int> variable, int state)
    {
        variable[ID] = state;
        PlayerPrefs.SetInt(key + ID, state);
    }

    #endregion

    #region Save data

    /// <summary>
    /// Key_Name
    /// if(bool) true == 1
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public void SetIntData(string key, ref int variable, int value)
    {
        variable = value;
        PlayerPrefs.SetInt(key, value);
    }

    public void SetBoolData(string key, ref bool variable, bool value)
    {
        variable = value;
        PlayerPrefs.SetInt(key, value ? 1 : 0);
    }

    public void SetFloatData(string key, ref float variable, float value)
    {
        variable = value;
        PlayerPrefs.GetFloat(key, value);
    }

    public void SetStringData(string key, ref string variable, string value)
    {
        variable = value;
        PlayerPrefs.SetString(key, value);
    }

    #endregion

    #region Class

    public void SetClassData<T>(string key, T t) where T : class
    {
        string s = JsonConvert.SerializeObject(t);
        PlayerPrefs.SetString(key, s);
    }

    public T GetClassData<T>(string key) where T : class
    {
        string s = PlayerPrefs.GetString(key);
        return string.IsNullOrEmpty(s) ? null : JsonConvert.DeserializeObject<T>(s);
    }

    #endregion

    public void OnInitData()
    {

#if UNITY_EDITOR
        if (IsTestCheckData)
        {
            return;
        }
#endif
        Debug.Log("Init data...");

        List<PlayerItem> initItems = new List<PlayerItem>(7);
        PlayerInventory initInventory = new PlayerInventory();

        PlayerItem[] itemsDefault = {
            new PlayerItem(ItemId.EMPTY),
            new PlayerItem(ItemId.KINIFE),
            new PlayerItem(ItemId.EMPTY),
            new PlayerItem(ItemId.EMPTY),
            new PlayerItem(ItemId.EMPTY),
            new PlayerItem(ItemId.EMPTY),
            new PlayerItem(ItemId.PANT_0),
        };

        initItems.AddRange(itemsDefault);

        initInventory.AddMany(new InventorySlot[] { new InventorySlot(ItemId.KINIFE), new InventorySlot(ItemId.PANT_0) });

        gold = PlayerPrefs.GetInt(Key_Gold, 44444);
        currentStage = PlayerPrefs.GetInt(Key_Current_Stage, 0);
        userName = PlayerPrefs.GetString(Key_UserName, "Unkown Name");
        currentItems = JsonConvert.DeserializeObject<List<PlayerItem>>(PlayerPrefs.GetString(Key_Current_Items, JsonConvert.SerializeObject(initItems)));
        playerInventory = JsonConvert.DeserializeObject<PlayerInventory>(PlayerPrefs.GetString(Key_Inventory, JsonConvert.SerializeObject(initInventory)));
    }

    public void OnResetData()
    {
        PlayerPrefs.DeleteAll();
        OnInitData();
    }

    public const string Key_Gold = "Gold";
    public const string Key_Current_Stage = "CurrentStage";
    public const string Key_UserName = "UserName";
    public const string Key_Current_Items = "CurrentItems";
    public const string Key_Inventory = "Inventory";
}


