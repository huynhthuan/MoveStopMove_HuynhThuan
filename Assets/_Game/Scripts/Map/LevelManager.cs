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
    internal WayPointIndicator wayPointIndicator;

    [SerializeField]
    private List<StageConfig> stages;

    private DataManager dataManager;

    private Transform TF;

    internal Stage currentStage;

    internal NavMeshSurface navMeshSurface;

    internal Player player;

    public void OnInit()
    {
        Debug.Log("Oninit level manager...");
        dataManager = DataManager.Ins;
        TF = gameObject.transform;
        navMeshSurface = GetComponent<NavMeshSurface>();
    }

    public void LoadStage()
    {

        Debug.Log("Load stage data...");
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
        Debug.Log("Build navmesh stage...");
        navMeshSurface.BuildNavMesh();
        currentStage = stageObj.GetComponent<Stage>();

        // Init stage has loaded
        currentStage.OnInit();
    }




}
