using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabCollisionDeactivator : MonoBehaviour
{
    public GameObject cameraObject;
    private GameObject grabbedObject;

    public void FixedUpdate()
    {
        grabbedObject = cameraObject.GetComponent<GrabScript>().inGripObj;
        if (grabbedObject != null)
        {
            Physics.IgnoreCollision(grabbedObject.GetComponent<Collider>(), GetComponent<Collider>());
        }
    }
}
