using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : Singleton<CameraFollow>
{
    [SerializeField]
    internal Transform target;

    [SerializeField]
    internal Vector3 offset;

    [SerializeField]
    private Vector3 cameraLobbyPos;

    [SerializeField]
    private Vector3 cameraSkinPos;
    internal bool isMoveCameraToLobby = false;
    internal bool isMoveCameraToSkin = false;
    internal bool isMoveCameraToFollow = false;
    private float time = 0;
    private float duration = 8f;
    internal Transform TF;
    internal Player player;

    private void Start()
    {
        TF = transform;
    }

    private void FixedUpdate()
    {
        if (time < duration)
        {
            if (isMoveCameraToFollow)
            {
                TF.position = Vector3.Lerp(
                    TF.position,
                    LevelManager.Ins.player.TF.position + offset,
                    time / duration
                );
            }

            if (isMoveCameraToSkin)
            {
                TF.position = Vector3.Lerp(
                    TF.position,
                    new Vector3(
                        LevelManager.Ins.player.TF.position.x,
                        cameraSkinPos.y,
                        cameraSkinPos.z
                    ),
                    time / duration
                );
            }

            if (isMoveCameraToLobby)
            {
                TF.position = Vector3.Lerp(
                    TF.position,
                    new Vector3(
                        LevelManager.Ins.player.TF.position.x,
                        cameraLobbyPos.y,
                        cameraLobbyPos.z
                    ),
                    time / duration
                );
            }
            time += Time.fixedDeltaTime;
        }

        if (target != null && time >= duration && isMoveCameraToFollow)
        {
            TF.position = target.position + offset;
        }
    }

    public void SetCameraLobby()
    {
        time = 0;
        isMoveCameraToLobby = true;
        isMoveCameraToFollow = false;
        isMoveCameraToSkin = false;
    }

    public void SetCameraFollow()
    {
        time = 0;
        isMoveCameraToFollow = true;
        isMoveCameraToLobby = false;
        isMoveCameraToSkin = false;
    }

    public void SetCameraSkin()
    {
        time = 0;
        isMoveCameraToSkin = true;
        isMoveCameraToFollow = false;
        isMoveCameraToLobby = false;
    }

    public void LevelUp()
    {
        Debug.Log($"player level {player.level}");
        offset += new Vector3(0, 2.5f, -2.5f) * player.level;
    }
}
