using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryShield : MonoBehaviour, Hittable
{
    public float damageMultiplier = 10f;

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
        //If we parried a missile, make the missile explode
        Missile missileScript = p.GetComponent<Missile>();
        if(missileScript) 
        {
            missileScript.Death();
            return;
        }
        else 
        {
            p.gameObject.layer = Layers.FRIENDLY_ATTACK;
        }

        //Increase damage of deflected projectile
        p.damage *= damageMultiplier;

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
