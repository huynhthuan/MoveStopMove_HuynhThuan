using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character, IAction<Transform>
{
    [SerializeField]
    public float speed;

    [SerializeField]
    private DynamicJoystick joystick;

    public void AddTagert(Transform target)
    {
        targets.Add(target);
    }

    public void Attack()
    {
        characterEquipment.HiddenWeapon();
        ChangeAnim(ConstString.ANIM_ATTACK);
        characterEquipment.ShowWeapon();
    }

    public Transform FindNearestEnemy()
    {
        Debug.Log("targets " + targets);
        Transform nearestEnemy = targets[0];
        float minDistance = GetDistanceFromTarget(nearestEnemy.transform.position);

        foreach (Transform target in targets)
        {
            if (Vector3.Distance(transform.position, target.position) < minDistance)
            {
                nearestEnemy = target;
                break;
            }
        }

        return nearestEnemy;
    }

    public void RemoveTarget(Transform target)
    {
        targets.Remove(target);
    }

    public void SelectTarget(Transform target)
    {
        if (currentTarget != null)
        {
            UnSelectTarget(currentTarget);
        }
        currentTarget = target;
        GameObject enemyObj = target.gameObject;
        TargetIndicator enemyIndicator = enemyObj.GetComponentInChildren<TargetIndicator>();
        enemyIndicator.EnableIndicator();
    }

    public void UnSelectTarget(Transform target)
    {
        GameObject enemyObj = target.gameObject;
        TargetIndicator enemyIndicator = enemyObj.GetComponentInChildren<TargetIndicator>();
        enemyIndicator.DisableIndicator();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Move(Vector3.forward * joystick.Vertical + Vector3.right * joystick.Horizontal);
    }

    private void Move(Vector3 direction)
    {
        if (Vector3.Distance(direction, Vector3.zero) > 0.01f)
        {
            Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
            rb.transform.rotation = rotation;
        }

        rb.velocity = direction * speed * Time.fixedDeltaTime;

        if (Vector3.Distance(Vector3.zero, rb.velocity) <= 0)
        {
            if (targets.Count > 0)
            {
                RotationToTarget();
                Attack();
            }
            else
            {
                ChangeAnim(ConstString.ANIM_IDLE);
            }
        }
        else
        {
            Debug.Log("Run");
            ChangeAnim(ConstString.ANIM_RUN);
        }
    }
}
