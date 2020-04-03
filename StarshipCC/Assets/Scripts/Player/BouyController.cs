using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouyController : MonoBehaviour
{

    public float chargeNeededForRevive = 200f;

    public GameObject ship, reviver;
    public Sprite greenSprite, redSprite;
    public SpriteRenderer renderer;

    private float charge;
    private bool charging, reset;

	// Use this for initialization
	void Start () 
    {
        renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = redSprite;
        transform.position = ship.transform.position;        
        charge = 0f;
        charging = false;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if(reset)
        {
            transform.position = ship.transform.position;        
            charge = 0f;
            charging = false;
            reset = false;
            renderer.sprite = redSprite;
        }
        
        if(charging)
        {
            charge++;
            charging = false;
            renderer.sprite = greenSprite;
        }
        else
        {
            renderer.sprite = redSprite;
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
