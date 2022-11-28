using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IStateBotAttack : IStateBot
{
    public void OnEnter(Bot bot)
    {
        bot.navMeshAgent.velocity = Vector3.zero;
        bot.navMeshAgent.isStopped = true;
    }

    public void OnExecute(Bot bot)
    {
        // Check can attack
        if (bot.currentTarget != null)
        {
            if (!bot.currentTarget.isDead && bot.isCanAtk)
            {
                // Disable can attack
                bot.isCanAtk = false;
                bot.RotationToTarget();

                if (bot.currentTarget == null)
                {
                    return;
                }

                if (bot.currentTarget.isDead)
                {
                    return;
                }

                bot.isAttackAnimEnd = false;

                if (bot.delayAttack >= 0.01f)
                {
                    bot.ChangeAnim(ConstString.ANIM_IDLE);
                    return;
                }

                bot.isCoolDownAttack = true;
                bot.delayAttack = 2f;

                Vector3 direction = bot.GetDirToFireWeapon();
                bot.characterEquipment.HiddenWeapon();

                bot.SpawnWeaponBullet(direction);

                bot.ChangeAnim(ConstString.ANIM_ATTACK);

                float animLength = bot.anim.GetCurrentAnimatorStateInfo(0).length;

                bot.waitAfterAtkCoroutine = bot.StartCoroutine(bot.WaitAnimEnd(animLength, () =>
                {
                    bot.StopCoroutine(bot.waitAfterAtkCoroutine);
                    bot.isAttackAnimEnd = true;
                    bot.characterEquipment.ShowWeapon();
                    Debug.Log($"{bot.name} Show weapon");
                    bot.ChangeState(new IStateBotFindEnemy());
                }));
            }
        }
    }

    public void OnExit(Bot bot) { }
}