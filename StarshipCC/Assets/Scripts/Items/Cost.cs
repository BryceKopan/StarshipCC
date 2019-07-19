using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Item))]
public class Cost : MonoBehaviour {
	public int minCost, maxCost;
	private Item item;
	private int cost = -1;
	private GameController controller;
	private GameObject ui;

	void Start()
	{
		item = gameObject.GetComponent<Item>();
		item.isInteractable = false;

		controller = GameObject.Find("GameController").GetComponent<GameController>();
	}

	void Update()
	{
	}

	void OnTriggerEnter2D(Collider2D other)
	{
        if(other.gameObject.tag == Tags.PLAYER)
        { 
			if(controller.GetCoins() >= cost)
			{
				item.isInteractable = true;
				controller.AddCoins(-cost);
				Destroy(ui);
			}
			else
			{
				Debug.Log("Not Enough Coins");
			}
        }
	}

    public int GetCost()
    {
        // Cost is lazily instantiated because otherwise it causes weird errors
        if(cost == -1)
        {
            cost = Random.Range(minCost, maxCost);
        }

        return cost;
    }
}
