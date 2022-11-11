using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bot : Character
{
    [SerializeField]
    private LayerMask layerMask;

    internal NavMeshAgent navMeshAgent;
    private float rangeSearchPoint = 10.0f;

    private IStateBot currentState;
    private List<Transform> enemyInVision = new List<Transform>();

    public override void OnDespawn()
    {
        base.OnDespawn();
    }

    public override void OnInit()
    {
        base.OnInit();
        targetIndicator.OnInit();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.enabled = true;

        // Equip random weapon
        WeaponConfig weaponRandom = characterEquipment.RandomWeapon();
        PantsConfig pantsRandom = characterEquipment.RandomPants();

        characterEquipment.EquipWeapon(weaponRandom);
        characterEquipment.WearPants(pantsRandom);

        ChangeState(new IStateBotIdle());
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

    public Vector3 FindEnemy()
    {
        GetEnemysInVision();
        int randomEnemyIndex = Random.Range(0, enemyInVision.Count);

        return new Vector3(enemyInVision[randomEnemyIndex].position.x, 1.08f, enemyInVision[randomEnemyIndex].position.z);
    }

    public List<Transform> GetEnemysInVision()
    {
        for (int i = 0; i < currentStage.characterInStage.Count; i++)
        {
            Character enemy = currentStage.characterInStage[i];
            Debug.Log("Check distance " + TF.position + " " + enemy.TF.position);
            if (Vector3.Distance(TF.position, enemy.TF.position) >= 5f && enemy != this)
            {
                enemyInVision.Add(enemy.TF);
            }
        }

        return enemyInVision;
    }
}
