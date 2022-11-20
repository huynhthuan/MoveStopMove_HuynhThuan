using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WayPointIndicator : GameUnit
{
    [SerializeField]
    internal TextMeshProUGUI text;
    [SerializeField]
    internal Image imageInfo;
    [SerializeField]
    internal Image imageArrow;
    [SerializeField]
    internal Transform arrowObj;
    [SerializeField]
    internal GameObject waypoint;
    internal Camera cameraMain;
    internal Bot targetFowllow;
    internal Color currentColor;
    internal bool isStartFollow = false;
    private float padding = 60f;

    public override void OnDespawn()
    {
        isStartFollow = false;
    }

    public override void OnInit()
    {
        ChangeColor(currentColor);
        cameraMain = GameManager.Ins.mainCamera;
        isStartFollow = true;
    }

    public void ChangeColor(Color newColor)
    {
        imageInfo.color = new Color(newColor.r, newColor.g, newColor.b, 1f);
        imageArrow.color = new Color(newColor.r, newColor.g, newColor.b, 1f);
    }

    public Vector3 ConvertWPtoCP(Vector3 point)
    {

        Vector3 viewPos = cameraMain.WorldToViewportPoint(point);

        // Debug.Log($"View point {viewPos}");

        Vector3 screenPos = cameraMain.ViewportToScreenPoint(viewPos);

        // Debug.Log($"Screen point {screenPos}");

        Vector3 canvasPos = screenPos - new Vector3(Screen.width / 2, Screen.height / 2, 0);

        // Debug.Log($"{targetFowllow.name} | Canvas point {canvasPos}");

        float canvasPosXFix = Mathf.Clamp(canvasPos.x, (-Screen.width / 2) + padding, Screen.width / 2 - padding);
        float canvasPosYFix = Mathf.Clamp(canvasPos.y, (-Screen.height / 2) + padding, Screen.height / 2 - padding);

        // Debug.Log($"Canvas point fix {new Vector3(canvasPosXFix, canvasPosYFix, canvasPos.z)}");

        // Debug.Log($"canvasPos point fix {screenPos.x} [{(-Screen.width / 2) + padding} | {Screen.width / 2 - padding}]");

        return new Vector3(canvasPosXFix, canvasPosYFix, canvasPos.z);
    }

    private void FixedUpdate()
    {
        if (isStartFollow)
        {
            TF.localPosition = ConvertWPtoCP(targetFowllow.TF.position);
            RotationToTarget();

            Debug.Log($"check {targetFowllow.name} in view {TargetInView()}");

            if (TargetInView())
            {
                waypoint.SetActive(false);
            }
            else
            {
                waypoint.SetActive(true);
            }

        }

    }

    private void RotationToTarget()
    {
        Vector3 directionToTarget = (targetFowllow.TF.position - LevelManager.Ins.player.TF.position).normalized;
        float angleToTarget = TF.localPosition.x > 0 ? -Vector3.Angle(TF.forward, directionToTarget) : Vector3.Angle(TF.forward, directionToTarget);
        // Debug.Log($"{targetFowllow.name} angleToTarget = {angleToTarget}");
        arrowObj.localRotation = Quaternion.Euler(0f, 0f, angleToTarget);
    }

    private bool TargetInView()
    {
        return (TF.localPosition.x < (Screen.width / 2) - padding && TF.localPosition.x > (-Screen.width / 2) + padding) && (TF.localPosition.y < (Screen.height / 2) - padding && TF.localPosition.y > (-Screen.height / 2) + padding);
    }
}
