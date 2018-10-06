using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour 
{
    public int PlayerNumber = 0;
    public float speed = 1f;

    public float health;
    public float damage;

    public GameObject bulletPrefab;

    Vector2 moveDirection;
    Vector2 aimDirection;

    //InputAxes
    string horizontalMovementAxis, verticalMovementAxis, 
           horizontalAimAxis, verticalAimAxis, fireAxis;
    

	// Use this for initialization
	void Start () 
    {
	    SetInputAxes();	
	}
	
	// Update is called once per frame
	void Update () 
    {
        HandleInput();

        //Move Along moveDirection
        transform.position += new Vector3(moveDirection.x, moveDirection.y, 0).normalized * speed * Time.deltaTime;

        //Rotate to face aimDirection
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 2);
    }

    void HandleInput()
    {
        float xMovementAxis = Input.GetAxis(horizontalMovementAxis);
        float yMovementAxis = Input.GetAxis(verticalMovementAxis);
        moveDirection = new Vector2(xMovementAxis, yMovementAxis);

        float xAimAxis = Input.GetAxis(horizontalAimAxis);
        float yAimAxis = Input.GetAxis(verticalAimAxis);

        aimDirection = new Vector2(xAimAxis, yAimAxis);

        if(Input.GetAxis(fireAxis) > 0)
        {
            Fire();
        }
    }

    void Fire()
    {
        Transform bulletSpawn = transform.GetChild(0);

        var bullet = (GameObject)Instantiate (
                bulletPrefab,
                bulletSpawn.position,
                bulletSpawn.rotation);

        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.bulletDamage = damage;

        //Add velocity to the bullet
        bulletScript.moveVector = transform.up * bulletScript.bulletMoveSpeed * Time.deltaTime;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
            Destroy(gameObject);
    }

    public void SetInputAxes()
    {
        if(PlayerNumber == 0)
        {
            horizontalMovementAxis = "KeyboardHorizontal";
            verticalMovementAxis = "KeyboardVertical";
            horizontalAimAxis = "ArrowKeysHorizontal";
            verticalAimAxis = "ArrowKeysVertical";
            fireAxis = "KeyboardFire";
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
            fireAxis = "RightJoystickVertical_P1";
        }
    }
}
