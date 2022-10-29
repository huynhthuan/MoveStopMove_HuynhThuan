using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    internal Rigidbody rb;

    [SerializeField]
    private Animator anim;

    private string currentAnimName;
    internal CharacterEquipment characterEquipment;

    private void Start()
    {
        characterEquipment = anim.GetComponent<CharacterEquipment>();
    }

    private void OnInit()
    {
        InitItemEquipment();
    }

    public virtual void InitItemEquipment() { }

    public void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            anim.ResetTrigger(animName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }
}
