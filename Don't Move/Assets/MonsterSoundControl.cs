using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSoundControl : MonoBehaviour
{
    public AudioSource monster;

    private void Start()
    {
        monster = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip sound)
    {
        monster.PlayOneShot(sound);
    }
}
