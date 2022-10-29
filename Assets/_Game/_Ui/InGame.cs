using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGame : UICanvas
{
    [SerializeField]
    private Transform aliveBox;

    [SerializeField]
    private Transform settingButton;

    public override void AnimationOpen()
    {
        base.AnimationOpen();
    }

    public override void AnimationClose()
    {
        base.AnimationClose();
    }

    public void ButtonSetting()
    {
        UIManager.Ins.OpenUI<Settings>();
        Close();
    }
}
