using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
        homeButton.DOMoveX(304f, 1f);
        continueButton.DOMoveX(780f, 1f);
        listSetting.DOMoveX(540f, 1f);
        backButton.DOMoveX(540f, 1f);
    }

    public override void AnimationClose()
    {
        base.AnimationClose();

        homeButton.DOMoveX(-600f, 1f);
        continueButton.DOMoveX(2400f, 1f);
        listSetting.DOMoveX(2400f, 1f);
        backButton.DOMoveX(-3000f, 1f);
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
        UIManager.Ins.OpenUI<InGame>();
        Close();
    }
}
