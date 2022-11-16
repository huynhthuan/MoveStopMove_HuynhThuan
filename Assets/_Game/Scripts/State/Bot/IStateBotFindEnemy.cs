using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IStateBotFindEnemy : IStateBot
{
    private Vector3 target;
    public void OnEnter(Bot bot)
    {
        if (bot.targetCanSee.Count <= 0)
        {
            return;
        }

        target = bot.GetRandomTargetInVision();
        Debug.Log($"{bot.name} go to point {bot.GetRandomTargetInVision()}");
    }

    public void OnExecute(Bot bot)
    {
        if (bot.targetCanSee.Count <= 0)
        {
            return;
        }


        Debug.Log($"Targt next {target}");
        bot.navMeshAgent.SetDestination(target);
        bot.ChangeAnim(ConstString.ANIM_RUN);

        Debug.Log($"{bot.name} - Pos {bot.TF.position} --> {target} <{bot.navMeshAgent.remainingDistance}> ");

        // if (bot.navMeshAgent.remainingDistance <= 0.01f)
        // {
        //     bot.ChangeState(new IStateBotIdle());
        // }
    }

    public void OnExit(Bot bot) { }
}