using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New list skin", menuName = "Items/List/List Skin Item")]
public class ListSkin : ScriptableObject
{
    public List<ItemSkin> skins;

    public ItemSkin GetSkin(SkinId skinId)
    {

        ItemSkin currentSkin = null;

        for (int i = 0; i < skins.Count; i++)
        {
            if (skins[i].skinId == skinId)
            {
                currentSkin = skins[i];
                break;
            }
        }

        return currentSkin;
    }
}