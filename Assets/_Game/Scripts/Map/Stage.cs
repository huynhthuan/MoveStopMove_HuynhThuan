using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField]
    private StageConfig levelConfig;

    private int playerAlive;
    private int maxBot;
    private Vector3 startPoint;

    private List<Character> botInStage = new List<Character>();

    public void OnInit()
    {
        // Set player alive
        playerAlive = levelConfig.numberOfPlayer;
        maxBot = levelConfig.maxBot;
        startPoint = levelConfig.startPoint;

        //Spawn player
        Player playerObj = (Player)SimplePool.Spawn(LevelManager.Ins.playerPrefab, startPoint, Quaternion.identity);
        // playerObj.OnInit();

        // Spawn bot of stage
        if (IsCanSpawnBot())
        {
            SpawnBot(9);
        }

    }

    public void SpawnBot(int numberBot)
    {
        for (int i = 1; i <= numberBot; i++)
        {
            Bot botOjb = (Bot)SimplePool.Spawn(LevelManager.Ins.botPrefab, Vector3.zero, Quaternion.identity);
            botOjb.OnInit();
            botInStage.Add(botOjb);
        }
    }

    public bool IsCanSpawnBot()
    {
        return playerAlive > 1 && botInStage.Count < maxBot;
    }

    public void OnCharacterDie(Character character)
    {
        botInStage.Remove(character);

        if (IsCanSpawnBot())
        {
            SpawnBot(1);
        }
    }
}
