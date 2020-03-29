﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {
	public int ScoreValue = 10;

    public AudioClip pickupSound;
    public float soundVolume = 1f;

	GameController controller;

	// Use this for initialization
	void Start () {
        GameObject gameControllerObj = GameObject.Find("GameController");
        if(gameControllerObj)
        {
            controller = gameControllerObj.GetComponent<GameController>();
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other)
    {
        string otherTag = other.gameObject.tag;
        if(otherTag == Tags.PLAYER)
        {
            if(pickupSound)
            {
                AudioSource.PlayClipAtPoint(pickupSound, Camera.main.transform.position, soundVolume);
            }

            if(controller)
            {
                controller.AddCoins(1);
                controller.AddScore(ScoreValue);
            }

			Destroy(gameObject);
        }
    }
}
