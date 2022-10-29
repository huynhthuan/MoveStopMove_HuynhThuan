using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InGame : UICanvas
{
    [SerializeField]
    private Transform aliveBox;

    [SerializeField]
    private Transform settingButton;

    public override void AnimationOpen()
    {
        base.AnimationOpen();
        aliveBox.DOMoveY(1900f, 1f);
        settingButton.DOMoveY(1900f, 1f);
    }

    public override void AnimationClose()
    {
        base.AnimationClose();
        aliveBox.DOMoveY(2000f, 1f);
        settingButton.DOMoveY(2000f, 1f);
    }

    public void ButtonSetting()
    {
        UIManager.Ins.OpenUI<Settings>();
        Close();
    }
}
