using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : GameUnit, IHit
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
    [SerializeField]
    internal TargetIndicator targetIndicator;
    [SerializeField]
    internal CharacterEquipment characterEquipment;
    private string currentAnimName;
    private Vector3 scaleRatio = new Vector3(0.5f, 0.5f, 0.5f);
    internal bool isCanAtk = true;
    private Coroutine waitAfterAtkCoroutine;
    private Coroutine waitAfterDeathCoroutine;
    internal Stage currentStage;
    internal float attackRadius;
    internal float range = 1f;
    public delegate void CallbackMethod();
    public CallbackMethod m_callback;
    internal bool isDead = false;

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
        return (new Vector3(currentTarget.position.x, TF.position.y, currentTarget.position.z) - TF.position).normalized;
    }

    public void RotationToTarget()
    {
        Vector3 direction = GetDirToTarget();
        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
        rb.transform.rotation = rotation;
    }

    public void AddTagert(Transform target)
    {
        targets.Add(target);
    }

    public void Attack()
    {
        Character currentTargetCharacter = currentTarget.GetComponent<Character>();

        if (currentTargetCharacter.isDead)
        {
            return;
        }

        ChangeAnim(ConstString.ANIM_ATTACK);
        Vector3 direction = GetDirToTarget();
        characterEquipment.HiddenWeapon();
        GameUnit weaponBulletUnit = SimplePool.Spawn(characterEquipment.currentWeaponBullet, TF.position, Quaternion.LookRotation(direction, Vector3.up));

        Weapon weaponBullet = weaponBulletUnit.GetComponent<Weapon>();
        weaponBullet.SetDir(direction);
        weaponBullet.owner = this;
        weaponBulletUnit.OnInit();
        float animLength = anim.GetCurrentAnimatorStateInfo(0).length;

        waitAfterAtkCoroutine = StartCoroutine(WaitAnimEnd(animLength, () =>
        {
            StopCoroutine(waitAfterAtkCoroutine);
            characterEquipment.ShowWeapon();
            Debug.Log("Show weapon");
        }));
    }

    public IEnumerator WaitAnimEnd(float animLength, CallbackMethod cb)
    {
        yield return new WaitForSeconds(animLength);
        if (cb != null)
        {
            cb();
        }
    }

    public Transform FindNearestEnemy()
    {
        Transform nearestEnemy = targets[0];
        float minDistance = GetDistanceFromTarget(nearestEnemy.position);

        Debug.Log("pos " + nearestEnemy.position + "origin " + TF.position);

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
        Bot enemy = target.GetComponent<Bot>();
        if (this is Player)
        {
            TargetIndicator enemyIndicator = enemy.targetIndicator;
            enemyIndicator.EnableIndicator();
        }
    }

    public void UnSelectTarget(Transform target)
    {
        Bot enemy = target.GetComponent<Bot>();
        if (this is Player)
        {
            TargetIndicator enemyIndicator = enemy.targetIndicator;
            enemyIndicator.DisableIndicator();
        }
    }

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

    public override void OnDespawn()
    {
        SimplePool.Despawn(this);
    }

    public void LevelUp()
    {
        attackRange.transform.localScale += scaleRatio * 2f;
        anim.transform.localScale += scaleRatio;
        if (targetIndicator != null)
        {
            targetIndicator.transform.localScale += scaleRatio;
        }
    }
}
