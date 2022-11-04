using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField]
    private GameObject playerPrefab;

    [SerializeField]
    internal Bot botPrefab;

    [SerializeField]
    private List<StageConfig> stages;

    private DataManager dataManager;

    private Transform TF;

    private Stage currentStage;

    private NavMeshSurface navMeshSurface;

    public Player player;

    public void OnInit()
    {
        dataManager = DataManager.Ins;
        TF = gameObject.transform;
        navMeshSurface = GetComponent<NavMeshSurface>();
    }

    public void LoadStage()
    {
        // Check has stage
        if (currentStage != null)
        {
            // Clear current stage
            Destroy(currentStage.gameObject);
        }

        // Load new stage
        int currentPlayerStage = dataManager.currentPlayerData.currentStage;
        StageConfig currentStageConfig = stages[currentPlayerStage];
        GameObject stageObj = Instantiate(currentStageConfig.stagePrefab, Vector3.zero, Quaternion.identity, TF);
        navMeshSurface.BuildNavMesh();
        currentStage = stageObj.GetComponent<Stage>();
        currentStage.OnInit();
    }

    public Vector3 RandomPointInStage()
    {
        Bounds stageBounds = GetComponent<NavMeshSurface>().navMeshData.sourceBounds;
        float rx = Random.Range(stageBounds.min.x, stageBounds.max.x);
        float rz = Random.Range(stageBounds.min.z, stageBounds.max.z);
        return new Vector3(rx, 0.9f, rz);
    }

}
