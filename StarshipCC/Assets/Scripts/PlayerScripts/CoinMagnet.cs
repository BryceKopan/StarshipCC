using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMagnet : MonoBehaviour {

	public float magnetForce;

	void OnTriggerStay2D(Collider2D other)
	{
		GameObject otherObject = other.gameObject;

		if(otherObject.tag == Tags.COIN)
		{
			Vector3 force = transform.position - otherObject.transform.position;
			force.Normalize();
			force *= magnetForce;
			other.gameObject.GetComponent<Rigidbody2D>().AddForce(force);
		}
	}
}
