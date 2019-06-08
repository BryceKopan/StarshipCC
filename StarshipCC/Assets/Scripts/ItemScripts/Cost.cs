using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Item))]
public class Cost : MonoBehaviour {
	public int minCost, maxCost;
	public GameObject coinDisplayPrefab;
	public Vector3 coinDisplayeOffset;
	private Item item;
	private int cost;
	private GameController controller;
	private GameObject ui;

	void Start()
	{
		item = gameObject.GetComponent<Item>();
		item.isInteractable = false;

		controller = GameObject.Find("GameController").GetComponent<GameController>();

		cost = Random.Range(minCost, maxCost);
		CreateUIElement();
	}

	void Update()
	{
		if(ui)
		{
			Vector3 uiPosition = Camera.main.WorldToScreenPoint(transform.position + coinDisplayeOffset);
			ui.transform.position = uiPosition;
		}
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

	public void CreateUIElement()
	{
		Vector3 uiPosition = Camera.main.WorldToScreenPoint(transform.position + coinDisplayeOffset);

		ui = Instantiate(
			coinDisplayPrefab,
			uiPosition,
			transform.rotation);

		ui.transform.SetParent(GameObject.Find("Canvas").transform);
		ui.GetComponent<UnityEngine.UI.Text>().text = ": " + cost;
	}
}
