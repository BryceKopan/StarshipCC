using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour {

	// Use this for initialization
	public virtual void Start () {
		
	}
	
	// Update is called once per frame
	public virtual void Update () {
		
	}

    public abstract void OnEquip(PlayerController player);
    public abstract void OnUnequip(PlayerController player);

    public void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;
        if(other.tag == Tags.PLAYER)
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if(player)
            {
                this.OnEquip(player);
            }
        }
    }
}

