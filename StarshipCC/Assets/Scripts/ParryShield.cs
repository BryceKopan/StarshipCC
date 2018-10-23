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
    }
}
