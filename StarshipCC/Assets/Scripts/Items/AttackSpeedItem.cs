using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpeedItem : Item
{
    public float modifier = 2;

    public override string Description
    {
        get
        {
            return "Attack Speed x" + (int)modifier;
        }
    }

    protected override void OnEquip(PlayerController player)
    {
        foreach(Weapon w in player.weapons)
        {
            w.cooldown = w.cooldown / modifier;
        }
    }

    protected override void OnUnequip(PlayerController player)
    {
        foreach (Weapon w in player.weapons)
        {
            w.cooldown = w.cooldown * modifier;
        }
    }
}
