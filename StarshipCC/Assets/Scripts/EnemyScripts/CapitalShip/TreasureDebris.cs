using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureDebris : Debris {

	protected override void Death()
	{
		GameObject.Find("CapitalShip").GetComponent<CapitalController>().SpawnItem(transform.position);

		Instantiate(
                deathEffectPrefab,
                transform.position,
                transform.rotation);

		Destroy(gameObject);
	}
}
