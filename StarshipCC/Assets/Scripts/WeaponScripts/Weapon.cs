using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour {

    public List<Transform> bulletSpawns;

    public float fireCooldown = 1f;
    public float damage;
    public float range;

    public bool canFire = true;

    public float soundVolume = 1f;

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

    public abstract void OnEquip(PlayerController player);
    public abstract void OnUnequip(PlayerController player);
    public abstract void Fire();
    public abstract void OnFireStart();
    public abstract void OnFireEnd();
}
