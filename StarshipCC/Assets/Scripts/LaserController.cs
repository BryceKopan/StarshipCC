using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour {
    public float Speed, ChargeTime, AttackDistance;
    public GameObject AttackPrefab, ChargePrefab;

    bool charging;
    float chargeStartTime;
    Vector2 targetPosition;

	// Use this for initialization
	void Start () 
    {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
		if(chargeStartTime + ChargeTime <= Time.time && charging)
        {
            charging = false;
            Attack();
        }

        if(!charging)
        {
            transform.position += new Vector3(1, 0, 0) * Speed * Time.deltaTime;
            if(transform.position.x % AttackDistance == 0)
            {
                Debug.Log("Charging");
                ChargeAttack();
            }
        }
	}

    void ChargeAttack()
    {
        charging = true;
        chargeStartTime = Time.time;
        var charge = (GameObject)Instantiate (
                AttackPrefab,
                transform.position,
                transform.rotation);

        Destroy(charge, ChargeTime);
    }

    void Attack()
    {
        var attack = (GameObject)Instantiate (
                AttackPrefab,
                transform.position,
                transform.rotation);
    }
}
