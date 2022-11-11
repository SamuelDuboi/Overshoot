using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Objects
{
    private Ammo ammo;
    private WeaponType weaponType;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(Ammo ammo, WeaponType weaponType)
    {
        this.ammo = ammo;
        this.weaponType = weaponType;
    }
}
