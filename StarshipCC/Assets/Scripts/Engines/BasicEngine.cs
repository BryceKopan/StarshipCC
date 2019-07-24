using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEngine : Engine
{
    public override float MoveSpeed()
    {
        return 100;
    }

    public override float TurnSpeed()
    {
        return 3;
    }

    protected override void OnEquip(PlayerController player)
    {
    }

    protected override void OnStartThrusting()
    {
    }

    protected override void OnStopThrusting()
    {
    }

    protected override void OnUnequip()
    {
    }
}
