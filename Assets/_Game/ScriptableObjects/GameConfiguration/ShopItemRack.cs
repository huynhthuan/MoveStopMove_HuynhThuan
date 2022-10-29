using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shop Item Rack", menuName = "GameData/Shop/Item Rack", order = 1)]
public class ShopItemRack : ScriptableObject
{
    public List<ItemEquipment> listItem = new List<ItemEquipment>();
}
