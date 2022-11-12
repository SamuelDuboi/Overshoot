using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objects : MonoBehaviour
{
    public Rigidbody rb;
    public float weight;

    public virtual void Grab()
    {
        GetComponent<Collider>().enabled = false;
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
    }

    public virtual void Dispose( float force)
    {
        rb.isKinematic = false;
        GetComponent<Collider>().enabled = true;

        rb.AddForce(transform.forward * force, ForceMode.Impulse);
    }
}
