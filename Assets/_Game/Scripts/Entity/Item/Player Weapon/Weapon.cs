using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    SWORD,
    HAMMER,
    AXE,
    WAND
}

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private WeaponType weaponType;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }
}
