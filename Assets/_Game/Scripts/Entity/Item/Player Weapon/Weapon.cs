using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    KNIFE,
    HAMMER,
    BOOMERANG,
}

public class Weapon : MonoBehaviour
{
    internal bool isHasFire;
    private Transform target;

    [SerializeField]
    private WeaponType weaponType;
    private Character character;

    public void OnInit(Character character)
    {
        this.character = character;
    }

    public void SetTarget(Transform enemyPosition)
    {
        target = enemyPosition;
    }

    public void FireWeapon()
    {
        isHasFire = true;
    }

    public virtual void Move(Transform target)
    {
        transform.position = Vector3.MoveTowards(
            character.transform.position,
            target.position,
            Time.fixedDeltaTime
        );
    }

    private void FixedUpdate()
    {
        if (isHasFire)
        {
            Move(target);
        }
    }
}
