using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : Item {

    public float healthAdded = 3;

    public override void OnEquip(PlayerController player)
    {
        player.SetCurrentHealth(player.GetCurrentHealth() + healthAdded);
    }

    public override void OnUnequip(PlayerController player)
    {
    }
}
