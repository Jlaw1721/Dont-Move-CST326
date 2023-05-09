using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GrabScript : MonoBehaviour
{
   [Header("Grab Setup Variables")]
   public Transform grabPoint;
   private GameObject inGripObj;
   private Hashtable throwSettings;

   [Header("Parameters")] 
   public float grabRange = 5.0f;
   public float grabSpeed = 100f;
   public float throwPower = 50f;

   [Header("UI")] 
   public GameObject grabTutorial;
   public Image centerDot;
   public GameObject inGripTutorials;
   public void Start()
   {
      throwSettings = new Hashtable();
      throwSettings.Add(1f, 15f);
      throwSettings.Add(1.5f, 30f);
      throwSettings.Add(0.05f, 1f);
      throwSettings.Add(0.15f, 2f);
   }

   public void Update()
   {
      CheckObject();
      
      RaycastHit check;
      if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out check, grabRange))
      {
         if (check.transform.gameObject.tag == "Grab Object" && inGripObj == null)
         {
            centerDot.color = Color.red;
            grabTutorial.gameObject.SetActive(true);
         }
      }
      else if (!Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out check, grabRange) || check.transform.gameObject.tag != "Grab Object")
      {
         centerDot.color = Color.white;
         grabTutorial.gameObject.SetActive(false);
      }
      
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

   private void OnCollisionEnter(Collision collision)
   {
      if (collision.gameObject.CompareTag("Grab Object"))
      {
         if (inGripObj != null)
         {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
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
            string key = thrownObject.gameObject.tag;
            Drop(inGripObj);
            throwPower = (float)Convert.ToDouble(throwSettings[thrownObject.GetComponent<Rigidbody>().mass]);
            thrownObject.GetComponent<Rigidbody>().AddForce(transform.forward * throwPower, ForceMode.Impulse);
            thrownObject = null;
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
         while (inGripObj.transform.position != grabPoint.position)
         {
            inGripObj.transform.position = Vector3.MoveTowards(inGripObj.transform.position, grabPoint.position, grabSpeed * Time.deltaTime);
         }
      }
   }

   public void GrabRotation()
   {
      Rigidbody rb = inGripObj.GetComponent<Rigidbody>();
      
      if (Input.GetKey(KeyCode.Q))
      {
         rb.constraints = RigidbodyConstraints.None;
         inGripObj.transform.Rotate(grabSpeed * Time.deltaTime * Vector3.up, Space.World);
      } else if (Input.GetKey(KeyCode.E))
      {
         rb.constraints = RigidbodyConstraints.None;
         inGripObj.transform.Rotate(grabSpeed * Time.deltaTime * Vector3.down, Space.World);
      } else if (Input.GetKey(KeyCode.Alpha1))
      {
         rb.constraints = RigidbodyConstraints.None;
         inGripObj.transform.Rotate(grabSpeed * Time.deltaTime * new Vector3(0,0,-1), Space.World);
      } else if (Input.GetKey(KeyCode.Alpha3))
      {
         rb.constraints = RigidbodyConstraints.None;
         inGripObj.transform.Rotate(grabSpeed * Time.deltaTime * new Vector3(0,0,1), Space.World);
      }
      rb.constraints = RigidbodyConstraints.FreezeRotation;
   }
   
   public void GrabObject(GameObject grabbed)
   {
      inGripTutorials.gameObject.SetActive(true);
      grabTutorial.gameObject.SetActive(false);
      Rigidbody rb = grabbed.GetComponent<Rigidbody>();
      rb.constraints = RigidbodyConstraints.FreezeRotation;
      rb.useGravity = false;
      rb.drag = 10;
      rb.transform.parent = grabPoint;
      inGripObj = grabbed;
   }
   
   public void Drop(GameObject grabbed)
   {
      inGripTutorials.gameObject.SetActive(false);
      Rigidbody rb = grabbed.GetComponent<Rigidbody>();
      inGripObj = null;
      rb.transform.parent = null;
      rb.useGravity = true;
      rb.constraints = RigidbodyConstraints.None;
      rb.drag = 1;
   }
      
}
