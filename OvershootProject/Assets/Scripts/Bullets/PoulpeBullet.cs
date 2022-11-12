using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PoulpeBullet : Bullet
{
    public int maxRebounds;
    private int nbRebounds;
    
    public override void Init(float weight, float speed)
    {
        base.Init(weight, speed);
        rb.velocity = speed * transform.forward;
        nbRebounds = 0;
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
            return;
        base.OnTriggerEnter(other);
        if (nbRebounds < maxRebounds)
        {
            RaycastHit hit;
            Physics.Raycast(transform.position, rb.velocity.normalized, out hit);
            rb.velocity = speed * Vector3.Reflect(rb.velocity.normalized, hit.normal);
            transform.forward = rb.velocity.normalized;
            ++nbRebounds;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}