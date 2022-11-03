using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    [SerializeField]
    internal TargetIndicator targetIndicator;

    private NavMeshAgent navMeshAgent;
    public float range = 1000.0f;

    public override void OnDespawn()
    {
        base.OnDespawn();
    }

    public override void OnInit()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.enabled = true;
        Vector3 point;
        base.OnInit();

        // if (RandomPoint(TF.position, range, out point))
        // {
        //     Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
        // }
    }

    public bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 3000; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 300.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }



    // Update is called once per frame
    void Update()
    {
        // Vector3 point;
        // if (RandomPoint(transform.position, range, out point))
        // {
        //     Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
        // }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 1);
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 1);
    }
}
