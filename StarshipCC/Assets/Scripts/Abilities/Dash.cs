using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : Ability
{
    public float dashSpeed = 200f;
    public float dashLength = 0.3f;
    public float dashCooldown = 1f;

    public ParticleSystem emitter;

    protected override void Start()
    {
        base.Start();
        activeLength = dashLength;
        cooldownLength = dashCooldown;
    }

    protected override void OnActivate()
    {
        player.GetComponent<Rigidbody2D>().AddForce(player.moveDirection * dashSpeed, ForceMode2D.Impulse);
        EmitParticles();
    }

    protected override void OnDeactivate()
    {
        player.GetComponent<Rigidbody2D>().velocity = player.GetComponent<Rigidbody2D>().velocity * 0.1f;
    }

    protected override void OnReady()
    {
       
    }

    protected void EmitParticles()
    {
        if(emitter)
        {
            emitter.Play();
        }
    }
}
