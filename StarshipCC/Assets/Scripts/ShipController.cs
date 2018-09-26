using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour 
{
    public int PlayerNumber = 0;
    public float speed = 1f;
    Vector2 moveDirection;
    Vector2 aimDirection;

	// Use this for initialization
	void Start () 
    {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
        HandleInput();

        transform.position += new Vector3(moveDirection.x, moveDirection.y, 0).normalized * speed * Time.deltaTime;

        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 2);
    }

    void HandleInput()
    {
        string horizontalMovementAxis, verticalMovementAxis,
               horizontalAimAxis, verticalAimAxis;

        if(PlayerNumber == 0)
        {
            horizontalMovementAxis = "KeyboardHorizontal";
            verticalMovementAxis = "KeyboardVertical";
            horizontalAimAxis = "MouseHorizontal";
            verticalAimAxis = "MouseVertical";
        }
        else
        {
            horizontalMovementAxis = "LeftJoystickHorizontal_P" 
                + PlayerNumber;
            verticalMovementAxis = "LeftJoystickVertical_P"
                + PlayerNumber;
            horizontalAimAxis = "RightJoystickHorizontal_P"
                + PlayerNumber;
            verticalAimAxis = "RightJoystickVertical_P"
                + PlayerNumber;
        }

        float xMovementAxis = Input.GetAxis(horizontalMovementAxis);
        float yMovementAxis = Input.GetAxis(verticalMovementAxis);
        moveDirection = new Vector2(xMovementAxis, yMovementAxis);

        float xAimAxis = Input.GetAxis(horizontalAimAxis);
        float yAimAxis = Input.GetAxis(verticalAimAxis);
        aimDirection = new Vector2(xAimAxis, yAimAxis);
    }
}
