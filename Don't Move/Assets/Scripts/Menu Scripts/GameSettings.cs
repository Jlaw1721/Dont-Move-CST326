using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameSettings : MonoBehaviour
{
    public Slider mouseSensitivitySlider;
    public float mouseSens = 0f;
    private static GameSettings _instance;
    public static GameSettings Instance => _instance;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
    }

    private void Start()
    {
        GamePreferenceManager.Instance.LoadPrefs();
        MouseSensitivity();
    }

    //public static int MouseSens;
    public void MouseSensitivity()
    {
        mouseSens = mouseSensitivitySlider.value * 50f;
        GamePreferenceManager.Instance.SavePrefs();
    }
}
