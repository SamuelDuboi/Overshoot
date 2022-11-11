using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Weapon")
            return;
        if ((CompareTag("Team1Bullet") && other.CompareTag("Team2Player")) || 
            (CompareTag("Team2Bullet") && other.CompareTag("Team1Player")))
        {
            // Damage player
        }
    }
}
