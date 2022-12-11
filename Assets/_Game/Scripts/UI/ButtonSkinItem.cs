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

        if (uiSkin.currentItemSelect != null)
        {
            Debug.Log($"currentItemSelect {uiSkin.currentItemSelect.itemId}");
            uiSkin.currentItemSelect.UnUse(uiSkin.player);
        }

        isSelect = true;
        uiSkin.currentItemSelect = itemData;
        uiSkin.ShowButtonOnItemSelect();

        if (itemData.equipmentSlot == EquipmentSlot.SKIN)
        {
            uiSkin.player.characterEquipment.oldItemSkinEquip = new List<Item>();
            PlayerItem currentSkinId = uiSkin.dataManager.playerData.currentItems[(int)EquipmentSlot.SKIN];

            if (currentSkinId != null && currentSkinId.itemId != ItemId.EMPTY)
            {
                Debug.Log($"Current item skin before apply skin {currentSkinId.itemId}");

                SkinEquipment currentSkin = uiSkin.dataManager.listEquipment.GetItem<SkinEquipment>(currentSkinId.itemId);
                List<Item> currentItemsSkin = currentSkin.itemsOfSkin;

                for (int i = 0; i < currentItemsSkin.Count; i++)
                {
                    if (uiSkin.player.characterEquipment.currentEquipments[(int)currentItemsSkin[i].equipmentSlot] == null)
                    {
                        continue;
                    }

                    uiSkin.player.characterEquipment.oldItemSkinEquip.Add(uiSkin.player.characterEquipment.currentEquipments[(int)currentItemsSkin[i].equipmentSlot].itemData);
                }

                for (int i = 0; i < uiSkin.player.characterEquipment.oldItemSkinEquip.Count; i++)
                {
                    Debug.Log($"Old item skin {uiSkin.player.characterEquipment.oldItemSkinEquip[i].itemId}");
                }
            }
        }

        itemData.Use(uiSkin.player);
    }

    public void DeselectItem()
    {
        isSelect = false;
        uiSkin.ShowButtonOnItemSelect();
    }
}
