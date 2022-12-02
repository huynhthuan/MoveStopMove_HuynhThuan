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
    private List<UiTabSkin> tabBtns;
    [SerializeField]
    private ButtonSkinItem buttonSkinItemPrefab;
    [SerializeField]
    private Transform buyBtn;
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
    private DataManager dataManager;
    private ListEquipment allItem;
    private List<ButtonSkinItem> listItemTab = new List<ButtonSkinItem>();
    internal PlayerInventory playerInventory;
    internal UserData playerData;
    internal Player player;

    public override void Open()
    {
        base.Open();
        GameManager.Ins.cameraFollow.SetCameraSkin();
        dataManager = DataManager.Ins;
        allItem = dataManager.listEquipment;
        playerData = dataManager.playerData;
        playerInventory = playerData.playerInventory;
        player = LevelManager.Ins.player;

        ActiveTab(TabName.TAB_HEAD);
        DiasableAllBtn();
        DeSelectAllItem();

        listItemTab[0].SelectItem();

    }

    public void CloseButton()
    {
        UIManager.Ins.OpenUI<Lobby>();
        player.UnEquipAllItems();
        player.EquipAllItems();
        Close();

    }

    private void LoadTabDataItem<T>(EquipmentSlot equipmentSlot) where T : Item
    {
        ClearItemOfTab();
        listItemTab.Clear();
        List<T> itemsOfTab = allItem.GetItemsBySlot<T>(equipmentSlot);

        Debug.Log($"Init {equipmentSlot} - {itemsOfTab.Count}");

        for (int i = 0; i < itemsOfTab.Count; i++)
        {
            T itemConfig = itemsOfTab[i];

            if (!itemConfig.isShowOnStore)
            {
                continue;
            }

            ButtonSkinItem itemObj = Instantiate(buttonSkinItemPrefab, contentList);

            itemObj.OnInit(this, !playerInventory.IsHasItem(itemConfig.itemId), false, itemConfig);
            listItemTab.Add(itemObj);
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

    public void OnChangeTab(int tabName)
    {
        Debug.Log($"Change tab {tabName}");
        for (int i = 0; i < tabBtns.Count; i++)
        {
            DisableTab((TabName)i);
        }

        if (currentItemSelect != null)
        {
            currentItemSelect.UnUse(player);
        }

        currentTabSelect = (TabName)tabName;
        ActiveTab(currentTabSelect);

        listItemTab[0].SelectItem();
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

    public void DiasableAllBtn()
    {
        buyBtn.gameObject.SetActive(false);
        selectBtn.gameObject.SetActive(value: false);
        notEnoughBtn.gameObject.SetActive(false);
    }

    public void EnableBuyBtn()
    {
        buyBtn.gameObject.SetActive(true);
        selectBtn.gameObject.SetActive(false);
        notEnoughBtn.gameObject.SetActive(false);
    }

    public void EnableSelectBtn()
    {
        buyBtn.gameObject.SetActive(false);
        selectBtn.gameObject.SetActive(true);
        notEnoughBtn.gameObject.SetActive(false);
    }

    public void EnableNotEnoughBtn()
    {
        buyBtn.gameObject.SetActive(false);
        selectBtn.gameObject.SetActive(false);
        notEnoughBtn.gameObject.SetActive(true);
    }

    public void ShowButtonOnItemSelect()
    {
        if (currentItemSelect == null)
        {
            DiasableAllBtn();
            return;
        }

        priceBuyBtn.text = currentItemSelect.price.ToString();
        priceNotEnoughBtn.text = currentItemSelect.price.ToString();

        if (playerInventory.IsHasItem(currentItemSelect.itemId))
        {
            EnableSelectBtn();
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
            playerData.gold -= currentItemSelect.price;

            UnlockItem(currentItemSelect.itemId);

            // PlayerPrefs.SetInt(DataManager.Ins.GetKey(DataKey.GOLD), playerData.gold);
            // PlayerPrefs.SetString(DataManager.Ins.GetKey(DataKey.INVENTORY), JsonUtility.ToJson(playerData.playerInventory));

            EnableSelectBtn();
        }
    }

    public void OnClickSelectBtn()
    {

    }

    public void UnlockItem(ItemId itemId)
    {
        ButtonSkinItem itemUnlock = listItemTab.Find((button) => button.itemData.itemId == itemId);
        itemUnlock.isLock = false;
    }

}
