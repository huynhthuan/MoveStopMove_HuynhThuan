using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public DataManager dataManager;
    public AudioManager audioManager;
    public LevelManager levelManager;

    // Start is called before the first frame update
    void Start()
    {
        dataManager = DataManager.Ins;
        audioManager = AudioManager.Ins;
    }

    public void OnInit()
    {
        // Init data player
        dataManager.OnInit();
        // Init audio
        audioManager.OnInit();
        // Init level
        levelManager.OnInit();
    }

    // Update is called once per frame
    void Update() { }
}
