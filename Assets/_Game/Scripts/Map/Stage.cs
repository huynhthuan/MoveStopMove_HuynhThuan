using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField]
    private StageConfig levelConfig;
    [SerializeField]
    internal List<Character> characterInStage = new List<Character>();
    private int playerAlive;
    private int maxBot;
    private Vector3 startPoint;


    public void OnInit()
    {
        Debug.Log("Oninit stage...");
        // Set player alive
        playerAlive = levelConfig.numberOfPlayer;
        maxBot = levelConfig.maxBot;
        startPoint = levelConfig.startPoint;

        //Spawn player
        Player playerObj = (Player)SimplePool.Spawn(LevelManager.Ins.playerPrefab, startPoint, Quaternion.identity);
        playerObj.currentStage = this;
        characterInStage.Add(playerObj);
        playerObj.OnInit();

        // Spawn bot of stage
        if (IsCanSpawnBot())
        {
            SpawnBot(9);
        }
    }

    public void SpawnBot(int numberBot)
    {
        Debug.Log("Start spawn bot...");
        for (int i = 1; i <= numberBot; i++)
        {
            Debug.Log("Start spawn bot index [" + i + "]...");
            Bot botOjb = (Bot)SimplePool.Spawn(LevelManager.Ins.botPrefab, Vector3.zero, Quaternion.identity);
            botOjb.currentStage = this;
            characterInStage.Add(botOjb);
            botOjb.OnInit();
        }
    }

    public bool IsCanSpawnBot()
    {
        return playerAlive > 1 && characterInStage.Count < maxBot;
    }

    public void OnCharacterDie(Character character)
    {
        characterInStage.Remove(character);

        if (IsCanSpawnBot())
        {
            SpawnBot(1);
        }
    }
}
