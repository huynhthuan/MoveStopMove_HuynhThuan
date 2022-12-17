using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[CustomEditor(typeof(Bot))]
public class FieldOfViewEditor : Editor
{
    private void OnSceneGUI()
    {
        Bot fov = (Bot)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.TF.position, Vector3.up, Vector3.forward, 360, fov.radius);

        Vector3 viewAngle01 = DirectionFromAngle(fov.TF.eulerAngles.y, -fov.angle / 2);
        Vector3 viewAngle02 = DirectionFromAngle(fov.TF.eulerAngles.y, fov.angle / 2);

        Handles.color = Color.yellow;
        Handles.DrawLine(fov.TF.position, fov.TF.position + viewAngle01 * fov.radius);
        Handles.DrawLine(fov.TF.position, fov.TF.position + viewAngle02 * fov.radius);

        for (int i = 0; i < fov.targetCanSee.Count; i++)
        {
            Handles.color = Color.green;
            Handles.DrawLine(fov.TF.position, fov.targetCanSee[i].TF.position);
            // Debug.Log("Draw line to target in view");
        }

        if (fov.attackTarget != null)
        {
            Handles.DrawWireCube(fov.attackTarget.TF.position, new Vector3(1f, 1f, 1f));
        }
        else
        {
            if (fov.moveTarget != null)
            {
                Handles.DrawWireCube(fov.moveTarget, new Vector3(0.5f, 0.5f, 0.5f));
            }
        }
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(
            Mathf.Sin(angleInDegrees * Mathf.Deg2Rad),
            0,
            Mathf.Cos(angleInDegrees * Mathf.Deg2Rad)
        );
    }
}
#endif
