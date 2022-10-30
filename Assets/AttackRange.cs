using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Character character;
    private IAction<Transform> action;

    public void OnInit(Character character)
    {
        this.character = character;
        action = character.GetComponent<IAction<Transform>>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Debug.Log(spriteRenderer);
    }

    private void OnTriggerEnter(Collider other)
    {
        string otherTag = other.tag;
        if (otherTag == ConstString.TAG_BOT)
        {
            spriteRenderer.color = Color.red;
            Transform enemy = other.transform;
            action.AddTagert(enemy);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        string otherTag = other.tag;
        if (otherTag == ConstString.TAG_BOT)
        {
            spriteRenderer.color = Color.white;
            Transform enemy = other.transform;
            action.UnSelectTarget(enemy);
            action.RemoveTarget(enemy);
        }
    }

    private void FixedUpdate()
    {
        if (character.targets.Count > 0 && action != null)
        {
            Debug.Log("action " + action);
            Transform nearestEnemy = action.FindNearestEnemy();
            action.SelectTarget(nearestEnemy);
        }
    }
}
