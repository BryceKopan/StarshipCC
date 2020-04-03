using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum State{Moving, Attacking, Idle};

public struct SimpleTransform{public Vector3 position; public Quaternion rotation;}

public abstract class EnemyController : MonoBehaviour, Hittable, AccessibleHealth
{
    public float moveSpeed = 20f, rotationSpeed = 1f, maxHealth = 150;
    public GameObject deathEffectPrefab;
    public int ScoreValue = 50;
    protected List<GameObject> targets;
    protected State currentState;

    Vector3 attackPosition;

    public Vector3 moveVector;

    private GameController controller;
    private float currentHealth;

    private DegradingSprite ds;

    private bool isDead = false;

    public List<Weapon> weapons;

    protected float longestWeaponFireTime = 0;
    protected float longestWeaponRange = 0;

    // Use this for initialization
    void Start () 
    {
        gameObject.layer = Layers.ENEMY;
        targets = FindTargets();
        currentHealth = maxHealth;

        ds = gameObject.GetComponent<DegradingSprite>();
        if(ds)
            ds.SetMaxHealth(maxHealth);

        GameObject gameControllerObj = GameObject.Find("GameController");

        if (gameControllerObj)
        {
            controller = gameControllerObj.GetComponent<GameController>();
        }

        OnStart();
	}

    protected virtual void OnStart(){}
	
	// Update is called once per frame
	void Update () 
    {
		
	}

    void FixedUpdate()
    {
        List<GameObject> viableTargets;
        SimpleTransform deltaTransform;

        targets = FindTargets();

        viableTargets = TrimTargets(targets);

        if(viableTargets.Count <= 0)
            currentState = State.Idle;
        else if(currentState == State.Idle)
            currentState = State.Moving;

        if(currentState == State.Moving)
        {
            attackPosition = GetAttackPosition(viableTargets);
            deltaTransform = GetTransformToAttack(attackPosition);
            MoveAlong(deltaTransform);

            if(IsInRange(attackPosition))
            {
                currentState = State.Attacking;
                Attack();
            }
        }

        OnFixedUpdate();
    }

    protected virtual void OnFixedUpdate(){}

    protected virtual void EquipAllChildWeapons()
    {
        foreach (Weapon weapon in GetComponentsInChildren<Weapon>(true))
        {
            AddWeapon(weapon);
        }
    }

    protected virtual List<GameObject> FindTargets()
    {
        List<GameObject> targets;

        targets = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));

        for(int i = 0; i < targets.Count; i++)
		{
			if(targets[i].name != "Ship")
			{
				targets.RemoveAt(i);
			}
		}

        return targets;
    }

    protected virtual List<GameObject> TrimTargets(List<GameObject> targets)
    {
        List<GameObject> viableTargets = new List<GameObject>();

        foreach(GameObject target in targets)
        {
            if(target.activeSelf)
                viableTargets.Add(target);
        }

        return viableTargets;
    }

    protected virtual Vector3 GetAttackPosition(List<GameObject> activeTargets)
    {
        Vector3 targetPosition = activeTargets[0].transform.position;
        float distanceToTargetPosition = 
            Vector3.Distance(targetPosition, transform.position);

        foreach(GameObject target in activeTargets)
        {
            float distanceToTarget = Vector3.Distance(target.transform.position, transform.position);
            if(distanceToTarget < distanceToTargetPosition)
            {
                targetPosition = target.transform.position;
                distanceToTargetPosition = distanceToTarget;
            }
        }

        return targetPosition;
    }

    protected abstract SimpleTransform GetTransformToAttack(Vector3 attackPosition);

    protected virtual void MoveAlong(SimpleTransform deltaTransform)
    {
        transform.position += deltaTransform.position * moveSpeed * Time.fixedDeltaTime;
        transform.rotation = Quaternion.Slerp(transform.rotation, deltaTransform.rotation, Time.fixedDeltaTime * rotationSpeed);
    }

    protected abstract bool IsInRange(Vector3 attackPosition);

    protected abstract void Attack();

    protected void DoneAttacking()
    {
        foreach(Weapon weapon in weapons)
        {
            weapon.StopAttack();
        }

        currentState = State.Moving;
    }

    void Death()
    {
        if(!isDead)
        {
            Instantiate(
                    deathEffectPrefab,
                    transform.position,
                    transform.rotation);

            isDead = true;
            OnDeath();
            Destroy(gameObject);
            if(controller)
            {
                controller.AddScore(ScoreValue);
            }   
        }
    }

    protected virtual void OnDeath(){}

    //Interface Methods
    void Hittable.OnHit(Projectile p)
    {
        currentHealth -= p.damage;

        if(ds)
            ds.SetSprite(currentHealth);

        if(currentHealth <= 0)
            Death();
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }
	public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public void SetMaxHealth(float health)
    {
        maxHealth = health;
    }
	public void SetCurrentHealth(float health)
    {
        currentHealth = health;
    }

    public void AddWeapon(Weapon weapon)
    {
        if (weapons != null)
        {
            weapons.Add(weapon);
            weapon.tag = Tags.ENEMY;
            weapon.transform.SetParent(transform);
            weapon.transform.localPosition = new Vector2(0, 0);
            weapon.transform.localRotation = Quaternion.Euler(0, 0, 0);
            weapon.Equip(this);
        }

        float weaponFireTime = weapon.delayBetweenAttacks * weapon.numAttacks + weapon.cooldown;
        if (longestWeaponFireTime < weaponFireTime)
        {
            longestWeaponFireTime = weaponFireTime;
        }

        if (longestWeaponRange < weapon.range)
        {
            longestWeaponRange = weapon.range;
        }
    }

    public void RemoveWeapon(Weapon weapon)
    {
        if (weapons != null)
        {
            weapons.Remove(weapon);
            weapon.Unequip(this);
        }

        // TODO update longestWeaponFireTime and range
    }
}
