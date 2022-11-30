using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public enum DataKey
{
    GOLD,
    USER_NAME,
    CURRENT_STAGE,
    CURRENT_EQUIPMENT_ITEMS_PLAYER,
    INVENTORY,
}

public class DataManager : Singleton<DataManager>
{
    [SerializeField]
    internal ListEquipment listEquipment;
    [SerializeField]
    internal ListMaterial listMaterial;
    [SerializeField]
    internal UserData playerData;
}

// #if UNITY_EDITOR
// [CustomEditor(typeof(DataManager))]
// public class DataButton : Editor
// {
//     public override void OnInspectorGUI()
//     {
//         DataManager self = ((DataManager)target);

//         DrawDefaultInspector();

//         if (GUILayout.Button("Set 99999 gold"))
//         {
//             PlayerPrefs.SetInt(self.GetKey(DataKey.GOLD), 99999);
//             Debug.Log($"Gold after set {PlayerPrefs.GetInt(self.GetKey(DataKey.GOLD))}");
//         }
//     }
// }
// #endif