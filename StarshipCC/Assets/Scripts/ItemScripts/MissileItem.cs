using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileItem : Item {
    public GameObject missileWeapon;
    private Weapon weaponScript;

    public override void Start()
    {
        base.Start();
        weaponScript = Instantiate(missileWeapon).GetComponent<Weapon>();
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
