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
    private MyController myController;
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
        if (weaponType.isShotgun)
        {
            Shotgun shotgun = newWeapon.AddComponent<Shotgun>();
            shotgun.Init(ammoType, weaponType);
            shotgun.rb.AddForce((transform.right + transform.up).normalized * 5.0f, ForceMode.Impulse);
        }
        else
        {
            Weapon weaponScript = newWeapon.AddComponent<Weapon>();
            weaponScript.Init(ammoType, weaponType);
            weaponScript.rb.AddForce((transform.right + transform.up).normalized * 5.0f, ForceMode.Impulse);
        }

        ammoType = null;
        weaponType = null;

    }

    private void Update()
    {
        var colliders = Physics.OverlapSphere(transform.position, 8);
        foreach (var collider in colliders)
        {
            if (collider.GetComponent<MyController>())
            {
                //TODO
                //check is space avaible 
                //active avaible workshop feedback here
                myController = collider.GetComponent<MyController>();
                myController.workshop = this;
                return;
            }
        }
        if (myController)
        {
            //To Do
            //disable aible workshop feedback here
            myController.workshop = null;
            myController = null;
        }
    }
}
