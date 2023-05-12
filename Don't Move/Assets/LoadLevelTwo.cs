using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelTwo : MonoBehaviour
{
    public SceneFader fader;
    public String nextScene = "Level2";
    public Boolean isLastLevel = false;
    public GameOver endScript;
    public GameObject manager;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit");
        if (other.gameObject.CompareTag("Player"))
        {
            LoadNextLevel(nextScene);
        }
    }

    public void LoadNextLevel(string level)
    {
        if (isLastLevel)
        {
           manager.GetComponent<GameOver>().Victory();
        }
        else
        {
            fader.FadeTo(level);
        }
    }
}
