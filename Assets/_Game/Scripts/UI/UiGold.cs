using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiGold : MonoBehaviour
{
    [SerializeField]
    private Text goldText;

    // Update is called once per frame
    void Update()
    {
        goldText.text = DataManager.Ins.playerData.gold.ToString();
    }
}
