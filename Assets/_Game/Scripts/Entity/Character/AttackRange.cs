using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    internal Character character;

    public void OnInit()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (character != null)
        {
            if (other.CompareTag(ConstString.TAG_BOT) || other.CompareTag(ConstString.TAG_PLAYER))
            {
                Debug.Log("On trigger enter" + other.name);
                // Transform enemy = other.GetComponent<Character>().anim.transform;
                // character.AddTagert(enemy);

                // if (character.targets.Count > 0 && spriteRenderer != null)
                // {
                //     spriteRenderer.color = Color.red;

                //     Transform nearestEnemy = character.FindNearestEnemy();

                //     return;
                //     character.SelectTarget(nearestEnemy);
                // }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (character != null)
        {
            if (other.CompareTag(ConstString.TAG_BOT) || other.CompareTag(ConstString.TAG_PLAYER))
            {
                Debug.Log("On trigger exits" + other.name);

                // Transform enemy = other.GetComponent<Character>().anim.transform;
                // character.UnSelectTarget(enemy);
                // character.RemoveTarget(enemy);
                // spriteRenderer.color = Color.white;
            }
        }
    }
}
