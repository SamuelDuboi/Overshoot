using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectSpawner : MonoBehaviour
{
    public List<GameObject> ObjectToSpawn;
    public float spawnEvery;
    public float speed;

    public Transform StartPoint;

    private void Start()
    {
        SpawnObject();
    }

    private void SpawnObject()
    {
        var obj = Instantiate(ObjectToSpawn[Random.Range(0, ObjectToSpawn.Count)], StartPoint.position, Quaternion.identity);
        obj.GetComponent<Rigidbody>().AddForce(transform.forward * speed,  ForceMode.VelocityChange);
        obj.GetComponent<Rigidbody>().useGravity = false;
        StartCoroutine(Timer());
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(spawnEvery);
        SpawnObject();
    }

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb is not null)
        {
            rb.velocity = transform.forward * speed;
            rb.useGravity = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb is not null)
        {
            rb.useGravity = true;
        }
    }
}
