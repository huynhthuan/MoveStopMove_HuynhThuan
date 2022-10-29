using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType
{
    BELT,
    CLOTH,
    GLOVE,
    HAT,
    SHOE,
    SHOULDER_PAD,
    BACK_PACK,
    WEAPON,
    SHIELD
}

[CreateAssetMenu(fileName = "Item Equipment", menuName = "GameData/Items/ItemEquipment", order = 1)]
public class ItemEquipment : ItemObject
{
    public EquipmentType equipmentType;
    public Vector3 positionEquip;
    public Vector3 rotateEquip;
    public Vector3 scaleEquip;
    public Bounds boundsMesh;
    public Mesh mesh;
    public List<ItemCustomMaterial> listCustomMaterial = new List<ItemCustomMaterial>();
}

[System.Serializable]
public class ItemCustomMaterial
{
    public bool isUnlock;
    public bool isActive;
    public Sprite icon;
    public Material material;
    public int price;
}
