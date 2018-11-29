﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureDebris : Debris {

	protected override void Death()
	{
		if(!isDead)
		{
			GameObject.Find("CapitalShip").GetComponent<CapitalController>().SpawnItem(transform.position);

			SpawnDeathPrefabs();

			isDead = true;
			Destroy(gameObject);
		}
	}
}
