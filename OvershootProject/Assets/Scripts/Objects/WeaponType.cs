using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponTypeEnum
{
    Shotgun,
    Ideal,
    Sniper,
    Rifles
}

public class WeaponType : Objects
{
    public float fireRate;
    public float bulletSpeed;

    public WeaponTypeEnum type;
    public override void Dispose(float force)
    {
        base.Dispose(force);
        foreach (Collider collider in Physics.OverlapSphere(transform.position, 3.0f))
        {
            Workshop workshop = collider.GetComponent<Workshop>();
            if (workshop is not null)
            {
                if (workshop.Dispose(this))
                    gameObject.SetActive(false);
                return;
            }
        }
    }
}
