using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossAnimation : MonoBehaviour
{
    public void PlayParticles()
    {
        ParticleSystem fx = GetComponent<ParticleSystem>();
        fx.Play();
    }
}
