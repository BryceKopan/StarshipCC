using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryShield : MonoBehaviour, Hittable
{
    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void Hittable.OnHit(Projectile p)
    {
        p.moveVector *= -1;
        if(p.tag == Tags.ENEMY_BULLET)
        {
            p.tag = Tags.FRIENDLY_BULLET;
        }
        else if(p.tag == Tags.FRIENDLY_BULLET)
        {
            p.tag = Tags.ENEMY_BULLET;
        }
    }
}
