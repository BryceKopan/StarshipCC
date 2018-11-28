using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCostToDestroy : MonoBehaviour {
	public int minCost, maxCost;
	public GameObject coinDisplayPrefab;

	void Start()
	{
		Vector3 uiPosition = transform.Find("UIPosition").position;
		Camera.main.WorldToScreenPoint(uiPosition);

		GameObject ui = Instantiate(
			coinDisplayPrefab,
			uiPosition,
			transform.rotation);

		ui.transform.parent = GameObject.Find("Canvas").transform;
	}

	void OnTriggerEnter2D(Collider2D other)
	{

	}
}
