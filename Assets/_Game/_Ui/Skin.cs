using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

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
    private Transform contentList;
    [SerializeField]
    private List<UiTabSkin> tabBtns;
    [SerializeField]
    private ButtonSkinItem buttonSkinItemPrefab;

    private Item currentItemSelect;
    private TabName currentTabSelect;
    private DataManager dataManager;
    private ListEquipment allItem;
    private List<ButtonSkinItem> listItemTab = new List<ButtonSkinItem>();

    public override void Open()
    {
        base.Open();
        GameManager.Ins.cameraFollow.SetCameraSkin();
        dataManager = DataManager.Ins;
        allItem = dataManager.listEquipment;
        ActiveTab(TabName.TAB_HEAD);
    }

    public void CloseButton()
    {
        UIManager.Ins.OpenUI<Lobby>();
        Close();
    }

    private void LoadTabDataItemEquipment<T>(EquipmentSlot equipmentSlot) where T : ItemEquipment
    {
        if (listItemTab.Count > 0)
        {
            for (int i = 0; i < listItemTab.Count; i++)
            {
                Destroy(listItemTab[i].gameObject);
            }
        }

        listItemTab.Clear();

        List<T> itemsOfTab = allItem.GetItemsBySlot<T>(equipmentSlot);

        Debug.Log($"Init {equipmentSlot} - {itemsOfTab.Count}");


        for (int i = 0; i < itemsOfTab.Count; i++)
        {
            ButtonSkinItem itemObj = Instantiate(buttonSkinItemPrefab, contentList);
            T itemConfig = itemsOfTab[i];
            itemObj.OnInit(true, false, itemConfig);
            listItemTab.Add(itemObj);
        }
    }

    private void LoadTabDataItemMeshEquipment<T>(EquipmentSlot equipmentSlot) where T : MeshEquipment
    {
        if (listItemTab.Count > 0)
        {
            for (int i = 0; i < listItemTab.Count; i++)
            {
                Destroy(listItemTab[i].gameObject);
            }
        }

        listItemTab.Clear();

        Debug.Log($"Load data slot {equipmentSlot}");
        List<T> itemsOfTab = allItem.GetItemsBySlot<T>(equipmentSlot);
        for (int i = 0; i < itemsOfTab.Count; i++)
        {
            ButtonSkinItem itemObj = Instantiate(buttonSkinItemPrefab, contentList);
            T itemConfig = itemsOfTab[i];
            itemObj.OnInit(true, false, itemConfig);
            listItemTab.Add(itemObj);
        }
    }

    public void GetItemsOfTab()
    {

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

        currentTabSelect = (TabName)tabName;
        ActiveTab(currentTabSelect);

    }

    public void OnActiveTab(TabName tabName)
    {
        switch (tabName)
        {
            case TabName.TAB_HEAD:
                LoadTabDataItemEquipment<HeadEquipment>(equipmentSlot: EquipmentSlot.HEAD);
                break;
            case TabName.TAB_PANT:
                LoadTabDataItemMeshEquipment<PantMeshEquipment>(equipmentSlot: EquipmentSlot.PANT);
                break;
            case TabName.TAB_SHIELD:
                LoadTabDataItemEquipment<ShieldEquipment>(equipmentSlot: EquipmentSlot.SHIELD);
                break;
            case TabName.TAB_SKIN:
                break;
        }
    }
}
