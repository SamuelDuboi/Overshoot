using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ammo : Objects
{
    public AmmoScriptable datas;

    public override void Dispose()
    {
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
