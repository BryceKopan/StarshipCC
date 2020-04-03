using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {
	
	SpriteRenderer renderer;

	float currentHealth, maxHealth;

	// Use this for initialization
	void Start () {
		renderer = gameObject.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		maxHealth = gameObject.transform.parent.GetComponent<AccessibleHealth>().GetMaxHealth();
		currentHealth = gameObject.transform.parent.GetComponent<AccessibleHealth>().GetCurrentHealth();
		renderer.material.SetFloat("_Cutoff", Mathf.InverseLerp(maxHealth, 0, currentHealth/2) + .0001f);
	}
}
