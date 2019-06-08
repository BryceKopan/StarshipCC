using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour {

    public List<Transform> bulletSpawns;

    public float fireCooldown = 1f;
    public float damage;
    public float range;
    public float soundVolume = 1f;

    public int numShots = 1;
    public float delayBetweenShots = 0;

    public bool canFire = true;

    private AudioSource audioSource;
    public AudioClip fireSound;

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

        audioSource = gameObject.AddComponent<AudioSource>();
	}
	
	// Update is called once per frame
	public virtual void Update ()
    {
		
	}

    public virtual void EnableFire()
    {
        canFire = true;
    }

    public virtual void PlayFireSound() 
    {
        if(fireSound) 
        {
            audioSource.PlayOneShot(fireSound, soundVolume);
        }
    }

    public virtual void OnEquip(PlayerController player)
    {
        gameObject.tag = Tags.PLAYER;
    }

    public virtual void OnEquip(EnemyController enemy)
    {
        gameObject.tag = Tags.ENEMY;
    }

    public virtual void OnUnequip(PlayerController player) { }
    public virtual void OnUnequip(EnemyController player) { }
    public abstract void Fire();
    public abstract void OnFireStart();
    public abstract void OnFireEnd();
}
