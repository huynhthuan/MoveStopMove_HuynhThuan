using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    internal DynamicJoystick joystick;

    internal bool isPlayGame;

    protected void Awake()
    {
        Input.multiTouchEnabled = false;
        Application.targetFrameRate = 60;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        int maxScreenHeight = 1280;
        float ratio =
            (float)Screen.currentResolution.width / (float)Screen.currentResolution.height;
        if (Screen.currentResolution.height > maxScreenHeight)
        {
            Screen.SetResolution(
                Mathf.RoundToInt(ratio * (float)maxScreenHeight),
                maxScreenHeight,
                true
            );
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        OnInit();
    }

    public void OnInit()
    {
        // Init data manager
        DataManager.Ins.OnInit();

        // Init audio
        // AudioManager.Ins.OnInit();

        // Init level manager
        LevelManager.Ins.OnInit();

        // UI init
        // UIManager.Ins.OpenUI<Lobby>();

        // Load new game
        NewGame();
    }

    public void NewGame()
    {
        // Load stage
        LevelManager.Ins.LoadStage();
    }



    // Update is called once per frame
    void Update() { }
}
