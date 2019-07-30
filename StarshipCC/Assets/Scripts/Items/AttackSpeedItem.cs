using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpeedItem : Item
{
    public float modifier = 1.5f;

    List<Weapon> affectedWeapons;

    public override string Description
    {
        get
        {
            return "Attack Speed x" + modifier.ToString("F1");
        }
    }

    protected override void OnEquip(PlayerController player)
    {
        affectedWeapons = new List<Weapon>();

        foreach(Weapon w in player.weapons)
        {
            w.cooldown = w.cooldown / modifier;
            affectedWeapons.Add(w);
        }
    }

    protected override void OnUnequip(PlayerController player)
    {
        foreach (Weapon w in player.weapons)
        {
            if(affectedWeapons.Contains(w))
            {
                w.cooldown = w.cooldown * modifier;
            }
        }
    }
}
