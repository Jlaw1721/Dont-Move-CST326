using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{

    public string levelToLoad = "Level01";
    public GameObject settingsUI;
    public GameObject creditsUI;
    public SceneFader sceneFader;

    private void Update()
    {
        if (settingsUI.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            SettingsToggle();
        }
        if (creditsUI.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            CreditsToggle();
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
    
    public void CreditsToggle()
    {
        creditsUI.SetActive(!creditsUI.activeSelf);
        GamePreferenceManager.Instance.SavePrefs();
    }

    public void QuitToDeskTop()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
    
}
