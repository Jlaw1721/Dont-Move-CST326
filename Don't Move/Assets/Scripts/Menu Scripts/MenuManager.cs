using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{

    public string levelToLoad = "Level01";
    public SceneFader sceneFader;
    
    public void Play()
    {
        sceneFader.FadeTo(levelToLoad);
    }

    public void Settings()
    {
        Debug.Log("Need to create function still");
    }

    public void QuitToDeskTop()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
    
}
