using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListEquipment : ScriptableObject
{
    public ListEquipmentSlot listEquipmentSlot;
}


public enum ListEquipmentSlot { HEAD, WEAPON, SHIELD, WING, TAIL, PANT }