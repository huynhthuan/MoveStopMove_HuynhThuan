using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Character character;

    public void OnInit(Character character)
    {
        this.character = character;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        string otherTag = other.tag;
        if (otherTag == ConstString.TAG_BOT)
        {
            Transform enemy = other.transform;
            character.AddTagert(enemy);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        string otherTag = other.tag;
        if (otherTag == ConstString.TAG_BOT)
        {
            Transform enemy = other.transform;
            character.UnSelectTarget(enemy);
            character.RemoveTarget(enemy);
        }
    }

    private void FixedUpdate()
    {
        if (character != null)
        {
            if (character.targets.Count > 0 && spriteRenderer != null)
            {
                spriteRenderer.color = Color.red;
                Transform nearestEnemy = character.FindNearestEnemy();
                character.SelectTarget(nearestEnemy);
            }
            else
            {
                spriteRenderer.color = Color.white;
            }
        }
    }
}
