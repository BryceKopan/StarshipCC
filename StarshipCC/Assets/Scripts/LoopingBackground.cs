﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LoopingBackground : MonoBehaviour
{
    public Vector2 speed = new Vector2(10, 10), direction = new Vector2(-1, 0);

    public List<Sprite> possibleSprites;
    public List<Color> possibleColors;

    private List<SpriteRenderer> backgroundPart;

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
            }
        }

        backgroundPart = backgroundPart.OrderBy(
                t => t.transform.position.x
                ).ToList();
    }

    void Update()
    {
        Vector3 movement = new Vector3(
                speed.x * direction.x,
                speed.y * direction.y,
                0);

        movement *= Time.deltaTime;
        transform.Translate(movement);

        SpriteRenderer firstChild = backgroundPart.FirstOrDefault();

        if (firstChild != null)
        {
            if (firstChild.transform.position.x < Camera.main.transform.position.x)
            {
                if (firstChild.isVisible == false)
                {
                    SpriteRenderer lastChild = backgroundPart.LastOrDefault();

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
            }
        }
    }
}
