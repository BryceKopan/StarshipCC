using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunItem : Item
{
    public GameObject shotgunWeapon;
    private Weapon weaponScript;

    public override void Start()
    {
        base.Start();
        weaponScript = Instantiate(shotgunWeapon).GetComponent<Weapon>();
    }

    public override void OnEquip(PlayerController player)
    {
        gameObject.SetActive(false);
        player.AddWeapon(weaponScript);
    }

    public override void OnUnequip(PlayerController player)
    {
        gameObject.SetActive(true);
        player.RemoveWeapon(weaponScript);
    }
}
