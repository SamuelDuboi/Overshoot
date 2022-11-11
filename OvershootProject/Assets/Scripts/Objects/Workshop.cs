using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Workshop : MonoBehaviour
{
    private Ammo ammoType;
    private WeaponType weaponType;
    public GameObject weaponPrefab;

    public Weapon Dispose(Ammo ammo)
    {
        ammoType = ammo;
        if (weaponType is not null)
            return CreateWeapon();
        return null;
    }
    
    public Weapon Dispose(WeaponType weapon)
    {
        weaponType = weapon;
        if (ammoType is not null)
            return CreateWeapon();
        return null;
    }
    
    Weapon CreateWeapon()
    {
        Weapon weapon = weaponPrefab.GetComponent<Weapon>();
        weapon.Init(ammoType, weaponType);
        ammoType = null;
        weaponType = null;
        return weapon;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
