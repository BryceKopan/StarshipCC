using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour {
	public int minCost, maxCost;
	public GameObject coinDisplayPrefab;
	public Vector3 coinDisplayOffset;

	ConstructedCapitalController capitalController;

	void Start () 
	{
		capitalController = GameObject.Find("CapitalShip").GetComponent<ConstructedCapitalController>();

		foreach(Transform child in transform)
		{
			if(child.name == "ItemSpawnPoint")
			{
				GameObject item = capitalController.SpawnItem(child.transform.position);

				Cost cost = item.AddComponent<Cost>();
				cost.minCost = minCost;
				cost.maxCost = maxCost;
				cost.coinDisplayPrefab = coinDisplayPrefab;
				cost.coinDisplayeOffset = coinDisplayOffset;
			}
		}
	}
}
