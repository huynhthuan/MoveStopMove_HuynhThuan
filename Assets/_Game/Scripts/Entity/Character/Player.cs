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
        int characterIndex = currentStage.characterInStage.IndexOf(this);
        currentStage.OnCharacterDie(characterIndex);
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

        WeaponEquipment currentWeaponData = DataManager.Ins.listWeaponEquipment.weapons[DataManager.Ins.playerData.weaponId];
        PantEquipment currentPantsData = DataManager.Ins.listPantEquipment.pants[DataManager.Ins.playerData.pantsId];

        characterEquipment.EquipWeapon(currentWeaponData);
        Debug.Log("currentPantsData " + JsonUtility.ToJson(currentPantsData));
        characterEquipment.WearPants(currentPantsData);
    }

    private void Move(Vector3 direction)
    {
        // Rotation when move
        if (Vector3.Distance(direction, Vector3.zero) > 0.01f)
        {
            Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
            rb.transform.rotation = rotation;
        }

        rb.velocity = direction.normalized * speed * Time.fixedDeltaTime;
    }


    private void FixedUpdate()
    {
        if (isCoolDownAttack)
        {
            delayAttack -= Time.fixedDeltaTime;
        }

        if (joystick != null)
        {
            Move(Vector3.forward * joystick.Vertical + Vector3.right * joystick.Horizontal);
        }

        // Check character stop
        if (Vector3.Distance(Vector3.zero, rb.velocity) <= 0)
        {
            // Check can attack
            if (currentTarget != null)
            {
                if (!currentTarget.isDead && isCanAtk)
                {
                    // Disable can attack
                    isCanAtk = false;
                    // RotationToTarget();
                    // Attack();
                }
            }
            else
            {
                if (isAttackAnimEnd || isCanAtk)
                {
                    // Not has tartget, change idle anim
                    ChangeAnim(ConstString.ANIM_IDLE);
                }
            }
        }
        else
        {
            isCanAtk = true;
            characterEquipment.ShowWeapon();
            ChangeAnim(ConstString.ANIM_RUN);
        }
    }

    public override void LevelUp()
    {
        base.LevelUp();
        attackRange.transform.localScale += characterScaleRatio * 5f;
        anim.transform.localScale += characterScaleRatio;
        anim.transform.localPosition = new Vector3(anim.transform.localPosition.x, anim.transform.localPosition.y - characterScaleRatio.y, anim.transform.localPosition.z);
        attackRange.transform.localPosition = new Vector3(attackRange.transform.localPosition.x, attackRange.transform.localPosition.y - characterScaleRatio.y, attackRange.transform.localPosition.z);
        capsuleCollider.transform.localScale += characterScaleRatio;
        Vector3 cameraFollowOffset = GameManager.Ins.cameraFollow.offset;
        GameManager.Ins.cameraFollow.offset = cameraFollowOffset * cameraFollowScaleRatio;
        if (targetIndicator != null)
        {
            targetIndicator.transform.localScale += characterScaleRatio;
        }
    }
}
