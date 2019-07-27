﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapitalHull : MonoBehaviour, Hittable 
{
    public float health = 1;

    public int minChunkAmount, maxChunkAmount;

    public GameObject chunkPrefab;

	void Hittable.OnHit(Projectile p)
    {  
        health -= p.damage;

        if(health <= 0)
            Death();

        p.Death();
    }

    private void Death()
    {
        Destroy(gameObject);

        /*int numberOfChunks = Random.Range(minChunkAmount, maxChunkAmount);
        for(int i = 0; i < numberOfChunks; i++)
        {
            Instantiate(chunkPrefab, transform.position, Quaternion.identity);
        }*/
    }
}
