using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffScreenDestroy : MonoBehaviour {

	void OnBecameInvisible()
	{
		Destroy(gameObject);
	}
}
