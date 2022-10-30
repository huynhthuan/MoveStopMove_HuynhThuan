using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEquipment : MonoBehaviour
{
    [SerializeField]
    private GameObject weaponSlot;

    private Transform weaponSlotTransform;

    // Start is called before the first frame update
    void Start()
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
}
