using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    public float Damage = 1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject otherObject = other.gameObject;

        if(otherObject.tag == "Player")
        {
            PlayerController pc = otherObject.GetComponent<PlayerController>();
            pc.TakeDamage(Damage);
        }
    }
}
