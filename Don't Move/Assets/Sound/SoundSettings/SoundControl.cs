using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

public class SoundControl : MonoBehaviour
{
    [SerializeField] AudioMixer _mixer;
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;

    public const string MIXER_MASTER = "MasterVolume";
    public const string MIXER_MUSIC = "MusicVolume";
    public const string MIXER_SFX = "SFXVolume";
    void Awake()
    {
        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSfxVolume);
    }

    private void Start()
    {
        masterSlider.value = PlayerPrefs.GetFloat(MIXER_MASTER, 1f);
        musicSlider.value = PlayerPrefs.GetFloat(MIXER_MUSIC, 1f);
        sfxSlider.value = PlayerPrefs.GetFloat(MIXER_SFX, 1f);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(AudioManager.MASTER_KEY, masterSlider.value);
        PlayerPrefs.SetFloat(AudioManager.MUSIC_KEY, musicSlider.value);
        PlayerPrefs.SetFloat(AudioManager.SFX_KEY, sfxSlider.value);
    }

    void SetMusicVolume(float value)
    {
        _mixer.SetFloat(MIXER_MUSIC, Mathf.Log10(value) * 20f);
    }
    void SetSfxVolume(float value)
    {
        _mixer.SetFloat(MIXER_SFX, Mathf.Log10(value) * 20f);
    }
    void SetMasterVolume(float value)
    {
        _mixer.SetFloat(MIXER_MASTER, Mathf.Log10(value) * 20f);
    }
}
