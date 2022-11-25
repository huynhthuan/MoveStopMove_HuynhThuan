using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IStateBotIdle : IStateBot
{
    float randomTime;
    float timer;

    public void OnEnter(Bot bot)
    {
        bot.navMeshAgent.isStopped = true;
        randomTime = Random.Range(1f, 3);
        bot.isStartCheckView = false;
    }

    public void OnExecute(Bot bot)
    {
        if (bot.navMeshAgent.enabled)
        {
            bot.navMeshAgent.isStopped = true;

            bot.ChangeAnim(ConstString.ANIM_IDLE);

            timer += Time.deltaTime;

            if (timer >= randomTime)
            {
                bot.ChangeState(new IStateBotFindEnemy());

                // Debug.Log($"Change to state find enmy");
            }
        }

    }

    public void OnExit(Bot bot) { }
}