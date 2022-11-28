using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class UiTabSkin : MonoBehaviour
{
    [SerializeField]
    internal bool isActive = false;
    [SerializeField]
    private Image backgroundSprite;
    [SerializeField]
    internal bool isTabSkin;
    [SerializeField]
    internal EquipmentSlot equipmentSlot;
    [SerializeField]
    internal Button button;


    private void Update()
    {
        backgroundSprite.color = new Color(backgroundSprite.color.r, backgroundSprite.color.g, backgroundSprite.color.b, isActive ? 0f : 1f);
    }
}
