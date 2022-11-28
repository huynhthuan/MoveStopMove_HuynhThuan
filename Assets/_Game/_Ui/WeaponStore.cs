using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponStore : UICanvas
{
    [SerializeField]
    internal UiButton btnGold;
    [SerializeField]
    internal TextMeshProUGUI weaponName;
    [SerializeField]
    internal Transform selectBtn;
    [SerializeField]
    internal Transform notEnoughBtn;
    [SerializeField]
    internal Transform buyBtn;
    [SerializeField]
    internal TextMeshProUGUI buyBtnPrice;
    [SerializeField]
    internal TextMeshProUGUI notEnoughBtnPrice;
    [SerializeField]
    internal Transform equipedBtn;
    Camera cameraItem;
    private PlayerData playerData;
    private ListEquipment listEquipment;
    private PlayerInventory playerInventory;
    private List<WeaponEquipment> listWeapon = new List<WeaponEquipment>();
    private List<Weapon> listWeaponObj = new List<Weapon>();
    private int currentItemIndex = 0;
    private WeaponEquipment currentWeapon;
    private List<ItemEquip> currentEqipments;
    private CharacterEquipment characterEquipment;
    private Player player;

    private void Start()
    {
        player = LevelManager.Ins.player;
        characterEquipment = LevelManager.Ins.player.characterEquipment;
        currentEqipments = characterEquipment.currentEquipments;
        playerData = DataManager.Ins.playerData;
        listEquipment = DataManager.Ins.listEquipment;
        playerInventory = playerData.playerInventory;
        cameraItem = GameManager.Ins.cameraItem;
        RenderAllWeapon();
        ShowItem(currentItemIndex);
    }

    public override void Open()
    {
        base.Open();
        if (listWeapon.Count > 0)
        {
            ShowItem(0);
        }
    }

    public void RenderAllWeapon()
    {
        listWeapon = listEquipment.GetItemsBySlot<WeaponEquipment>(EquipmentSlot.WEAPON);

        for (int i = 0; i < listWeapon.Count; i++)
        {
            GameObject itemObject = Instantiate(listWeapon[i].prefab, GameManager.Ins.itemForCamera);
            Weapon itemComp = itemObject.GetComponent<Weapon>();
            itemComp.gameObject.layer = 8;
            itemComp.TF.localPosition = Vector3.zero;
            listWeaponObj.Add(itemComp);
        }
    }

    public void ShowItem(int itemIndexShow)
    {
        for (int i = 0; i < listWeapon.Count; i++)
        {

            listWeaponObj[i].gameObject.SetActive(false);
        }
        currentWeapon = listWeapon[itemIndexShow];
        weaponName.text = currentWeapon.itemName;

        if (playerInventory.CheckHasItem(currentWeapon.itemId))
        {
            if (currentEqipments[(int)EquipmentSlot.WEAPON].itemData.itemId == currentWeapon.itemId)
            {
                EnableEquipedBtn();
            }
            else
            {
                EnableSelectBtn();
            }
        }
        else
        {
            if (playerData.gold < currentWeapon.price)
            {
                EnableNotEnoughBtn();
            }
            else
            {
                EnableBuyBtn();
            }

        }

        listWeaponObj[itemIndexShow].gameObject.SetActive(true);
    }

    public void onClickBuyWeapon()
    {
        if (playerData.gold >= currentWeapon.price)
        {
            playerInventory.Add(new InventorySlot((ItemId)currentWeapon.itemId));
            playerData.gold -= currentWeapon.price;

            PlayerPrefs.SetInt(DataManager.Ins.GetKey(DataKey.GOLD), playerData.gold);
            PlayerPrefs.SetString(DataManager.Ins.GetKey(DataKey.INVENTORY), JsonUtility.ToJson(playerData.playerInventory));

            EnableSelectBtn();
        }
    }

    public void OnClickSelect()
    {
        EnableEquipedBtn();
        playerData.weaponId = (int)currentWeapon.itemId;
        PlayerPrefs.SetInt(DataManager.Ins.GetKey(DataKey.WEAPON_ID), playerData.weaponId);
        characterEquipment.EquipItem(currentWeapon.itemId, EquipmentSlot.WEAPON);
    }

    private void DisableAllBtn()
    {
        selectBtn.gameObject.SetActive(false);
        buyBtn.gameObject.SetActive(false);
        notEnoughBtn.gameObject.SetActive(false);
        equipedBtn.gameObject.SetActive(false);
    }

    private void EnableSelectBtn()
    {
        DisableAllBtn();
        selectBtn.gameObject.SetActive(true);
    }

    private void EnableEquipedBtn()
    {
        DisableAllBtn();
        equipedBtn.gameObject.SetActive(true);
    }

    private void EnableBuyBtn()
    {
        DisableAllBtn();
        buyBtnPrice.text = currentWeapon.price.ToString();
        buyBtn.gameObject.SetActive(true);
    }

    private void EnableNotEnoughBtn()
    {
        DisableAllBtn();
        notEnoughBtnPrice.text = currentWeapon.price.ToString();
        notEnoughBtn.gameObject.SetActive(true);
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

    public void CloseButton()
    {
        player.gameObject.SetActive(true);
        UIManager.Ins.OpenUI<Lobby>();
        Close();
    }

    public override void AnimationClose()
    {
        base.AnimationClose();
        btnGold.Hide();
    }

    public override void AnimationOpen()
    {
        base.AnimationOpen();
        btnGold.Show();
    }
}
