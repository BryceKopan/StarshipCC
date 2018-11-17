using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserWeapon : Weapon {

    public override void Fire()
    {
    }

    public override void OnEquip(PlayerController player)
    {
    }

    public override void OnUnequip(PlayerController player)
    {
    }

    public override void OnFireEnd()
    {
        throw new NotImplementedException();
    }

    public override void OnFireStart()
    {
        throw new NotImplementedException();
    }

}
