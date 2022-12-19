using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IStateBotAttack : IStateBot
{
    public void OnEnter(Bot bot) { }

    public void OnExecute(Bot bot)
    {
        // Debug.Log($"{bot.isCanAtk}");
        // Debug.Log($"{bot.delayAttack <= 0.01f}");
        // Debug.Log($"{bot.currentTarget != null}");
        // Debug.Log($"{!bot.currentTarget.isDead}");

        if (bot.currentTarget != null)
        {
            if (bot.delayAttack <= 0.01f && !bot.currentTarget.isDead)
            {
                bot.navMeshAgent.SetDestination(bot.TF.position);
                Debug.Log("Attack");
                bot.RotationToTarget();
                bot.Attack();
            }
        }

        if (bot.isAttackAnimEnd)
        {
            bot.ChangeState(new IStateBotIdle());
        }
    }

    public void OnExit(Bot bot) { }
}
