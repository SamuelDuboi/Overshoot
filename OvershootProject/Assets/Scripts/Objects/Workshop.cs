using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;

public class Workshop : MonoBehaviour
{
    private Ammo ammoType;
    private WeaponType weaponType;
    public GameObject weaponPrefab;

    public bool Dispose(Ammo ammo)
    {
        if (ammoType) return false;
        ammoType = ammo;
        if (weaponType is not null)
        {
            CreateWeapon();
        }
        return true;
    }
    
    public bool Dispose(WeaponType weapon)
    {
        if (weaponType) return false;
        weaponType = weapon;
        if (ammoType is not null)
            CreateWeapon();
        return true;
    }
    
    void CreateWeapon()
    {
        GameObject newWeapon = Instantiate(weaponPrefab, transform.position + transform.right * 5.0f, Quaternion.identity);
        Weapon weaponScript = newWeapon.AddComponent<Weapon>();
        weaponScript.Init(ammoType, weaponType);
        ammoType = null;
        weaponType = null;
        weaponScript.rb.AddForce((transform.right + transform.up).normalized * 5.0f, ForceMode.Impulse);
    }
}
