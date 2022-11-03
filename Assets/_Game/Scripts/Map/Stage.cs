using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField]
    private StageConfig levelConfig;

    private int playerAlive;

    public void OnInit()
    {
        playerAlive = levelConfig.numberOfPlayer;
        SpawnBot(1);
    }

    public void SpawnBot(int numberBot)
    {
        for (int i = 1; i <= numberBot; i++)
        {
            Bot botOjb = (Bot)SimplePool.Spawn(LevelManager.Ins.botPrefab, Vector3.zero, Quaternion.identity);
            botOjb.OnInit();
        }

    }

    // Update is called once per frame
    void Update() { }
}
