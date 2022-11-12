using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinataBullet : Bullet
{
    public GameObject explosionBullet;

    public override void Init(float weight, float speed, bool ideal)
    {
        base.Init(weight, speed, ideal);
        rb.velocity = speed * transform.forward;
    }
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.gameObject.name == "Socket")
            return;
        if ((CompareTag("Team1Bullet") && other.CompareTag("Team1Player")) ||
            (CompareTag("Team2Bullet") && other.CompareTag("Team2Player")))
            return;
        Instantiate(explosionBullet,transform.position, Quaternion.identity);
        Destroy(gameObject);   
    }
}
