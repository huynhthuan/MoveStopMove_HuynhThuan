using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CharacterEquipment : MonoBehaviour
{
    [SerializeField]
    public Transform[] currentEquipementTransform;

    internal Item[] currentEquipment;
    internal GameObject[] currentEquipmentObj;
    private DataManager dataManager;

    public void Oninit()
    {
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Item[numSlots];
        currentEquipmentObj = new GameObject[numSlots];
        dataManager = DataManager.Ins;
    }

    public void EquipWeapon(WeaponEquipment weapon)
    {
        UnEquipWeapon();
        GameObject newWeaponObj = Instantiate(weapon.weaponPrefab.gameObject, currentEquipementTransform[(int)EquipmentSlot.WEAPON]);
        currentEquipment[(int)EquipmentSlot.WEAPON] = weapon;
        currentEquipmentObj[(int)EquipmentSlot.WEAPON] = newWeaponObj;
    }

    public WeaponEquipment GetCurrentWeapon()
    {
        return (WeaponEquipment)currentEquipment[(int)EquipmentSlot.WEAPON];
    }

    public void UnEquipWeapon()
    {
        if (currentEquipment[(int)EquipmentSlot.WEAPON] != null)
        {
            currentEquipment[(int)EquipmentSlot.WEAPON] = null;
            Destroy(currentEquipmentObj[(int)EquipmentSlot.WEAPON].gameObject);
        }
    }


    public void HiddenWeapon()
    {
        currentEquipementTransform[(int)EquipmentSlot.WEAPON].gameObject.SetActive(false);
    }

    public void ShowWeapon()
    {
        currentEquipementTransform[(int)EquipmentSlot.WEAPON].gameObject.SetActive(true);
    }


    public void WearPants(PantEquipment pant)
    {
        SkinnedMeshRenderer pantsMeshRenderer = currentEquipementTransform[(int)EquipmentSlot.PANT].GetComponent<SkinnedMeshRenderer>();
        Material[] mats = new Material[] { pant.material };
        pantsMeshRenderer.materials = mats;
    }

    public WeaponEquipment RandomWeapon()
    {
        int weaponIdRandom = Random.Range(0, dataManager.listWeaponEquipment.weapons.Count);
        return dataManager.listWeaponEquipment.weapons[weaponIdRandom];
    }

    public PantEquipment RandomPants()
    {
        int pantIdRandom = Random.Range(0, dataManager.listPantEquipment.pants.Count);
        return dataManager.listPantEquipment.pants[pantIdRandom];
    }
}
