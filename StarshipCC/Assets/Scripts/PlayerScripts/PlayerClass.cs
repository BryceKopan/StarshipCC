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

    void Awake()
    {
        for(int i = 0; i < startingWeapons.Count; i++)
        {
            Weapon instance = Instantiate(startingWeapons[i]);
            startingWeapons[i] = instance;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
