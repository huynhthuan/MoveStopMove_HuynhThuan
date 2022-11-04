using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    [SerializeField]
    internal TargetIndicator targetIndicator;
    [SerializeField]
    private LayerMask layerMask;
    private NavMeshAgent navMeshAgent;
    private float rangeSearchPoint = 10.0f;
    private NavMeshHit hit;

    public override void OnDespawn()
    {
        base.OnDespawn();
    }

    public override void OnInit()
    {
        base.OnInit();

        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.enabled = true;

        bool isContinueSearch = true;

        while (isContinueSearch)
        {
            NavMesh.SamplePosition(LevelManager.Ins.RandomPointInStage(), out hit, 1.0f, NavMesh.AllAreas);

            if (!IsHasTargetInRange())
            {
                isContinueSearch = false;
            }
        }

        TF.position = hit.position;
    }

    public bool IsHasTargetInRange()
    {
        Collider[] hitColliders = Physics.OverlapSphere(hit.position, 3f, layerMask);
        return hitColliders.Length > 0;
    }
}
