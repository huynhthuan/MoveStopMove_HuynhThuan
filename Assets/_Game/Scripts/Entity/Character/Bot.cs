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
    private float rangeSearchPoint = 10.0f;
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
        isStartCheckView = false;
    }

    public override void OnInit()
    {
        base.OnInit();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.enabled = true;

        // Equip random weapon
        WeaponEquipment weaponRandom = (WeaponEquipment)characterEquipment.RandomItem<WeaponEquipment>(EquipmentSlot.WEAPON);
        MeshEquipment pantsRandom = characterEquipment.RandomItem<MeshEquipment>(EquipmentSlot.PANT);

        weaponRandom.Use(this);
        pantsRandom.Use(this);

        // ChangeState(new IStateBotIdle());

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
            delayAttack -= Time.fixedDeltaTime;
        }

        if (isStartCheckView)
        {
            FieldOfViewCheck();
        }
    }

    public void OnHit(Transform attacker)
    {
        if (isDead)
        {
            return;
        }

        AudioManager.Ins.PlayAudioInGameFX(AudioType.DIE);

        navMeshAgent.isStopped = true;
        isDead = true;
        int characterIndex = currentStage.characterInStage.IndexOf(this);
        wayPoint.OnDespawn();
        currentStage.characterColorAvaible.Add(currentColor);
        currentStage.OnCharacterDie(characterIndex);
        rb.detectCollisions = false;

        attacker.GetComponent<Character>().ExpUp();

        if (attacker.GetComponent<Character>().expLevelUp.Contains(attacker.GetComponent<Character>().exp))
        {
            attacker.GetComponent<Character>().LevelUp();
            CameraFollow.Ins.LevelUp();
        }

        ChangeState(new IStateBotDie());
        waitAfterDeathCoroutine = StartCoroutine(WaitAnimEnd(anim.GetCurrentAnimatorStateInfo(0).length, () =>
              {
                  StopCoroutine(waitAfterDeathCoroutine);
                  Debug.Log("Anim dead end");
                  OnDespawn();
              }));
    }

    private void FieldOfViewCheck()
    {

        Collider[] targetInVision = Physics.OverlapSphere(TF.position, radius, targetMask);

        if (targetInVision.Length > 0)
        {
            for (int i = 0; i < targetInVision.Length; i++)
            {
                Character targetCharacter = ColliderCache.GetCharacter(targetInVision[i]);

                if (targetCharacter.isDead || targetCharacter == this || !CheckCanSeeTarget(targetCharacter))
                {
                    if (targetCanSee.Contains(targetCharacter))
                    {
                        targetCanSee.Remove(targetCharacter);
                    }
                }
                else
                {
                    if (!targetCanSee.Contains(targetCharacter))
                    {
                        targetCanSee.Add(targetCharacter);
                    }
                }
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
