using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDash : Dash
{
    public Weapon weapon;

    public override void Start()
    {
        base.Start();

        weapon = Instantiate(weapon);
        weapon.transform.parent = transform;
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.Euler(Vector3.zero);
        weapon.transform.localScale = new Vector3(1, 1, 1);
    }

    protected override void OnActivate()
    {
        base.OnActivate();

        Vector3 aimDirection = new Vector3(-player.moveDirection.x, -player.moveDirection.y, 0);
        weapon.transform.up = aimDirection;
        weapon.Attack();
    }

    protected override void OnEquip(PlayerController player)
    {
        base.OnEquip(player);

        weapon.Equip(player);
    }

    protected override void OnUnequip()
    {
        base.OnUnequip();

        weapon.Unequip(player);
    }
}
