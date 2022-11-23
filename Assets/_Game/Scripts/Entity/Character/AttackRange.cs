using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    internal Character character;
    private SpriteRenderer spriteRenderer;
    private Transform TF;
    internal Collider[] colliderInRange;
    private float radiusRatio = 1.23f;
    private float radiusDebug;
    private void Start()
    {
        TF = GetComponent<Transform>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private bool IsCanTrigger(Collider other)
    {
        return other.CompareTag(ConstString.TAG_BOT) || other.CompareTag(ConstString.TAG_PLAYER);
    }

    private void FixedUpdate()
    {
        GetTargetInRange();

        if (colliderInRange.Length > 0)
        {
            Character nearestEnemy = FindNearestEnemy();
            character.currentTarget = nearestEnemy;
            nearestEnemy.OnSelect();
        }
        else
        {
            spriteRenderer.color = Color.white;
        }
    }

    private void GetTargetInRange()
    {
        radiusDebug = radiusRatio * character.attackRange.TF.localScale.x;
        colliderInRange = Physics.OverlapSphere(TF.position, radiusDebug, layerMask);


        for (int i = 0; i < colliderInRange.Length; i++)
        {
            Collider other = colliderInRange[i];
            Character enemy = ColliderCache.GetCharacter(other);

            if (enemy != character && Vector3.Distance(character.TF.position, enemy.TF.position) <= radiusDebug && !enemy.isDead)
            {
                spriteRenderer.color = Color.red;
            }
        }
    }

    public Character FindNearestEnemy()
    {
        Character nearestEnemy = ColliderCache.GetCharacter(colliderInRange[0]);
        float minDistance = GetDistanceFromTarget(nearestEnemy.TF.position);

        for (int i = 0; i < colliderInRange.Length; i++)
        {
            Character characterInRange = ColliderCache.GetCharacter(colliderInRange[i]);

            if (Vector3.Distance(TF.position, characterInRange.TF.position) < minDistance)
            {
                nearestEnemy = characterInRange;
            }
        }

        return nearestEnemy;
    }

    public float GetDistanceFromTarget(Vector3 targetPosition)
    {
        return Vector3.Distance(character.TF.position, targetPosition);
    }

    // public void SelectTarget(Character target)
    // {
    //     if (character.currentTarget != null)
    //     {
    //         target.OnDeSelect();
    //     }

    //     character.currentTarget = target;
    //     if (character is Player)
    //     {
    //         target.OnSelect();
    //     }
    // }

    // public void UnSelectTarget()
    // {

    //     if (character is Player)
    //     {
    //         TargetIndicator enemyIndicator = character.currentTarget.targetIndicator;
    //         enemyIndicator.DisableIndicator();
    //         character.currentTarget = null;
    //     }

    //     RemoveTarget(character.currentTarget);
    // }
}
