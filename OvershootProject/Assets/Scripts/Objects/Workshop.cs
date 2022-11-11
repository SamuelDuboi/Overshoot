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

    public void Dispose(Ammo ammo)
    {
        ammoType = ammo;
        if (weaponType is not null)
        {
            CreateWeapon();
        }
    }
    
    public void Dispose(WeaponType weapon)
    {
        weaponType = weapon;
        if (ammoType is not null)
            CreateWeapon();
    }
    
    void CreateWeapon()
    {
        GameObject newWeapon = Instantiate(weaponPrefab, transform.position + transform.forward * 5.0f, Quaternion.identity);
        Weapon weaponScript = newWeapon.AddComponent<Weapon>();
        weaponScript.Init(ammoType, weaponType);
        ammoType = null;
        weaponType = null;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
