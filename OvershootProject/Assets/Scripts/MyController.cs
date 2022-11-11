using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Timeline.TimelinePlaybackControls;


public class MyController : MonoBehaviour
{
    public float speed = 15;
    public float dashForce = 1;
    public float dashForceMax = 5;
    private bool canDash = true;
    public AnimationCurve dashCurve;
    public float dashCooldown = 2;
    private bool dashing;
    private float Timer;
    public BoxCollider CollectColliBox;
    public Rigidbody myRigidbody;

    public float slowVelocity = 8;
    public float minVelocityMangitude = 0.1f;
    private Vector3 directionAngle;
    Vector3 lookingAt;

    private Objects carriedObject;

    public float throwForce = 100;
    // Start is called before the first frame update
    void Start()
    {
        Timer = dashCurve.length;
    }

    // Update is called once per frame
    void Update()
    {
        myRigidbody.velocity = directionAngle * dashForce;
        if (myRigidbody.velocity.magnitude < minVelocityMangitude)
            myRigidbody.velocity = Vector3.zero;


        if (lookingAt.magnitude > 0.1)
            transform.LookAt(transform.position+ lookingAt);
        else
            transform.LookAt(transform.position + directionAngle.normalized);


        if (Timer < dashCurve.length)
        {
            Timer += Time.deltaTime;
            dashForce = 1 + dashCurve.Evaluate(Timer) * dashForceMax;
            return;
        }
        if (dashing)
        {
            dashing = false;
            gameObject.layer = 7;
        }

    }
    public void Throw(InputAction.CallbackContext context)
    {
        if (context.performed && carriedObject)
        {
            carriedObject.Dispose(throwForce);
            carriedObject.transform.parent = null;
            carriedObject = null;
        }

    }
    public void Throw()
    {
        if (carriedObject)
        {
            carriedObject.Dispose(throwForce);
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
            if (carriedObject)
            {
                var Weapon = carriedObject as Weapon;
                if (Weapon)
                    Weapon.Fire();
            }
        }
    }

    public void Aim(InputAction.CallbackContext context)
    {
        lookingAt = new Vector3(context.ReadValue<Vector2>().x, 0, context.ReadValue<Vector2>().y);


    }

    public void Move(InputAction.CallbackContext context)
    {
        if (!carriedObject)
        {
            directionAngle = speed * new Vector3(context.ReadValue<Vector2>().x, 0, context.ReadValue<Vector2>().y);
            return;
        }
        directionAngle = slowVelocity * new Vector3(context.ReadValue<Vector2>().x, 0, context.ReadValue<Vector2>().y);


    }

    public void Interract(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            CollectColliBox.enabled = false;

        }
        if (carriedObject)
            return;
        if (context.started)
        {
            CollectColliBox.enabled = true;
        }


    }

    private void OnTriggerEnter(Collider collision)
    {
        if (carriedObject || collision.gameObject.layer == 0)
            return;
        collision.transform.SetParent(transform.GetChild(0));
        collision.transform.localPosition = Vector3.zero;
        collision.transform.localRotation = Quaternion.identity;
        carriedObject = collision.gameObject.GetComponent<Objects>();
        if (carriedObject)
        {
            carriedObject.Grab();
        }
    }
}
