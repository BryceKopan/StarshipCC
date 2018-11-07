﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : Item {

    public float healthAdded = 3;

    public override void OnEquip(PlayerController player)
    {
        gameObject.SetActive(false);
        player.health += healthAdded;
        Destroy(gameObject);
    }

    public override void OnUnequip(PlayerController player)
    {
        //Since the object is single-use,
        //this should not be called
    }
}
