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
        if(p.tag == Tags.ENEMY_BULLET)
        {
            p.tag = Tags.FRIENDLY_BULLET;
        }
        else if(p.tag == Tags.FRIENDLY_BULLET)
        {
            p.tag = Tags.ENEMY_BULLET;
        }

        //Deflect projectile
        Vector2 normal = (p.transform.position - transform.position);
        p.moveVector = Vector2.Reflect(p.moveVector, normal).normalized;
    }
}
