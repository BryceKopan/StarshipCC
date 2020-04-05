using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float moveSpeed;

    public Vector2 targetPosition;

    public PlayerController[] players;

    public bool freezeCamera = true;

	void Start () 
	{
        players = FindObjectsOfType<PlayerController>();
    }
	
	// Update is called once per frame
	void FixedUpdate () 
	{
        if(!freezeCamera)
        {
            updateTargetPosition();

            // Move towards target position
            Vector2 moveVector = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            transform.position = new Vector3(moveVector.x, moveVector.y, transform.position.z);
        }
    }

    public void updateTargetPosition()
    {
        Vector2 avgPlayerPosition = Vector2.zero;

        int numPositionsAveraged = 0;

        foreach (PlayerController player in players)
        {
            avgPlayerPosition += new Vector2(player.transform.position.x, player.transform.position.y);
            numPositionsAveraged++;
        }

        if (avgPlayerPosition != Vector2.zero)
        {
            avgPlayerPosition /= numPositionsAveraged;
        }

        targetPosition = avgPlayerPosition;
    }
}
