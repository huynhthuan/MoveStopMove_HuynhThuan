using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiSoundButton : UiSwitchButton
{
    public override void OnInit()
    {
        base.OnInit();
        isEnable = DataManager.Ins.playerData.isEnableAudio;
    }

    public override void OnClick()
    {
        base.OnClick();
        DataManager.Ins.playerData.SetBoolData(UserData.Key_IsEnableSound, ref DataManager.Ins.playerData.isEnableAudio, !DataManager.Ins.playerData.isEnableAudio);
    }

    public override void BeforeUpdate()
    {
        base.BeforeUpdate();
        isEnable = DataManager.Ins.playerData.isEnableAudio;
        if (isEnable)
        {
            AudioManager.Ins.UnMuteAudio();
        }
        else
        {
            AudioManager.Ins.MuteAudio();
        }
    }
}
