using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum AmmoType
{
    Ideal,
    Rebound,
    Explosive,
    Big
}

public class Ammo : Objects
{
    public GameObject bullet;

    public AmmoType type;
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
            }
        }
    }
}
