using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    [SerializeField]
    private LayerMask layerMask;
    private SpriteRenderer spriteRenderer;
    internal Character character;
    private Transform TF;
    private Collider[] hitColliders;
    private float radiusRatio = 1.23f;
    private void Start()
    {
        TF = GetComponent<Transform>();
    }
    public void OnInit(Character character)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        this.character = character;

    }

    private bool IsCanTrigger(Collider other)
    {
        return other.CompareTag(ConstString.TAG_BOT) || other.CompareTag(ConstString.TAG_PLAYER);
    }

    private void FixedUpdate()
    {
        Debug.Log($"Target count {character.targets.Count}");
        if (character.targets.Count > 0)
        {
            spriteRenderer.color = Color.red;
            Character nearestEnemy = character.FindNearestEnemy();
            character.SelectTarget(nearestEnemy);
        }

        GetTargetInRange();
    }

    private void GetTargetInRange()
    {
        float radius = radiusRatio * character.attackRange.TF.localScale.x;
        hitColliders = Physics.OverlapSphere(TF.position, radius, layerMask);
        Debug.Log($"Collider hit length {hitColliders.Length}");
        for (int i = 0; i < hitColliders.Length; i++)
        {
            Collider other = hitColliders[i];
            Character enemy = ColliderCache.GetCharacter(other);

            if (enemy != this.character)
            {

                character.AddTagert(enemy);

                // bool isCharacterDead = enemy.isDead;
                // bool iSCharacterOutRange = Vector3.Distance(character.TF.position, enemy.TF.position) > radius ? true : false;

                // // Debug.Log($"Range from {character.name} to {enemy.name} - {Vector3.Distance(character.TF.position, enemy.TF.position)}");

                // if (!character.targets.Contains(enemy))
                // {
                //     character.AddTagert(enemy);
                // }

                // if (iSCharacterOutRange || isCharacterDead)
                // {
                //     character.UnSelectTarget(enemy);
                //     spriteRenderer.color = Color.white;
                // }
            }
        }
    }
}
