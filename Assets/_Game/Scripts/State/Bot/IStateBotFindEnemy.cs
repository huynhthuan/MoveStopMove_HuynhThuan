using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IStateBotFindEnemy : IStateBot
{
    private Vector3 target;

    public void OnEnter(Bot bot)
    {
        Debug.Log("State find bot");
        bot.isStartCheckView = true;
        GoToNextTarget(bot);
    }

    public void OnExecute(Bot bot)
    {
        bot.ChangeAnim(ConstString.ANIM_RUN);

        if (bot.currentTarget != null)
        {
            bot.ChangeState(new IStateBotAttack());
        }

        if (bot.navMeshAgent.remainingDistance <= 0f)
        {
            bot.ChangeState(new IStateBotIdle());
        }
    }

    public void OnExit(Bot bot) { }

    private void GoToNextTarget(Bot bot)
    {
        NavMeshHit hit;

        if (bot.currentTarget != null)
        {
            Debug.Log("Have bot and move next point nearest bot");
            bool isContinueSearch = true;
            while (isContinueSearch)
            {
                if (NavMesh.SamplePosition(bot.TF.position + Random.insideUnitSphere * 20F, out hit, 4.0f, NavMesh.AllAreas))
                {
                    isContinueSearch = false;
                    target = new Vector3(hit.position.x, 0f, hit.position.z);
                    bot.moveTarget = target;
                    break;
                }
            }
        }
        else
        {
            if (Random.Range(1, 100) > 5 && bot.targetCanSee.Count > 0)
            {
                // Get random target in view
                bot.attackTarget = bot.GetRandomTargetInVision();
                // Debug.Log($"{bot.name} Get next target in target in view - point: {target}");
                bot.navMeshAgent.SetDestination(bot.attackTarget.TF.position);
            }
            else
            {
                // Get random next point in navmesh when no target in view
                bot.attackTarget = null;
                bool isContinueSearch = true;
                while (isContinueSearch)
                {
                    if (NavMesh.SamplePosition(bot.TF.position + Random.insideUnitSphere * 20F, out hit, 4.0f, NavMesh.AllAreas))
                    {
                        isContinueSearch = false;
                        target = new Vector3(hit.position.x, 0f, hit.position.z);
                        bot.moveTarget = target;
                        break;
                    }
                }

                // Move to next target
            }
        }

        Debug.Log("Have bot and move next point nearest bot");
        bot.navMeshAgent.SetDestination(target);
        bot.isCanAtk = true;
    }
}