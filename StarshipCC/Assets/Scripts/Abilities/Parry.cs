using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parry : Ability
{
    public GameObject parryShield;
    protected GameObject shieldInstance;

    public float damageMultiplier = 10f;

    public override float Duration
    {
        get
        {
            return 0.5f;
        }
    }

    public override float Cooldown
    {
        get
        {
            return 2f;
        }
    }

    protected override void OnActivate()
    {
        shieldInstance.SetActive(true);
    }

    protected override void OnDeactivate()
    {
        shieldInstance.SetActive(false);
    }

    protected override void OnEquip(PlayerController player)
    {
        if (parryShield)
        {
            shieldInstance = Instantiate(parryShield);
            shieldInstance.transform.parent = transform;
            shieldInstance.transform.localPosition = Vector3.zero;
            shieldInstance.SetActive(false);
        }
    }

    protected override void OnReady()
    {
    }

    protected override void OnUnequip()
    {
        if (shieldInstance)
        {
            Destroy(shieldInstance);
        }
    }
}
