using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

    public float Damage;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        string thisTag = gameObject.tag;
        string otherTag = other.gameObject.tag;

        if (thisTag.Equals(Tags.ENEMY_BULLET) && otherTag.Equals(Tags.PLAYER))
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();

            if(player)
            {
                player.TakeDamage(Damage);
            }
        }
    }
}
