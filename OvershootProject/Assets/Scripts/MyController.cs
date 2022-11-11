using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class MyController : MonoBehaviour
{
    public float speed = 15;
    public float dashForce = 1;
    public float dashForceMax =5;
    private bool canDash = true;
    public AnimationCurve dashCurve;
    public float dashCooldown = 2;
    private bool dashing;
    private float Timer;
    public BoxCollider CollectColliBox;
    public Rigidbody myRigidbody;

    private Vector3 directionAngle;
    
    
    private Objects carriedObject;
    private Weapon carriedWeapon;

    public float throwForce= 10;
    // Start is called before the first frame update
    void Start()
    {
        Timer = dashCurve.length;
    }

    // Update is called once per frame
    void Update()
    {
        myRigidbody.velocity = directionAngle*dashForce;
        if (Timer < dashCurve.length)
        {
            Timer += Time.deltaTime;
            dashForce = 1+dashCurve.Evaluate(Timer)*dashForceMax;
            return;
        }
        if(dashing)
        {
            dashing = false;
            gameObject.layer = 7;
        }

    }
    public void Throw(InputAction.CallbackContext context)
    {
        if (context.performed && carriedObject)
        {
            carriedObject.Dispose();
            carriedObject.transform.parent = null;
            carriedObject = null;
        }
    }

    public void Dash(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (canDash)
            {
                canDash = false;
                Timer = 0;
                gameObject.layer = 10;
                dashing = true;
                StartCoroutine(DashCooldown());
            }

        }
    }

    IEnumerator DashCooldown()
    {
       yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
    public void Fire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Pew Pew");
        }
    }

    public void Aim(InputAction.CallbackContext context)
    {
        if (context.performed)
        {

            transform.LookAt(transform.position + new Vector3(context.ReadValue<Vector2>().x,0,context.ReadValue<Vector2>().y));
        }

    }

    public  void Move(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            directionAngle = speed *new Vector3( context.ReadValue<Vector2>().x,0,context.ReadValue<Vector2>().y);
        }

    }

    public void Interract(InputAction.CallbackContext context)
    {
        if(carriedObject || carriedWeapon)
            return;
        if (context.started)
        {
            CollectColliBox.enabled = true;
        }

        if (context.canceled)
        {
            CollectColliBox.enabled = false;
            
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        collision.transform.SetParent(transform.GetChild(0));
        collision.transform.localPosition = Vector3.zero;
        collision.transform.localRotation = Quaternion.identity;
        carriedObject= collision.gameObject.GetComponent<Objects>();
        if (!carriedObject)
            carriedWeapon = collision.gameObject.GetComponent<Weapon>();

    }
}
