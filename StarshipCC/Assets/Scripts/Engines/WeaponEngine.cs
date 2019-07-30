using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEngine : Engine
{
    public Weapon weapon;

    public void Start()
    {
        weapon = Instantiate(weapon);
        weapon.transform.parent = transform;
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.Euler(Vector3.zero);
        weapon.transform.localScale = new Vector3(1, 1, 1);
    }

    public override float MoveSpeed()
    {
        return 0;
    }

    public override float TurnSpeed()
    {
        return 3.5f;
    }

    protected override void OnEquip(PlayerController player)
    {
        if(weapon)
        {
            weapon.Equip(player);
        }
    }

    protected override void OnStartThrusting()
    {
        FireWeapon();
    }

    protected override void OnStopThrusting()
    {
        CancelInvoke("FireWeapon");
    }

    protected override void OnUnequip()
    {
        if(weapon)
        {
            weapon.Unequip(player);
        }
    }

    IEnumerator FireWeapon()
    {
        if(weapon)
        {
            weapon.StartAttack();
            Invoke("FireWeapon", (weapon.numAttacks * weapon.delayBetweenAttacks) + weapon.cooldown);
        }

        return null;
    }
}
