using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField]
    internal Player playerPrefab;

    [SerializeField]
    internal Bot botPrefab;

    [SerializeField]
    private List<StageConfig> stages;

    private DataManager dataManager;

    private Transform TF;

    private Stage currentStage;

    private NavMeshSurface navMeshSurface;

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
        int currentPlayerStage = dataManager.playerData.currentStage;
        StageConfig currentStageConfig = stages[currentPlayerStage];
        GameObject stageObj = Instantiate(currentStageConfig.stagePrefab, Vector3.zero, Quaternion.identity, TF);
        // Bake nav mesh
        navMeshSurface.BuildNavMesh();
        currentStage = stageObj.GetComponent<Stage>();

        // Init stage has loaded
        currentStage.OnInit();
    }

    public Vector3 RandomPointInStage()
    {
        // Get bounds current stage
        Bounds stageBounds = GetComponent<NavMeshSurface>().navMeshData.sourceBounds;
        // Random x
        float rx = Random.Range(stageBounds.min.x, stageBounds.max.x);
        // Random z
        float rz = Random.Range(stageBounds.min.z, stageBounds.max.z);
        // Return random poin in stage
        return new Vector3(rx, 0.9f, rz);
    }

}
