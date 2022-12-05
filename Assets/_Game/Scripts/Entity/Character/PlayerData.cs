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
    public List<PlayerItem> playerItems = new List<PlayerItem>();
    [Space(20)]
    public PlayerInventory playerInventory;

    public PlayerData(int gold, string userName, int currentStage, List<PlayerItem> playerItems, PlayerInventory playerInventory)
    {
        this.gold = gold;
        this.currentStage = currentStage;
        this.userName = userName;
        this.playerItems = playerItems;
        this.playerInventory = playerInventory;
    }

}
[System.Serializable]
public class PlayerItem : IEquatable<PlayerItem>
{
    public ItemId itemId;

    public PlayerItem(ItemId itemId)
    {
        this.itemId = itemId;
    }

    public bool Equals(PlayerItem other)
    {
        if (this.itemId == other.itemId)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}