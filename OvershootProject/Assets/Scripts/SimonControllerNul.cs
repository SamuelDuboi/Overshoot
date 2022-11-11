using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonControllerNul : MonoBehaviour
{
    public Rigidbody rb;

    private Vector3 lastDirection;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movementVector = Vector3.zero;
        if (Input.GetKey(KeyCode.Z))
        {
            movementVector.z = -1.0f;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            movementVector.x = 1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movementVector.z = 1f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movementVector.x = -1f;
        }

        rb.velocity = movementVector * Time.fixedDeltaTime * 300;
        if (movementVector != Vector3.zero && movementVector != lastDirection)
            lastDirection = movementVector;

        Debug.DrawRay(transform.position, lastDirection * 100f, Color.red);

        if (Input.GetKeyDown(KeyCode.F))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, lastDirection * 10f, out hit))
            {
                if (hit.collider.GetComponent<Objects>())
                {
                    hit.collider.GetComponent<Objects>().Grab();
                    hit.collider.transform.SetParent(transform);
                }
            }
        }
    }
}
