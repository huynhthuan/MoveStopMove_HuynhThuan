using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : GameUnit
{
    [SerializeField]
    internal Rigidbody rb;
    [SerializeField]
    internal Animator anim;
    [SerializeField]
    private AttackRange attackRange;
    [SerializeField]
    internal Transform currentTarget;
    [SerializeField]
    internal List<Transform> targets = new List<Transform>();
    internal CharacterEquipment characterEquipment;
    private string currentAnimName;
    private int scaleRatio = 1;
    internal bool isCanAtk = true;
    internal Coroutine waitAfterAtkCoroutine;
    internal Stage currentStage;
    internal float attackRadius;

    public override void OnInit()
    {
        attackRange.OnInit(this);
        attackRange.character = this;
        characterEquipment = anim.GetComponent<CharacterEquipment>();
        characterEquipment.Oninit();
    }


    public void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            anim.ResetTrigger(animName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }

    public float GetDistanceFromTarget(Vector3 targetPosition)
    {
        return Vector3.Distance(TF.position, targetPosition);
    }

    public Vector3 GetDirToTarget()
    {
        return (currentTarget.position - TF.position).normalized;
    }

    public void RotationToTarget()
    {
        Vector3 direction = GetDirToTarget();
        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
        TF.rotation = rotation;
    }

    public void AddTagert(Transform target)
    {
        targets.Add(target);
    }

    public void Attack()
    {
        ChangeAnim(ConstString.ANIM_ATTACK);
        characterEquipment.HiddenWeapon();
        Vector3 direction = GetDirToTarget();
        GameUnit weaponBulletUnit = SimplePool.Spawn(characterEquipment.currentWeaponBullet, TF.position, Quaternion.LookRotation(direction, Vector3.up));
        Weapon weaponBullet = weaponBulletUnit.GetComponent<Weapon>();
        weaponBullet.SetDir(direction);
        weaponBulletUnit.OnInit();
        waitAfterAtkCoroutine = StartCoroutine(WaitAfterAttack(0.7f));
    }

    public IEnumerator WaitAfterAttack(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        characterEquipment.ShowWeapon();
        Debug.Log("Show weapon");
    }

    public Transform FindNearestEnemy()
    {
        Transform nearestEnemy = targets[0];
        float minDistance = GetDistanceFromTarget(nearestEnemy.position);

        for (int i = 0; i < targets.Count; i++)
        {
            if (Vector3.Distance(TF.position, targets[i].position) < minDistance)
            {
                nearestEnemy = targets[i];
                break;
            }
        }

        return nearestEnemy;
    }

    public void RemoveTarget(Transform target)
    {
        targets.Remove(target);
    }

    public void SelectTarget(Transform target)
    {
        if (currentTarget != null)
        {
            UnSelectTarget(currentTarget);
        }
        currentTarget = target;
        GameObject enemyObj = target.gameObject;
        TargetIndicator enemyIndicator = enemyObj.GetComponentInChildren<TargetIndicator>();
        enemyIndicator.EnableIndicator();
    }

    public void UnSelectTarget(Transform target)
    {
        GameObject enemyObj = target.gameObject;
        TargetIndicator enemyIndicator = enemyObj.GetComponentInChildren<TargetIndicator>();
        enemyIndicator.DisableIndicator();
    }

    public override void OnDespawn()
    {

    }

    public override void OnHit()
    {
        base.OnHit();
        ChangeAnim(ConstString.ANIM_DEAD);
    }
}
