using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New list material", menuName = "Items/List/List material")]
public class ListMaterial : ScriptableObject
{
    public List<MaterialItem> materials;

    public MaterialItem GetMaterial(MaterialId materialId)
    {

        MaterialItem currentMaterial = null;

        for (int i = 0; i < materials.Count; i++)
        {
            if (materials[i].materialId == materialId)
            {
                currentMaterial = materials[i];
                break;
            }
        }

        return currentMaterial;
    }
}