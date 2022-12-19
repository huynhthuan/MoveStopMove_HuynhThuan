using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR_WIN
[CustomEditor(typeof(Bot))]
public class FieldOfViewEditor : Editor
{
    private void OnSceneGUI()
    {
        Handles.color = Color.green;

        Bot fov = (Bot)target;

        Handles.DrawWireArc(fov.TF.position, Vector3.up, Vector3.forward, 360, fov.radius);

        Vector3 viewAngle01 = DirectionFromAngle(fov.TF.eulerAngles.y, -fov.angle / 2);
        Vector3 viewAngle02 = DirectionFromAngle(fov.TF.eulerAngles.y, fov.angle / 2);

        Handles.DrawLine(fov.TF.position, fov.TF.position + viewAngle01 * fov.radius);
        Handles.DrawLine(fov.TF.position, fov.TF.position + viewAngle02 * fov.radius);

        for (int i = 0; i < fov.targetCanSee.Count; i++)
        {
            Handles.DrawLine(fov.TF.position, fov.targetCanSee[i].TF.position);
        }

        Handles.DrawWireCube(fov.navMeshAgent.destination, new Vector3(0.5f, 0.5f, 0.5f));
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
