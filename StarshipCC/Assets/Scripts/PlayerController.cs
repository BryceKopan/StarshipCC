using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class PlayerController : MonoBehaviour 
{
    public int PlayerNumber = 0;
    public float joystickDeadzone = 0.1f;
    public float speed = 1f;
    public float turnSpeed = 1f;

    public float health;
    public float damage;

    public float fireDelay = 1;
    bool canFire = true;

    Rigidbody2D rigidbody;

    public GameObject bulletPrefab;

    Vector2 moveDirection;
    Vector2 aimDirection;

    XboxController controller;

	// Use this for initialization
	void Start () 
    {
        rigidbody = GetComponent<Rigidbody2D>();

        InitInput();
	}
	
	// FixedUpdate is independent of framerate
	void FixedUpdate () 
    {
        HandleInput();

        //Move Along moveDirection
        rigidbody.AddForce(moveDirection * speed);

        //Rotate to face aimDirection
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * turnSpeed);
    }

    void HandleInput()
    {
        float xMovementAxis;
        float yMovementAxis;
        float xAimAxis;
        float yAimAxis;
        bool fire;

        // Keyboard input
        if(PlayerNumber == 0)
        {
            xMovementAxis = Input.GetAxis("KeyboardX");
            yMovementAxis = Input.GetAxis("KeyboardY");

            Debug.Log("KeyboardX: " + xMovementAxis);

            xAimAxis = Input.GetAxis("MouseX");
            yAimAxis = Input.GetAxis("MouseY");

            fire = Input.GetButton("MousePrimaryClick");
        }
        // Controller input
        else
        {
            xMovementAxis = XCI.GetAxis(XboxAxis.LeftStickX, controller);
            yMovementAxis = XCI.GetAxis(XboxAxis.LeftStickY, controller);

            xAimAxis = XCI.GetAxis(XboxAxis.RightStickX, controller);
            yAimAxis = XCI.GetAxis(XboxAxis.RightStickY, controller);

            float fireAxis = XCI.GetAxis(XboxAxis.RightTrigger, controller);
            fire = fireAxis > 0;
        }

        moveDirection = new Vector2(xMovementAxis, yMovementAxis);
        // Implement joystick deadzone
        if (Mathf.Abs(xAimAxis) > joystickDeadzone || Mathf.Abs(yAimAxis) > joystickDeadzone)
        {
            aimDirection = new Vector2(xAimAxis, yAimAxis);
        }
        
        if (fire && canFire)
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

        canFire = false;
        Invoke("EnableFiring", fireDelay);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
            Destroy(gameObject);
    }

    void InitInput()
    {
        // 0 means keyboard, 1-4 means controller
        if (PlayerNumber != 0)
        {
            switch (PlayerNumber)
            {
                case 1:
                    controller = XboxController.First;
                    break;
                case 2:
                    controller = XboxController.Second;
                    break;
                case 3:
                    controller = XboxController.Third;
                    break;
                case 4:
                    controller = XboxController.Fourth;
                    break;
                default:
                    Debug.LogError("Error: Invalid player number (must be 0-4)");
                    break;
            }
        }
    }

    void EnableFiring()
    {
        canFire = true;
    }
}
