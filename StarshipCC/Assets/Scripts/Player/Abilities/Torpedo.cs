using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torpedo : Ability
{
    public Weapon torpedoWeapon;
    protected Weapon weaponInstance;

    public override float Duration
    {
        get
        {
            return 1;
        }
    }

    public override float Cooldown
    {
        get
        {
            return 1;
        }
    }

    protected override void OnActivate()
    {
        weaponInstance.OnAttack();
    }

    protected override void OnDeactivate()
    {

    }

    protected override void OnEquip(PlayerController player)
    {
        if(torpedoWeapon)
        {
            weaponInstance = Instantiate(torpedoWeapon);
            weaponInstance.transform.parent = transform;
            weaponInstance.transform.localPosition = Vector3.zero;
            weaponInstance.transform.up = player.transform.up;

            weaponInstance.Equip(player);
        }
    }

    protected override void OnReady()
    {
    }

    protected override void OnUnequip()
    {
        if (weaponInstance)
        {
            weaponInstance.Unequip(player);
            Destroy(weaponInstance);
        }
    }
}
