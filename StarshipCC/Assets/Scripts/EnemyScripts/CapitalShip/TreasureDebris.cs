using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureDebris : Debris {

	protected override void Death()
	{
		if(!isDead)
		{
            GameObject capitalShip = GameObject.Find("CapitalShip");
            if(capitalShip)
            {
                capitalShip.GetComponent<ConstructedCapitalController>().SpawnItem(transform.position);
            }

			SpawnDeathPrefabs();

			isDead = true;
			Destroy(gameObject);
		}
	}
}
