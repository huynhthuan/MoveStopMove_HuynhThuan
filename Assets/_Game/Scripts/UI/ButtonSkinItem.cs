using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;

public class ButtonSkinItem : MonoBehaviour
{
    [SerializeField]
    private Image thumbnail;

    [SerializeField]
    private Image selectBorder;

    [SerializeField]
    private Image lockIcon;

    internal Item itemData;
    internal List<Item> itemsOfSkin;
    internal bool isSelect = false;
    internal bool isLock = true;
    internal bool isEquip = true;
    internal Transform TF;

    private Skin uiSkin;

    private void Start()
    {
        TF = transform;
    }

    public void OnInit<T>(Skin uiSkin, bool isLock, bool isSelect, T itemData) where T : Item
    {
        this.thumbnail.sprite = itemData.thumbnail;
        this.isLock = isLock;
        this.isSelect = isSelect;
        this.itemData = itemData;
        this.uiSkin = uiSkin;
        if (itemData.equipmentSlot == EquipmentSlot.SKIN)
        {
            SkinEquipment skinEquipment = itemData as SkinEquipment;
            this.itemsOfSkin = skinEquipment.itemsOfSkin;
        }
    }

    private void Update()
    {
        if (isLock)
        {
            lockIcon.gameObject.SetActive(true);
        }
        else
        {
            lockIcon.gameObject.SetActive(false);
        }

        if (isSelect)
        {
            selectBorder.gameObject.SetActive(true);
        }
        else
        {
            selectBorder.gameObject.SetActive(false);
        }
    }

    public void OnClickItem()
    {
        uiSkin.DeSelectAllItem();
        SelectItem();
    }

    public void SelectItem()
    {
        uiSkin.player.EquipAllItems();

        // if (uiSkin.currentItemSelect != null)
        // {
        //     // Debug.Log($"currentItemSelect {uiSkin.currentItemSelect.itemId}");

        //     if (uiSkin.currentEquipments[(int)uiSkin.currentItemSelect.equipmentSlot] != null)
        //     {
        //         if (
        //             uiSkin.currentItemSelect.itemId
        //             != uiSkin.currentEquipments[(int)uiSkin.currentItemSelect.equipmentSlot]
        //                 .itemData
        //                 .itemId
        //         )
        //         {
        //             uiSkin.currentItemSelect.UnUse(uiSkin.player);
        //         }
        //     }
        // }

        isSelect = true;
        uiSkin.currentItemSelect = itemData;
        uiSkin.ShowButtonOnItemSelect();

        // if (itemData.equipmentSlot == EquipmentSlot.SKIN)
        // {
        //     for (int i = 0; i < uiSkin.currentEquipments.Count; i++)
        //     {
        //         if (uiSkin.currentEquipments[i] != null)
        //         {
        //             if (
        //                 !itemsOfSkin.Contains(uiSkin.currentEquipments[i].itemData)
        //                 && uiSkin.currentEquipments[i].itemData.equipmentSlot
        //                     != EquipmentSlot.WEAPON
        //             )
        //             {
        //                 Debug.Log($"Un use item {uiSkin.currentEquipments[i].itemData.itemId}");
        //                 uiSkin.currentEquipments[i].itemData.UnUse(uiSkin.player);
        //             }
        //         }
        //     }
        // }

        itemData.Use(uiSkin.player);
    }

    public void DeselectItem()
    {
        isSelect = false;
        uiSkin.ShowButtonOnItemSelect();
    }
}
