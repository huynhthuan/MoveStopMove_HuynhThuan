using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IStateBotIdle : IStateBot
{
    float randomTime;
    float timer;

    public void OnEnter(Bot bot)
    {
        randomTime = Random.Range(1f, 3);
    }

    public void OnExecute(Bot bot)
    {
        bot.rb.velocity = Vector3.zero;
        bot.ChangeAnim(ConstString.ANIM_IDLE);

        timer += Time.deltaTime;

        if (timer >= randomTime)
        {
            bot.ChangeState(new IStateBotFindEnemy());
        }

        // if (GameManager.Ins.isPlayGame)
        // {


        // }

    }

    public void OnExit(Bot bot) { }
}