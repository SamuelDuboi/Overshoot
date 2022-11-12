using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Bullet : MonoBehaviour
{
    public Rigidbody rb;

    protected float weight;
    protected float speed;
    protected bool ideal;

    public virtual void Init(float weight, float speed, bool idealBullet)
    {
        this.weight = weight;
        this.speed = speed;
        ideal = idealBullet;
    }
    
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Socket")
            return;
        if ((CompareTag("Team1Bullet") && other.CompareTag("Team1Player")) ||
            (CompareTag("Team2Bullet") && other.CompareTag("Team2Player")))
            return;
        if ((CompareTag("Team1Bullet") && other.CompareTag("Team2Player")) || 
            (CompareTag("Team2Bullet") && other.CompareTag("Team1Player")))
        {
            // Damage player
            if (CompareTag("Team1Bullet"))
            {
                GameManager.instance.GaugeTeam1.FillGauge(rb.velocity.magnitude * weight);
                Destroy(gameObject);
                if (ideal)
                    SceneManager.LoadScene("EndScreen");
            }
            else
            {
                GameManager.instance.GaugeTeam2.FillGauge(rb.velocity.magnitude * weight);
                Destroy(gameObject);
                if (ideal)
                    SceneManager.LoadScene("EndScreen");
            }
            other.GetComponent<MyController>().Throw();
        }
        else if ((other.CompareTag("Team1Bullet") && CompareTag("Team2Bullet")) ||
                 (other.CompareTag("Team2Bullet") && CompareTag("Team1Bullet")))
        {
            rb.velocity = -rb.velocity;
            other.GetComponent<Bullet>().rb.velocity = -other.GetComponent<Bullet>().rb.velocity;
        }
    }
}
