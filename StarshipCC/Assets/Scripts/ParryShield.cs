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
        //Since we aren't given a collision point, we must calculate it ourselves
        Collider2D collider = GetComponent<Collider2D>();
        Collider2D pCollider = p.GetComponent<Collider2D>();
        ColliderDistance2D collision = collider.Distance(pCollider);

        Vector2 normal;
        if (collision.isValid)
        {
            normal = collision.normal;
        }
        else
        {
            Debug.LogWarning("Invalid collision during parry");
            normal = (p.transform.position - transform.position);
        }
        p.moveVector = Vector2.Reflect(p.moveVector, normal).normalized;
    }
}
