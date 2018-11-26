using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {
	CapitalController controller;

	// Use this for initialization
	void Start () {
		controller = GameObject.Find("CapitalShip").GetComponent<CapitalController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other)
    {
        string otherTag = other.gameObject.tag;
        if(otherTag == Tags.PLAYER)
        { 
			controller.AddCoins(1);
			Destroy(gameObject);
        }
    }
}
