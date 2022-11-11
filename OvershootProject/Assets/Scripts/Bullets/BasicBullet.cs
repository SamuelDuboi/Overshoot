using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : Bullet
{
    private void Start()
    {
        rb.velocity = speed * transform.forward;
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Weapon")
            return;
        base.OnTriggerEnter(other);
        Destroy(gameObject);
    }
}
