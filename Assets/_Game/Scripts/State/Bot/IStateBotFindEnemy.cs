using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IStateBotFindEnemy : IStateBot
{
    private Vector3 target;
    public void OnEnter(Bot bot)
    {

        target = bot.FindEnemy();
    }

    public void OnExecute(Bot bot)
    {
        bot.ChangeAnim(ConstString.ANIM_RUN);
        bot.navMeshAgent.SetDestination(target);
        // if (bot.navMeshAgent.remainingDistance <= 0.01f)
        // {
        //     bot.ChangeState(new IStateBotIdle());
        // }
    }

    public void OnExit(Bot bot) { }
}