using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lobby : UICanvas
{
    [SerializeField]
    private UiButton goldBtn;

    [SerializeField]
    private UiButton adsBtn;

    [SerializeField]
    private UiButton vibrationBtn;

    [SerializeField]
    private UiButton soundBtn;

    [SerializeField]
    private UiButton weaponBtn;

    [SerializeField]
    private UiButton skinBtn;

    [SerializeField]
    private UiButton playBtn;

    public override void Setup()
    {
        base.Setup();
        UIManager.Ins.isEnableWaypoint = false;
    }

    public override void Open()
    {
        base.Open();
        GameManager.Ins.cameraFollow.SetCameraLobby();
    }

    public override void Close()
    {
        base.Close();
    }

    public void PlayButton()
    {
        UIManager.Ins.OpenUI<InGame>();
        Close();
        LevelManager.Ins.currentStage.botCanPlay = true;
        UIManager.Ins.isEnableWaypoint = true;
    }

    public void WeaponButton()
    {
        LevelManager.Ins.player.gameObject.SetActive(value: false);
        UIManager.Ins.OpenUI<WeaponStore>();
        Close();
    }

    public void SkinButton()
    {
        UIManager.Ins.OpenUI<Skin>();
        Close();
    }

    public override void AnimationClose()
    {
        base.AnimationClose();
        goldBtn.Hide();
        adsBtn.Hide();
        vibrationBtn.Hide();
        soundBtn.Hide();
        playBtn.Hide();
        weaponBtn.Hide();
        skinBtn.Hide();
    }

    public override void AnimationOpen()
    {
        base.AnimationOpen();
        goldBtn.Show();
        adsBtn.Show();
        vibrationBtn.Show();
        soundBtn.Show();
        playBtn.Show();
        weaponBtn.Show();
        skinBtn.Show();
    }

    public void TestNewGame()
    {
        GameManager.Ins.NewGame();
    }
}
