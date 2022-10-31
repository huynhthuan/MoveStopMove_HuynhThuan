using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class ItemConfig : ScriptableObject
{
    public string id;
    public string description;
    public int price;
    public Sprite icon;
    public List<MaterialItem> materialItems;
}

[System.Serializable]
public class MaterialItem
{
    public int materialIndex;
    public List<materialAvaibleItem> materialAvaibleItems;
}

[System.Serializable]
public class materialAvaibleItem
{
    public Material material;
    public Sprite icon;
    public int price;
}
