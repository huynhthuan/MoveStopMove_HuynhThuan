using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class Skin : UICanvas
{
    [SerializeField]
    private Transform listSkin;

    private string currentItemSelect;
    private string currentTabSelect;

    public override void Open()
    {
        base.Open();
        GameManager.Ins.cameraFollow.SetCameraSkin();
    }

    public void CloseButton()
    {
        UIManager.Ins.OpenUI<Lobby>();
        Close();
    }

    private void LoadData()
    {

    }

    private void ChangeTab()
    {

    }



}
