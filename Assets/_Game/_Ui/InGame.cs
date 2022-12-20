using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InGame : UICanvas
{
    [SerializeField]
    private UiButton aliveBtn;

    [SerializeField]
    private TMP_Text aliveNumber;

    [SerializeField]
    private UiButton settingButton;

    public override void Setup()
    {
        base.Setup();
        UIManager.Ins.isEnableWaypoint = true;
    }

    public override void Open()
    {
        base.Open();
        GameManager.Ins.cameraFollow.SetCameraFollow();
    }

    public override void AnimationOpen()
    {
        base.AnimationOpen();
        aliveBtn.Show();
        settingButton.Show();
    }

    public override void AnimationClose()
    {
        base.AnimationClose();
        aliveBtn.Hide();
        settingButton.Hide();
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
