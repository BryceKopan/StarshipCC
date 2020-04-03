using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultShield : Shield
{
    public override float MaxCharge
    {
        get
        {
            return 2;
        }
        set { }
    }

    public override float RechargeDelay
    {
        get
        {
            return 8;
        }
        set { }
    }

    public override float RechargeAmount
    {
        get
        {
            return 2;
        }
        set { }
    }

    protected override void OnRecharge()
    {
    }

    protected override void OnDisabled()
    {
    }

    protected override bool ShouldHit(Projectile p)
    {
        return true;
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
