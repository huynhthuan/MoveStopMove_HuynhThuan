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
    private Transform listSkin;
    [SerializeField]
    private List<UiTabSkin> tabBtns;

    private Item currentItemSelect;
    private TabName currentTabSelect;

    public override void Open()
    {
        base.Open();
        GameManager.Ins.cameraFollow.SetCameraSkin();
        tabBtns[0].isActive = true;
    }

    public void CloseButton()
    {
        UIManager.Ins.OpenUI<Lobby>();
        Close();
    }

    private void LoadData()
    {

    }

    public void OnChangeTab(int tabName)
    {
        Debug.Log($"Change tab {tabName}");
        for (int i = 0; i < tabBtns.Count; i++)
        {
            tabBtns[i].isActive = false;
        }

        currentTabSelect = (TabName)tabName;
        tabBtns[tabName].isActive = true;
    }
}
