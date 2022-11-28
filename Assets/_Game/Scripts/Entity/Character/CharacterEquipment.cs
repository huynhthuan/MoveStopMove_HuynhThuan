using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public enum EquipmentSlot { HEAD, WEAPON, SHIELD, WING, TAIL, PANT }

public class CharacterEquipment : MonoBehaviour
{
    [SerializeField]
    public List<Transform> equipmentSlots = new List<Transform>();
    [SerializeField]
    internal List<ItemEquip> currentEquipments = new List<ItemEquip>();
    private DataManager dataManager;
    private ListEquipment allItem;
    private ListMaterial allMaterial;

    public void Oninit()
    {
        dataManager = DataManager.Ins;
        allItem = dataManager.listEquipment;
        allMaterial = dataManager.listMaterial;
    }

    public void EquipItem(ItemId itemId, EquipmentSlot equipmentSlot)
    {
        UnEquipItem(equipmentSlot);

        if (equipmentSlot == EquipmentSlot.WEAPON)
        {
            WeaponEquipment weapon = allItem.GetItem<WeaponEquipment>(itemId);
            GameObject newWeaponObject = Instantiate(weapon.prefab, equipmentSlots[(int)equipmentSlot]);
            Weapon newWeapon = newWeaponObject.GetComponent<Weapon>();
            newWeapon.itemData = weapon;
            currentEquipments[(int)equipmentSlot] = newWeapon;
        }

    }

    public void UnEquipItem(EquipmentSlot equipmentSlot)
    {
        if (currentEquipments[(int)equipmentSlot] != null)
        {
            Destroy(currentEquipments[(int)equipmentSlot].gameObject);
            currentEquipments[(int)equipmentSlot] = null;
        }
    }

    public Weapon GetCurrentWeaponBullet()
    {
        WeaponEquipment weaponEquipment = (WeaponEquipment)currentEquipments[(int)EquipmentSlot.WEAPON].itemData;
        return weaponEquipment.weaponBullet;
    }

    public void HiddenWeapon()
    {
        equipmentSlots[(int)EquipmentSlot.WEAPON].gameObject.SetActive(false);
    }

    public void ShowWeapon()
    {
        equipmentSlots[(int)EquipmentSlot.WEAPON].gameObject.SetActive(true);
    }


    public void WearItem(ItemId itemId, EquipmentSlot equipmentSlot)
    {
        MeshEquipment item = dataManager.listEquipment.GetItem<MeshEquipment>(itemId);
        SkinnedMeshRenderer slotMeshRenderer = equipmentSlots[(int)equipmentSlot].GetComponent<SkinnedMeshRenderer>();
        Material[] mats = new Material[] { allMaterial.GetMaterial(item.materialId).material };
        slotMeshRenderer.materials = mats;
    }

    public T RandomItem<T>(EquipmentSlot slot) where T : Item
    {
        List<Item> itemsBySlot = allItem.GetItemsBySlot<Item>(slot);
        int itemIdRandom = Random.Range(0, maxExclusive: itemsBySlot.Count);
        return allItem.GetItem<T>(itemsBySlot[itemIdRandom].itemId);
    }

    // public void ApplySkin(SkinId skinId, ){
    //     owner
    // }
}
