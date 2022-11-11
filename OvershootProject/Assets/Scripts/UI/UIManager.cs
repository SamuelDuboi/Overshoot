using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{

    public void OnClickResume()
    {
        Time.timeScale = 1;
        this.gameObject.SetActive(false);
        Debug.Log("resume");
    }

    public void OnClickQuit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("SampleScene");
        Debug.Log("quit");
    }

}
