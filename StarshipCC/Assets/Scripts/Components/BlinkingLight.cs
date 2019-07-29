using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingLight : MonoBehaviour
{
    public Light light;
    public SpriteRenderer renderer;

    public float cycleDuration = 1f;

    [ReadOnly]
    public float maxIntensity;

    public float minIntensity = 0f;
    protected float targetIntensity;

    public float maxAlpha = 1;
    public float minAlpha = 0;
    protected float targetAlpha;

    // Start is called before the first frame update
    void Start()
    {
        cycleDuration *= 50; // Convert cycleDuration from seconds to updates

        if(light)
        {
            maxIntensity = light.intensity;
        }
        else
        {
            Debug.LogError("Blinking light does not have light object assigned");
        }
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float delta = (maxIntensity - minIntensity) / (cycleDuration / 2);

        //float newIntensity = Mathf.MoveTowards(l.intensity, targetIntensities[i], delta * Time.deltaTime);

        float newIntensity = light.intensity + (delta * Mathf.Sign(targetIntensity - light.intensity));

        if (newIntensity >= maxIntensity)
        {
            // Start decreasing intensity
            newIntensity = maxIntensity;
            targetIntensity = minIntensity;
        }
        else if (newIntensity <= minIntensity)
        {
            // Start increasing intensity
            newIntensity = minIntensity;
            targetIntensity = maxIntensity;
        }

        light.intensity = newIntensity;
    }
}
