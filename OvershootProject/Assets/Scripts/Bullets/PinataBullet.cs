using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinataBullet : Bullet
{
    public GameObject explosionBullet;

    public override void Init(float weight, float speed)
    {
        base.Init(weight, speed);
        rb.velocity = speed * transform.forward;
    }
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        Instantiate(explosionBullet,transform.position, Quaternion.identity);
        Destroy(gameObject);   
    }
}
