using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class PlayerController : MonoBehaviour, Hittable, AccessibleHealth
{
    public PlayerClass defaultClass;

    // Determines which controller will move the ship (1-4 = xbox, 5 = keyboard and mouse)
    public int PlayerNumber = 5;

    // How far the user must move the stick before the ship will rotate (0-1)
    public float joystickAimDeadzone = 0.1f;

    // How far the user must move the stick before the ship will move (0-1)
    public float joystickMoveDeadzone = 0.1f;

    // How far the user must move the stick before the ship will fire (0-1)
    public float twinStickFireThreshold = 0.5f;

    [ReadOnly]
    public float thrustLevel = 0f;
    
    [ReadOnly]
    public PlayerClass playerClass = null;

    public List<Weapon> weapons;

    public bool twinStick = false;

    private float maxHealth;
    private float currentHealth;

    // TODO move all these into abilities
    public float parryLength = 0.3f;
    public float invincibilityLength = 1f;
    public float parryCooldown = 0.1f;
    bool canParry = true;
    bool invincible = false;

    Rigidbody2D rigidbody;

    public GameObject explosionPrefab;
    public GameObject SetActiveOnDeath;

    public bool isAttacking = false;

    [HideInInspector]
    public Vector2 moveDirection;
    [HideInInspector]
    public Vector2 aimDirection;

    XboxController controller;

	// Use this for initialization
	void Awake () 
    {
        rigidbody = GetComponent<Rigidbody2D>();

        weapons = new List<Weapon>();

        // Set the player class, which determines weapons, abilities, etc
        SetPlayerClass(defaultClass);

        SetCurrentHealth(GetMaxHealth());
	}

    void Start()
    {
        InitInput();
        transform.localPosition = Vector3.zero;
    }

    void InitInput()
    {
        // 1-4 means controller, 5 means keyboard 
        if (PlayerNumber != 5)
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
                    Debug.LogError("Error: Invalid player number (must be 1-5)");
                    break;
            }
        }
    }

    public void SetPlayerClass(PlayerClass pClass)
    {
        // If player class is already set, remove the existing weapons
        if(playerClass)
        {
            playerClass.Unequip();
        }

        // Equip the new class
        playerClass = Instantiate(pClass);
        playerClass.Equip(this);
    }

    // FixedUpdate is independent of framerate
    void FixedUpdate () 
    {
        HandleInput();
        
        //Rotate to face aimDirection
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90;
        rigidbody.MoveRotation(Mathf.LerpAngle(rigidbody.rotation, angle, playerClass.engine.TurnSpeed() * Time.deltaTime));
    }

    void HandleInput()
    {
        float xMovementAxis;
        float yMovementAxis;
        float xAimAxis;
        float yAimAxis;
        bool attack;
        bool ability1;
        bool ability2;
        bool ability3;
        bool ability4;

        // Keyboard input
        if(PlayerNumber == 5)
        {
            xMovementAxis = Input.GetAxis("KeyboardX");
            yMovementAxis = Input.GetAxis("KeyboardY");

            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            xAimAxis = mousePos.x - transform.position.x;
            yAimAxis = mousePos.y - transform.position.y;

            attack = Input.GetMouseButton(0);

            ability1 = Input.GetButtonDown("KeyboardAbility1");
            ability2 = Input.GetButtonDown("KeyboardAbility2");
            ability3 = Input.GetButtonDown("KeyboardAbility3");
            ability4 = Input.GetButtonDown("KeyboardAbility4");
        }
        // Controller input
        else 
        {
            xMovementAxis = XCI.GetAxis(XboxAxis.LeftStickX, controller);
            yMovementAxis = XCI.GetAxis(XboxAxis.LeftStickY, controller);

            xAimAxis = XCI.GetAxis(XboxAxis.RightStickX, controller);
            yAimAxis = XCI.GetAxis(XboxAxis.RightStickY, controller);

            ability1 = XCI.GetAxis(XboxAxis.RightTrigger, controller) > 0.5;
            ability2 = XCI.GetAxis(XboxAxis.LeftTrigger, controller) > 0.5;
            ability3 = XCI.GetButton(XboxButton.RightBumper, controller);
            ability4 = XCI.GetButton(XboxButton.LeftBumper, controller);

            // Twin stick mode
            if (twinStick)
            {
                Vector2 aimVector = new Vector2(xAimAxis, yAimAxis);
                float aimMagnitude = aimVector.magnitude;

                float angleBetweenAimAndJoystick = Vector2.Angle(transform.up, aimDirection);

                attack = aimMagnitude > twinStickFireThreshold;
            }
            // Normal stick mode
            else
            {
                float fireAxis = XCI.GetAxis(XboxAxis.RightTrigger, controller);
                attack = fireAxis > 0;
            }
        }

        // Joystick moving deadzone (x and y are combined then normalized to create circular deadzone, not square)
        Vector2 moveJoystickPos = new Vector2(xMovementAxis, yMovementAxis);
        if (moveJoystickPos.magnitude > joystickMoveDeadzone)
        {
            thrustLevel = moveJoystickPos.magnitude / (1 - joystickMoveDeadzone);

            moveDirection = moveJoystickPos.normalized;
        }
        else
        {
            thrustLevel = 0;
        }

        moveDirection = new Vector2(xMovementAxis, yMovementAxis).normalized;


        // Joystick aiming deadzone (x and y are combined then normalized to create circular deadzone, not square)
        Vector2 aimJoystickPos = new Vector2(xAimAxis, yAimAxis);
        if(aimJoystickPos.magnitude > joystickAimDeadzone)
        {
            aimDirection = aimJoystickPos.normalized;
        }
        
        if (attack && !isAttacking)
        {
            foreach (Weapon weapon in weapons)
            {
                weapon.StartAttack();
            }

            isAttacking = true;
        }
        else if(!attack && isAttacking)
        {
            foreach (Weapon weapon in weapons)
            {
                weapon.StopAttack();
            }

            isAttacking = false;
        }

        if (ability1)
        {
            ActivateAbility1();
        }

        if (ability2)
        {
            ActivateAbility2();
        }

        if (ability3)
        {
            ActivateAbility3();
        }

        if (ability4)
        {
            ActivateAbility4();
        }
    }

    void ActivateAbility1()
    {
        if (playerClass)
        {
            if (playerClass.ability1)
            {
                playerClass.ability1.Activate();
            }
        }
    }

    void ActivateAbility2()
    {
        if (playerClass)
        {
            if (playerClass.ability2)
            {
                playerClass.ability2.Activate();
            }
        }
    }

    void ActivateAbility3()
    {
        if (playerClass)
        {
            if (playerClass.ability3)
            {
                playerClass.ability3.Activate();
            }
        }
    }

    void ActivateAbility4()
    {
        if (playerClass)
        {
            if (playerClass.ability4)
            {
                playerClass.ability4.Activate();
            }
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
            Death();
    }

    void StartInvincibility()
    {
        invincible = true;
        Invoke("EndInvincibility", invincibilityLength);
    }

    void EndInvincibility()
    {
        invincible = false;
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
        weapons.Add(weapon);
        weapon.transform.SetParent(transform.Find("Weapons"));
        weapon.transform.localPosition = new Vector2(0, 0);
        weapon.transform.localRotation = Quaternion.Euler(0, 0, 0);
        weapon.Equip(this);
    }

    public bool RemoveWeapon(Weapon weapon)
    {
        if(weapons.Contains(weapon))
        {
            weapon.transform.parent = null;
            weapons.Remove(weapon);
            weapon.Unequip(this);
            return true;
        }
        return false;
    }

    //Interface Methods
    void Hittable.OnHit(Projectile p)
    {
        if (!invincible)
        {
            float damageBlocked = 0;
            // First try to block damage with shield
            if (playerClass.shield != null)
            {
                if (playerClass.shield.HasCharge())
                {
                    damageBlocked = Mathf.Min(playerClass.shield.currentCharge, p.damage);
                    playerClass.shield.Hit(p);
                }
            }

            float damageDealt = p.damage - damageBlocked;
            TakeDamage(p.damage - damageBlocked);

            if (damageDealt > 0)
            {
                StartInvincibility();
            }
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

    public void SetColor(Color color)
    {
        SpriteRenderer overlayRenderer = transform.Find("ColorOverlay").GetComponent<SpriteRenderer>();
        if(overlayRenderer)
        {
            overlayRenderer.color = color;
        }
    }
}
