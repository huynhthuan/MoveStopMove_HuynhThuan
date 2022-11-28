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
    internal bool isSelect = false;
    internal bool isLock = true;
    internal Transform TF;

    private Skin uiSkin;
    private void Start()
    {
        TF = transform;
    }

    public void OnInit<T>(Skin uiSkin, bool isLock, bool isSelect, T itemData) where T : Item
    {
        this.thumbnail.sprite = itemData.thumbnail;
        this.isLock = isLock;
        this.isSelect = isSelect;
        this.itemData = itemData;
        this.uiSkin = uiSkin;
    }

    private void Update()
    {
        if (isLock)
        {
            lockIcon.gameObject.SetActive(true);
        }
        else
        {
            lockIcon.gameObject.SetActive(false);
        }

        if (isSelect)
        {
            selectBorder.gameObject.SetActive(true);
        }
        else
        {
            selectBorder.gameObject.SetActive(false);
        }
    }

    public void OnClickItem()
    {
        uiSkin.DeSelectAllItem();
        SelectItem();
    }

    public void SelectItem()
    {
        isSelect = true;
        uiSkin.currentItemSelect = itemData;
        uiSkin.ShowButtonOnItemSelect();
        itemData.Use(uiSkin.player);
    }

    public void DeselectItem()
    {
        uiSkin.currentItemSelect = null;
        isSelect = false;
        uiSkin.ShowButtonOnItemSelect();
    }
}
