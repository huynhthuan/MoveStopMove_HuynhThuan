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
        Player playerObj = SimplePool.Spawn<Player>(
            LevelManager.Ins.playerPrefab,
            startPoint,
            new Quaternion(0f, 180f, 0f, 0f)
        );
        LevelManager.Ins.player = playerObj;

        playerObj.currentStage = this;
        characterInStage.Add(playerObj);
        playerObj.OnInit();

        // Spawn bot of stage
        if (IsCanSpawnBot())
        {
            SpawnBot(maxBot);
        }
    }

    public void SpawnBot(int numberBot)
    {
        // Debug.Log($"Start spawn {numberBot} bot...");
        for (int i = 1; i <= numberBot; i++)
        {
            // Debug.Log("Start spawn bot index [" + i + "]...");
            Vector3 pointToSpawn = GetPointToSpawn();

            Bot botOjb = SimplePool.Spawn<Bot>(
                LevelManager.Ins.botPrefab,
                pointToSpawn,
                Quaternion.identity
            );


            // Init bot
            botOjb.name = $"Bot {i}";
            botOjb.currentStage = this;
            characterInStage.Add(botOjb);
            botOjb.OnInit();
            // botOjb.navMeshAgent.Warp(pointToSpawn);
            Color newColor = characterColorAvaible[0];
            characterColorAvaible.Remove(newColor);
            botOjb.ChangeColorBody(newColor);

            // Debug.Log($"pointToSpawn {pointToSpawn}");

            ParticlePool.Play(
                LevelManager.Ins.respawnParticle,
                pointToSpawn,
                Quaternion.Euler(90f, 0, 0)
            );

            WayPointIndicator waypointObj = SimplePool.Spawn<WayPointIndicator>(
                LevelManager.Ins.wayPointIndicator,
                Vector3.zero,
                Quaternion.identity
            );


            // Init waypoint indicator
            // waypointObj.targetFowllow = botOjb;
            // waypointObj.currentColor = botOjb.currentColor;
            // waypointObj.OnInit();

            // botOjb.wayPoint = waypointObj;
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

        if (playerAlive == 1)
        {
            if (characterInStage[0] is Player)
            {
                UIManager.Ins.OpenUI<Win>();
            }
            else
            {
                UIManager.Ins.OpenUI<Lose>();
            }
        }
        if (IsCanSpawnBot() && playerAlive - 1 > maxBot)
        {
            SpawnBot(1);
        }
    }

    public Vector3 GetPointToSpawn()
    {
        bool isContinueSearch = true;

        while (isContinueSearch)
        {
            if (NavMesh.SamplePosition(RandomPointInStage(), out hit, 1.0f, NavMesh.AllAreas))
            {
                if (!IsHasTargetInRange())
                {
                    isContinueSearch = false;
                    break;
                }
            }
        }

        // Debug.Log($"Point to spawn bot. {hit.position}");

        return hit.position;
    }

    public Vector3 RandomPointInStage()
    {
        // Get bounds current stage
        Bounds stageBounds = LevelManager.Ins.navMeshSurface.navMeshData.sourceBounds;
        // Debug.Log($"Bounds stage {stageBounds}");
        // Random x
        float rx = Random.Range(stageBounds.min.x + 10f, stageBounds.max.x - 10f);
        // Random z
        float rz = Random.Range(stageBounds.min.z + 10f, stageBounds.max.z - 10f);
        // Return random poin in stage
        return new Vector3(rx, 0.9f, rz);
    }

    // public bool isInAttackRadiusPlayer()
    // {
    //     Vector3 playerPos = LevelManager.Ins.player.TF.position;
    //     float playerAttackRadius = LevelManager.Ins.player.attackRange.GetAttackRadius();
    //     Debug.Log($"Player radius {playerAttackRadius}");
    //     return Vector3.Distance(hit.position, playerPos) <= playerAttackRadius;
    // }


    public bool IsHasTargetInRange()
    {
        int numberCharacterInStage = characterInStage.Count;
        bool isInTarget = false;

        for (int i = 0; i < numberCharacterInStage; i++)
        {
            // Debug.Log($"Distance with {characterInStage[i].name} | {Vector3.Distance(characterInStage[i].TF.position, hit.position)}");

            if (
                !(
                    Vector3.Distance(characterInStage[i].TF.position, hit.position)
                    > characterInStage[i].attackRange.GetAttackRadius() + 4f
                )
            )
            {
                isInTarget = true;
                break;
            }
        }

        return isInTarget;
    }
}
