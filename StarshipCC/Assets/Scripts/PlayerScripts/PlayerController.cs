using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class PlayerController : MonoBehaviour, Hittable, AccessibleHealth
{
    public PlayerClass defaultClass;

    public int PlayerNumber = 5;
    public float joystickDeadzone = 0.1f;

    [HideInInspector]
    public PlayerClass playerClass;

    List<Weapon> weapons;

    public bool twinStick = false;
    public float twinStickFireThreshold = 0.5f;

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
    
    ParryShield parryShield; // TODO make this an ability

    [HideInInspector]
    public Vector2 moveDirection;
    [HideInInspector]
    public Vector2 aimDirection;

    XboxController controller;

	// Use this for initialization
	void Awake () 
    {
        rigidbody = GetComponent<Rigidbody2D>();
        parryShield = GetComponentInChildren<ParryShield>();
        parryShield.gameObject.SetActive(false);

        weapons = new List<Weapon>();

        // Set the player class, which determines weapons, abilities, etc
        SetPlayerClass(defaultClass);

        SetCurrentHealth(GetMaxHealth());

        InitInput();
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
            for(int i = 0; i < weapons.Count; i++)
            {
                RemoveWeapon(weapons[i]);
                i--;
            }
        }

        // Equip the new class
        playerClass = Instantiate(pClass);
        playerClass.Equip(this);
        SetMaxHealth(playerClass.startingMaxHealth);

        foreach (Weapon weapon in playerClass.startingWeapons)
        {
            AddWeapon(weapon);
        }

        // Set the ship sprite to reflect the class
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = playerClass.playerSprite;

        GameObject colorOverlay = transform.Find("ColorOverlay").gameObject;
        colorOverlay.GetComponent<SpriteRenderer>().sprite = playerClass.colorMask;
        colorOverlay.GetComponent<SpriteMask>().sprite = playerClass.colorMask;
    }

    // FixedUpdate is independent of framerate
    void FixedUpdate () 
    {
        HandleInput();

        //Move Along moveDirection
        rigidbody.AddForce(moveDirection * playerClass.engine.moveSpeed);
        
        //Rotate to face aimDirection
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90;
        rigidbody.MoveRotation(Mathf.LerpAngle(rigidbody.rotation, angle, playerClass.engine.turnSpeed * Time.deltaTime));
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

        //TODO make these abilities
        bool parry;

        // Keyboard input
        if(PlayerNumber == 5)
        {
            xMovementAxis = Input.GetAxis("KeyboardX");
            yMovementAxis = Input.GetAxis("KeyboardY");

            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            xAimAxis = mousePos.x - transform.position.x;
            yAimAxis = mousePos.y - transform.position.y;

            attack = Input.GetMouseButton(0);
            parry = Input.GetMouseButton(1);

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

            ability1 = XCI.GetButton(XboxButton.A, controller);
            ability2 = XCI.GetButton(XboxButton.X, controller);
            ability3 = XCI.GetButton(XboxButton.Y, controller);
            ability4 = XCI.GetButton(XboxButton.B, controller);

            // Twin stick mode
            if (twinStick)
            {
                Vector2 aimVector = new Vector2(xAimAxis, yAimAxis);
                float aimMagnitude = aimVector.magnitude;
                attack = aimMagnitude > twinStickFireThreshold;

                parry = XCI.GetAxis(XboxAxis.LeftTrigger, controller) > 0;
            }
            // Normal stick mode
            else
            {
                float fireAxis = XCI.GetAxis(XboxAxis.RightTrigger, controller);
                attack = fireAxis > 0;

                parry = XCI.GetButton(XboxButton.RightBumper, controller);
            }
        }

        moveDirection = new Vector2(xMovementAxis, yMovementAxis);

        // Implement joystick deadzone
        if (Mathf.Abs(xAimAxis) > joystickDeadzone || Mathf.Abs(yAimAxis) > joystickDeadzone)
        {
            aimDirection = new Vector2(xAimAxis, yAimAxis);
        }
        
        if (attack)
        {
            Attack();
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

        if (parry && canParry)
        {
            Parry();
        }
    }

    void Attack()
    {
        foreach (Weapon weapon in weapons)
        {
            weapon.Attack();
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

    void EndParry()
    {
        parryShield.gameObject.SetActive(false);
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
            TakeDamage(p.damage);
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

    public void SetColor(Color color)
    {
        SpriteRenderer overlayRenderer = transform.Find("ColorOverlay").GetComponent<SpriteRenderer>();
        if(overlayRenderer)
        {
            overlayRenderer.color = color;
        }
    }
}
