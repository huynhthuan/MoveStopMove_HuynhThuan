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
        randomTime = Random.Range(1f, 3f);
        bot.isStartCheckView = false;
    }

    public void OnExecute(Bot bot)
    {
        if (bot.navMeshAgent.enabled)
        {
            if (bot.currentTarget != null)
            {
                Debug.Log($"Idle and has target. change to attack");
                bot.ChangeState(new IStateBotAttack());
            }

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
