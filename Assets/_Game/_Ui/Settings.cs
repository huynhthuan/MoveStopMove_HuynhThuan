using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : UICanvas
{
    [SerializeField]
    private UiSoundButton uiSoundButton;

    public override void Setup()
    {
        uiSoundButton.time = 0;
        base.Setup();
        UIManager.Ins.isEnableWaypoint = false;
    }

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
        GameManager.Ins.NewGame();
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
