using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;

[System.Serializable]
public class PlayerData
{
    public int gold;
    public string userName;
    public int currentStage;
    [Space(20)]
    public int weaponId;
    public int weaponMaterial1;
    public int weaponMaterial2;
    public int weaponMaterial3;
    [Space(20)]
    public int pantsId;
    [Space(20)]
    public PlayerInventory playerInventory;

    public PlayerData(int gold, string userName, int currentStage, int weaponId, int weaponMaterial1, int weaponMaterial2, int weaponMaterial3, int pantsId, PlayerInventory playerInventory)
    {
        this.gold = gold;
        this.currentStage = currentStage;
        this.userName = userName;
        this.weaponId = weaponId;
        this.weaponMaterial1 = weaponMaterial1;
        this.weaponMaterial2 = weaponMaterial2;
        this.weaponMaterial3 = weaponMaterial3;
        this.pantsId = pantsId;
        this.playerInventory = playerInventory;
    }

}

