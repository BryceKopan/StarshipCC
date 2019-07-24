using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutDelete : MonoBehaviour
{
    public float fadeDuration;

    public bool fadingOut = false;

    private Color originalColor;
    public Color fadeColor;

    private Renderer sprite;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Delete()
    {
        sprite = GetComponent<Renderer>();

        if (sprite)
        {
            fadingOut = true;

            if (sprite is SpriteRenderer)
            {
                originalColor = ((SpriteRenderer)sprite).color;
            }
            else if (sprite is MeshRenderer)
            {
                originalColor = ((MeshRenderer)sprite).material.color;
            }

            StartCoroutine(FadeSprite(fadeDuration));
        }

        Destroy(gameObject, fadeDuration);
    }

    void Delete(Color color)
    {
        fadeColor = color;
        Delete();
    }

    IEnumerator FadeSprite(float duration)
    {
        if(fadeColor == null)
        {
            fadeColor = Color.clear;
        }

        for(float t = 0f; t < duration; t += Time.deltaTime)
        {
            float normalizedTime = t / duration;

            if (sprite is SpriteRenderer)
            {
                ((SpriteRenderer)sprite).color = Color.Lerp(originalColor, fadeColor, normalizedTime);
            }
            else if (sprite is MeshRenderer)
            {
                ((MeshRenderer)sprite).material.color = Color.Lerp(originalColor, fadeColor, normalizedTime); ;
            }

            yield return null;
        }

        if (sprite is SpriteRenderer)
        {
            ((SpriteRenderer)sprite).color = fadeColor;
        }
        else if (sprite is MeshRenderer)
        {
            ((MeshRenderer)sprite).material.color = fadeColor;
        }
    }
}
