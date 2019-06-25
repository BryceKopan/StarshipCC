using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : Item {

    public float healthAdded = 3;

    public override void OnEquip(PlayerController player)
    {
        base.OnEquip(player);
        gameObject.SetActive(false);
        player.SetCurrentHealth(player.GetCurrentHealth() + healthAdded);
        Destroy(gameObject);
    }

    public override void OnUnequip(PlayerController player)
    {
        base.OnUnequip(player);
        //Since the object is single-use,
        //this should not be called
    }
}
