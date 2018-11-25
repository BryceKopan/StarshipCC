using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnScreenEnabled : MonoBehaviour {

	private EnemyController controllerScript;

	void Start ()
	{
		controllerScript = gameObject.GetComponent<EnemyController>();

		SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();

		if(renderer)
		{
			if(!renderer.isVisible)
			{
				controllerScript.enabled = false;
			}
		}
	}
	
	void OnBecameInvisible()
	{
		controllerScript.enabled = false;
	}

	void OnBecameVisible()
	{
		controllerScript.enabled = true;
	}
}
