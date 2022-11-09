using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetIndicator : MonoBehaviour
{
    private SpriteRenderer spriteIndicator;


    public void OnInit()
    {
        spriteIndicator = GetComponent<SpriteRenderer>();
        DisableIndicator();
    }

    public void EnableIndicator()
    {
        spriteIndicator.enabled = true;
    }

    public void DisableIndicator()
    {
        spriteIndicator.enabled = false;
    }
}
