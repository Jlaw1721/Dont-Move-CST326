using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeMonsterAnimations : MonoBehaviour
{
    public Animator monsterAnimator;

    public bool frozen;
    // Start is called before the first frame update
    void Start()
    {
        monsterAnimator = GetComponent<Animator>();
    }

    public void ToggleFreeze()
    {
        frozen = !frozen;
        if (!frozen)
        {
            monsterAnimator.speed = 1f;
        }
        else
        {
            monsterAnimator.speed = 0f;
        }
    }
}
