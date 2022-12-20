using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiVibrateButton : UiSwitchButton
{
    public override void OnInit()
    {
        base.OnInit();
        isEnable = DataManager.Ins.playerData.isEnableVibration;
    }

    public override void OnClick()
    {
        base.OnClick();
        DataManager.Ins.playerData.SetBoolData(
            UserData.Key_IsEnableVibration,
            ref DataManager.Ins.playerData.isEnableVibration,
            !DataManager.Ins.playerData.isEnableVibration
        );
    }

    public override void BeforeUpdate()
    {
        base.BeforeUpdate();
        isEnable = DataManager.Ins.playerData.isEnableVibration;
    }
}
