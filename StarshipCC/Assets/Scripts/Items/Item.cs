using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour {

    public bool isInteractable = true;

    public float soundVolume = 1f;

    public AudioClip pickupSound;

    // Use this for initialization
    public virtual void Start () 
    {
    }
	
	// Update is called once per frame
	public virtual void Update () {
		
	}

    public virtual void OnEquip(PlayerController player) 
    {
        if (pickupSound) 
        {
            AudioSource.PlayClipAtPoint(pickupSound, Camera.main.transform.position, soundVolume);
        }
    }

    public virtual void OnUnequip(PlayerController player) 
    {
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject other = collider.gameObject;
        if(other.tag == Tags.PLAYER)
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if(player && isInteractable)
            {
                this.OnEquip(player);
            }
        }
    }
}

