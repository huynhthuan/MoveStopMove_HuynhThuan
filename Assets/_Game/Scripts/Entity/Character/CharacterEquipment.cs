using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CharacterEquipment : MonoBehaviour
{
    [SerializeField]
    public Transform[] currentEquipementTransform;

    internal Item[] currentEquipment;
    internal GameObject[] currentEquipmentObj;
    private DataManager dataManager;
    private ListEquipment allItem;
    private ListMaterial allMaterial;

    public void Oninit()
    {
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Item[numSlots];
        currentEquipmentObj = new GameObject[numSlots];
        dataManager = DataManager.Ins;
        allItem = dataManager.listEquipment;
        allMaterial = dataManager.listMaterial;
    }

    public void EquipItem(ItemId itemId, EquipmentSlot equipmentSlot)
    {
        UnEquipItem(equipmentSlot);

        if (equipmentSlot == EquipmentSlot.WEAPON)
        {
            WeaponEquipment weapon = dataManager.listEquipment.GetItem<WeaponEquipment>(itemId);
            GameObject newWeaponObj = Instantiate(weapon.prefab.gameObject, currentEquipementTransform[(int)equipmentSlot]);
            currentEquipment[(int)equipmentSlot] = weapon;
            currentEquipmentObj[(int)equipmentSlot] = newWeaponObj;
        }

    }

    public void UnEquipItem(EquipmentSlot equipmentSlot)
    {
        if (currentEquipment[(int)equipmentSlot] != null)
        {
            currentEquipment[(int)equipmentSlot] = null;
            Destroy(currentEquipmentObj[(int)equipmentSlot].gameObject);
        }
    }

    public Weapon GetCurrentWeaponBullet()
    {
        WeaponEquipment weaponEquipment = (WeaponEquipment)currentEquipment[(int)EquipmentSlot.WEAPON];
        return weaponEquipment.weaponBullet;
    }

    public void HiddenWeapon()
    {
        currentEquipementTransform[(int)EquipmentSlot.WEAPON].gameObject.SetActive(false);
    }

    public void ShowWeapon()
    {
        currentEquipementTransform[(int)EquipmentSlot.WEAPON].gameObject.SetActive(true);
    }


    public void WearItem(ItemId itemId, EquipmentSlot equipmentSlot)
    {
        MeshEquipment item = dataManager.listEquipment.GetItem<MeshEquipment>(itemId);
        SkinnedMeshRenderer slotMeshRenderer = currentEquipementTransform[(int)equipmentSlot].GetComponent<SkinnedMeshRenderer>();
        Material[] mats = new Material[] { allMaterial.GetMaterial(item.materialId).material };
        slotMeshRenderer.materials = mats;
    }

    public T RandomItem<T>(EquipmentSlot slot) where T : Item
    {
        List<Item> itemsBySlot = allItem.GetItemsBySlot<Item>(slot);
        int itemIdRandom = Random.Range(0, maxExclusive: itemsBySlot.Count);
        return allItem.GetItem<T>(itemsBySlot[itemIdRandom].itemId);
    }
}
