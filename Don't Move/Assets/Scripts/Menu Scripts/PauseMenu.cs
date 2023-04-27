using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    //public GameObject player;
    public GameObject ui;
    public SceneFader sceneFader;
    public string menuSceneName = "MainMenu";
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePaused();
        }
    }

    public void TogglePaused()
    {
        
        ui.SetActive(!ui.activeSelf);

        if (ui.activeSelf)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1f;
        }
    }

    public void Retry()
    {
        TogglePaused();
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        TogglePaused();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        sceneFader.FadeTo(menuSceneName);
        
    }
}
