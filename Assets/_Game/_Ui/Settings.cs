using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : UICanvas
{
    [SerializeField]
    private Transform backButton;

    [SerializeField]
    private Transform homeButton;

    [SerializeField]
    private Transform continueButton;

    [SerializeField]
    private Transform listSetting;

    public override void AnimationOpen()
    {
        base.AnimationOpen();
    }

    public override void AnimationClose()
    {
        base.AnimationClose();
    }

    public void HomeButton()
    {
        UIManager.Ins.OpenUI<Lobby>();
        Close();
    }

    public void ContinueButton()
    {
        UIManager.Ins.OpenUI<InGame>();
        Close();
    }

    public void BackButton()
    {
        UIManager.Ins.OpenUI<Lobby>();
        Close();
    }
}
