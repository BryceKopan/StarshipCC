using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class PlayerController : MonoBehaviour, Hittable, AccessibleHealth
{
    public int PlayerNumber = 0;
    public float joystickDeadzone = 0.1f;
    public float speed = 80f;
    public float dashSpeed = 200f;
    public float turnSpeed = 5f;

    public GameObject startingWeapon;

    public float maxHealth;
    private float currentHealth;

    public float dashLength = 0.3f;
    public float parryLength = 0.3f;
    public float invincibilityLength = 1f;
    public float dashCooldown = 1f;
    public float parryCooldown = 0.1f;
    bool canDash = true;
    bool canParry = true;
    bool invincible = false;

    Rigidbody2D rigidbody;

    Animator animator;

    public GameObject explosionPrefab;
    public GameObject SetActiveOnDeath;
    
    ParryShield parryShield;

    List<Weapon> weapons;

    Vector2 moveDirection;
    Vector2 aimDirection;

    XboxController controller;

	// Use this for initialization
	void Start () 
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        parryShield = GetComponentInChildren<ParryShield>();
        parryShield.gameObject.SetActive(false);

        // Attach starting weapon to the ship
        weapons = new List<Weapon>();
        AddWeapon(GameObject.Instantiate(startingWeapon).GetComponent<Weapon>());

        currentHealth = maxHealth;

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
        rigidbody.MoveRotation(Mathf.LerpAngle(rigidbody.rotation, angle, turnSpeed * Time.deltaTime));
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
        
        if (fire)
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
        foreach (Weapon weapon in weapons)
        {
            weapon.Fire();
        }
    }

    void Dash()
    {
        rigidbody.AddForce(moveDirection * dashSpeed, ForceMode2D.Impulse);
        canDash = false;
        Invoke("EndDash", dashLength);
        Invoke("EnableDash", dashLength + dashCooldown);
    }

    void Parry()
    {
        canParry = false;
        Invoke("EndParry", parryLength);
        Invoke("EnableParry", parryLength + parryCooldown);
        parryShield.gameObject.SetActive(true);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
            Death();
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

    void StartInvincibility()
    {
        invincible = true;
        animator.SetBool("Invincible", true);
        Invoke("EndInvincibility", invincibilityLength);
    }

    void EndInvincibility()
    {
        animator.SetBool("Invincible", false);
        invincible = false;
    }

    void EnableDash()
    {
        canDash = true;
    }

    void EnableParry()
    {
        canParry = true;
    }

    void Death()
    {
        Instantiate(
                explosionPrefab,
                transform.position,
                transform.rotation);

        SetActiveOnDeath.SetActive(true);
        gameObject.SetActive(false);
    }

    public void Revive()
    {
        currentHealth = 1;
        StartInvincibility();
    }

    public void AddWeapon(Weapon weapon)
    {
        if(weapons != null)
        {
            weapons.Add(weapon);
            weapon.transform.SetParent(transform.Find("Weapons"));
            weapon.transform.localPosition = new Vector2(0, 0);
            weapon.transform.localRotation = Quaternion.Euler(0, 0, 0);
            weapon.OnEquip(this);
        }
    }

    public void RemoveWeapon(Weapon weapon)
    {
        if (weapons != null)
        {
            weapons.Remove(weapon);

            weapon.OnUnequip(this);
        }
    }

    //Interface Methods
    void Hittable.OnHit(Projectile p)
    {
        if (!invincible)
        {
            TakeDamage(p.damage);
            p.Death();
            StartInvincibility();
        }
    }
    public float GetMaxHealth()
    {
        return maxHealth;
    }
	public float GetCurrentHealth()
    {
        return currentHealth;
    }

    public void SetMaxHealth(float health)
    {
        maxHealth = health;
    }
	public void SetCurrentHealth(float health)
    {
        currentHealth = health;
    }
}
