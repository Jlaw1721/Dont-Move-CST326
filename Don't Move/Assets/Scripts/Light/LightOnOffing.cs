using UnityEngine;
using UnityEngine.Rendering;

public class LightOnOffing : MonoBehaviour
{
    public float minOnTime = 0.2f;
    public float maxOnTime = 0.5f;
    public float minOffTime = 0.2f;
    public float maxOffTime = 0.5f;

    private Light newlight;
    private bool isLightOn;
    private float timeToToggleLight;

    void Start()
    {
        newlight = GetComponent<Light>();
        isLightOn = false;
        timeToToggleLight = 0f;
    }

    void Update()
    {
        if (Time.time >= timeToToggleLight)
        {
            isLightOn = !isLightOn;
            newlight.enabled = isLightOn;

            if (isLightOn)
            {
                timeToToggleLight = Time.time + Random.Range(minOnTime, maxOnTime);
            }
            else
            {
                timeToToggleLight = Time.time + Random.Range(minOffTime, maxOffTime);
            }
        }
    }
}