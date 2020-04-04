using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissile : Missile 
{
	public float rotationSpeed;
	GameObject target;

	public override void OnStart()
	{
		List<GameObject> targets;
        GameObject[] targetsArray;

        targetsArray = GameObject.FindGameObjectsWithTag("Player");
        targets = new List<GameObject>(targetsArray);

		if(targets[0])
		{
			this.target = targets[0];
			float distanceToTargetPosition = 
				Vector3.Distance(targets[0].transform.position, transform.position);

			foreach(GameObject target in targets)
			{
				float distanceToTarget = Vector3.Distance(target.transform.position, transform.position);
				if(distanceToTarget < distanceToTargetPosition)
				{
					this.target = target;
					distanceToTargetPosition = distanceToTarget;
				}
			}
		}
	}

	public override void FixedUpdate()
	{
		transform.position += transform.up * speed * Time.fixedDeltaTime;

		if(target)
		{
			Vector3 vectorToTarget = target.transform.position - transform.position;
			float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90;
			Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
			transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.fixedDeltaTime * rotationSpeed);
		}
	}
}
