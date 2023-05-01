using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePreferenceManager : MonoBehaviour
{
    private const string MouseSensitivityKey = "MouseSensitivity";
    
    private static GamePreferenceManager _instance;
    public static GamePreferenceManager Instance => _instance;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
    }

    private void Start()
    {
        LoadPrefs();
    }

    private void OnApplicationQuit()
    {
        SavePrefs();
    }

    public void SavePrefs()
    {
        PlayerPrefs.SetFloat(MouseSensitivityKey, GameSettings.Instance.mouseSensitivitySlider.value);
        PlayerPrefs.Save();
    }

    public void LoadPrefs()
    {
        if (PlayerPrefs.HasKey(MouseSensitivityKey))
        {
            float mouseSensOption = PlayerPrefs.GetFloat(MouseSensitivityKey);
            GameSettings.Instance.mouseSensitivitySlider.value = mouseSensOption;
        }
    }
}
