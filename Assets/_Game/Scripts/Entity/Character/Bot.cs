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
        List<Character> characterInStage = currentStage.characterInStage;
        int numberCharacterInStage = currentStage.characterInStage.Count;
        bool isInTarget = false;

        for (int i = 0; i < numberCharacterInStage; i++)
        {
            if (Vector3.Distance(hit.position, characterInStage[i].TF.position) <= 1.2f)
            {
                Debug.Log("In target: " + hit.position + " - " + characterInStage[i].TF.position + " Distance: " + Vector3.Distance(hit.position, characterInStage[i].TF.position));
                isInTarget = true;
                break;
            }
        }

        return isInTarget;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 1.2f * 3);
    }
#endif
}
