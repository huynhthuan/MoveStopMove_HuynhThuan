using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IStateBotFindEnemy : IStateBot
{
    private Vector3 target;

    public void OnEnter(Bot bot)
    {
        GoToNextTarget(bot);
    }

    public void OnExecute(Bot bot)
    {
        bot.ChangeAnim(ConstString.ANIM_RUN);

        if (bot.navMeshAgent.remainingDistance <= 0.01f)
        {
            bot.ChangeState(new IStateBotIdle());
        }
    }

    public void OnExit(Bot bot) { }

    private void GoToNextTarget(Bot bot)
    {
        NavMeshHit hit;

        if (Random.Range(1, 100) > 50 && bot.targetCanSee.Count > 0)
        {
            // Get random target in view
            target = bot.GetRandomTargetInVision();
            Debug.Log($"{bot.name} Get next target in target in view - point: {target}");
        }
        else
        {
            // Get random next point in navmesh when no target in view
            bool isContinueSearch = true;
            while (isContinueSearch)
            {
                if (NavMesh.SamplePosition(bot.TF.position + Random.insideUnitSphere * 20F, out hit, 4.0f, NavMesh.AllAreas))
                {
                    isContinueSearch = false;
                    target = new Vector3(hit.position.x, bot.TF.position.y, hit.position.z);
                    break;
                }
            }

            Debug.Log($"{bot.name} Get next target in navmesh - point: {target}");
        }

        Debug.Log($"{bot.name} Go to target: {target}");
        // Move to next target
        bot.navMeshAgent.SetDestination(target);
    }
}