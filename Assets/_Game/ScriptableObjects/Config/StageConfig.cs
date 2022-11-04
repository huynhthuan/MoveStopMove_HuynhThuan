using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stage", menuName = "ScriptableObjects/Stage", order = 1)]
public class StageConfig : ScriptableObject
{
    public int numberOfPlayer = 100;
    public int maxBot = 9;
    public GameObject stagePrefab;
    public Transform startPoint;
}
