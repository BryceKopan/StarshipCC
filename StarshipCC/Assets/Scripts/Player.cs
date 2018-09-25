using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		HandleInput();
	}

	void HandleInput()
	{
		if(Input.GetAxis("LeftJoystickHorizontal") > 0)
		{
			Debug.Log("Detecting Input");
		}
	}
}
