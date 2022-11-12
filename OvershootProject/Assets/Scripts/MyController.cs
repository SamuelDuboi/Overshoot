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
    private Objects carriedObject;


    public float throwForce = 100;
    public bool grab;
    public bool isGrab;
    public GameObject aroundTarget;
    private float reloadTime;
    public GameObject ui;
    public Image filImage;

    public Workshop workshop;
    public float startMagnitudeDamage = 3;
    private float knockBack= 1;

    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        Timer = dashCurve.keys[1].time;
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        myRigidbody.velocity = directionAngle * dashForce*knockBack;
        if (myRigidbody.velocity.magnitude < minVelocityMangitude)
            myRigidbody.velocity = Vector3.zero;


        if (lookingAt.magnitude > 0.1)
            transform.LookAt(transform.position+ lookingAt);
        else
            transform.LookAt(transform.position + directionAngle);


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

        /*if (myRigidbody.velocity == Vector3.zero)
        {
            animator.SetTrigger("isStopped");
        }*/
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 7 && dashing)
        {
            Timer = dashCurve.keys[dashCurve.keys.Length - 1].time;
        }
        else if (collision.gameObject.layer == 6)
        {
            float velocityMagnitude = collision.gameObject.GetComponent<Rigidbody>().velocity.magnitude;
            if (velocityMagnitude > startMagnitudeDamage)
            {
                if (CompareTag("Team1Player"))
                    GameManager.instance.GaugeTeam1.FillGauge(velocityMagnitude * collision.gameObject.GetComponent<Objects>().weight);
                else
                {
                    GameManager.instance.GaugeTeam2.FillGauge(velocityMagnitude * collision.gameObject.GetComponent<Objects>().weight);
                }
            }
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
            StartCoroutine(KnockBock());
            carriedObject.transform.parent = null;
            carriedObject = null;
        }

    }

    IEnumerator KnockBock()
    {
        knockBack = -1;
        yield return new WaitForSeconds(0.1f);
        knockBack = 1;
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
                animator.SetTrigger("isDashing");
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
        animator.SetTrigger("isWalking");
        if (dashing || knockBack<0)
            return;
        if (!carriedObject)
        {
            directionAngle = speed * new Vector3(context.ReadValue<Vector2>().x, 0, context.ReadValue<Vector2>().y).normalized;
            return;
        }
        directionAngle = slowVelocity * new Vector3(context.ReadValue<Vector2>().x, 0, context.ReadValue<Vector2>().y).normalized;
    }

    public void Grab(InputAction.CallbackContext context)
    {
       
        if (context.started)
        {

            if (isGrab&& carriedObject)
            {
                Throw();
                isGrab = false;
                return;
            }
                grab = true;

        }
        if (context.canceled)
        {
            grab = false;

        }


    }
    
    public void SetCarriedObject(GameObject obj)
    {
        obj.transform.SetParent(transform);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.identity;
        isGrab = true;
        carriedObject = obj.GetComponent<Objects>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Team1Workshop") || other.CompareTag("Team2Workshop")|| carriedObject) return;
        if (!registerInteractable)
            registerInteractable = other.gameObject.GetComponent<Objects>();
    }
    public void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("Team1Workshop") || other.CompareTag("Team2Workshop")|| carriedObject) return;
        if(!registerInteractable)
        registerInteractable = other.gameObject.GetComponent<Objects>();

        if (registerInteractable)
        {
            aroundTarget.SetActive(true);
            aroundTarget.transform.position = registerInteractable.transform.position;
            if (!grab)
            {
                return;
            }
            registerInteractable.transform.SetParent(transform.GetChild(0));
            registerInteractable.transform.localPosition = Vector3.zero;
            registerInteractable.transform.localRotation = Quaternion.identity;
            carriedObject = registerInteractable.gameObject.GetComponent<Objects>();
            if (carriedObject)
            {
                carriedObject.Grab();
                isGrab = true;
                aroundTarget.SetActive(false);
            }
        }
        else
            aroundTarget.SetActive(false);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Team1Workshop") || other.CompareTag("Team2Workshop")) return;
        if (registerInteractable)
        {
            aroundTarget.SetActive(false);
            registerInteractable = null;
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
