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

    [SerializeField]
    private UiSoundButton uiSoundButton;

    public override void Setup()
    {
        uiSoundButton.time = 0;
        base.Setup();
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
