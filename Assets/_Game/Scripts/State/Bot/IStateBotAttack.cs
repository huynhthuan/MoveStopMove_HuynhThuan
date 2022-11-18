using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IStateBotAttack : IStateBot
{
    private bool isHasAttack = false;
    public void OnEnter(Bot bot)
    {
        isHasAttack = true;
        bot.Attack();
    }


    public void OnExecute(Bot bot)
    {
        if (isHasAttack && bot.isAttackAnimEnd)
        {
            isHasAttack = false;
            bot.ChangeState(new IStateBotFindEnemy());
        }
    }

    public void OnExit(Bot bot) { }
}