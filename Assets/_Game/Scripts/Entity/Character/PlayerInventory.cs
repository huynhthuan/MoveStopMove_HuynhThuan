using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class PlayerInventory
{
    public List<InventorySlot> items = new List<InventorySlot>();

    public PlayerInventory()
    {
        this.items = new List<InventorySlot>();
    }

    public void Add(InventorySlot inventorySlot)
    {
        items.Add(inventorySlot);
    }

    public void AddMany(InventorySlot[] inventorySlots)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            Add(inventorySlots[i]);
        }
    }

    public bool CheckHasItem(ItemId itemId)
    {
        return items.Exists(x => x.itemId == itemId);
    }
}

[System.Serializable]
public class InventorySlot
{
    public ItemId itemId;
    public InventorySlot(ItemId itemId)
    {
        this.itemId = itemId;
    }


}
