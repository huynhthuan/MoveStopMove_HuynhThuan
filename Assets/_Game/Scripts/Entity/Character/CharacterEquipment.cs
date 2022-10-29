using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEquipment : MonoBehaviour
{
    [SerializeField]
    private GameObject[] equipItems;

    public void EquipItem(EquipmentSlotType slot, ItemEquipment itemObject, int materialIndex)
    {
        int equipSlot = (int)slot;
        GameObject equipItemSlot = equipItems[equipSlot];

        Transform equipItemTransform = equipItemSlot.GetComponent<Transform>();
        Quaternion equipItemRotation = equipItemTransform.rotation;
        SkinnedMeshRenderer skinnedMeshRenderer = equipItemSlot.GetComponent<SkinnedMeshRenderer>();
        MeshFilter meshFilter = equipItemSlot.GetComponent<MeshFilter>();
        MeshRenderer meshRenderer = equipItemSlot.GetComponent<MeshRenderer>();
        Material material = itemObject.listCustomMaterial[materialIndex].material;

        equipItemTransform.position = itemObject.positionEquip;
        equipItemRotation = Quaternion.Euler(itemObject.rotateEquip);

        if (skinnedMeshRenderer != null)
        {
            skinnedMeshRenderer.sharedMesh = itemObject.mesh;
            skinnedMeshRenderer.material = material;
        }

        if (meshFilter != null)
        {
            meshFilter.mesh = itemObject.mesh;
        }

        if (meshRenderer != null)
        {
            meshRenderer.material = material;
        }
    }
}
