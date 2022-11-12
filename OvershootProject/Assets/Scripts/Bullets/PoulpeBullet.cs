using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PoulpeBullet : Bullet
{
    public override void Init(float weight, float speed)
    {
        base.Init(weight, speed);
        rb.velocity = speed * transform.forward;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
            return;
        base.OnTriggerEnter(other);
        
    }
}