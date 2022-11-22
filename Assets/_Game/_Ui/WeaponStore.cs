using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStore : UICanvas
{
    [SerializeField]
    Camera cameraItem;
    private ListWeaponEquipment listWeaponEquipment;
    private List<Transform> listWeapon = new List<Transform>();
    private int currentItemIndex = 0;
    private RenderTexture rt;

    private void Start()
    {
        listWeaponEquipment = DataManager.Ins.listWeaponEquipment;
        cameraItem = GameManager.Ins.cameraItem;
        RenderAllWeapon();
        ShowItem(currentItemIndex);
    }

    public void RenderAllWeapon()
    {
        List<WeaponEquipment> weapons = listWeaponEquipment.weapons;
        for (int i = 0; i < weapons.Count; i++)
        {
            Weapon itemObject = Instantiate(weapons[i].weaponPrefab, GameManager.Ins.itemForCamera);
            itemObject.gameObject.layer = 8;
            itemObject.TF.localPosition = Vector3.zero;
            listWeapon.Add(itemObject.TF);
        }
    }

    public void ShowItem(int itemIndexShow)
    {
        for (int i = 0; i < listWeapon.Count; i++)
        {
            listWeapon[i].gameObject.SetActive(false);
        }
        listWeapon[itemIndexShow].gameObject.SetActive(true);
    }

    public void OnClickNext()
    {
        currentItemIndex++;

        if (currentItemIndex > listWeapon.Count - 1)
        {
            currentItemIndex = 0;
            ShowItem(0);
        }
        else
        {

            ShowItem(currentItemIndex);
        }
    }

    public void OnClickPrev()
    {
        currentItemIndex--;

        if (currentItemIndex < 0)
        {
            currentItemIndex = listWeapon.Count - 1;
            ShowItem(listWeapon.Count - 1);
        }
        else
        {
            ShowItem(currentItemIndex);
        }
    }
}
