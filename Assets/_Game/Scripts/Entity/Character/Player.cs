using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character, IHit

{
    [SerializeField]
    public float speed;
    private DynamicJoystick joystick;
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
        joystick = GameManager.Ins.joystick;

        WeaponConfig currentWeaponData = DataManager.Ins.GetWeaponConfig(DataManager.Ins.playerData.weaponId);
        PantsConfig currentPantsData = DataManager.Ins.GetPantsConfig(DataManager.Ins.playerData.pantsId);

        characterEquipment.EquipWeapon(currentWeaponData);
        Debug.Log("currentPantsData " + JsonUtility.ToJson(currentPantsData));
        characterEquipment.WearPants(currentPantsData);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (joystick != null)
        {
            Move(Vector3.forward * joystick.Vertical + Vector3.right * joystick.Horizontal);
        }

    }

    private void Move(Vector3 direction)
    {
        // Rotation when move
        if (Vector3.Distance(direction, Vector3.zero) > 0.01f)
        {
            Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
            rb.transform.rotation = rotation;
        }

        rb.velocity = direction * speed * Time.fixedDeltaTime;

        // Check character stop
        if (Vector3.Distance(Vector3.zero, rb.velocity) <= 0)
        {
            // Check has target
            if (targets.Count > 0)
            {
                // Debug.Log("isCanAtk " + isCanAtk);
                // Check can attack
                if (isCanAtk)
                {
                    // Disable can attack
                    isCanAtk = false;
                    if (currentTarget != null && !currentTarget.isDead)
                    {
                        RotationToTarget();
                    }
                    Attack();
                }
            }
            else
            {
                // Not has tartget, change idle anim
                ChangeAnim(ConstString.ANIM_IDLE);
            }
        }
        else
        {
            // While player move
            // if (waitAfterAtkCoroutine != null)
            // {
            //     StopCoroutine(waitAfterAtkCoroutine);
            // }
            isCanAtk = true;
            characterEquipment.ShowWeapon();
            ChangeAnim(ConstString.ANIM_RUN);
        }
    }
}
