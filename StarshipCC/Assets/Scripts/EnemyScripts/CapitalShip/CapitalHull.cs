using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapitalHull : MonoBehaviour, Hittable 
{
	void Hittable.OnHit(Projectile p)
    {  
        //p.Death();
    }
}
