using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : Item {

    public float healthAdded = 3;

    public override string Description
    {
        get
        {
            return "Health +" + (int)healthAdded;
        }
    }

    protected override void OnEquip(PlayerController player)
    {
        player.SetCurrentHealth(player.GetCurrentHealth() + healthAdded);
    }

    protected override void OnUnequip(PlayerController player)
    {
    }
}
