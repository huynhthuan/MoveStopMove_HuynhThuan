using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum CharacterColor
{
    COLOR_1,
    COLOR_2,
    COLOR_3,
    COLOR_4,
    COLOR_5,
    COLOR_6,
    COLOR_7,
    COLOR_8,
    COLOR_9,
    COLOR_10,
}

public class Stage : MonoBehaviour
{
    [SerializeField]
    private StageConfig levelConfig;
    [SerializeField]
    internal List<Character> characterInStage = new List<Character>();
    [SerializeField]
    internal Color[] botColors;
    internal int playerAlive;
    private int maxBot;
    private Vector3 startPoint;
    private NavMeshHit hit;
    internal List<Color> characterColorAvaible = new List<Color>();

    public void OnInit()
    {
        Debug.Log("Oninit stage...");
        characterColorAvaible.AddRange(botColors);
        // Set player alive
        playerAlive = levelConfig.numberOfPlayer;
        maxBot = levelConfig.maxBot;
        startPoint = levelConfig.startPoint;
        Debug.Log("Start point: " + startPoint);

        //Spawn player
        Player playerObj = SimplePool.Spawn<Player>(LevelManager.Ins.playerPrefab, startPoint, Quaternion.identity);
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
        Debug.Log($"Start spawn {numberBot} bot...");
        for (int i = 1; i <= numberBot; i++)
        {
            // Debug.Log("Start spawn bot index [" + i + "]...");
            Vector3 pointToSpawn = GetPointToSpawn();
            Bot botOjb = SimplePool.Spawn<Bot>(LevelManager.Ins.botPrefab, Vector3.zero, Quaternion.identity);
            WayPointIndicator waypointObj = SimplePool.Spawn<WayPointIndicator>(LevelManager.Ins.wayPointIndicator, Vector3.zero, Quaternion.identity);

            // Init bot
            botOjb.name = $"Bot - {i}";
            botOjb.currentStage = this;
            characterInStage.Add(botOjb);
            botOjb.OnInit();
            Color newColor = characterColorAvaible[0];
            characterColorAvaible.Remove(newColor);
            botOjb.ChangeColorBody(newColor);
            botOjb.TF.position = pointToSpawn;

            // Init waypoint indicator
            waypointObj.targetFowllow = botOjb;
            waypointObj.currentColor = botOjb.currentColor;
            waypointObj.OnInit();

            botOjb.wayPoint = waypointObj;
        }
    }

    public bool IsCanSpawnBot()
    {
        return (playerAlive > 2 && characterInStage.Count - 1 < maxBot);
    }

    public void OnCharacterDie(int characterIndex)
    {
        characterInStage.RemoveAt(characterIndex);
        playerAlive--;
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

        // Debug.Log("Spawn bot to point: " + hit.position);

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
