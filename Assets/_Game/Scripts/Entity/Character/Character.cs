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
    internal AttackRange attackRange;
    [SerializeField]
    public Character currentTarget;
    [SerializeField]
    internal TargetIndicator targetIndicator;
    [SerializeField]
    internal CharacterEquipment characterEquipment;
    [SerializeField]
    internal CapsuleCollider capsuleCollider;
    [SerializeField]
    public float speed;
    [SerializeField]
    internal DynamicJoystick joystick;
    internal float delayAttack = 0f;
    internal bool isCoolDownAttack = false;
    private string currentAnimName;
    internal int level = 0;
    internal int maxLevel = 5;
    internal bool isCanAtk = true;
    internal Coroutine waitAfterAtkCoroutine;
    internal bool isAttackAnimEnd = false;
    internal Coroutine waitAfterDeathCoroutine;
    internal Stage currentStage;
    internal float attackRadius;
    internal float range = 1f;
    public delegate void CallbackMethod();
    public CallbackMethod m_callback;
    internal bool isDead = false;
    internal Transform colliderTF;
    internal float cameraFollowScaleRatio;
    internal Vector3 characterScaleRatio;
    internal int exp = 0;
    internal List<int> expLevelUp = new List<int>(new int[] { 5, 10, 15, 25, 45, 65 });

    public override void OnInit()
    {
        characterEquipment = anim.GetComponent<CharacterEquipment>();
        characterEquipment.Oninit();
        colliderTF = capsuleCollider.transform;
        cameraFollowScaleRatio = GameManager.Ins.cameraFollowScaleRatio;
        characterScaleRatio = GameManager.Ins.characterScaleRatio;
        joystick = GameManager.Ins.joystick;
        targetIndicator.OnInit();
        attackRange.OnInit();
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

    public Vector3 GetDirToTarget()
    {
        Transform targetColliderTF = currentTarget.colliderTF;
        return (new Vector3(targetColliderTF.position.x, TF.position.y, targetColliderTF.position.z) - TF.position).normalized;
    }

    public Vector3 GetDirToFireWeapon()
    {
        Transform targetColliderTF = currentTarget.colliderTF;
        return (targetColliderTF.position - TF.position).normalized;
    }

    public void RotationToTarget()
    {
        Vector3 direction = GetDirToTarget();
        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
        rb.transform.rotation = rotation;
    }

    public void Attack()
    {
        if (currentTarget == null)
        {
            return;
        }

        if (currentTarget.isDead)
        {
            return;
        }

        isAttackAnimEnd = false;

        if (delayAttack >= 0.01f)
        {
            ChangeAnim(ConstString.ANIM_IDLE);
            return;
        }

        isCoolDownAttack = true;
        delayAttack = 2f;

        Vector3 direction = GetDirToFireWeapon();
        characterEquipment.HiddenWeapon();

        SpawnWeaponBullet(direction);

        StartCoroutineAttack();
    }

    public void StartCoroutineAttack()
    {
        AudioManager.Ins.PlayAudioInGameFX(AudioType.WEAPON_FLY);
        ChangeAnim(ConstString.ANIM_ATTACK);

        float animLength = anim.GetCurrentAnimatorStateInfo(0).length;

        waitAfterAtkCoroutine = StartCoroutine(WaitAnimEnd(animLength, () =>
        {
            StopCoroutine(waitAfterAtkCoroutine);
            isAttackAnimEnd = true;
            characterEquipment.ShowWeapon();
            Debug.Log("Show weapon");
        }));
    }

    public void SpawnWeaponBullet(Vector3 dir)
    {
        Quaternion rotation = Quaternion.LookRotation(dir, Vector3.up);
        Weapon weaponPrefab = characterEquipment.GetCurrentWeaponBullet();
        Weapon weaponBulletUnit = SimplePool.Spawn<Weapon>(weaponPrefab, TF.position, rotation);
        weaponBulletUnit.TF.localScale += level * characterScaleRatio * 20f;
        weaponBulletUnit.SetDir(dir);
        weaponBulletUnit.owner = this;
        weaponBulletUnit.isHasFire = true;
    }

    public IEnumerator WaitAnimEnd(float animLength, CallbackMethod cb)
    {
        yield return new WaitForSeconds(animLength);
        if (cb != null)
        {
            cb();
        }
    }

    public override void OnDespawn()
    {
        SimplePool.Despawn(this);
    }

    public virtual void LevelUp()
    {
        level++;
        AudioManager.Ins.PlayAudioFX(AudioType.LEVEL_UP);
    }

    public void ExpUp()
    {
        exp++;
    }

    public void OnSelect()
    {
        targetIndicator.EnableIndicator();
    }


    public void OnDeSelect()
    {
        targetIndicator.DisableIndicator();
    }
}
