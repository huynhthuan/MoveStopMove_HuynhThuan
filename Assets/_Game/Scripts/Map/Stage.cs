using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField]
    private StageConfig levelConfig;

    private int playerAlive;
    private int maxBot;

    private List<Character> botInStage;

    public void OnInit()
    {
        // Set player alive
        playerAlive = levelConfig.numberOfPlayer;
        maxBot = levelConfig.maxBot;

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

    // Update is called once per frame
    void Update() { }
}
