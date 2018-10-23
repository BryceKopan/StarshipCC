using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDelete : MonoBehaviour {
    public float TimeUntilDestruction;

	// Use this for initialization
	void Start () {
		Destroy(gameObject, TimeUntilDestruction);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
