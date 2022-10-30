using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField]
    private StageConfig levelConfig;


    // Start is called before the first frame update
    void Start()
    {
        OnInit();
    }

    public void OnInit()
    {
        MapManager.Ins.currentStage = this;
    }

    // Update is called once per frame
    void Update() { }
}
