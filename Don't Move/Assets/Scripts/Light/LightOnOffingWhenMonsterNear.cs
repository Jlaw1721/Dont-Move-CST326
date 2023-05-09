using UnityEngine;

public class LightOnOffingWhenMonsterNear : MonoBehaviour
{
    public float minOnTime = 0.2f;
    public float maxOnTime = 2f;
    public float minOffTime = 0.1f;
    public float maxOffTime = 0.5f;

    public float distance = 10f;

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
        if ((MonsterMovement.instance.position - transform.position).magnitude > distance)
        {
            newlight.enabled = true;
            return;
        }
        
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