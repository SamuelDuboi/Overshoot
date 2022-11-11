using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : Bullet
{
    public Rigidbody rb;
    private void Start()
    {
        rb.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        
        Destroy(gameObject);
    }
}
