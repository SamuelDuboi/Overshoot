using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
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

    private Objects registerInteractable;
    private Objects carriedObejct;


    public float throwForce = 100;
    public bool grab;
    public bool isGrab;
    public GameObject aroundTarget;
    private float reloadTime;
    public GameObject ui;
    public Image filImage;

    public Workshop workshop;
    // Start is called before the first frame update
    void Start()
    {
        Timer = dashCurve.keys[1].time;
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


        if (Timer < dashCurve.keys[dashCurve.keys.Length - 1].time)
        {
            Timer += Time.deltaTime;
            dashForce = 1 + dashCurve.Evaluate(Timer) * dashForceMax;
            return;
        }
        if (dashing)
        {

            dashing = false;
            dashForce = 1;
            gameObject.layer = 7;
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer ==7 && dashing)
        {
            Timer = dashCurve.keys[dashCurve.keys.Length-1].time;
        }
    }
    public void Throw(InputAction.CallbackContext context)
    {
        if (context.performed && carriedObejct)
        {
            carriedObejct.Dispose(throwForce);
            carriedObejct.transform.parent = null;
            carriedObejct = null;
        }

    }
    public void Throw()
    {
        if (carriedObejct)
        {
            carriedObejct.Dispose(throwForce);
            carriedObejct.transform.parent = null;
            carriedObejct = null;
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
            if (carriedObejct)
            {
                var Weapon = carriedObejct as Weapon;
                if (Weapon)
                {
                    reloadTime = Weapon.Fire();
                    if (reloadTime > -1)
                    {
                        myRigidbody.AddForce(-transform.forward * 15, ForceMode.Impulse);
                        StartCoroutine(ReloadTimeFeedback(reloadTime));
                        ui.SetActive(true);
                    }
                }
                    

            }
        }
    }
    IEnumerator ReloadTimeFeedback(float maxReloadTime)
    {
        while (reloadTime > 0)
        {
            reloadTime -= 0.01f;
            filImage.fillAmount =1- reloadTime / maxReloadTime;
            yield return new WaitForSeconds(0.01f);
        }
        ui.SetActive(false);
    }

    public void Aim(InputAction.CallbackContext context)
    {
        lookingAt = new Vector3(context.ReadValue<Vector2>().x, 0, context.ReadValue<Vector2>().y);


    }

    public void Move(InputAction.CallbackContext context)
    {
        if (dashing)
            return;
        if (!registerInteractable)
        {
            directionAngle = speed * new Vector3(context.ReadValue<Vector2>().x, 0, context.ReadValue<Vector2>().y);
            return;
        }
        directionAngle = slowVelocity * new Vector3(context.ReadValue<Vector2>().x, 0, context.ReadValue<Vector2>().y);


    }

    public void Grab(InputAction.CallbackContext context)
    {
       
        if (context.started)
        {

            if (isGrab&&registerInteractable)
            {
                Throw();
                isGrab = false;
                return;
            }
            Debug.Log("grab");
                grab = true;

        }
        if (context.canceled)
        {
            grab = false;

        }


    }
    private void OnTriggerStay(Collider other)
    {

        registerInteractable = other.gameObject.GetComponent<Objects>();

        if (registerInteractable)
        {
            aroundTarget.SetActive(true);
            aroundTarget.transform.position = registerInteractable.transform.position;
            if (!grab)
            {
                return;
            }
            other.transform.SetParent(transform.GetChild(0));
            other.transform.localPosition = Vector3.zero;
            other.transform.localRotation = Quaternion.identity;
            carriedObejct = other.gameObject.GetComponent<Objects>();
            if (carriedObejct)
            {
                carriedObejct.Grab();
                isGrab = true;
                aroundTarget.SetActive(false);
            }
        }
        else
            aroundTarget.SetActive(false);
    }
    private void OnTriggerExit(Collider other)
    {
        if (registerInteractable)
        {
            aroundTarget.SetActive(false);
        }
    }
    public void Interract(InputAction.CallbackContext context)
    {
        var colliders =Physics.OverlapSphere(transform.position, 5);
        foreach (var collider in colliders)
        {
            if (collider.GetComponent<Workshop>())
            {

                return;
            }
        }
    }

}
