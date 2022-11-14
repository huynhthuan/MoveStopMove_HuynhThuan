using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IStateBotDie : IStateBot
{
    public void OnEnter(Bot bot)
    {

    }

    public void OnExecute(Bot bot)
    {
        bot.ChangeAnim(ConstString.ANIM_DEAD);
    }

    public void OnExit(Bot bot) { }
}