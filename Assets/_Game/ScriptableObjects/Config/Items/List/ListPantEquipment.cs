using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

[CreateAssetMenu(fileName = "New List Pant Equipment", menuName = "Items/List/List Pant Equipment")]
public class ListPantEquipment : ListEquipment
{
    public List<PantEquipment> pants;
}

public enum PantId { PANT_1, PANT_2, PANT_3, PANT_4, PANT_5, PANT_6, PANT_7, PANT_8, PANT_9, }