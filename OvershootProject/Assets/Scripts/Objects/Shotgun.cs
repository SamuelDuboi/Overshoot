using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon
{
    public override float Fire()
    {
        if (!canShoot) return -1;
        for (int i = 0; i < 3; i++)
        {
            var bullet = Instantiate(ammo.bullet, firePosition.position, transform.rotation);

            Bullet b = bullet.GetComponent<Bullet>();
            b.Init(ammo.weight, weaponType.bulletSpeed);
            b.rb.velocity = weaponType.bulletSpeed * transform.forward;

            if (transform.parent.parent.CompareTag("Team1Player"))
            {
                bullet.tag = "Team1Bullet";
            }
            else
            {
                bullet.tag = "Team2Bullet";
            }
        }

        
        
        StartCoroutine(FireRateTimer());
        return weaponType.fireRate;
    }
}
