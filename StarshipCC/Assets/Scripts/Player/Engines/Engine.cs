using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Engine : MonoBehaviour
{
    public abstract float MoveSpeed();
    public abstract float TurnSpeed();

    public bool isThrusting = false;
    
    [ReadOnly]
    public PlayerController player = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    void FixedUpdate()
    {
        ApplyThrust();
    }

    public void Equip(PlayerController player)
    {
        this.player = player;
        OnEquip(player);
    }

    public void Unequip()
    {
        OnUnequip();
        player = null;
    }

    protected virtual void ApplyThrust()
    {
        if (player)
        {
            if (player.thrustLevel > 0)
            {
                if(!isThrusting)
                {
                    isThrusting = true;
                    OnStartThrusting();
                }

                Rigidbody2D rigidbody = player.GetComponent<Rigidbody2D>();
                if(rigidbody)
                {
                    //Apply force along moveDirection, proportional to thrustLevel
                    rigidbody.AddForce(player.moveDirection * MoveSpeed() * player.thrustLevel);
                }
            }
            else
            {
                if(isThrusting)
                {
                    isThrusting = false;
                    OnStopThrusting();
                }
            }
        }
    }

    protected abstract void OnEquip(PlayerController player);
    protected abstract void OnUnequip();
    protected abstract void OnStartThrusting();
    protected abstract void OnStopThrusting();
}
