﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum State{Moving, Attacking, Idle};

public struct SimpleTransform{public Vector3 position; public Quaternion rotation;}

public abstract class EnemyController : MonoBehaviour, Hittable 
{
    public float moveSpeed = 20f, rotationSpeed = 1f, maxHealth = 150;
    public GameObject deathEffectPrefab;

    protected List<GameObject> targets;
    protected State currentState;

    private float currentHealth;

    private DegradingSprite ds;

	// Use this for initialization
	void Start () 
    {
        targets = FindTargets();
        currentHealth = maxHealth;

        ds = gameObject.GetComponent<DegradingSprite>();
        if(ds)
            ds.SetMaxHealth(maxHealth);

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
        Vector3 attackPosition;
        SimpleTransform deltaTransform;

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
    }

    protected virtual List<GameObject> FindTargets()
    {
        List<GameObject> targets;
        GameObject[] targetsArray;

        targetsArray = GameObject.FindGameObjectsWithTag("Player");
        targets = new List<GameObject>(targetsArray);

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
        currentState = State.Moving;
    }

    void Death()
    {
        Instantiate(
                deathEffectPrefab,
                transform.position,
                transform.rotation);

        Destroy(gameObject);
    }

    void Hittable.OnHit(Projectile p)
    {
        currentHealth -= p.damage;

        if(ds)
            ds.SetSprite(currentHealth);

        if(currentHealth <= 0)
            Death();

        p.Death();
    }
}
