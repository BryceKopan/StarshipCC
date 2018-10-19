using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class PlayerController : MonoBehaviour, Hittable 
{
    public int PlayerNumber = 0;
    public float joystickDeadzone = 0.1f;
    public float speed = 80f;
    public float dashSpeed = 200f;
    public float turnSpeed = 5f;

    public float health;
    public float damage;

    public float dashLength = 0.3f;
    public float parryLength = 0.3f;
    public float fireCooldown = 0.1f;
    public float dashCooldown = 1f;
    public float parryCooldown = 0.1f;
    bool canFire = true;
    bool canDash = true;
    bool canParry = true;

    Rigidbody2D rigidbody;

    public GameObject bulletPrefab;

    ParryShield parryShield;

    Vector2 moveDirection;
    Vector2 aimDirection;
    Vector2 dashDirection;

    XboxController controller;

    List<Transform> bulletSpawns;

	// Use this for initialization
	void Start () 
    {
        rigidbody = GetComponent<Rigidbody2D>();
        parryShield = GetComponentInChildren<ParryShield>();
        parryShield.gameObject.SetActive(false);

        // Find all bullet spawns attached to the ship
        bulletSpawns = new List<Transform>();
        Transform[] children = gameObject.GetComponentsInChildren<Transform>();

        foreach (Transform child in children)
        {
            if (child.gameObject.tag == "BulletSpawn")
            {
                bulletSpawns.Add(child);
            }
        }

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
        bool dash;
        bool parry;

        // Keyboard input
        if(PlayerNumber == 0)
        {
            xMovementAxis = Input.GetAxis("KeyboardX");
            yMovementAxis = Input.GetAxis("KeyboardY");

            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            xAimAxis = mousePos.x - transform.localPosition.x;
            yAimAxis = mousePos.y - transform.localPosition.y;

            fire = Input.GetMouseButton(0);
            dash = Input.GetButton("KeyboardDash");
            parry = Input.GetMouseButton(1);
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

            dash = XCI.GetButton(XboxButton.LeftBumper, controller);
            parry = XCI.GetButton(XboxButton.RightBumper, controller);
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

        if (dash && canDash)
        {
            Dash();
        }

        if (parry && canParry)
        {
            Parry();
        }
    }

    void Fire()
    {
        foreach (Transform bulletSpawn in bulletSpawns)
        {
            var bullet = (GameObject)Instantiate(
                    bulletPrefab,
                    bulletSpawn.position,
                    bulletSpawn.rotation);

            bullet.tag = Tags.FRIENDLY_BULLET;

            Bullet bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.bulletDamage = damage;

            //Add velocity to the bullet
            bulletScript.moveVector = transform.up * bulletScript.bulletMoveSpeed * Time.deltaTime;
        }
        canFire = false;
        Invoke("EnableFire", fireCooldown);
    }

    void Dash()
    {
        rigidbody.AddForce(moveDirection * dashSpeed, ForceMode2D.Impulse);
        dashDirection = moveDirection;
        canDash = false;
        Invoke("EndDash", dashLength);
        Invoke("EnableDash", dashLength + dashCooldown);
    }

    void Parry()
    {
        canParry = false;
        canDash = false;
        canFire = false;
        Invoke("EndParry", parryLength);
        Invoke("EnableParry", parryLength + parryCooldown);
        parryShield.gameObject.SetActive(true);
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

    void EndDash()
    {
        rigidbody.velocity = rigidbody.velocity * 0.1f;
    }

    void EndParry()
    {
        parryShield.gameObject.SetActive(false);
    }

    void EnableFire()
    {
        canFire = true;
    }

    void EnableDash()
    {
        canDash = true;
    }

    void EnableParry()
    {
        canParry = true;
        canDash = true;
        canFire = true;
    }

    void Hittable.OnHit(Projectile p)
    {
        TakeDamage(p.damage);
        Destroy(p.gameObject);
    }
}
