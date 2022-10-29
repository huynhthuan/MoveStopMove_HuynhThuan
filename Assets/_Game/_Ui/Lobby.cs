using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    }

    public override void AnimationOpen()
    {
        base.AnimationOpen();
    }
}
