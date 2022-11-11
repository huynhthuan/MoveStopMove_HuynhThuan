using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CharacterEquipment : MonoBehaviour
{
    [SerializeField]
    private GameObject weaponSlot;
    [SerializeField]
    private GameObject pantsSlot;

    public Transform weaponSlotTransform;

    private GameObject currentWeapon;
    internal Weapon currentWeaponBullet;

    // Start is called before the first frame update
    public void Oninit()
    {
        weaponSlotTransform = weaponSlot.transform;
    }

    public void HiddenWeapon()
    {
        weaponSlot.SetActive(false);
    }

    public void ShowWeapon()
    {
        weaponSlot.SetActive(true);
    }

    public void UnEquipWeapon()
    {
        if (currentWeapon != null)
        {
            Destroy(currentWeapon.gameObject);
        }
    }

    public void EquipWeapon(WeaponConfig weaponData)
    {
        UnEquipWeapon();
        GameObject newWeaponObj = Instantiate(weaponData.prefabWeapon, weaponSlotTransform);
        currentWeapon = newWeaponObj;
        currentWeaponBullet = weaponData.prefabWeaponBullet;
    }

    public void WearPants(PantsConfig pantsConfig)
    {
        SkinnedMeshRenderer pantsMeshRenderer = pantsSlot.GetComponent<SkinnedMeshRenderer>();

        Material[] mats = new Material[] { pantsConfig.material };
        pantsMeshRenderer.materials = mats;
    }

    public WeaponConfig RandomWeapon()
    {
        int weaponIndexRandom = Random.Range(0, DataManager.Ins.dataWeapon.weaponItems.Count);
        return DataManager.Ins.dataWeapon.weaponItems[weaponIndexRandom];
    }

    public PantsConfig RandomPants()
    {
        int pantsIndexRandom = Random.Range(0, DataManager.Ins.dataPants.pantsItems.Count);
        return DataManager.Ins.dataPants.pantsItems[pantsIndexRandom];
    }
}
