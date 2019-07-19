using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveShieldItem : Item
{
    float oldMaxCharge;
    float oldRechargeAmount;

    public override string Description
    {
        get
        {
            return "Shield Disabled";
        }
    }

    protected override void OnEquip(PlayerController player)
    {
        oldMaxCharge = player.playerClass.shield.MaxCharge;
        oldRechargeAmount = player.playerClass.shield.RechargeAmount;

        player.playerClass.shield.MaxCharge = 0;
        player.playerClass.shield.currentCharge = -1;
        player.playerClass.shield.Disable();
        player.playerClass.shield.RechargeAmount = 0;
    }

    protected override void OnUnequip(PlayerController player)
    {
        player.playerClass.shield.MaxCharge = oldMaxCharge;
        player.playerClass.shield.RechargeAmount = oldRechargeAmount;
    }
}
