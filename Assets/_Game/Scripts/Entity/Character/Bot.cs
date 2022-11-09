using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    internal TargetIndicator targetIndicator;
    private NavMeshAgent navMeshAgent;
    private float rangeSearchPoint = 10.0f;

    public override void OnDespawn()
    {
        base.OnDespawn();
    }

    public override void OnInit()
    {
        base.OnInit();
        targetIndicator.OnInit();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.enabled = true;
    }

    // #if UNITY_EDITOR
    //     private void OnDrawGizmos()
    //     {
    //         Gizmos.color = Color.yellow;
    //         Gizmos.DrawWireSphere(transform.position, 1.2f * 3);
    //     }
    // #endif
}
