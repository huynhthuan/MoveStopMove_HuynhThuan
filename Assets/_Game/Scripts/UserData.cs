using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Numerics;
using System.Globalization;
// using Newtonsoft.Json;

[CreateAssetMenu(fileName = "UserData", menuName = "ScriptableObjects/UserData", order = 1)]
public class UserData : ScriptableObject
{
#if UNITY_EDITOR
    [Header(" ----Test Data----")]

    public bool IsTestCheckData = false;
#endif

    [Header("----Data----")]

    public int PlayingLevel = 0;

    public string Cash;
    public bool removeAds = false;

    public bool musicIsOn = true;
    public bool vibrationIsOn = true;
    public bool fxIsOn = true;
    public bool tutorialed = false;

    public int maxLevelMeleeUnlock = 0;
    public int maxLevelRangeUnlock = 0;

    public int meleeHaveOwned = 0;
    public int rangeHaveOwned = 0;

    public string lastTimePlay;

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

    // public void SetClassData<T>(string key, T t) where T : class
    // {
    //     string s = JsonConvert.SerializeObject(t);
    //     PlayerPrefs.SetString(key, s);
    // }

    // public T GetClassData<T>(string key) where T : class
    // {
    //     string s = PlayerPrefs.GetString(key);
    //     return string.IsNullOrEmpty(s) ? null : JsonConvert.DeserializeObject<T>(s);
    // }

    #endregion

    public void OnInitData()
    {
#if UNITY_EDITOR
        if (IsTestCheckData)
        {
            return;
        }
#endif

        PlayingLevel = PlayerPrefs.GetInt(Key_PlayingLevel, 1);
        Cash = PlayerPrefs.GetString(Key_Cash, "50");
        musicIsOn = PlayerPrefs.GetInt(Key_MusicIsOn, 1) == 1;
        vibrationIsOn = PlayerPrefs.GetInt(Key_VibrationIsOn, 1) == 1;
        fxIsOn = PlayerPrefs.GetInt(Key_FxIsOn, 1) == 1;
        removeAds = PlayerPrefs.GetInt(Key_RemoveAds, 0) == 1;
        tutorialed = PlayerPrefs.GetInt(Key_Tutorial, 0) == 1;
        lastTimePlay = PlayerPrefs.GetString(Key_Last_Time_Play, System.DateTime.Now.ToString(CultureInfo.InvariantCulture));

        maxLevelMeleeUnlock = PlayerPrefs.GetInt(Key_Max_Level_Melee_Unlock, 1);
        maxLevelRangeUnlock = PlayerPrefs.GetInt(Key_Max_Level_Range_Unlock, 1);

        meleeHaveOwned = PlayerPrefs.GetInt(Key_Melee_Have_Owned, 0);
        rangeHaveOwned = PlayerPrefs.GetInt(Key_Range_Have_Owned, 0);

    }

    public void OnResetData()
    {
        PlayerPrefs.DeleteAll();
        OnInitData();
    }

    public const string Key_PlayingLevel = "Level";
    public const string Key_Cash = "Cash";
    public const string Key_FxIsOn = "SoundIsOn";
    public const string Key_MusicIsOn = "MusicIsOn";
    public const string Key_VibrationIsOn = "VibrationIsOn";
    public const string Key_RemoveAds = "RemoveAds";
    public const string Key_Tutorial = "Tutorial";
    public const string Key_Last_Time_Play = "Key_Last_Time_Play";

    public const string Key_Slot_Type_ = "KeySlotType_";
    public const string Key_Slot_Level_ = "KeySlotLevel_";

    public const string Key_Max_Level_Melee_Unlock = "Key_Max_Level_Melee_Unlock";
    public const string Key_Max_Level_Range_Unlock = "Key_Max_Level_Range_Unlock";

    public const string Key_Melee_Have_Owned = "Key_Melee_Have_Owned";
    public const string Key_Range_Have_Owned = "Key_Range_Have_Owned";
}


