using UnityEngine;

public class Hanging : MonoBehaviour
{
    public float speed = 1f;
    public float angle = 30f;

    //private float timer = 0.0f;



    void Update()
    {
        //float yOffset = Mathf.Sin(Time.time * speed) * angle;
        float xOffset = Mathf.Cos(Time.time * speed) * angle;
        //float zOffset = Mathf.Sin(Time.time * speed) * angle;

        //Vector3 newPosition = transform.position + new Vector3(xOffset, yOffset, zOffset);
        Quaternion newRotation = Quaternion.Euler(xOffset, 0f, 0f);

        //transform.position = newPosition;
        transform.rotation = newRotation;
    }
}