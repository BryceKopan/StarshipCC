using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float moveSpeed;

    public Vector2 targetPosition;

    public PlayerController[] players;

    public CapitalController capitalShip;

	void Start () 
	{
        players = FindObjectsOfType<PlayerController>();
        capitalShip = FindObjectOfType<CapitalController>();
    }
	
	// Update is called once per frame
	void FixedUpdate () 
	{
        updateTargetPosition();

        // Move towards target position
        Vector2 moveVector = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        transform.position = new Vector3(moveVector.x, moveVector.y, transform.position.z);
    }

    public void updateTargetPosition()
    {
        Vector2 avgPlayerPosition = Vector2.zero;

        int numPositionsAveraged = 0;

        Vector2 capitalShipPosition = Vector2.zero;
        if (capitalShip)
        {
            capitalShipPosition = capitalShip.transform.position;
        }

        foreach (PlayerController player in players)
        {
            avgPlayerPosition += new Vector2(player.transform.position.x, player.transform.position.y);
            numPositionsAveraged++;

            if (capitalShip)
            {
                avgPlayerPosition += new Vector2(player.transform.position.x, capitalShipPosition.y);
                numPositionsAveraged++;
            }
        }

        if (avgPlayerPosition != Vector2.zero)
        {
            avgPlayerPosition /= numPositionsAveraged;
        }

        targetPosition = avgPlayerPosition;
    }
}
