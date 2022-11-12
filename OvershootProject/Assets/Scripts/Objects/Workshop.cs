using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class Workshop : MonoBehaviour
{
    private Ammo ammoType;
    private WeaponType weaponType;
    public GameObject weaponPrefab;
    private MyController myController;

    public List<GameObject> weaponMeshes;
    
    public bool Dispose(Ammo ammo)
    {
        if (ammoType) return false;
        if (weaponType is not null)
        {
            if (weaponType.type == WeaponTypeEnum.Ideal && ammo.type != AmmoType.Ideal) return false;
            ammoType = ammo;
            CreateWeapon();
        }
        else
        {
            ammoType = ammo;
        }
        return true;
    }
    
    public bool Dispose(WeaponType weapon)
    {
        if (weaponType) return false;
        if (ammoType is not null)
        {
            if (weapon.type != WeaponTypeEnum.Ideal && ammoType.type == AmmoType.Ideal) return false;
            weaponType = weapon;
            CreateWeapon();
        }
        else
        {
            weaponType = weapon;
        }
        return true;
    }

    int GetMeshIndex()
    {
        switch (ammoType.type)
        {
            case AmmoType.Ideal:
                switch (weaponType.type)
                {
                    case WeaponTypeEnum.Ideal:
                        return 0;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            case AmmoType.Rebound:
                switch (weaponType.type)
                {
                    case WeaponTypeEnum.Shotgun:
                        return 1;
                    case WeaponTypeEnum.Sniper:
                        return 2;
                    case WeaponTypeEnum.Rifles:
                        return 3;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            case AmmoType.Explosive:
                switch (weaponType.type)
                {
                    case WeaponTypeEnum.Shotgun:
                        return 4;
                    case WeaponTypeEnum.Sniper:
                        return 5;
                    case WeaponTypeEnum.Rifles:
                        return 6;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            case AmmoType.Big:
                switch (weaponType.type)
                {
                    case WeaponTypeEnum.Shotgun:
                        return 7;
                    case WeaponTypeEnum.Sniper:
                        return 8;
                    case WeaponTypeEnum.Rifles:
                        return 9;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    void CreateWeapon()
    {
        GameObject newWeapon = Instantiate(weaponPrefab, transform.position + transform.right * 5.0f, Quaternion.identity);
        if (weaponType.type == WeaponTypeEnum.Shotgun)
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
        int index = GetMeshIndex();
        Instantiate(weaponMeshes[index], newWeapon.transform);

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
