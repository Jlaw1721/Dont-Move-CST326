using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{

    public string levelToLoad = "Level01";
    public GameObject settingsUI;
    public SceneFader sceneFader;

    private void Update()
    {
        if (settingsUI.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            SettingsToggle();
        }
    }

    public void Play()
    {
        sceneFader.FadeTo(levelToLoad);
    }

    public void SettingsToggle()
    {
        settingsUI.SetActive(!settingsUI.activeSelf);
        GamePreferenceManager.Instance.SavePrefs();
    }

    public void QuitToDeskTop()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
    
}
