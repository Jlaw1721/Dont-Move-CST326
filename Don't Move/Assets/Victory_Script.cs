using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory_Script : MonoBehaviour
{
    // Start is called before the first frame update

    public GameOver vscript;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter()
    {
        vscript.Victory();
    }
}
