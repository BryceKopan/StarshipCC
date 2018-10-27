using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouyController : MonoBehaviour {

    public float chargeNeededForRevive = 200f;

    public GameObject ship, reviver;
    
    private float charge;
    private bool charging, reset;

	// Use this for initialization
	void Start () {
            transform.position = ship.transform.position;        
            charge = 0f;
            charging = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if(reset)
        {
            transform.position = ship.transform.position;        
            charge = 0f;
            charging = false;
            reset = false;
        }
        
        if(charging)
        {
            charge++;
            charging = false;
        }

		if(charge >= chargeNeededForRevive)
        {
            reset = true;
            PlayerController player = ship.GetComponent<PlayerController>();
            player.Revive();
            ship.SetActive(true);
            gameObject.SetActive(false);
        }
	}

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == Tags.PLAYER)
        {
            charging = true;
        }
    }
}
