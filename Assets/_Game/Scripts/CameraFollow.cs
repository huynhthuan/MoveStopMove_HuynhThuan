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

    [SerializeField]
    public LayerMask maskObsctruction;

    [SerializeField]
    private Material materialTransparent;

    internal bool isMoveCameraToLobby = false;
    internal bool isMoveCameraToSkin = false;
    internal bool isMoveCameraToFollow = false;
    private float time = 0;
    private float duration = 8f;
    internal Transform TF;
    internal Player player;
    RaycastHit hit;

    // public List<GameObject> obstructionInactive = new List<GameObject>();

    public Dictionary<MeshRenderer, Material[]> oldMaterials =
        new Dictionary<MeshRenderer, Material[]>();

    private void Start()
    {
        TF = transform;
    }

    private void FixedUpdate()
    {
        Ray rayToCameraPos = new Ray(TF.position, TF.position - player.TF.position);

        if (
            Physics.Raycast(
                TF.position,
                player.TF.position - TF.position,
                out hit,
                Vector3.Distance(TF.position, player.TF.position),
                maskObsctruction
            )
        )
        {
            Debug.DrawRay(TF.position, player.TF.position - TF.position, Color.yellow);

            Collider curentCollider = hit.collider;
            MeshRenderer currentMesh = curentCollider.GetComponent<MeshRenderer>();

            if (!oldMaterials.ContainsKey(currentMesh))
            {
                Material[] currentMaterials = new Material[currentMesh.materials.Length];

                for (int i = 0; i < currentMaterials.Length; i++)
                {
                    currentMaterials[i] = currentMesh.materials[i];
                }

                oldMaterials.Add(currentMesh, currentMaterials);

                Material[] listMaterialTransparent = new Material[currentMaterials.Length];

                for (int i = 0; i < currentMaterials.Length; i++)
                {
                    listMaterialTransparent[i] = materialTransparent;
                }

                currentMesh.materials = listMaterialTransparent;
            }
        }
        else
        {
            foreach (KeyValuePair<MeshRenderer, Material[]> mesh in oldMaterials)
            {
                if (mesh.Key != null)
                {
                    mesh.Key.GetComponent<MeshRenderer>().materials = mesh.Value;
                }
            }

            oldMaterials.Clear();
        }

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
