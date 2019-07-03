using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour {

    public bool isInteractable = true;

    public float soundVolume = 1f;

    public AudioClip pickupSound;

    [ReadOnly]
    public PlayerController player;

    bool isTimed = false;
    float duration = 0;

    // Use this for initialization
    public virtual void Start () 
    {
    }
	
	// Update is called once per frame
	public virtual void Update () {
		
	}

    protected void Equip(PlayerController player)
    {
        this.player = player;
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);

        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<Collider2D>().enabled = false;

        OnEquip(player);

        if (pickupSound)
        {
            AudioSource.PlayClipAtPoint(pickupSound, Camera.main.transform.position, soundVolume);
        }

        if (isTimed)
        {
            Invoke("Unequip", duration);
        }
    }

    protected void Unequip()
    {
        OnUnequip(player);

        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<Collider2D>().enabled = true;

        this.transform.parent = null;
        this.player = null;
    }

    public abstract void OnEquip(PlayerController player);

    public abstract void OnUnequip(PlayerController player);

    public void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject other = collider.gameObject;
        if(other.tag == Tags.PLAYER)
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if(player && isInteractable)
            {
                this.Equip(player);
            }
        }
    }
}

