using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New item mesh", menuName = "Items/Mesh Equipment")]
public class MeshEquipment : Item
{
    public MaterialId materialId;

    public override void Use(Character owner)
    {
        base.Use(owner);
        owner.characterEquipment.WearItem(itemId, equipmentSlot);
    }
}
