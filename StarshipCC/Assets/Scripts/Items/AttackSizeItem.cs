using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSizeItem : Item
{
    public float sizeModifier = 1.5f;

    public List<Weapon> affectedWeapons;

    public override string Description
    {
        get
        {
            return "Attack Size x" + sizeModifier.ToString("F1");
        }
    }

    protected override void OnEquip(PlayerController player)
    {
        // Keep track of affected weapons so only these weapons are changed on unequip
        affectedWeapons = new List<Weapon>();

        foreach (Weapon w in player.weapons)
        {
            w.attackScaleModifier *= sizeModifier;
            affectedWeapons.Add(w);
        }
    }

    protected override void OnUnequip(PlayerController player)
    {
        // Only remove buff from affected weapons
        foreach (Weapon w in player.weapons)
        {
            if(affectedWeapons.Contains(w))
            {
                w.attackScaleModifier /= sizeModifier;
            }
        }
    }
}
