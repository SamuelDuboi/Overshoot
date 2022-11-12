using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : Bullet
{
    public float lifeDuration;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(lifeDuration); 
        Destroy(gameObject);
    }
}
