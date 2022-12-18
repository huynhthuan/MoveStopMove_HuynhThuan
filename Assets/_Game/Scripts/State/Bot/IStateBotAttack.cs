using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IStateBotAttack : IStateBot
{
    public void OnEnter(Bot bot) { }

    public void OnExecute(Bot bot)
    {
        Debug.Log(
            $"{bot.isCanAtk} | {bot.delayAttack <= 0.01f}  | {bot.currentTarget != null} | {!bot.currentTarget.isDead}"
        );

        if (bot.delayAttack <= 0.01f && bot.currentTarget != null && !bot.currentTarget.isDead)
        {
            bot.navMeshAgent.SetDestination(bot.TF.position);
            Debug.Log("Attack");
            bot.StartCoroutineAttackBot();
        }

        if (bot.isAttackAnimEnd)
        {
            bot.ChangeState(new IStateBotIdle());
        }
    }

    public void OnExit(Bot bot) { }
}
