using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItem : Item {
    public GameObject weapon;
    private Weapon weaponScript;

    public override void Start()
    {
        base.Start();
        weaponScript = Instantiate(weapon).GetComponent<Weapon>();
    }

    public override void OnEquip(PlayerController player)
    {
        player.AddWeapon(weaponScript);
    }

    public override void OnUnequip(PlayerController player)
    {
        player.RemoveWeapon(weaponScript);
    }
}
