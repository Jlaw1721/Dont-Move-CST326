using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{
    public SceneFader sceneFader;
    public GameObject gameOverUI;
    public string menuSceneName = "MainMenu";
    private static GameOver _instance;
    public static GameOver Instance => _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    public void EndGame()
    {
        ToggleUI();
    }

    public void RetryLevel()
    {
        Time.timeScale = 1f;
        //ToggleUI();
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }
    
    public void Menu()
    {
        sceneFader.FadeTo(menuSceneName);
        Time.timeScale = 1f;
        // ToggleUI();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void ToggleUI()
    {
        gameOverUI.SetActive(!gameOverUI.activeSelf);
        
        if (gameOverUI.activeSelf)
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
}
