using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : UICanvas
{
    public override void Setup()
    {
        base.Setup();
        LevelManager.Ins.player.isWin = true;
        if (DataManager.Ins.playerData.currentStage + 1 <= 1)
        {
            DataManager.Ins.playerData.SetIntData(
                UserData.Key_Current_Stage,
                ref DataManager.Ins.playerData.currentStage,
                DataManager.Ins.playerData.currentStage + 1
            );
        }
    }

    public override void Open()
    {
        base.Open();
        GameManager.Ins.TriggerVibrate();
    }

    public override void Close()
    {
        base.Close();
    }

    public void OnClickNextStage()
    {
        if (DataManager.Ins.playerData.currentStage > 1)
        {
            return;
        }
        else
        {
            Close();
            GameManager.Ins.NewGame();
        }
    }
}
