using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Stage : MonoBehaviour
{
    [SerializeField]
    private StageConfig levelConfig;
    [SerializeField]
    internal List<Character> characterInStage = new List<Character>();
    private int playerAlive;
    private int maxBot;
    private Vector3 startPoint;

    private NavMeshHit hit;

    public void OnInit()
    {
        Debug.Log("Oninit stage...");
        // Set player alive
        playerAlive = levelConfig.numberOfPlayer;
        maxBot = levelConfig.maxBot;
        startPoint = levelConfig.startPoint;
        Debug.Log("Start point: " + startPoint);

        //Spawn player
        Player playerObj = (Player)SimplePool.Spawn(LevelManager.Ins.playerPrefab, startPoint, Quaternion.identity);
        playerObj.currentStage = this;
        characterInStage.Add(playerObj);
        playerObj.OnInit();
        LevelManager.Ins.player = playerObj;

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
            Vector3 pointToSpawn = GetPointToSpawn();
            Bot botOjb = (Bot)SimplePool.Spawn(LevelManager.Ins.botPrefab, Vector3.zero, Quaternion.identity);
            botOjb.currentStage = this;
            characterInStage.Add(botOjb);
            botOjb.OnInit();
            botOjb.TF.position = pointToSpawn;
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

    public Vector3 GetPointToSpawn()
    {
        Debug.Log("Get point to spawn.");
        bool isContinueSearch = true;

        while (isContinueSearch)
        {
            NavMesh.SamplePosition(LevelManager.Ins.RandomPointInStage(), out hit, 1.0f, NavMesh.AllAreas);

            if (!IsHasTargetInRange() && (Vector3.Distance(hit.position, LevelManager.Ins.player.TF.position) >= 4f))
            {
                isContinueSearch = false;
                break;
            }
        }

        Debug.Log("Spawn bot to point: " + hit.position);

        return hit.position;
    }

    public bool IsHasTargetInRange()
    {
        int numberCharacterInStage = characterInStage.Count;
        bool isInTarget = false;

        for (int i = 0; i < numberCharacterInStage; i++)
        {
            // Debug.Log("In target: " + hit.position + " - " + characterInStage[i].TF.position + " Distance: " + Vector3.Distance(hit.position, characterInStage[i].TF.position));
            if (Vector3.Distance(hit.position, characterInStage[i].TF.position) <= 5f)
            {

                isInTarget = true;
                break;
            }
        }

        return isInTarget;
    }
}
