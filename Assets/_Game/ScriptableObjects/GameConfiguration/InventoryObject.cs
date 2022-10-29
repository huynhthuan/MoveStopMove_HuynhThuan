using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "GameData/Inventory", order = 1)]
public class InventoryObject : ScriptableObject
{
    public List<InventorySlot> inventorySlot = new List<InventorySlot>();
}

[System.Serializable]
public class InventorySlot
{
    public ItemEquipment[] itemEquipment;
}
