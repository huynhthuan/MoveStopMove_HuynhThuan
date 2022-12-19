using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    internal DynamicJoystick joystick;

    [SerializeField]
    internal CameraFollow cameraFollow;

    [SerializeField]
    internal Camera mainCamera;

    [SerializeField]
    internal Vector3 characterScaleRatio = new Vector3(0.5f, 0.5f, 0.5f);

    [SerializeField]
    internal float cameraFollowScaleRatio = 2.5f;

    [SerializeField]
    internal Transform itemForCamera;
    internal Camera cameraItem;
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
        DataManager.Ins.playerData.OnInitData();

        // Init audio
        AudioManager.Ins.OnInit();
        AudioManager.Ins.PlayAudioBackground(AudioType.BACKGROUND);

        // Init level manager
        LevelManager.Ins.OnInit();

        // UI init
        UIManager.Ins.OpenUI<Lobby>();

        // Load new game
        NewGame();
    }

    public void NewGame()
    {
        SimplePool.CollectAll();
        // Load stage
        LevelManager.Ins.LoadStage();
        UIManager.Ins.OpenUI<Lobby>();
    }

    // Update is called once per frame
    void Update() { }

    public void TriggerVibrate()
    {
        if (DataManager.Ins.playerData.isEnableVibration)
        {
            Handheld.Vibrate();
        }
    }
}
