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
    internal Transform TF;
    internal float radiusRatio = 1.23f;
    private float attackRadius;

    [SerializeField]
    internal List<Character> targetsInRange;

    public void OnInit()
    {
        TF = GetComponent<Transform>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        radiusRatio = 1.23f;
    }

    private void FixedUpdate()
    {
        if (character.currentStage.characterInStage.Count > 0)
        {
            GetTargetsInRange();
        }

        if (targetsInRange.Count > 0)
        {
            spriteRenderer.color = Color.red;
            Character nearestEnemy = FindNearestEnemy();
            if (nearestEnemy != null)
            {
                if (character is Player)
                {
                    if (character.currentTarget != null)
                    {
                        character.currentTarget.OnDeSelect();
                        character.currentTarget.OnSelect();
                    }
                }

                character.currentTarget = nearestEnemy;
            }
        }
        else
        {
            spriteRenderer.color = Color.white;
            if (character.currentTarget != null)
            {
                character.currentTarget.OnDeSelect();
                character.currentTarget = null;
            }
        }
    }

    private void GetTargetsInRange()
    {
        // Debug.Log("Get target in range");
        attackRadius = GetAttackRadius();

        for (int i = 0; i < character.currentStage.characterInStage.Count; i++)
        {
            Character enemy = character.currentStage.characterInStage[i];

            if (enemy != character && !enemy.isDead)
            {
                if (Vector3.Distance(character.TF.position, enemy.TF.position) <= attackRadius)
                {
                    if (!targetsInRange.Contains(enemy))
                    {
                        targetsInRange.Add(enemy);
                    }
                }
                else
                {
                    if (targetsInRange.Contains(enemy))
                    {
                        targetsInRange.Remove(enemy);
                    }
                }
            }
        }
    }

    public Character FindNearestEnemy()
    {
        Character nearestEnemy = targetsInRange[0];

        float minDistance = GetDistanceFromTarget(nearestEnemy.TF.position);

        for (int i = 0; i < targetsInRange.Count; i++)
        {
            Character characterInRange = targetsInRange[i];

            if (characterInRange.isDead)
            {
                targetsInRange.Remove(characterInRange);
            }

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

    public float GetAttackRadius()
    {
        return radiusRatio * TF.localScale.x;
    }
}
