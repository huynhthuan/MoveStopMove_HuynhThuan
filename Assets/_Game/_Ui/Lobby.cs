using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Lobby : UICanvas
{
    [SerializeField]
    private Transform statusBar;

    [SerializeField]
    private Transform userInfor;

    [SerializeField]
    private Transform buttonPlay;

    [SerializeField]
    private Transform buttonBoss;

    [SerializeField]
    private Transform buttonEquipment;

    [SerializeField]
    private Transform buttonSkin;

    public override void Setup()
    {
        base.Setup();
    }

    public override void Open()
    {
        base.Open();
    }

    public override void Close()
    {
        base.Close();
    }

    public void PlayButton()
    {
        UIManager.Ins.OpenUI<InGame>();
        Close();
    }

    public override void AnimationClose()
    {
        base.AnimationClose();

        statusBar.DOMoveY(2025f, 1f);
        userInfor.DOMoveY(2080f, 1f);
        buttonPlay.DOMoveX(1400f, 1f);
        buttonBoss.DOMoveX(1400f, 1f);
        buttonEquipment.DOMoveY(-200f, 1f);
        buttonSkin.DOMoveY(-200f, 1f);
    }

    public override void AnimationOpen()
    {
        base.AnimationOpen();

        statusBar.DOMoveY(1876f, 1f);
        userInfor.DOMoveY(1900f, 1f);
        buttonPlay.DOMoveX(1040f, 1f);
        buttonBoss.DOMoveX(1040f, 1f);
        buttonEquipment.DOMoveY(89f, 1f);
        buttonSkin.DOMoveY(89f, 1f);
    }
}
