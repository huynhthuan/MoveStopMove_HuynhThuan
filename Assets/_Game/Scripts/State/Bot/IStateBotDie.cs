using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IStateBotDie : IStateBot
{
    public void OnEnter(Bot bot)
    {
        bot.navMeshAgent.isStopped = true;
    }

    public void OnExecute(Bot bot) { }

    public void OnExit(Bot bot) { }
}
