using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float currentTime = 0.0f;
    public int duration;

    public TMP_Text Text;
    
    // Start is called before the first frame update
    void Start()
    {
        currentTime = duration;
        int minutes = duration / 60;
        int seconds = duration % 60;
        Text.text = String.Format("{00:00}", minutes, seconds);
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        int minutes = (int)currentTime / 60;
        int seconds = (int)currentTime % 60;
        Text.text = String.Format("{0:00}:{1:00}", minutes, seconds);
        if (currentTime <= 0.0f)
        {
            Debug.Log("FIN");
        }
    }
}
