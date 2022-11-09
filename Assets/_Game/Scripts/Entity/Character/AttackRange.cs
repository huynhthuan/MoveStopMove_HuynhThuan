using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    internal Character character;

    public void OnInit(Character character)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        this.character = character;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsCanTrigger(other))
        {
            Debug.Log("On trigger enter" + other.name);
            Transform enemy = ColliderCache.GetTransform(other);

            if (!character.targets.Contains(enemy))
            {
                character.AddTagert(enemy);
            }

            if (character.targets.Count > 0)
            {
                spriteRenderer.color = Color.red;
                Transform nearestEnemy = character.FindNearestEnemy();
                character.SelectTarget(nearestEnemy);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (IsCanTrigger(other))
        {
            Debug.Log("On trigger exits" + other.name);
            Transform enemy = ColliderCache.GetTransform(other);
            character.UnSelectTarget(enemy);
            character.RemoveTarget(enemy);
            spriteRenderer.color = Color.white;
        }
    }

    private bool IsCanTrigger(Collider other)
    {
        return other.CompareTag(ConstString.TAG_BOT) || other.CompareTag(ConstString.TAG_PLAYER);
    }
}
