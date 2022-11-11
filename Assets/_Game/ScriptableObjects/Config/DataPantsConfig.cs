using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Database Pants", menuName = "ScriptableObjects/Database/DatabasePants", order = 1)]
public class DataPantsConfig : ScriptableObject
{
    public List<PantsConfig> pantsItems;

    public PantsConfig FindById(string id)
    {
        return pantsItems.Find(item => item.id == id);
    }
}
