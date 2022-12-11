using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MeshEquipment : Item
{
    public MaterialId materialId;

    public override void Use(Character owner)
    {
        base.Use(owner);
        Debug.Log(message: $"Use {(ItemId)itemId}");
        owner.characterEquipment.WearItem(itemId, equipmentSlot);
    }

    public override void UnUse(Character owner)
    {
        base.UnUse(owner);
        Debug.Log(message: $"Unuse {(ItemId)itemId}");
        owner.characterEquipment.UnEquipItemMesh(equipmentSlot);

    }
}
