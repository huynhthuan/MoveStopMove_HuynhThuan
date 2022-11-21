using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class Item : ScriptableObject
{
    public EquipmentSlot equipSlot;
    public string itemName;
    public string description;
    public int price;
    public bool isShowOnStore = true;

    public virtual void Use()
    {
        Debug.Log("Equipment " + itemName);
    }
}

public enum EquipmentSlot { HEAD, WEAPON, SHIELD, WING, TAIL, PANT }