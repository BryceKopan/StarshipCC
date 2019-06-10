using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LoopingBackground : MonoBehaviour
{
    public Vector2 speed = new Vector2(10, 10), direction = new Vector2(-1, 0);

    public List<Sprite> possibleSprites;
    public List<Color> possibleColors;

    private List<SpriteRenderer> backgroundPart;
    private bool sceneIsRendering = false;

    private Vector3 cameraDirection;
    private Vector3 oldCameraPosition;

    void Start()
    {
        backgroundPart = new List<SpriteRenderer>();

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            SpriteRenderer r = child.GetComponent<SpriteRenderer>();

            if (r != null)
            {
                backgroundPart.Add(r);

                int spriteNum, colorNum;
                spriteNum = Random.Range(0, possibleSprites.Count);
                colorNum = Random.Range(0, possibleColors.Count);

                r.sprite = possibleSprites[spriteNum];
                r.color = possibleColors[colorNum];
            }
        }

        backgroundPart = backgroundPart.OrderBy(
                t => t.transform.position.x
                ).ToList();

        oldCameraPosition = Camera.main.transform.position;
    }

    void Update()
    {
        Vector3 movement = new Vector3(speed.x * direction.x, speed.y * direction.y, 0);
        movement *= Time.deltaTime;

        foreach(SpriteRenderer sprite in backgroundPart)
        {
            sprite.transform.Translate(movement);
        }

        SpriteRenderer firstChild = backgroundPart.FirstOrDefault();
        if(firstChild.isVisible)
        {
            sceneIsRendering = true;
        }

        cameraDirection = Camera.main.transform.position - oldCameraPosition;
        oldCameraPosition = Camera.main.transform.position;

        if(sceneIsRendering)
            MoveSprite();
    }

    void MoveSprite()
    {
        SpriteRenderer firstChild = backgroundPart.FirstOrDefault();
        SpriteRenderer lastChild = backgroundPart.LastOrDefault();

        if (cameraDirection.x > 0 && firstChild != null && firstChild.transform.position.x < Camera.main.transform.position.x && firstChild.isVisible == false)
        {
            Vector3 lastPosition = lastChild.transform.position;
            Vector3 lastSize = (lastChild.bounds.max - lastChild.bounds.min);

            firstChild.transform.position = new Vector3(lastPosition.x + lastSize.x, firstChild.transform.position.y, firstChild.transform.position.z);

            int spriteNum, colorNum;
            spriteNum = Random.Range(0, possibleSprites.Count);
            colorNum = Random.Range(0, possibleColors.Count);

            firstChild.sprite = possibleSprites[spriteNum];
            firstChild.color = possibleColors[colorNum];

            backgroundPart.Remove(firstChild);
            backgroundPart.Add(firstChild);
        }
        else if (cameraDirection.x < 0 && lastChild != null && lastChild.transform.position.x > Camera.main.transform.position.x && lastChild.isVisible == false)
        {
            Vector3 firstPosition = firstChild.transform.position;
            Vector3 firstSize = (firstChild.bounds.max - firstChild.bounds.min);

            lastChild.transform.position = new Vector3(firstPosition.x - firstSize.x, lastChild.transform.position.y, lastChild.transform.position.z);

            int spriteNum, colorNum;
            spriteNum = Random.Range(0, possibleSprites.Count);
            colorNum = Random.Range(0, possibleColors.Count);

            lastChild.sprite = possibleSprites[spriteNum];
            lastChild.color = possibleColors[colorNum];

            backgroundPart.Remove(lastChild);
            backgroundPart.Insert(0, lastChild);
        }
    }
}
