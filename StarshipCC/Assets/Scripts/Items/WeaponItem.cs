using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItem : Item {
    public GameObject weapon;
    private Weapon weaponScript;

    public override string Description
    {
        get
        {
            return "New Weapon";
        }
    }

    public override void Start()
    {
        base.Start();
        weaponScript = Instantiate(weapon).GetComponent<Weapon>();
    }

    protected override void OnEquip(PlayerController player)
    {
        player.AddWeapon(weaponScript);
    }

    protected override void OnUnequip(PlayerController player)
    {
        player.RemoveWeapon(weaponScript);
    }
}
