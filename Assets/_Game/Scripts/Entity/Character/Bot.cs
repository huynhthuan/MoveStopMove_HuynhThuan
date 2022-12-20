using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character, IHit, ISelectable
{
    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private LayerMask targetMask;

    [SerializeField]
    private LayerMask obstructionMask;

    [SerializeField]
    internal SkinnedMeshRenderer bodyRenderer;
    internal NavMeshAgent navMeshAgent;

    // private float rangeSearchPoint = 10.0f;
    private IStateBot currentState;
    private List<Transform> enemyInVision = new List<Transform>();
    public float radius;

    [Range(0, 360)]
    public float angle;
    public List<Character> targetCanSee;
    public bool isStartCheckView = false;

    [SerializeField]
    internal Character attackTarget;
    internal Vector3 moveTarget;
    internal Color currentColor;
    internal WayPointIndicator wayPoint;

    public override void OnDespawn()
    {
        base.OnDespawn();
        targetCanSee.Clear();
        attackTarget = null;
    }

    public override void OnInit()
    {
        base.OnInit();
        isStartCheckView = true;
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.enabled = true;

        // Equip random weapon
        WeaponEquipment weaponRandom = (WeaponEquipment)
            characterEquipment.RandomItem<WeaponEquipment>(EquipmentSlot.WEAPON);
        MeshEquipment pantsRandom = characterEquipment.RandomItem<MeshEquipment>(
            EquipmentSlot.PANT
        );

        HeadEquipment headRandom = characterEquipment.RandomItem<HeadEquipment>(EquipmentSlot.HEAD);

        ShieldEquipment shieldEquipment = characterEquipment.RandomItem<ShieldEquipment>(
            EquipmentSlot.SHIELD
        );

        weaponRandom.Use(this);
        pantsRandom.Use(this);
        headRandom.Use(this);
        shieldEquipment.Use(this);

        ChangeState(new IStateBotIdle());
    }

    public override void OnReset()
    {
        base.OnReset();
    }

    public void ChangeColorBody(Color newColor)
    {
        currentColor = newColor;
        bodyRenderer.material.SetColor("_Color", newColor);
    }

    public void ChangeState(IStateBot newState)
    {
        // Check has current state
        if (currentState != null)
        {
            // Exit current state
            currentState.OnExit(this);
        }

        // Set new state
        currentState = newState;

        // Check set new state success
        if (currentState != null)
        {
            // Enter new state
            currentState.OnEnter(this);
        }
    }

    void Update()
    {
        if (currentState != null)
        {
            currentState.OnExecute(this);
        }
    }

    private void FixedUpdate()
    {
        if (isCoolDownAttack)
        {
            delayAttack -= Time.deltaTime;
        }

        if (isStartCheckView)
        {
            FieldOfViewCheck();
        }
    }

    public void OnHit(Transform attacker)
    {
        wayPoint.OnDespawn();

        if (attacker.GetComponent<Character>() is Player)
        {
            GameManager.Ins.TriggerVibrate();
            UserData playerData = DataManager.Ins.playerData;
            DataManager.Ins.playerData.SetIntData(
                UserData.Key_Gold,
                ref playerData.gold,
                playerData.gold + Random.Range(50, 200)
            );
        }

        ChangeState(new IStateBotDie());

        ParticlePool.Play(
            LevelManager.Ins.explodeParticle,
            new Vector3(TF.position.x, TF.localScale.y / 2, TF.position.z),
            Quaternion.identity
        );

        AudioManager.Ins.PlayAudioInGameFX(AudioType.DIE);

        navMeshAgent.isStopped = true;
        isDead = true;

        currentStage.characterColorAvaible.Add(currentColor);
        currentStage.OnCharacterDie(this);
        rb.detectCollisions = false;

        attacker.GetComponent<Character>().ExpUp();

        if (
            attacker
                .GetComponent<Character>()
                .expLevelUp.Contains(attacker.GetComponent<Character>().exp)
        )
        {
            attacker.GetComponent<Character>().LevelUp();
            CameraFollow.Ins.LevelUp();
        }

        ChangeAnim(ConstString.ANIM_DEAD);
        Debug.Log($"Run die anim");
        waitAfterDeathCoroutine = StartCoroutine(
            WaitAnimEnd(
                anim.GetCurrentAnimatorStateInfo(0).length,
                () =>
                {
                    ParticlePool.Play(
                        LevelManager.Ins.deathParticle,
                        new Vector3(TF.position.x, 2f, TF.position.z),
                        Quaternion.Euler(0f, 180f, 0f)
                    );
                    StopCoroutine(waitAfterDeathCoroutine);
                    Debug.Log("Anim dead end");
                    OnDespawn();
                }
            )
        );
    }

    private void FieldOfViewCheck()
    {
        for (int i = 0; i < currentStage.characterInStage.Count; i++)
        {
            Character currentCharacter = currentStage.characterInStage[i];

            if (currentCharacter != this)
            {
                if (Vector3.Distance(TF.position, currentCharacter.TF.position) <= radius)
                {
                    if (
                        currentCharacter.enabled == false
                        || currentCharacter.isDead
                        || currentCharacter == this
                        || !CheckCanSeeTarget(currentCharacter)
                    )
                    {
                        if (targetCanSee.Contains(currentCharacter))
                        {
                            targetCanSee.Remove(currentCharacter);
                        }
                    }
                    else
                    {
                        if (!targetCanSee.Contains(currentCharacter))
                        {
                            targetCanSee.Add(currentCharacter);
                        }
                    }
                }
            }
        }

        for (int i = 0; i < targetCanSee.Count; i++)
        {
            Character currentTarGetCanSee = targetCanSee[i];
            if (currentTarGetCanSee.isDead || currentTarGetCanSee.enabled == false)
            {
                targetCanSee.Remove(currentTarGetCanSee);
            }
        }

        if (attackTarget != null)
        {
            if (
                attackTarget.isDead
                || attackTarget.enabled == false
                || Vector3.Distance(TF.position, attackTarget.TF.position)
                    > attackRange.GetAttackRadius()
            )
            {
                attackTarget = null;
            }
        }
    }

    private bool CheckCanSeeTarget(Character target)
    {
        bool canSeePlayer = false;
        Vector3 directionToTarget = (target.TF.position - TF.position).normalized;

        // Debug.Log($"Angle between {TF.name} - {target.name} -> {Vector3.Angle(TF.forward, directionToTarget)} | <{angle / 2}>");

        if (Vector3.Angle(TF.forward, directionToTarget) < angle / 2)
        {
            float distanceToTarget = Vector3.Distance(TF.position, target.TF.position);

            if (!Physics.Raycast(TF.position, directionToTarget, distanceToTarget, obstructionMask))
            {
                canSeePlayer = true;
            }
            else
            {
                canSeePlayer = false;
            }
        }
        else
        {
            canSeePlayer = false;
        }

        return canSeePlayer;
    }

    internal Character GetRandomTargetInVision()
    {
        Character randomTarget = targetCanSee[Random.Range(0, targetCanSee.Count)];

        return randomTarget;
    }
}
