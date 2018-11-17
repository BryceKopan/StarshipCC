using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface AccessibleHealth
{
	float GetMaxHealth();
	float GetCurrentHealth();
	void SetMaxHealth(float health);
	void SetCurrentHealth(float health);
}
