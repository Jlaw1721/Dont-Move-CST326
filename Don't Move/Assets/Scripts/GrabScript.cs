using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrabScript : MonoBehaviour
{
   [Header("Grab Setup Variables")]
   public Transform grabPoint;
   private GameObject inGripObj;

   [Header("Parameters")] 
   public float grabRange = 5.0f;
   public float grabSpeed = 100f;
   public float throwPower = 50f;
   public void Update()
   {
      CheckObject();
      
      if (Input.GetKeyDown(KeyCode.G))
      {
         if (inGripObj == null)
         {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, grabRange))
            {
               if (hit.transform.gameObject.tag == "Grab Object")
               {
                  GrabObject(hit.rigidbody.gameObject);
               }
            }
         }
         else
         {
            Drop(inGripObj);
         }
      }
   }

   public void CheckObject()
   {
      if (inGripObj != null)
      {
         if (Input.GetKeyDown(KeyCode.R))
         {
            GameObject thrownObject = inGripObj;
            Drop(inGripObj);
            thrownObject.GetComponent<Rigidbody>().AddForce(transform.forward * throwPower, ForceMode.Impulse);
         }
         else
         {
            ChangeGrabbedPosition();
            GrabRotation();
         }
      }
   }

   public void ChangeGrabbedPosition()
   {
      if (Vector3.Distance(inGripObj.transform.position, grabPoint.position) > 0.1f)
      {
         Vector3 dir = grabPoint.position - inGripObj.transform.position;
         inGripObj.GetComponent<Rigidbody>().AddForce(grabSpeed * dir);
      }
   }

   public void GrabRotation()
   {
      Rigidbody rb = inGripObj.GetComponent<Rigidbody>();
      
      if (Input.GetKey(KeyCode.Q))
      {
         rb.constraints = RigidbodyConstraints.None;
         inGripObj.transform.Rotate(grabSpeed * Time.deltaTime * Vector3.left);
      } else if (Input.GetKey(KeyCode.E))
      {
         rb.constraints = RigidbodyConstraints.None;
         inGripObj.transform.Rotate(grabSpeed * Time.deltaTime * Vector3.right);
      } else if (Input.GetKey(KeyCode.Alpha1))
      {
         rb.constraints = RigidbodyConstraints.None;
         inGripObj.transform.Rotate(grabSpeed * Time.deltaTime * Vector3.up);
      } else if (Input.GetKey(KeyCode.Alpha3))
      {
         rb.constraints = RigidbodyConstraints.None;
         inGripObj.transform.Rotate(grabSpeed * Time.deltaTime * Vector3.down);
      }
      rb.constraints = RigidbodyConstraints.FreezeRotation;
   }
   
   public void GrabObject(GameObject grabbed)
   {
      Rigidbody rb = grabbed.GetComponent<Rigidbody>();
      rb.constraints = RigidbodyConstraints.FreezeRotation;
      rb.useGravity = false;
      rb.drag = 10;
      rb.transform.parent = grabPoint;
      inGripObj = grabbed;
   }
   
   public void Drop(GameObject grabbed)
   {
      Rigidbody rb = grabbed.GetComponent<Rigidbody>();
      inGripObj = null;
      rb.transform.parent = null;
      rb.useGravity = true;
      rb.constraints = RigidbodyConstraints.None;
      rb.drag = 1;
   }
      
}
