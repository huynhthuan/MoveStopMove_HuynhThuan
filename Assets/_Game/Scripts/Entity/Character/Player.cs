using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField]
    public float speed;

    [SerializeField]
    private DynamicJoystick joystick;

    public override void InitItemEquipment()
    {
        base.InitItemEquipment();
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
        if (Vector3.Distance(Vector3.zero, rb.velocity) > 0f)
        {
            ChangeAnim(ConstString.ANIM_RUN);
        }
        else
        {
            ChangeAnim(ConstString.ANIM_IDLE);
        }
    }
}
