using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Bullet : MonoBehaviour
{
    public Rigidbody rb;
    
    public float speed;
    public float damage;

    protected void OnTriggerEnter(Collider other)
    {
        if ((CompareTag("Team1Bullet") && other.CompareTag("Team2Player")) || 
            (CompareTag("Team2Bullet") && other.CompareTag("Team1Player")))
        {
            // Damage player
        }

        if (other.CompareTag("Team1Bullet") || other.CompareTag("Team2Bullet"))
        {
            rb.velocity = -rb.velocity;
            other.GetComponent<Bullet>().rb.velocity = -other.GetComponent<Bullet>().rb.velocity;
        }
    }
}
