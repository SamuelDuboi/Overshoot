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
    }

    public void OnClickQuit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MenuScene");
    }

    public void OnClickPlay()
    {
        SceneManager.LoadScene("SimonEnviroScenees 1");
    }

    public void OnClickQuitGame()
    {
        Application.Quit();
        Debug.Log("Quit game");
    }

}
