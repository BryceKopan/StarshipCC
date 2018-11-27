using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour {
	public GameObject nextTrigger;
	public float activationDelayForNextTrigger;
	public bool pauseAtLastTarget;

	private List<Vector3> cameraTargetPositions = new List<Vector3>();
	private CameraController cameraController;
	
	void Start ()
	{
		cameraController = Camera.main.GetComponent<CameraController>();

		foreach(Transform child in transform)
        {
			if(child.name == "CameraTargetPosition")
				cameraTargetPositions.Add(child.transform.position);
		}
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == Tags.PLAYER)
		{
			cameraController.AddTargets(cameraTargetPositions, pauseAtLastTarget);

			Invoke("ActivateNextTrigger", activationDelayForNextTrigger);

			gameObject.SetActive(false);
		}
	}

	void ActivateNextTrigger()
	{
		if(nextTrigger)
			nextTrigger.SetActive(true);
	}
}
