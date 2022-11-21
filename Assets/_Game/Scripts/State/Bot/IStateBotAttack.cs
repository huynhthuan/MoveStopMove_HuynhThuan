using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IStateBotAttack : IStateBot
{
    public void OnEnter(Bot bot)
    {
        bot.navMeshAgent.isStopped = true;

    }

    public void OnExecute(Bot bot)
    {
        // bot.Attack();
        // Debug.Log("Attack");
    }

    public void OnExit(Bot bot) { }
}