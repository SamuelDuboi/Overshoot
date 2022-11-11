using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class HandlingUI : MonoBehaviour
{

    public GameObject pauseMenu;
    public EventSystem eventSystem;
    public GameObject resume;

    public void StartPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            eventSystem.SetSelectedGameObject(resume);
        }
    }
}
