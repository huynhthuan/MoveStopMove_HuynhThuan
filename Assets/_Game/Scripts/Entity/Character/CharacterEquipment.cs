using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public enum EquipmentSlot { HEAD, WEAPON, SHIELD, WING, TAIL, PANT, SKIN, BODY }

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

        ItemEquipment item = allItem.GetItem<ItemEquipment>(itemId);
        GameObject newItemEquipmentObject = Instantiate(item.prefab, equipmentSlots[(int)equipmentSlot]);
        ItemEquip newItem = newItemEquipmentObject.GetComponent<ItemEquip>();
        newItem.itemData = item;
        currentEquipments[(int)equipmentSlot] = newItem;
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

    public List<ItemId> GetRandomAllItem()
    {
        List<ItemId> itemIdsRandom = new List<ItemId>();
        for (int i = 0; i < System.Enum.GetNames(typeof(EquipmentSlot)).Length; i++)
        {
            Item itemRandom = RandomItem<Item>((EquipmentSlot)i);
            itemIdsRandom.Add(itemRandom.itemId);
        }
        return itemIdsRandom;
    }

    public void ApplySkin(List<Item> items, Character owner)
    {
        for (int i = 0; i < items.Count; i++)
        {
            items[i].Use(owner);
        }
    }

    public void LoadAllEquipments(Character owner, List<ItemId> items)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] == ItemId.EMPTY)
            {
                continue;
            }
            Item itemOnSlot = allItem.GetItem<Item>(items[i]);
            itemOnSlot.Use(owner);
        }
    }
}
