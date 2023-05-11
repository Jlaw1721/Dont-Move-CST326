using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    // The light to be flickered
    Light flickeringLight;
    
    [Header("Light parameters")]
    // Intensity range for flickering
    public float minIntensity = 3f;
    public float maxIntensity = 6f;

    // Flicker frequency
    public float flickerFrequency = 1.0f;
    //public float intsensity;
    
    [Header("Turn Off")]
    public bool turnsOff;

    public float distance;
    
    // Random seed for flickering
    private float randomSeed;
    
    private Transform player;

    void Start()
    {
        // Store the initial intensity of the light
        randomSeed = Random.Range(0f, 65535f);
        flickeringLight = GetComponent<Light>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // Calculate the flickering intensity based on a random seed and time
        float flickerIntensity = Mathf.PerlinNoise(Time.time * flickerFrequency, randomSeed);
        
        // Map the intensity to the range between min and max intensity
        flickerIntensity = Mathf.Lerp(minIntensity, maxIntensity, flickerIntensity);
        
        // Set the intensity of the light
        flickeringLight.intensity = flickerIntensity;
        //intsensity = flickerIntensity;

        float d = (transform.position - player.position).magnitude;
        if (d < distance && turnsOff)
        {
            flickeringLight.gameObject.SetActive(false);
        }
    }
}
