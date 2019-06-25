using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : Ability
{
    public float dashSpeed = 200f;
    public float dashLength = 0.3f;
    public float dashCooldown = 1f;

    protected override void Start()
    {
        base.Start();
        activeLength = dashLength;
        cooldownLength = dashCooldown;
    }

    protected override void OnActivate()
    {
        Debug.Log("Dash activated player " + player.PlayerNumber);
        player.GetComponent<Rigidbody2D>().AddForce(player.moveDirection * dashSpeed, ForceMode2D.Impulse);
    }

    protected override void OnDeactivate()
    {
        Debug.Log("Dash deactivated player " + player.PlayerNumber);
        player.GetComponent<Rigidbody2D>().velocity = player.GetComponent<Rigidbody2D>().velocity * 0.1f;
    }

    protected override void OnReady()
    {
       
    }
}
