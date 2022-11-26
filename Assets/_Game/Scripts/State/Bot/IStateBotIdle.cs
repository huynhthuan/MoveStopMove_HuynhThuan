using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IStateBotIdle : IStateBot
{
    float randomTime;
    float timer;

    public void OnEnter(Bot bot)
    {
        bot.navMeshAgent.velocity = Vector3.zero;
        randomTime = Random.Range(1f, 3f);
        bot.isStartCheckView = false;
    }

    public void OnExecute(Bot bot)
    {
        if (bot.navMeshAgent.enabled)
        {

            bot.ChangeAnim(ConstString.ANIM_IDLE);

            timer += Time.deltaTime;

            if (timer >= randomTime)
            {
                bot.ChangeState(new IStateBotFindEnemy());
            }
        }

    }

    public void OnExit(Bot bot) { }
}