using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour {

	CapitalController capitalController;

	void Start () 
	{
		capitalController = GameObject.Find("CapitalShip").GetComponent<CapitalController>();

		foreach(Transform child in transform)
		{
			if(child.name == "ItemSpawnPoint")
			{
				capitalController.SpawnItem(child.transform.position);
			}
		}
	}
}
