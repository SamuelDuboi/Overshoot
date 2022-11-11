using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Gauge GaugeTeam1;
    public Gauge GaugeTeam2;

    public static GameManager instance;
    
    // Start is called before the first frame update
    void Start()
    {
        if (instance is null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
