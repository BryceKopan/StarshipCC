using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClass : MonoBehaviour
{
    public List<Weapon> startingWeapons;

    [ReadOnly]
    public Engine engine;
    [ReadOnly]
    public Shield shield;
    
    [ReadOnly]
    public Ability ability1;
    [ReadOnly]
    public Ability ability2;
    [ReadOnly]
    public Ability ability3;
    [ReadOnly]
    public Ability ability4;

    public float startingMaxHealth;

    public Sprite shipSprite;
    public Sprite colorMask;

    private PlayerController player;

    void Awake()
    {
        for(int i = 0; i < startingWeapons.Count; i++)
        {
            Weapon instance = Instantiate(startingWeapons[i]);
            startingWeapons[i] = instance;
        }

        // Engine and shield must be components on the class prefab
        engine = gameObject.GetComponent<Engine>();
        shield = gameObject.GetComponent<Shield>();

        Ability[] abilities = GetComponents<Ability>();
        for(int i = 0; i < abilities.Length; i++)
        {
            if(i == 0)
            {
                ability1 = abilities[i];
            }
            else if(i == 1)
            {
                ability2 = abilities[i];
            }
            else if (i == 2)
            {
                ability3 = abilities[i];
            }
            else if (i == 4)
            {
                ability4 = abilities[i];
            }
            else
            {
                Debug.Log("Warning: player class has more than 4 abilities");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Equip(PlayerController player)
    {
        this.player = player;
        this.transform.parent = player.transform;
        this.transform.position = player.transform.position;

        player.SetMaxHealth(startingMaxHealth);

        // Set the ship sprite to reflect the class
        SpriteRenderer renderer = player.GetComponent<SpriteRenderer>();
        renderer.sprite = shipSprite;

        GameObject colorOverlay = player.transform.Find("ColorOverlay").gameObject;
        colorOverlay.GetComponent<SpriteRenderer>().sprite = colorMask;
        colorOverlay.GetComponent<SpriteMask>().sprite = colorMask;

        foreach (Weapon weapon in startingWeapons)
        {
            player.AddWeapon(weapon);
        }

        if (ability1)
        {
            ability1.player = player;
        }
        if (ability2)
        {
            ability2.player = player;
        }
        if (ability3)
        {
            ability3.player = player;
        }
        if (ability4)
        {
            ability4.player = player;
        }
    }

    public void Unequip()
    {
        if(player)
        {
            for (int i = 0; i < startingWeapons.Count; i++)
            {
                player.RemoveWeapon(startingWeapons[i]);
            }

            player = null;

            Destroy(gameObject);
        }
    }
}
