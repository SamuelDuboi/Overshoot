using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objects : MonoBehaviour
{
    public Rigidbody rb;

    public virtual void Grab()
    {
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
    }

    public virtual void Dispose( float force)
    {
        rb.isKinematic = false;
        rb.AddForce(transform.forward * force, ForceMode.Impulse);
    }
}
