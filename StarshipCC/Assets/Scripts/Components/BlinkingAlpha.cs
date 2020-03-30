using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingAlpha : MonoBehaviour
{
    public SpriteRenderer renderer;

    public float cycleDuration = 1f;

    public float maxAlpha = 1;
    public float minAlpha = 0;
    protected float targetAlpha;

    // Start is called before the first frame update
    void Start()
    {
        cycleDuration *= 50; // Convert cycleDuration from seconds to updates

        if (!renderer)
        {
            Debug.LogError("BlinkingAlpha does not have light object assigned");
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float delta = (maxAlpha - minAlpha) / (cycleDuration / 2);

        float newAlpha = renderer.color.a + (delta * Mathf.Sign(targetAlpha - renderer.color.a));

        if (newAlpha >= maxAlpha)
        {
            // Start decreasing alpha
            newAlpha = maxAlpha;
            targetAlpha = minAlpha;
        }
        else if (newAlpha <= minAlpha)
        {
            // Start increasing alpha
            newAlpha = minAlpha;
            targetAlpha = maxAlpha;
        }

        Color oldColor = renderer.color;

        renderer.color = new Color(oldColor.r, oldColor.g, oldColor.b, newAlpha);
    }
}
