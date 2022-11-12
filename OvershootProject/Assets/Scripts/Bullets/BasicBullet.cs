using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : Bullet
{
    public override void Init(float weight, float speed, bool idealBullet)
    {
        base.Init(weight, speed, idealBullet);
        rb.velocity = speed * transform.forward;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
            return;
        base.OnTriggerEnter(other);
        if (!other.CompareTag("Team1Bullet") && !other.CompareTag("Team2Bullet"))
            Destroy(gameObject);
    }
}
