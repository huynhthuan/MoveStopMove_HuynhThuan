using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InGame : UICanvas
{
    [SerializeField]
    private TMP_Text aliveNumber;

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

    private void Update()
    {
        aliveNumber.SetText(LevelManager.Ins.currentStage.playerAlive.ToString());
    }
}
