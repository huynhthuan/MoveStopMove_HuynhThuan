using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;
using Newtonsoft.Json;

public enum TabName
{
    TAB_HEAD,
    TAB_PANT,
    TAB_SHIELD,
    TAB_SKIN
}

public class Skin : UICanvas
{
    [SerializeField]
    internal UiButton btnGold;

    [SerializeField]
    private Transform contentList;

    [SerializeField]
    private ScrollRect contentListScroll;

    [SerializeField]
    private List<UiTabSkin> tabBtns;

    [SerializeField]
    private ButtonSkinItem buttonSkinItemPrefab;

    [SerializeField]
    private Transform buyBtn;

    [SerializeField]
    private Transform equipedBtn;

    [SerializeField]
    private Transform selectBtn;

    [SerializeField]
    private Transform notEnoughBtn;

    [SerializeField]
    internal TextMeshProUGUI priceNotEnoughBtn;

    [SerializeField]
    internal TextMeshProUGUI priceBuyBtn;
    internal Item currentItemSelect;
    internal List<Item> currentItemSkinSelect = new List<Item>();
    private TabName currentTabSelect;
    internal DataManager dataManager;
    private ListEquipment allItem;
    private List<ButtonSkinItem> listItemTab = new List<ButtonSkinItem>();
    internal PlayerInventory playerInventory;
    internal UserData playerData;
    internal Player player;
    internal List<ItemEquip> currentEquipments = new List<ItemEquip>();

    public override void Open()
    {
        base.Open();
        GameManager.Ins.cameraFollow.SetCameraSkin();
        dataManager = DataManager.Ins;
        allItem = dataManager.listEquipment;
        playerData = dataManager.playerData;
        playerInventory = playerData.playerInventory;
        player = LevelManager.Ins.player;
        currentEquipments = player.characterEquipment.currentEquipments;
        DisableAllTab();
        ActiveTab(TabName.TAB_HEAD);
    }

    public void CloseButton()
    {
        UIManager.Ins.OpenUI<Lobby>();
        for (int i = 0; i < currentEquipments.Count; i++)
        {
            if (currentEquipments[i] != null)
            {
                Destroy(currentEquipments[i].gameObject);
                currentEquipments[i] = null;
            }
        }
        player.EquipAllItems();
        Close();
    }

    private void LoadTabDataItem<T>(EquipmentSlot equipmentSlot) where T : Item
    {
        ClearItemOfTab();
        listItemTab.Clear();
        List<T> itemsOfTab = allItem.GetItemsBySlot<T>(equipmentSlot);

        Debug.Log($"Init {equipmentSlot} - {itemsOfTab.Count} items");

        GridLayoutGroup layoutGroup = contentList.GetComponent<GridLayoutGroup>();

        if (itemsOfTab.Count < 7)
        {
            layoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            layoutGroup.constraintCount = 3;
        }
        else
        {
            layoutGroup.constraint = GridLayoutGroup.Constraint.FixedRowCount;
            layoutGroup.constraintCount = 2;
        }

        for (int i = 0; i < itemsOfTab.Count; i++)
        {
            T itemConfig = itemsOfTab[i];

            if (
                !itemConfig.isShowOnStore
                || (itemConfig.isItemOfSkin && !playerInventory.IsHasItem(itemConfig.itemId))
            )
            {
                continue;
            }

            ButtonSkinItem itemObj = Instantiate(buttonSkinItemPrefab, contentList);

            itemObj.OnInit(this, !playerInventory.IsHasItem(itemConfig.itemId), false, itemConfig);
            listItemTab.Add(itemObj);
        }

        contentListScroll.normalizedPosition = new Vector2(0, 0.5f);

        DisableAllBtn();
        DeSelectAllItem();

        bool isEquipItemUnlock = false;

        for (int i = 0; i < listItemTab.Count; i++)
        {
            if (
                playerData.currentItems[(int)equipmentSlot] != null
                && playerData.currentItems[(int)equipmentSlot].itemId
                    == listItemTab[i].itemData.itemId
            )
            {
                isEquipItemUnlock = true;
                listItemTab[i].SelectItem();
                break;
            }
        }

        if (!isEquipItemUnlock)
        {
            listItemTab[0].SelectItem();
        }
    }

    public void ClearItemOfTab()
    {
        if (listItemTab.Count > 0)
        {
            for (int i = 0; i < listItemTab.Count; i++)
            {
                Destroy(listItemTab[i].gameObject);
            }
        }
    }

    public void ActiveTab(TabName tabName)
    {
        tabBtns[(int)tabName].isActive = true;
        OnActiveTab(tabName);
    }

    public void DisableTab(TabName tabName)
    {
        tabBtns[(int)tabName].isActive = false;
    }

    public void DisableAllTab()
    {
        for (int i = 0; i < tabBtns.Count; i++)
        {
            DisableTab((TabName)i);
        }
    }

    public void OnChangeTab(int tabName)
    {
        Debug.Log($"Change tab {tabName}");

        for (int i = 0; i < tabBtns.Count; i++)
        {
            DisableTab((TabName)i);
        }

        if (currentItemSelect != null)
        {
            if (currentEquipments[(int)currentItemSelect.equipmentSlot] != null)
            {
                if (
                    currentItemSelect.itemId
                    != currentEquipments[(int)currentItemSelect.equipmentSlot].itemData.itemId
                )
                {
                    currentItemSelect.UnUse(player);
                }
            }
        }

        currentTabSelect = (TabName)tabName;
        ActiveTab(currentTabSelect);
    }

    public void OnActiveTab(TabName tabName)
    {
        switch (tabName)
        {
            case TabName.TAB_HEAD:
                LoadTabDataItem<HeadEquipment>(equipmentSlot: EquipmentSlot.HEAD);
                break;
            case TabName.TAB_PANT:
                LoadTabDataItem<PantMeshEquipment>(equipmentSlot: EquipmentSlot.PANT);
                break;
            case TabName.TAB_SHIELD:
                LoadTabDataItem<ShieldEquipment>(equipmentSlot: EquipmentSlot.SHIELD);
                break;
            case TabName.TAB_SKIN:
                LoadTabDataItem<SkinEquipment>(equipmentSlot: EquipmentSlot.SKIN);
                break;
        }
    }

    public void DisableAllBtn()
    {
        buyBtn.gameObject.SetActive(false);
        selectBtn.gameObject.SetActive(value: false);
        notEnoughBtn.gameObject.SetActive(false);
        equipedBtn.gameObject.SetActive(false);
    }

    public void EnableBuyBtn()
    {
        DisableAllBtn();
        buyBtn.gameObject.SetActive(true);
    }

    public void EnableSelectBtn()
    {
        DisableAllBtn();
        selectBtn.gameObject.SetActive(true);
    }

    public void EnableNotEnoughBtn()
    {
        DisableAllBtn();
        notEnoughBtn.gameObject.SetActive(true);
    }

    public void EnableEquipedBtn()
    {
        DisableAllBtn();
        equipedBtn.gameObject.SetActive(true);
    }

    public void ShowButtonOnItemSelect()
    {
        if (currentItemSelect == null)
        {
            DisableAllBtn();
            return;
        }

        priceBuyBtn.text = currentItemSelect.price.ToString();
        priceNotEnoughBtn.text = currentItemSelect.price.ToString();

        if (playerInventory.IsHasItem(currentItemSelect.itemId))
        {
            List<PlayerItem> currentItems = playerData.currentItems;

            // Debug.Log($"currentItems {JsonConvert.SerializeObject(currentItems)}");
            // Debug.Log($"currentItemSelect {JsonConvert.SerializeObject(currentItemSelect.itemId)}");

            if (currentItems.Contains(new PlayerItem(currentItemSelect.itemId)))
            {
                EnableEquipedBtn();
            }
            else
            {
                EnableSelectBtn();
            }
        }
        else
        {
            if (playerData.gold < currentItemSelect.price)
            {
                EnableNotEnoughBtn();
            }
            else
            {
                EnableBuyBtn();
            }
        }
    }

    public void DeSelectAllItem()
    {
        for (int i = 0; i < listItemTab.Count; i++)
        {
            listItemTab[i].DeselectItem();
        }
    }

    public override void AnimationClose()
    {
        base.AnimationClose();
        btnGold.Hide();
    }

    public override void AnimationOpen()
    {
        base.AnimationOpen();
        btnGold.Show();
    }

    public void OnClickBuyBtn()
    {
        if (playerData.gold >= currentItemSelect.price)
        {
            playerInventory.Add(new InventorySlot((ItemId)currentItemSelect.itemId));

            if (currentItemSelect.equipmentSlot == EquipmentSlot.SKIN)
            {
                SkinEquipment skinEquipment = currentItemSelect as SkinEquipment;
                for (int i = 0; i < skinEquipment.itemsOfSkin.Count; i++)
                {
                    Item currentItemSkin = skinEquipment.itemsOfSkin[i];
                    playerInventory.Add(new InventorySlot((ItemId)currentItemSkin.itemId));
                }
            }

            playerData.gold -= currentItemSelect.price;

            UnlockItem(currentItemSelect.itemId);

            playerData.SetIntData(UserData.Key_Gold, ref playerData.gold, playerData.gold);
            playerData.SetClassData<PlayerInventory>(
                UserData.Key_Inventory,
                playerData.playerInventory
            );

            EnableSelectBtn();
        }
    }

    public void OnClickSelectBtn()
    {
        EnableEquipedBtn();
        currentItemSelect.Use(player);

        if (currentItemSelect.equipmentSlot == EquipmentSlot.SKIN)
        {
            // // Un use old item skin before equip new skin
            // for (int i = 0; i < player.characterEquipment.oldItemSkinEquip.Count; i++)
            // {
            //     Debug.Log(
            //         $"Un use item be fore equip skin {player.characterEquipment.oldItemSkinEquip[i].itemId}"
            //     );
            //     player.characterEquipment.oldItemSkinEquip[i].UnUse(player);

            //     if (
            //         player.characterEquipment.oldItemSkinEquip[i].equipmentSlot
            //         == EquipmentSlot.BODY
            //     )
            //     {
            //         playerData.currentItems[
            //             (int)player.characterEquipment.oldItemSkinEquip[i].equipmentSlot
            //         ] = new PlayerItem(ItemId.BODY_0);
            //     }
            //     else
            //     {
            //         playerData.currentItems[
            //             (int)player.characterEquipment.oldItemSkinEquip[i].equipmentSlot
            //         ] = new PlayerItem(ItemId.EMPTY);
            //     }
            // }

            // SkinEquipment skinEquipment = currentItemSelect as SkinEquipment;

            // for (int i = 0; i < currentEquipments.Count; i++)
            // {
            //     if (currentEquipments[i] != null)
            //     {
            //         if (
            //             !skinEquipment.itemsOfSkin.Contains(currentEquipments[i].itemData)
            //             && currentEquipments[i].itemData.equipmentSlot != EquipmentSlot.WEAPON
            //         )
            //         {
            //             currentEquipments[i].itemData.UnUse(player);
            //         }
            //     }
            // }

            playerData.currentItems[(int)currentItemSelect.equipmentSlot] = new PlayerItem(
                currentItemSelect.itemId
            );

            SkinEquipment itemSkin = allItem.GetItem<SkinEquipment>(currentItemSelect.itemId);
            for (int i = 0; i < itemSkin.itemsOfSkin.Count; i++)
            {
                playerData.currentItems[(int)itemSkin.itemsOfSkin[i].equipmentSlot] =
                    new PlayerItem(itemSkin.itemsOfSkin[i].itemId);
            }
        }
        else
        {
            if (currentEquipments[(int)EquipmentSlot.SKIN] != null)
            {
                currentEquipments[(int)EquipmentSlot.SKIN].itemData.UnUse(player);
            }

            playerData.currentItems[(int)currentItemSelect.equipmentSlot] = new PlayerItem(
                currentItemSelect.itemId
            );
        }

        playerData.SetClassData<List<PlayerItem>>(
            UserData.Key_Current_Items,
            playerData.currentItems
        );
    }

    public void UnlockItem(ItemId itemId)
    {
        ButtonSkinItem itemUnlock = listItemTab.Find((button) => button.itemData.itemId == itemId);
        itemUnlock.isLock = false;
    }
}
