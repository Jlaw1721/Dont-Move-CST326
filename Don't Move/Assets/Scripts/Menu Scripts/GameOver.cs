using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{
    public SceneFader sceneFader;
    public GameObject gameOverUI;
    public string menuSceneName = "MainMenu";
    private static GameOver _instance;
    public static GameOver Instance => _instance;
    public AudioSource gameOverSound;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    public void EndGame()
    {
        gameOverUI.SetActive(true);
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        Time.timeScale = 0f;
        gameOverSound.Play();
    }

    public void RetryLevel()
    {
        gameOverUI.SetActive(false);
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
       
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        
        Time.timeScale = 1f;
    }
    
    public void Menu()
    {
        gameOverUI.SetActive(false);
        sceneFader.FadeTo(menuSceneName);
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        Time.timeScale = 1f;
 
    }
}
