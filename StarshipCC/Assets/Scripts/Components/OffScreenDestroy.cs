using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffScreenDestroy : MonoBehaviour {
	public GameObject gameObjectToDestroy;

	void OnBecameInvisible()
	{
		if(gameObjectToDestroy)
			Destroy(gameObjectToDestroy);
		else	
			Destroy(gameObject);
	}
}
