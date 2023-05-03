using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class PlayAnimation : MonoBehaviour
{
    public Animator targetAnimator;

    public void ToggleBoolState(string var)
    {
       targetAnimator.SetBool(var, !targetAnimator.GetBool(var));
    }

    public void CycleSpeed()
    {
        float currentSpeed = targetAnimator.GetFloat("speed");
        if (currentSpeed < 10)
        {
            targetAnimator.SetFloat("speed", currentSpeed += 1);
        }
        else
        {
            targetAnimator.SetFloat("speed", 0);
        }
    }
}
