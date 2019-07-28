using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour {

    public List<Transform> attackSpawns;

    public Vector3 avgAttackDirection;

    public float damage;
    public float range;
    public float cooldown = 1f;

    public float soundVolume = 1f;

    public int numAttacks = 1;
    public float delayBetweenAttacks = 0;

    public float attackScaleModifier = 1f;

    public float recoilMagnitude;

    public bool canAttack;
    [ReadOnly]
    public bool isAttacking;

    private AudioSource audioSource;
    public AudioClip attackSound;

    public PlayerController player;
    public EnemyController enemy;

    // Use this for initialization
    public virtual void Awake()
    {
        attackSpawns = new List<Transform>();
        foreach (Transform child in transform)
        {
            if (child.tag == Tags.ATTACK_SPAWN)
            {
                attackSpawns.Add(child);
            }
        }

        CalculateAttackDirection();

        audioSource = gameObject.AddComponent<AudioSource>();

        canAttack = false;
        isAttacking = false;

        gameObject.SetActive(false);
    }

    // Update is called once per frame
    public virtual void FixedUpdate()
    {
        if(isAttacking && canAttack)
        {
            OnAttack();
        }
    }

    public void Equip(PlayerController player)
    { 
        gameObject.tag = Tags.PLAYER;
        gameObject.layer = Layers.PLAYER;
        this.player = player;
        gameObject.SetActive(true);
        OnEquip(player);
        canAttack = true;
    }

    public void Equip(EnemyController enemy)
    {
        gameObject.tag = Tags.ENEMY;
        gameObject.layer = Layers.ENEMY;
        this.enemy = enemy;
        gameObject.SetActive(true);
        OnEquip(enemy);
        canAttack = true;
    }

    public void Unequip(PlayerController player)
    {
        gameObject.tag = Tags.UNTAGGED;
        gameObject.layer = Layers.DEFAULT;
        this.player = null;
        gameObject.SetActive(false);
        OnUnequip(player);
        canAttack = false;
    }

    public void Unequip(EnemyController enemy)
    {
        gameObject.tag = Tags.UNTAGGED;
        gameObject.layer = Layers.DEFAULT;
        gameObject.SetActive(false);
        this.enemy = null;
        OnUnequip(enemy);
        canAttack = false;
    }

    public void StartAttack()
    {
        isAttacking = true;
        OnAttackStart();
    }

    public void StopAttack()
    {
        isAttacking = false;
        OnAttackStop();
    }

    public IEnumerator EnableAttack()
    {
        canAttack = true;
        OnCanAttack();
        return null;
    }

    public virtual void PlayAttackSound()
    {
        if (attackSound)
        {
            audioSource.PlayOneShot(attackSound, soundVolume);
        }
    }

    protected void CalculateAttackDirection()
    {
        Vector3 sumDir = Vector3.zero;
        foreach (Transform attack in attackSpawns)
        {
            sumDir += attack.up;
        }

        avgAttackDirection = sumDir.normalized;
    }

    public void AddAttackSpawn(Transform t)
    {
        attackSpawns.Add(t);
        CalculateAttackDirection();
    }

    public void RemoveAttackSpawn(Transform t)
    {
        if(attackSpawns.Contains(t))
        {
            attackSpawns.Remove(t);
            CalculateAttackDirection();
        }
    }

    // Once StartAttack is called, OnAttack is called every frame until StopAttack is called
    // Thus OnAttack can be treated like an Update() that's only called while attacking
    public abstract void OnAttack();

    public abstract void OnEquip(PlayerController player);
    public abstract void OnEquip(EnemyController enemy); 
    public abstract void OnUnequip(PlayerController player);
    public abstract void OnUnequip(EnemyController player);
    public abstract void OnAttackStart();
    public abstract void OnAttackStop();
    public abstract void OnCanAttack();
}
