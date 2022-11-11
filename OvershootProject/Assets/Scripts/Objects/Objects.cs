using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objects : MonoBehaviour
{
    public Rigidbody rb;

    public void Grab()
    {
        rb.isKinematic = true;
    }

    public virtual void Dispose()
    {
        rb.isKinematic = false;
    }
}
