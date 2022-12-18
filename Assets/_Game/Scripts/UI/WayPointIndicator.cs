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

    [SerializeField]
    private RectTransform rectTransform;
    internal Camera cameraMain;
    internal Bot targetFowllow;
    internal Color currentColor;
    internal bool isStartFollow = false;
    private float padding = 60f;

    public override void OnDespawn()
    {
        isStartFollow = false;
        SimplePool.Despawn(this);
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

        viewPos.x = Mathf.Clamp(viewPos.x, 0.06f, 0.9f);
        viewPos.y = Mathf.Clamp(viewPos.y, 0.06f, 0.9f);

        if ((viewPos.x > 0.06f && viewPos.x < 0.9f) && (viewPos.y > 0.06f && viewPos.y < 0.9f))
        {
            waypoint.SetActive(false);
        }
        else
        {
            waypoint.SetActive(true);
        }

        Vector3 screenPos = cameraMain.ViewportToScreenPoint(viewPos);

        if (screenPos.z < 0f)
        {
            screenPos *= -1f;
        }

        Vector3 canvasPos = screenPos - new Vector3(Screen.width / 2, Screen.height / 2, 0);

        return canvasPos;
    }

    private void FixedUpdate()
    {
        if (isStartFollow)
        {
            text.text = (targetFowllow.level + 1).ToString();
            rectTransform.anchoredPosition = ConvertWPtoCP(targetFowllow.TF.position);
            RotationToTarget();

            // Debug.Log($"check {targetFowllow.name} in view {TargetInView()}");
        }
    }

    private void RotationToTarget()
    {
        Vector3 directionToTarget = (
            targetFowllow.TF.position - LevelManager.Ins.player.TF.position
        ).normalized;
        float angleToTarget =
            TF.localPosition.x > 0
                ? -Vector3.Angle(TF.forward, directionToTarget)
                : Vector3.Angle(TF.forward, directionToTarget);
        // Debug.Log($"{targetFowllow.name} angleToTarget = {angleToTarget}");
        arrowObj.localRotation = Quaternion.Euler(0f, 0f, angleToTarget);
    }
}
