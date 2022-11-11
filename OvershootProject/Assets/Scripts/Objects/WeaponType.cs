using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponType : Objects
{
    public float fireRate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void Dispose()
    {
        Debug.Log("Weapon");
        base.Dispose();
        foreach (Collider collider in Physics.OverlapSphere(transform.position, 10.0f))
        {
            Workshop workshop = collider.GetComponent<Workshop>();
            if (workshop is not null)
            {
                workshop.Dispose(this);
                Destroy(gameObject);
            }
        }
    }
}
