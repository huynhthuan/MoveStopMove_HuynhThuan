using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class Bot : Character, IHit
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
    public Collider[] targetInVision;
    public List<Transform> targetCanSee;
    public bool isStartCheckView = false;
    internal Transform attackTarget;
    internal Color currentColor;
    internal WayPointIndicator wayPoint;

    public override void OnDespawn()
    {
        base.OnDespawn();
        isStartCheckView = false;
        currentStage.characterColorAvaible.Add(currentColor);
    }

    public override void OnInit()
    {
        base.OnInit();
        targetIndicator.OnInit();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.enabled = true;

        // Equip random weapon
        WeaponEquipment weaponRandom = characterEquipment.RandomWeapon();
        PantEquipment pantsRandom = characterEquipment.RandomPants();

        characterEquipment.EquipWeapon(weaponRandom);
        characterEquipment.WearPants(pantsRandom);

        ChangeState(new IStateBotIdle());

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

        Debug.Log("Character on hit " + gameObject.name);
        Debug.Log("Attacker make hit " + attacker.name);
        navMeshAgent.isStopped = true;
        isDead = true;
        currentStage.OnCharacterDie(this);
        wayPoint.OnDespawn();
        rb.detectCollisions = false;
        attacker.GetComponent<Character>().LevelUp();
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
        targetInVision = null;
        targetInVision = Physics.OverlapSphere(TF.position, radius, targetMask);

        if (targetInVision.Length > 0)
        {
            for (int i = 0; i < targetInVision.Length; i++)
            {


                Character targetCharacter = ColliderCache.GetCharacter(targetInVision[i]);
                Transform targetTF = targetCharacter.TF;

                if (targetCharacter.isDead)
                {
                    if (targetCanSee.Contains(targetTF))
                    {
                        targetCanSee.Remove(targetTF);
                    }
                    continue;
                }

                if (targetTF == this.TF)
                {
                    continue;
                }

                if (CheckCanSeeTarget(targetTF))
                {
                    if (!targetCanSee.Contains(targetTF))
                    {
                        targetCanSee.Add(targetTF);
                    }
                }
                else
                {
                    targetCanSee.Remove(targetTF);
                }
            }
        }
    }

    private bool CheckCanSeeTarget(Transform target)
    {
        bool canSeePlayer = false;
        Vector3 directionToTarget = (target.position - TF.position).normalized;

        // Debug.Log($"Angle between {TF.name} - {target.name} -> {Vector3.Angle(TF.forward, directionToTarget)} | <{angle / 2}>");

        if (Vector3.Angle(TF.forward, directionToTarget) < angle / 2)
        {
            float distanceToTarget = Vector3.Distance(TF.position, target.position);

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

    internal Vector3 GetRandomTargetInVision()
    {
        Transform randomTF = targetCanSee[Random.Range(0, targetCanSee.Count)];
        attackTarget = randomTF;
        return new Vector3(randomTF.position.x, TF.position.y, randomTF.position.z);
    }
}
