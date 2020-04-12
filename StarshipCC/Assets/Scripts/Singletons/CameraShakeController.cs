using UnityEngine;
using System.Collections;

public class CameraShakeController : MonoBehaviour
{
    private Vector3 originalPos;

    public float shakeFrequency = 20;

    public float decreaseOverTimeMultiplier = 1f;

    private int seed = 1;

    void Update()
    {

    }

    public void Shake(float amount, float duration)
    {
        originalPos = gameObject.transform.localPosition;
        StopAllCoroutines();
        StartCoroutine(cShake(amount, duration));
    }

    public IEnumerator cShake(float amount, float duration)
    {
        while (duration > 0)
        {
            // Using perlin noise because it is 'smoother' than pure random
            transform.localPosition = originalPos + new Vector3(Mathf.PerlinNoise(seed, Time.time * shakeFrequency) * 2 - 1,
                                                                Mathf.PerlinNoise(seed, Time.time * shakeFrequency) * 2 - 1,
                                                                0) * amount;

            duration -= Time.deltaTime;

            amount -= Time.deltaTime * decreaseOverTimeMultiplier;

            yield return null;
        }

        transform.localPosition = originalPos;
    }
}