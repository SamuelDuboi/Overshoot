using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponType : Objects
{
    public float fireRate;

    public override void Dispose(float force)
    {
        base.Dispose(force);
        foreach (Collider collider in Physics.OverlapSphere(transform.position, 10.0f))
        {
            Workshop workshop = collider.GetComponent<Workshop>();
            if (workshop is not null)
            {
                workshop.Dispose(this);
                gameObject.SetActive(false);
            }
        }
    }
}
