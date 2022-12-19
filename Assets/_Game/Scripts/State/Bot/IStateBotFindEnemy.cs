using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IStateBotFindEnemy : IStateBot
{
    private Vector3 target;

    public void OnEnter(Bot bot)
    {
        bot.navMeshAgent.isStopped = false;
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
        else
        {
            if (bot.navMeshAgent.remainingDistance <= 0f)
            {
                bot.ChangeState(new IStateBotIdle());
            }
        }
    }

    public void OnExit(Bot bot) { }

    private void GoToNextTarget(Bot bot)
    {
        if (bot.targetCanSee.Count == 0)
        {
            target = RandomPointInView(bot);
        }
        else
        {
            if (Random.Range(1, 100) > 5)
            {
                // Get random target in view
                bot.attackTarget = null;
                bot.attackTarget = bot.GetRandomTargetInVision();
                target = new Vector3(
                    bot.attackTarget.TF.position.x,
                    0f,
                    bot.attackTarget.TF.position.z
                );
            }
            else
            {
                target = RandomPointInView(bot);
            }
        }

        bot.navMeshAgent.SetDestination(target);
        bot.isCanAtk = true;
    }

    public Vector3 RandomPointInView(Bot bot)
    {
        NavMeshHit hit;
        Vector3 randomTarget = Vector3.zero;

        bool isContinueSearch = true;

        while (isContinueSearch)
        {
            if (
                NavMesh.SamplePosition(
                    bot.TF.position + Random.insideUnitSphere * 20F,
                    out hit,
                    4.0f,
                    NavMesh.AllAreas
                )
            )
            {
                randomTarget = new Vector3(hit.position.x, 0f, hit.position.z);
                isContinueSearch = false;
                break;
            }
        }

        return randomTarget;
    }
}
