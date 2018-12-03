using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	public float speed;

	private List<Vector3> cameraTargetPositions = new List<Vector3>();
	private List<Vector3> targets = new List<Vector3>();
	private List<bool> waitForNewTarget;

	
	
	void Start () 
	{
		foreach(Transform child in gameObject.transform)
        {
			if(child.name == "CameraTargetPosition")
				cameraTargetPositions.Add(child.transform.position);
		}

		AddTargets(cameraTargetPositions, false);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(targets.Count > 0)
		{
			if(targets[0] != new Vector3())
			{
				Vector3 targetPosition = targets[0];
				targetPosition.z = transform.position.z;
				transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

				if(transform.position == targetPosition)
					targets.RemoveAt(0);
			}
		}

		// Debug.Log("Targets");
		// foreach(Vector3 t in targets)
		// {
		// 	Debug.Log(t);
		// }
	}

	public void AddTargets(List<Vector3> targets, bool pauseAtLastTarget)
	{
		//Debug.Log("AddTarget" + targets + ":" + pauseAtLastTarget);

		if(this.targets.Count > 0 && this.targets[0] == new Vector3())
		{
			this.targets.RemoveAt(0);
		}

		if(pauseAtLastTarget)
			targets.Add(new Vector3());

		targets.AddRange(this.targets);
		this.targets = targets;

		// foreach(Vector3 t in targets)
		// {
		// 	Debug.Log(t);
		// }
	}
}
