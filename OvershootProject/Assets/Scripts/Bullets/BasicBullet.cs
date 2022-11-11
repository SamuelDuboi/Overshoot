using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : Bullet
{
    public override void Init(float weight, float speed)
    {
        base.Init(weight, speed);
        rb.velocity = speed * transform.forward;
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
            return;
        base.OnTriggerEnter(other);
        Destroy(gameObject);
    }
}
