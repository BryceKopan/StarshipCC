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
    public bool isAttacking;

    private AudioSource audioSource;
    public AudioClip attackSound;

    public PlayerController player;
    public EnemyController enemy;

    // Use this for initialization
    public virtual void Start()
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

        canAttack = true;
        isAttacking = false;
    }

    // Update is called once per frame
    public virtual void Update()
    {

    }

    public void Equip(PlayerController player)
    {
        gameObject.tag = Tags.PLAYER;
        gameObject.layer = Layers.PLAYER;
        this.player = player;
        OnEquip(player);
    }

    public void Equip(EnemyController enemy)
    {
        gameObject.tag = Tags.ENEMY;
        gameObject.layer = Layers.ENEMY;
        this.enemy = enemy;
        OnEquip(enemy);
    }

    public void Unequip(PlayerController player)
    {
        gameObject.tag = Tags.UNTAGGED;
        gameObject.layer = Layers.DEFAULT;
        this.player = null;
        OnUnequip(player);
    }

    public void Unequip(EnemyController enemy)
    {
        gameObject.tag = Tags.UNTAGGED;
        gameObject.layer = Layers.DEFAULT;
        this.enemy = null;
        OnUnequip(enemy);
    }

    public void Attack()
    {
        if(canAttack)
        {
            canAttack = false;
            isAttacking = true;
            Invoke("EndAttack", numAttacks * delayBetweenAttacks);
            OnAttackStart();
        }
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

    public abstract void OnEquip(PlayerController player);
    public abstract void OnEquip(EnemyController enemy); 
    public abstract void OnUnequip(PlayerController player);
    public abstract void OnUnequip(EnemyController player);
    public abstract void OnAttackStart();
    public abstract void OnAttackEnd();
    public abstract void OnCanAttack();
}
