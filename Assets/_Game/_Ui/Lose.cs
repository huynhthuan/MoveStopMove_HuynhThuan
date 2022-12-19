using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Lose : UICanvas
{
    [SerializeField]
    private TMP_Text textRank;

    public override void Setup()
    {
        base.Setup();
        textRank.text = "#" + (LevelManager.Ins.currentStage.playerAlive + 1).ToString();
        GameManager.Ins.TriggerVibrate();
    }

    public override void Open()
    {
        base.Open();
    }

    public void OnClickReplay()
    {
        GameManager.Ins.NewGame();
        UIManager.Ins.OpenUI<Lobby>();
        Close();
    }
}
