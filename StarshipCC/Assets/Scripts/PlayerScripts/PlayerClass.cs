using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClass : MonoBehaviour
{
    public List<Weapon> startingWeapons;
    public Engine engine;
    public Shield shield;

    public Ability ability1;
    public Ability ability2;
    public Ability ability3;
    public Ability ability4;

    public float startingMaxHealth;

    public Sprite playerSprite;
    public Sprite colorMask;

    private PlayerController player;

    void Awake()
    {
        for(int i = 0; i < startingWeapons.Count; i++)
        {
            Weapon instance = Instantiate(startingWeapons[i]);
            startingWeapons[i] = instance;
        }

        engine = (Engine)gameObject.AddComponent(engine.GetType());
        shield = (Shield)gameObject.AddComponent(shield.GetType());

        if(ability1)
        {
            ability1 = (Ability)gameObject.AddComponent(ability1.GetType());
        }
        if(ability2)
        {
            ability2 = (Ability)gameObject.AddComponent(ability2.GetType());
        }
        if(ability3)
        {
            ability3 = (Ability)gameObject.AddComponent(ability3.GetType());
        }
        if(ability4)
        {
            ability4 = (Ability)gameObject.AddComponent(ability4.GetType());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Equip(PlayerController player)
    {
        this.player = player;
        this.transform.parent = player.transform.parent;
        if(ability1)
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
}
