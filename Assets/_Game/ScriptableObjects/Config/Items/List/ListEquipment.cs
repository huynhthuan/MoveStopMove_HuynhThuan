using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New list item", menuName = "Items/List/List item")]
public class ListEquipment : ScriptableObject
{
    public List<Item> items;

    public List<T> GetItemsBySlot<T>(EquipmentSlot slot) where T : Item
    {
        List<T> listItemBySlot = new List<T>();

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].equipmentSlot == slot && items[i].isShowOnStore)
            {
                listItemBySlot.Add(items[i] as T);
            }
        }

        return listItemBySlot;
    }

    public T GetItem<T>(ItemId itemId) where T : Item
    {
        T currentItem = null;

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].itemId == itemId)
            {
                currentItem = items[i] as T;
                break;
            }
        }

        return currentItem;
    }

    public T GetItemOnStore<T>(ItemId itemId) where T : Item
    {
        T currentItem = null;

        for (int i = 0; i < items.Count; i++)
        {
            if (
                items[i].itemId == itemId
                && items[i].isShowOnStore
                && (items[i].itemId != ItemId.EMPTY || items[i].itemId != ItemId.PANT_0)
            )
            {
                currentItem = items[i] as T;
                break;
            }
        }

        return currentItem;
    }
}
