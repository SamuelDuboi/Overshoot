using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Bullet : MonoBehaviour
{
    public Rigidbody rb;

    protected float weight;
    protected float speed;

    public virtual void Init(float weight, float speed)
    {
        this.weight = weight;
        this.speed = speed;
    }
    
    protected virtual void OnTriggerEnter(Collider other)
    {
        if ((CompareTag("Team1Bullet") && other.CompareTag("Team2Player")) || 
            (CompareTag("Team2Bullet") && other.CompareTag("Team1Player")))
        {
            // Damage player
            if (CompareTag("Team1Bullet"))
            {
                GameManager.instance.GaugeTeam1.FillGauge(rb.velocity.magnitude * weight);
                Destroy(gameObject);
            }
            else
            {
                GameManager.instance.GaugeTeam2.FillGauge(rb.velocity.magnitude * weight);
                Destroy(gameObject);
            }
        }
        else if (other.CompareTag("Team1Bullet") || other.CompareTag("Team2Bullet"))
        {
            rb.velocity = -rb.velocity;
            other.GetComponent<Bullet>().rb.velocity = -other.GetComponent<Bullet>().rb.velocity;
        }
    }
}
