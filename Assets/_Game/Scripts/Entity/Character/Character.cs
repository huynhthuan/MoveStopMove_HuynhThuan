using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    internal Rigidbody rb;

    [SerializeField]
    private Animator anim;

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

    private void Start()
    {
        characterEquipment = anim.GetComponent<CharacterEquipment>();
        OnInit();
    }

    private void OnInit()
    {
        attackRange.OnInit(this);
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
        return Vector3.Distance(transform.position, targetPosition);
    }

    public void RotationToTarget()
    {
        Vector3 direction = (currentTarget.position - transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
        rb.transform.rotation = rotation;
    }

    public void AddTagert(Transform target)
    {
        targets.Add(target);
    }

    public void Attack()
    {
        ChangeAnim(ConstString.ANIM_ATTACK);
        characterEquipment.HiddenWeapon();
        GameObject weaponBullet = Instantiate(characterEquipment.currentWeaponBullet);
        Weapon weaponBulletComp = weaponBullet.GetComponent<Weapon>();
        weaponBulletComp.SetTarget(currentTarget);
        weaponBulletComp.OnInit(this);
        weaponBulletComp.FireWeapon();
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
        float minDistance = GetDistanceFromTarget(nearestEnemy.transform.position);

        foreach (Transform target in targets)
        {
            if (Vector3.Distance(transform.position, target.position) < minDistance)
            {
                nearestEnemy = target;
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

    public void FireWeapon() { }
}
