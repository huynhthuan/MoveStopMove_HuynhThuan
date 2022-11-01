using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CharacterEquipment : MonoBehaviour
{
    [SerializeField]
    private GameObject weaponSlot;

    private Transform weaponSlotTransform;

    private GameObject currentWeapon;
    internal Weapon currentWeaponBullet;

    public string weaponId;

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
}

#if UNITY_EDITOR
[CustomEditor(typeof(CharacterEquipment))]
public class ChangeWeaponHolderButton : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("Change Weapon"))
        {
            ((CharacterEquipment)target).EquipWeapon(
                DataManager.Ins.GetDataWeapon(((CharacterEquipment)target).weaponId)
            );
        }
    }
}
#endif
