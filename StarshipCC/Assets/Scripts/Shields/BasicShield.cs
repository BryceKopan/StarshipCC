using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicShield : Shield
{
    public override float MaxCharge()
    {
        return 1;
    }

    public override float RechargeDelay()
    {
        return 8;
    }

    public override float RechargeAmount()
    {
        return 1;
    }

    protected override void OnRecharge()
    {
    }

    protected override void OnDisabled()
    {
    }

    protected override bool ShouldHit(Projectile p)
    {
        return p.tag == Tags.ENEMY_BULLET || p.tag == Tags.ENEMY;
    }

    protected override void TakeDamageFrom(Projectile p)
    {
        currentCharge = Mathf.Max(0, currentCharge - p.damage);
    }

    protected override void OnEquip(PlayerController p)
    {
    }

    protected override void OnUnequip(PlayerController p)
    {
    }
}
