using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialBurstItem : WeaponItem
{
    public override void Start()
    {
        base.Start();
        isTimed = true;
        duration = 1f;
    }

    protected override void OnEquip(PlayerController player)
    {
        base.OnEquip(player);
        weaponScript.StartAttack();
    }

    protected override void OnUnequip(PlayerController player)
    {
        weaponScript.StopAttack();
        base.OnUnequip(player);
        Destroy(gameObject);
    }

    public override string Description
    {
        get
        {
            return "";
        }
    }
}
