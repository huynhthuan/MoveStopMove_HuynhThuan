using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSkinItem : MonoBehaviour
{
    [SerializeField]
    private Image thumbnail;
    [SerializeField]
    private Image selectBorder;
    [SerializeField]
    private Image lockIcon;

    internal Item itemData;
    internal bool isSelect;
    internal bool isLock;
    internal Transform TF;

    private void Start()
    {
        TF = transform;
    }

    public void OnInit<T>(bool isLock, bool isSelect, T itemData) where T : Item
    {
        this.thumbnail.sprite = itemData.thumbnail;
        this.isLock = isLock;
        this.isSelect = isSelect;
        this.itemData = itemData;
    }


}
