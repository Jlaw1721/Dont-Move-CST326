using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterActiveSoundControl : MonoBehaviour
{
    public AudioSource monsterActive;
    public AudioSource monsterContinuous;
    
    public void PlaySound(AudioClip sound)
    {
        monsterActive.PlayOneShot(sound);
    }
}
