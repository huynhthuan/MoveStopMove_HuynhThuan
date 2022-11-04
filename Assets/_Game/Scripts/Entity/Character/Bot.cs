using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    [SerializeField]
    internal TargetIndicator targetIndicator;

    private NavMeshAgent navMeshAgent;
    public float range = 10.0f;

    public override void OnDespawn()
    {
        base.OnDespawn();
    }

    public override void OnInit()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.enabled = true;

        base.OnInit();

        NavMeshHit hit;

        while (!NavMesh.SamplePosition(LevelManager.Ins.RandomPointInStage(), out hit, 1.0f, NavMesh.AllAreas))
        {
            NavMesh.SamplePosition(LevelManager.Ins.RandomPointInStage(), out hit, 1.0f, NavMesh.AllAreas);
        }

        TF.position = hit.position;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
