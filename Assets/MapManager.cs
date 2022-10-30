using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : Singleton<MapManager>
{
    [SerializeField]
    internal Stage currentStage;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    public void LoadStage() {

    }

    public void ClearStage()
    {
        Destroy(transform.GetChild(0));
    }
}
