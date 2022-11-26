using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New list item", menuName = "Items/List item")]
public class ListEquipment : ScriptableObject
{
    public List<Item> items;


}