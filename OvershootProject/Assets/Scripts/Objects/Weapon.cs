using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Objects
{
    [SerializeField]
    private Ammo ammo;
    [SerializeField]
    private WeaponType weaponType;

    public Transform firePosition;

    private bool canShoot = true;

    public void Init(Ammo ammo, WeaponType weaponType)
    {
        this.ammo = ammo;
        this.weaponType = weaponType;
        rb = GetComponent<Rigidbody>();
        firePosition = gameObject.GetComponentInChildren<Transform>();
        canShoot = true;
    }
    public override void Grab()
    {
        base.Grab();
        gameObject.layer = 9;
    }
    public override void Dispose(float force)
    {
        base.Dispose(force);
        gameObject.layer = 0;
    }
    public void Fire()
    {
        if (!canShoot) return;
        var bullet = Instantiate(ammo.bullet, firePosition.position, transform.rotation);
        if (transform.parent.parent.CompareTag("Team1Player"))
        {
            bullet.tag = "Team1Bullet";
        }
        else
        {
            bullet.tag = "Team2Bullet";
        }
        StartCoroutine(FireRateTimer());
    }

    private IEnumerator FireRateTimer()
    {
        canShoot = false;
        yield return new WaitForSeconds(weaponType.fireRate);
        canShoot = true;
    }
}