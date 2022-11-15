using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character, IHit
{
    private DataManager dataManager;

    public void OnHit(Transform attacker)
    {
        if (isDead)
        {
            return;
        }

        Debug.Log("Character on hit " + gameObject.name);
        Debug.Log("Attacker make hit " + attacker.name);
        isDead = true;
        rb.detectCollisions = false;
        attacker.GetComponent<Character>().LevelUp();
        ChangeAnim(ConstString.ANIM_DEAD);
        waitAfterDeathCoroutine = StartCoroutine(WaitAnimEnd(anim.GetCurrentAnimatorStateInfo(0).length, () =>
              {
                  StopCoroutine(waitAfterDeathCoroutine);
                  Debug.Log("Anim dead end");
                  OnDespawn();
              }));
    }

    public override void OnInit()
    {
        Debug.Log("Oninit player manager...");
        base.OnInit();
        CameraFollow.Ins.target = TF;

        WeaponConfig currentWeaponData = DataManager.Ins.GetWeaponConfig(DataManager.Ins.playerData.weaponId);
        PantsConfig currentPantsData = DataManager.Ins.GetPantsConfig(DataManager.Ins.playerData.pantsId);

        characterEquipment.EquipWeapon(currentWeaponData);
        Debug.Log("currentPantsData " + JsonUtility.ToJson(currentPantsData));
        characterEquipment.WearPants(currentPantsData);
    }
}
