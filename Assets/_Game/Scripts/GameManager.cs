using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public DataManager dataManager;
    public AudioManager audioManager;
    public LevelManager levelManager;

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
        // dataManager = DataManager.Ins;
        // audioManager = AudioManager.Ins;

        OnInit();
    }

    public void OnInit()
    {
        // // Init data player
        // dataManager.OnInit();
        // // Init audio
        // audioManager.OnInit();
        // // Init level
        // levelManager.OnInit();

        // UIManager.Ins.OpenUI<Lobby>();
    }

    // Update is called once per frame
    void Update() { }
}
