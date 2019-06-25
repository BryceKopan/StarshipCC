using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour {

    public List<Transform> bulletSpawns;

    public float damage;
    public float range;
    public float cooldown = 1f;

    public float soundVolume = 1f;

    public int numAttacks = 1;
    public float delayBetweenAttacks = 0;

    public bool canAttack = true;
    public bool isAttacking = false;

    private AudioSource audioSource;
    public AudioClip attackSound;

    // Use this for initialization
    public virtual void Start()
    {
        bulletSpawns = new List<Transform>();
        foreach (Transform child in transform)
        {
            if (child.tag == Tags.BULLET_SPAWN)
            {
                bulletSpawns.Add(child);
            }
        }

        audioSource = gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    public virtual void Update()
    {

    }

    public void Equip(PlayerController player)
    {
        gameObject.tag = Tags.PLAYER;
        OnEquip(player);
    }

    public void Equip(EnemyController enemy)
    {
        gameObject.tag = Tags.ENEMY;
        OnEquip(enemy);
    }

    public void Unequip(PlayerController player)
    {
        gameObject.tag = null;
        OnUnequip(player);
    }

    public void Unequip(EnemyController enemy)
    {
        gameObject.tag = null;
        OnUnequip(enemy);
    }

    public void Attack()
    {
        canAttack = false;
        isAttacking = true;
        Invoke("EndAttack", numAttacks * delayBetweenAttacks);
        OnAttackStart();
    }

    public void EndAttack()
    {
        isAttacking = false;
        OnAttackEnd();
        Invoke("EnableAttack", cooldown);
    }

    private void EnableAttack()
    {
        canAttack = true;
        OnCanAttack();
    }

    public virtual void PlayAttackSound()
    {
        if (attackSound)
        {
            audioSource.PlayOneShot(attackSound, soundVolume);
        }
    }

    public abstract void OnEquip(PlayerController player);
    public abstract void OnEquip(EnemyController enemy); 
    public abstract void OnUnequip(PlayerController player);
    public abstract void OnUnequip(EnemyController player);
    public abstract void OnAttackStart();
    public abstract void OnAttackEnd();
    public abstract void OnCanAttack();
}
