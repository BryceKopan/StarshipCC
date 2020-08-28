using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {
	
	SpriteRenderer renderer;

    AccessibleHealth healthObj;

	float currentHealth, maxHealth;

	// Use this for initialization
	void Start () {
		renderer = gameObject.GetComponent<SpriteRenderer>();

        healthObj = gameObject.transform.parent.GetComponent<AccessibleHealth>();
	}
	
	// Update is called once per frame
	void Update () {
		maxHealth = healthObj.GetMaxHealth();
		currentHealth = healthObj.GetCurrentHealth();
		renderer.material.SetFloat("_Cutoff", Mathf.InverseLerp(maxHealth, 0, currentHealth/2) + .0001f);
	}
}
