using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    internal Rigidbody rb;

    [SerializeField]
    private Animator anim;

    [SerializeField]
    private AttackRange attackRange;

    [SerializeField]
    internal Transform currentTarget;

    [SerializeField]
    internal List<Transform> targets = new List<Transform>();

    internal CharacterEquipment characterEquipment;

    private string currentAnimName;
    private int scaleRatio = 1;

    private void Start()
    {
        characterEquipment = anim.GetComponent<CharacterEquipment>();
        OnInit();
    }

    private void OnInit()
    {
        attackRange.OnInit(this);
    }

    public void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            anim.ResetTrigger(animName);
            currentAnimName = animName;
            anim.SetTrigger(currentAnimName);
        }
    }

    public float GetDistanceFromTarget(Vector3 targetPosition)
    {
        return Vector3.Distance(transform.position, targetPosition);
    }

    public void RotationToTarget()
    {
        Vector3 direction = (currentTarget.position - transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
        rb.transform.rotation = rotation;
    }
}
