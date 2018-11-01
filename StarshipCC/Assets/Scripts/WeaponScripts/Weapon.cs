﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour {

    public GameObject bulletPrefab;

    public List<Transform> bulletSpawns;

    public float fireCooldown = 1f;
    public float damage = 1f;
    public float bulletSpeed = 40f;
    public float bulletLife = 10f;

    public bool canFire = true;

	// Use this for initialization
	public virtual void Start ()
    {
        bulletSpawns = new List<Transform>();
        foreach (Transform child in transform)
        {
            if(child.tag == Tags.BULLET_SPAWN)
            {
                bulletSpawns.Add(child);
            }
        }
	}
	
	// Update is called once per frame
	public virtual void Update ()
    {
		
	}

    public virtual void EnableFire()
    {
        canFire = true;
    }

    public abstract void OnEquip(PlayerController player);
    public abstract void OnUnequip(PlayerController player);
    public abstract void Fire();
}
