using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinataBullet : Bullet
{
    public GameObject explosionBullet;
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        Instantiate(explosionBullet,transform.position, Quaternion.identity);
        Destroy(explosionBullet);   
    }
}
