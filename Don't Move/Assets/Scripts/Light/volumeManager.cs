using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GlobalVolumeManager : MonoBehaviour
{
    private Volume _globalVolume;

    private Transform monster;
    private Transform player;

    private Vignette v;
    private FilmGrain grain;

    private void Start()
    {
        // Find the global volume in the scene
        _globalVolume = GetComponent<Volume>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        monster = GameObject.FindGameObjectWithTag("Monster").transform;

        if (_globalVolume == null)
        {
            Debug.LogWarning("Global volume not found in the scene");
            return;
        }

        if (_globalVolume.profile.TryGet(out Vignette vignette))
        {
            vignette.intensity.value = 0.0f;
            v = vignette;
        }

        if (_globalVolume.profile.TryGet(out FilmGrain filmGrain))
        {
            filmGrain.intensity.value = 0f;
            filmGrain.response.value = 0.5f;
            grain = filmGrain;
        }
    }

    private void Update()
    {
        float distance = (monster.position - player.position).magnitude;
        v.intensity.value = Mathf.Clamp((20 - distance)/60, 0, 1/3f);
        grain.intensity.value = Mathf.Clamp((20 - distance)/40, 0, 0.5f);
    }
}