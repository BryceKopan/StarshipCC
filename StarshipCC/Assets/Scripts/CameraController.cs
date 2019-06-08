using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float moveSpeed;

	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
        Vector2 avgPlayerPosition = Vector2.zero;
        PlayerController[] players = FindObjectsOfType<PlayerController>();
        foreach (PlayerController player in players)
        {
            avgPlayerPosition += new Vector2(player.transform.position.x, player.transform.position.y);
        }

        avgPlayerPosition /= players.Length;

        Vector2 moveVector = Vector2.MoveTowards(transform.position, avgPlayerPosition, moveSpeed * Time.deltaTime);
        transform.position = new Vector3(moveVector.x, moveVector.y, transform.position.z);
	}
}
